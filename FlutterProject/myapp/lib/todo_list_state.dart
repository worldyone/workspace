import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:myapp/database.dart';
import 'package:myapp/todo.dart';

class ToDoListState extends StateNotifier<List<ToDoRecord>> {
  ToDoListState(List<ToDoRecord> state, this.dbHelper) : super(state);

  final DbHelper dbHelper;

  Future<void> find() async {
    state = await dbHelper.find();
  }

  Future<void> add(ToDo todo) async {
    final record = await dbHelper.add(todo);
    state = state.sublist(0)..insert(0, record);
  }

  Future<void> update(ToDoRecord record) async {
    await dbHelper.update(record.key, record.value);
    _replaceRecord(record);
  }

  Future<void> toggle(ToDoRecord record) async {
    final updateRecord = record.copyWith.value(
      archived: !record.value.archived,
    );

    await dbHelper.update(
      updateRecord.key,
      updateRecord.value,
    );

    _replaceRecord(updateRecord);
  }

  void _replaceRecord(ToDoRecord record) {
    final findIndex = state.indexWhere(
      (e) => e.key == record.key,
    );

    state = List.from(state)
      ..replaceRange(
        findIndex,
        findIndex + 1,
        [record],
      );
  }
}
