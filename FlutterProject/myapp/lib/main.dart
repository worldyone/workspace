import 'package:flutter/material.dart';
import 'package:myapp/database.dart';
import 'package:myapp/provider.dart';
import './todo.dart';
import 'package:myapp/todo_input.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';
import 'package:myapp/todo_list_screen.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();

  // await DbHelper.instance.initialize();
  final dbHelper = DbHelper();
  await dbHelper.initialize();

  runApp(
    ProviderScope(
      child: const App(),
      overrides: [
        // プロバイダの値を上書き
        databaseProvider.overrideWithValue(dbHelper),
      ],
    ),
  );
}

class App extends StatelessWidget {
  const App({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: ToDoListScreen(),
    );
  }
}

class HomeScreen extends StatefulWidget {
  @Deprecated('use [ToDoListScreen]')
  const HomeScreen({Key? key}) : super(key: key);

  @override
  _HomeScreenState createState() => _HomeScreenState();
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
            onPressed: () {
              ToDoInput.show(context);
            },
          ),
        );
      },
    );
  }
}
