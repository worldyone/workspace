import 'package:flutter/material.dart';
import 'package:flutter_hooks/flutter_hooks.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';
import 'package:myapp/provider.dart';
import 'package:myapp/todo_input.dart';
import 'package:myapp/todo.dart';
import 'package:myapp/todo_input_view.dart';

class ToDoListScreen extends HookConsumerWidget {
  const ToDoListScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final _todos = ref.watch(todoListProvider);
    final _todoNotifier = ref.read(todoListProvider.notifier);

    useEffect(() {
      _todoNotifier.find();
    }, []);

    return Scaffold(
      appBar: AppBar(
        title: const Text('ToDo'),
      ),
      body: ListView.builder(
        itemBuilder: (context, index) => CheckboxListTile(
          onChanged: (checked) {
            _todoNotifier.toggle(_todos[index]);
          },
          value: _todos[index].value.archived,
          title: GestureDetector(
            child: Text(_todos[index].value.title,
                style: TextStyle(
                  decoration: _todos[index].value.archived
                      ? TextDecoration.lineThrough
                      : TextDecoration.none,
                )),
            onTap: () {
              ToDoInputView.show(
                context,
                record: _todos[index],
              );
            },
          ),
        ),
        itemCount: _todos.length,
      ),
      floatingActionButton: FloatingActionButton(
        child: const Icon(Icons.add),
        onPressed: () {
          ToDoInputView.show(context);
        },
      ),
    );
  }
}
