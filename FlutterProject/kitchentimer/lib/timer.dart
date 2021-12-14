import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_picker/flutter_picker.dart';
import 'package:flutter_ringtone_player/flutter_ringtone_player.dart';
import 'package:intl/intl.dart';

class TimerPage extends StatefulWidget {
  const TimerPage({Key? key, required this.title}) : super(key: key);
  final String title;

  @override
  State<TimerPage> createState() => _TimerPageState();
}

class _TimerPageState extends State<TimerPage> {
  static final DateTime _initTime = DateTime(0, 0, 0, 0, 3, 0, 0).toUtc(); // 3分
  static final DateFormat formatter = DateFormat('mm:ss');
  DateTime _time = _initTime;
  bool _started = false;
  String _timeStr = DateFormat('mm:ss').format(_initTime);

  @override
  void initState() {
    Timer.periodic(
      const Duration(seconds: 1),
      _onTimer,
    );

    super.initState();
  }

  void _onTimer(Timer timer) {
    if (_started) {
      // もしタイムアップしたら
      if (_time.minute == 0 && _time.second == 0) {
        FlutterRingtonePlayer.play(
          android: AndroidSounds.notification, // Android用のサウンド
          ios: const IosSound(1023), // iOS用のサウンド
          looping: false, // Androidのみ。繰り返さない
          asAlarm: true, // Androidのみ。サイレントモードでも音を鳴らす
          volume: 0.1, // Androidのみ。0.0〜1.0
        );
      } else {
        // 1秒減らす
        _time = _time.add(const Duration(seconds: -1));
        setState(() => _timeStr = formatter.format(_time));
      }
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
              '残り時間',
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
            FloatingActionButton(
              child: const Icon(
                Icons.edit,
              ),
              tooltip: 'edit',
              onPressed: () async {
                Picker(
                  adapter: DateTimePickerAdapter(
                      type: PickerDateTimeType.kHMS,
                      value: _time,
                      customColumnType: [4, 5]),
                  title: const Text("Select Time"),
                  onConfirm: (Picker picker, List value) {
                    setState(
                      () => {
                        _time = DateTime.utc(0, 0, 0, 0, value[0], value[1]),
                        _timeStr = formatter.format(_time),
                      },
                    );
                  },
                ).showModal(context);
              },
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(onPressed: () {
        FlutterRingtonePlayer.play(
          android: AndroidSounds.notification, // Android用のサウンド
          ios: const IosSound(1023), // iOS用のサウンド
          looping: false, // Androidのみ。繰り返さない
          asAlarm: true, // Androidのみ。サイレントモードでも音を鳴らす
          volume: 0.1, // Androidのみ。0.0〜1.0
        );
      }),
    );
  }
}
