import 'package:flutter/material.dart';

import 'audio_player_test.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'キッチンタイマー',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      // home: const TimerPage(title: 'キッチンタイマー'),
      // home: const VideoPlayerScreen(),
      home: const AudioPlayerScreen(),
    );
  }
}