import 'package:flutter/material.dart';
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
      },
      title: 'キッチンタイマー',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      // home: const TimerPage(title: 'キッチンタイマー'),
      // home: const VideoPlayerScreen(),
      // home: const AudioPlayerScreen(),
      // home: const StopWatchPage(title: 'ストップウォッチ'),
    );
  }
}
