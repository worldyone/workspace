import 'package:flutter/material.dart';
import 'package:myapp/database.dart';
import 'package:myapp/todo.dart';

class ToDoInput extends StatefulWidget {
  @Deprecated('use ToDoInputView')
  const ToDoInput({Key? key}) : super(key: key);

  static Future<void> show(BuildContext context) {
    return showModalBottomSheet(
      context: context,
      builder: (context) => const ToDoInput(),
    );
  }

  @override
  _ToDoInputState createState() => _ToDoInputState();
}

class _ToDoInputState extends State<ToDoInput> {
  final _textController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        bottom: MediaQuery.of(context).viewInsets.bottom,
        right: 10,
        left: 10,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          TextField(
            controller: _textController,
            autofocus: true,
            onEditingComplete: () {
              if (_textController.text.isNotEmpty) {
                DbHelper.instance.add(
                  ToDo(title: _textController.text),
                );
              }
              Navigator.pop(context);
            },
            decoration: const InputDecoration(
              border: InputBorder.none,
              hintText: 'ToDoのタイトルを入力します',
            ),
          ),
        ],
      ),
    );
  }
}
