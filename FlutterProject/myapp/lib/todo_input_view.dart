import 'package:flutter/material.dart';
import 'package:flutter_hooks/flutter_hooks.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';
import 'package:myapp/provider.dart';
import 'package:myapp/todo.dart';

class ToDoInputView extends HookConsumerWidget {
  const ToDoInputView({
    Key? key,
    this.record,
  }) : super(key: key);

  final ToDoRecord? record;

  static Future<void> show(
    BuildContext context, {
    ToDoRecord? record,
  }) {
    return showModalBottomSheet(
      context: context,
      builder: (context) => ToDoInputView(record: record),
    );
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final _controller = useTextEditingController(
      text: record?.value.title,
    );

    final _todoListNotifier = ref.read(
      todoListProvider.notifier,
    );

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
            controller: _controller,
            autofocus: true,
            onEditingComplete: () async {
              if (_controller.text.isEmpty) {
                return;
              }

              if (record == null) {
                await _todoListNotifier.add(
                  ToDo(title: _controller.text),
                );
              } else {
                final updatedToDo = record!.value.copyWith(
                  title: _controller.text,
                );

                await _todoListNotifier.update(
                  record!.copyWith(
                    value: updatedToDo,
                  ),
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
