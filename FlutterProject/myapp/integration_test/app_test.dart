import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';
import 'package:myapp/main.dart' as app;

void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();

  testWidgets('test main', (tester) async {
    app.main();

    await tester.pumpAndSettle(
      const Duration(milliseconds: 500),
    );

    expect(find.text('ToDo'), findsOneWidget);
    expect(
      find.byType(FloatingActionButton),
      findsOneWidget,
    );

    // ToDoの追加ボタンをタップする
    await tester.tap(
      find.byType(FloatingActionButton),
    );

    // 次の画面が出るまで待機する
    await tester.pumpAndSettle();

    // 新しいToDoを追加する
    await tester.enterText(
      find.byType(TextField),
      'ToDo 1',
    );
    await tester.pumpAndSettle(const Duration(milliseconds: 5000));

    // 追加したToDoがリスト上にあることを検証する
    expect(
      find.text('ToDo 1'),
      findsOneWidget,
    );
  });
}
