import 'dart:async';

import 'package:audioplayers/audioplayers.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_picker/flutter_picker.dart';
import 'package:intl/intl.dart';

class IntervalTimerPage extends StatefulWidget {
  const IntervalTimerPage({Key? key, required this.title}) : super(key: key);
  final String title;

  @override
  State<IntervalTimerPage> createState() => _IntervalTimerPageState();
}

class _IntervalTimerPageState extends State<IntervalTimerPage> {
  static final DateTime _initTime = DateTime(0, 0, 0, 0, 3, 0, 0).toUtc(); // 3分
  static final DateFormat formatter = DateFormat('mm:ss');
  DateTime _workTime = _initTime;
  DateTime _setWorkTime = _initTime;
  DateTime _restTime = _initTime;
  DateTime _setRestTime = _initTime;
  String _workTimeStr = DateFormat('mm:ss').format(_initTime);
  String _restTimeStr = DateFormat('mm:ss').format(_initTime);
  static const SOUND_1 = 'sounds/Horagai01-1.mp3';
  static const SOUND_2 = 'sounds/Naruko02-1.mp3';
  bool _started = false;
  bool _working = true;

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
      if (_working) {
        _workTime = _workTime.add(const Duration(seconds: -1));
        setState(() => _workTimeStr = formatter.format(_workTime));

        // タイムアップ
        if (_workTime.minute == 0 && _workTime.second == 0) {
          _cache.play(SOUND_1, mode: PlayerMode.LOW_LATENCY);

          setState(() {
            _working = !_working; // 活動と休憩を切り替える
            _workTime = _setWorkTime; // 設定時間に戻す
            _workTimeStr = formatter.format(_workTime);
          });
        }
      } else {
        _restTime = _restTime.add(const Duration(seconds: -1));
        setState(() => _restTimeStr = formatter.format(_restTime));

        // タイムアップ
        if (_restTime.minute == 0 && _restTime.second == 0) {
          _cache.play(SOUND_2, mode: PlayerMode.LOW_LATENCY);

          setState(() {
            _working = !_working; // 活動と休憩を切り替える
            _restTime = _setRestTime; // 設定時間に戻す
            _restTimeStr = formatter.format(_restTime);
          });
        }
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
              '残り活動時間',
            ),
            Text(
              _workTimeStr,
              style: Theme.of(context).textTheme.headline4,
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
                      value: _workTime,
                      customColumnType: [4, 5]),
                  title: const Text("Select Time"),
                  onConfirm: (Picker picker, List value) {
                    setState(
                      () => {
                        _setWorkTime =
                            DateTime.utc(0, 0, 0, 0, value[0], value[1]),
                        _workTime = _setWorkTime,
                        _workTimeStr = formatter.format(_workTime),
                      },
                    );
                  },
                ).showModal(context);
              },
            ),
            const Text(
              '残り休憩時間',
            ),
            Text(
              _restTimeStr,
              style: Theme.of(context).textTheme.headline4,
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
                      value: _restTime,
                      customColumnType: [4, 5]),
                  title: const Text("Select Time"),
                  onConfirm: (Picker picker, List value) {
                    setState(
                      () => {
                        _setRestTime =
                            DateTime.utc(0, 0, 0, 0, value[0], value[1]),
                        _restTime = _setRestTime,
                        _restTimeStr = formatter.format(_restTime),
                      },
                    );
                  },
                ).showModal(context);
              },
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
              Navigator.pushNamed(context, '/repeat_timer');
            },
            child: const Icon(Icons.restart_alt_outlined),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.pushNamed(context, '/interval_timer');
            },
            child: const Icon(Icons.repeat),
            style: TextButton.styleFrom(backgroundColor: Colors.teal),
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
