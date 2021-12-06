// ignore: import_of_legacy_library_into_null_safe
import 'package:intl/intl.dart';
import 'package:flutter/material.dart';
import 'package:flutter_picker/flutter_picker.dart';
import 'dart:async';
import 'package:flutter_ringtone_player/flutter_ringtone_player.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'キッチンタイマー',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'キッチンタイマー'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key, required this.title}) : super(key: key);
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  static final DateTime _initTime = DateTime(0, 0, 0, 0, 3, 0, 0, 0); // 3分
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
      if (_time.compareTo(DateTime(0, 0, 0, 0, 0, 0, 0)) == 0) {
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
              onPressed: () async {
                Picker(
                  adapter: DateTimePickerAdapter(
                      type: PickerDateTimeType.kHMS,
                      value: _time,
                      customColumnType: [3, 4, 5]),
                  title: const Text("Select Time"),
                  onConfirm: (Picker picker, List value) {
                    setState(() => {
                          _time = DateTime.utc(
                              0, 0, 0, value[0], value[1], value[2]),
                          _timeStr = formatter.format(_time)
                        });
                  },
                ).showModal(context);
              },
            ),
          ],
        ),
      ),
    );
  }
}
