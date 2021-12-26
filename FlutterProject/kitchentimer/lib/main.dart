import 'package:flutter/material.dart';
import 'package:kitchentimer/repeat_timer.dart';
import 'package:kitchentimer/stopwatch.dart';
import 'package:kitchentimer/timer.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      initialRoute: '/',
      routes: {
        '/': (context) => const TimerPage(title: 'キッチンタイマー'),
        '/stopwatch': (context) => const StopWatchPage(title: 'ストップウォッチ'),
        '/repeat_timer': (context) => const RepeatTimerPage(title: '繰り返しタイマー'),
      },
      title: 'キッチンタイマー',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
    );
  }
}
