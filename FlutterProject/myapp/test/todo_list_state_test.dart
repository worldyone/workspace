import 'package:flutter_test/flutter_test.dart';
import 'package:mocktail/mocktail.dart';
import 'package:myapp/database.dart';
import 'package:myapp/todo.dart';
import 'package:myapp/todo_list_state.dart';

/// Databaseは初期化が必要なのでモックを作成する
class MockDbHelper extends Mock implements DbHelper {}

void main() {
  final dbHelper = MockDbHelper();

  when(() => dbHelper.add(
        ToDo(title: 'test'),
      )).thenAnswer((_) async => ToDoRecord(
        1,
        ToDo(title: 'test'),
      ));

  test('test todo state', () async {
    final state = ToDoListState(const [], dbHelper);
    await state.add(ToDo(title: 'test'));

    // protectedメンバーですが、値を確認するために使う
    expect(state.state.first.key, 1);
    expect(state.state.first.value.title, 'test');
  });
}
