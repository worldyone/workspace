import 'package:hooks_riverpod/hooks_riverpod.dart';
import 'package:myapp/database.dart';
import 'package:myapp/todo.dart';
import 'package:myapp/todo_list_state.dart';

final todoListProvider = StateNotifierProvider<ToDoListState, List<ToDoRecord>>(
  (ref) => ToDoListState(
    [],
    ref.read(databaseProvider),
  ),
);

final databaseProvider = Provider<DbHelper>(
  (_) => throw UnimplementedError(),
);
