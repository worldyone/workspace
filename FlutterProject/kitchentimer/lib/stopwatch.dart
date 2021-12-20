import 'dart:async';

import 'package:audioplayers/audioplayers.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class StopWatchPage extends StatefulWidget {
  const StopWatchPage({Key? key, required this.title}) : super(key: key);
  final String title;

  @override
  State<StopWatchPage> createState() => _StopWatchPageState();
}

class _StopWatchPageState extends State<StopWatchPage> {
  static final DateTime _initTime = DateTime(0, 0, 0, 0, 0, 0, 0).toUtc();
  static final DateFormat formatter = DateFormat('mm:ss');
  DateTime _time = _initTime;
  bool _started = false;
  String _timeStr = DateFormat('mm:ss').format(_initTime);
  static const SOUND_1 = 'sounds/Horagai01-1.mp3';
  static const SOUND_2 = 'sounds/Naruko02-1.mp3';

  final AudioCache _cache = AudioCache(
    fixedPlayer: AudioPlayer(),
  );

  @override
  void initState() {
    Timer.periodic(
      const Duration(seconds: 1),
      _onTimer,
    );
    _cache.loadAll([SOUND_1, SOUND_2]);
    super.initState();
  }

  void _onTimer(Timer timer) {
    if (_started) {
      // 1秒ずつ増やす
      _time = _time.add(const Duration(seconds: 1));
      setState(() => _timeStr = formatter.format(_time));
    }
  }

  void _reverseStarted() {
    setState(() {
      _started = !_started;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Text(
              '経過時間',
            ),
            Text(
              _timeStr,
              style: Theme.of(context).textTheme.headline4,
            ),
            if (_started)
              FloatingActionButton(
                onPressed: _reverseStarted,
                tooltip: 'stop',
                child: const Icon(
                  Icons.stop,
                ),
              )
            else
              FloatingActionButton(
                onPressed: _reverseStarted,
                tooltip: 'start',
                child: const Icon(
                  Icons.arrow_right_rounded,
                  size: 48.0,
                ),
              ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(onPressed: () {
        _cache.play(SOUND_1, mode: PlayerMode.LOW_LATENCY);
      }),
    );
  }
}
