import 'package:flutter/material.dart';
import 'package:myapp/database.dart';
import './todo.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();

  await DbHelper.instance.initialize();
  runApp(const App());
}

class App extends StatelessWidget {
  const App({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: HomeScreen(),
    );
  }
}

class HomeScreen extends StatefulWidget {
  const HomeScreen({Key? key}) : super(key: key);

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  final _todos = List.generate(
    10,
    (index) => ToDo(title: 'Todo ${index + 1}'),
  );

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<ToDoRecord>>(
      future: DbHelper.instance.find(),
      initialData: const [],
      builder: (context, snapshot) {
        if (!snapshot.hasData) {
          return const Scaffold(
            body: Center(
              child: CircularProgressIndicator.adaptive(),
            ),
          );
        }

        final todos = snapshot.data!;

        return Scaffold(
          appBar: AppBar(
            title: const Text('ToDo'),
          ),
          body: ListView.builder(
            itemCount: todos.length,
            itemBuilder: (context, index) {
              final todo = todos[index].value;

              return CheckboxListTile(
                onChanged: (checked) async {
                  final key = todos[index].key;
                  final update = todo.copyWith(
                    archived: !todo.archived,
                  );
                  await DbHelper.instance.update(
                    key,
                    update,
                  );

                  setState(() {});
                },
                value: todos[index].value.archived,
                title: Text(todo.title),
              );
            },
          ),
          floatingActionButton: FloatingActionButton(
            child: const Icon(Icons.add),
            onPressed: () async {
              await DbHelper.instance.add(
                ToDo(
                  title: 'ToDo ${todos.length + 1}',
                ),
              );
              setState(() {});
            },
          ),
        );
      },
    );
  }
}
