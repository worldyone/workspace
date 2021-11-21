import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';
import 'package:mocktail/mocktail.dart';
import 'package:myapp/provider.dart';
import 'package:myapp/todo.dart';
import 'package:myapp/todo_list_screen.dart';
import 'package:myapp/todo_list_state.dart';

import 'todo_list_state_test.dart';

void main() {
  // DBのモックを作成する
  final dbHelper = MockDbHelper();

  // 依存するStateを作成する
  final todoListState = ToDoListState(
    [],
    dbHelper,
  );

  testWidgets('todo list screen ...', (tester) async {
    // dbHelperのfindの戻り値を変更する
    when(() => dbHelper.find()).thenAnswer(
      (_) => Future.value(
        List.generate(
          10,
          (index) => ToDoRecord(
            index + 1,
            ToDo(title: 'ToDo ${index + 1}'),
          ),
        ),
      ),
    );

    await tester.pumpWidget(
      ProviderScope(
        overrides: [
          todoListProvider.overrideWithValue(
            todoListState,
          ),
        ],
        child: const MaterialApp(
          home: ToDoListScreen(),
        ),
      ),
    );

    // 非同期で更新されるリストが表示されるのを待ちます
    await tester.pumpAndSettle();

    expect(find.text('ToDo'), findsOneWidget);
    expect(find.byType(CheckboxListTile), findsNWidgets(10));
  });
}
