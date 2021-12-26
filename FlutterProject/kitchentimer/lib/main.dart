import 'package:flutter/material.dart';
import 'package:kitchentimer/interval_timer.dart';
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
        '/interval_timer': (context) =>
            const IntervalTimerPage(title: 'インターバルタイマー'),
        // todo: ラップ ストップウォッチの途中経過止め機能
        // todo: インターバルタイマーの追加機能として、活動・休憩の他に時間間隔名を追加できる機能
      },
      title: 'キッチンタイマー',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
    );
  }
}
