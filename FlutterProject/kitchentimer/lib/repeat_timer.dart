import 'dart:async';

import 'package:audioplayers/audioplayers.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_picker/flutter_picker.dart';
import 'package:intl/intl.dart';

class RepeatTimerPage extends StatefulWidget {
  const RepeatTimerPage({Key? key, required this.title}) : super(key: key);
  final String title;

  @override
  State<RepeatTimerPage> createState() => _RepeatTimerPageState();
}

class _RepeatTimerPageState extends State<RepeatTimerPage> {
  static final DateTime _initTime = DateTime(0, 0, 0, 0, 3, 0, 0).toUtc(); // 3分
  static final DateFormat formatter = DateFormat('mm:ss');
  DateTime _time = _initTime;
  DateTime _setTime = _initTime;
  bool _started = false;
  String _timeStr = DateFormat('mm:ss').format(_initTime);
  String _setTimeStr = DateFormat('mm:ss').format(_initTime);
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
      // もしタイムアップしたら音を鳴らし、再度設定値からタイマーを始める
      if (_time.minute == 0 && _time.second == 0) {
        _cache.play(SOUND_1, mode: PlayerMode.LOW_LATENCY);
        _time = _setTime;
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
              '設定時間',
            ),
            Text(
              _setTimeStr,
              style: Theme.of(context).textTheme.headline4,
            ),
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
                        _setTime = DateTime.utc(0, 0, 0, 0, value[0], value[1]),
                        _time = _setTime,
                        _timeStr = formatter.format(_time),
                        _setTimeStr = formatter.format(_setTime),
                      },
                    );
                  },
                ).showModal(context);
              },
            ),
          ],
        ),
      ),
      bottomNavigationBar: ButtonBar(
        children: [
          ElevatedButton(
            onPressed: () {
              Navigator.pushNamed(context, '/');
            },
            child: const Icon(CupertinoIcons.bell),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.pushNamed(context, '/stopwatch');
            },
            child: const Icon(CupertinoIcons.stopwatch),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.pushNamed(context, '/repeattimer');
            },
            child: const Icon(Icons.restart_alt_outlined),
            style: TextButton.styleFrom(backgroundColor: Colors.teal),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.pushNamed(context, '/interval_timer');
            },
            child: const Icon(Icons.repeat),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        child: const Icon(Icons.volume_up_sharp),
        onPressed: () {
          _cache.play(SOUND_1, mode: PlayerMode.LOW_LATENCY);
        },
      ),
    );
  }
}