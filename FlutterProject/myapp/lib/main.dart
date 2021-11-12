import 'package:flutter/material.dart';
import './todo.dart';

void main() {
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
    return Scaffold(
      appBar: AppBar(
        title: const Text('ToDo'),
      ),
      body: ListView.builder(
        itemBuilder: (context, index) => CheckboxListTile(
          onChanged: (checked) {
            setState(() {
              _todos[index].archived = !_todos[index].archived;
            });
          },
          value: _todos[index].archived,
          title: Text(_todos[index].title),
        ),
        itemCount: _todos.length,
      ),
      floatingActionButton: FloatingActionButton(
        child: const Icon(Icons.add),
        onPressed: () {
          setState(() {
            _todos.add(
              ToDo(title: 'ToDo ${_todos.length + 1}'),
            );
          });
        },
      ),
    );
  }
}
