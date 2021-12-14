import 'package:audioplayers/audioplayers.dart';
import 'package:flutter/material.dart';

class AudioPlayerScreen extends StatefulWidget {
  const AudioPlayerScreen({Key? key}) : super(key: key);

  @override
  _AudioPlayerScreenState createState() => _AudioPlayerScreenState();
}

class _AudioPlayerScreenState extends State<AudioPlayerScreen> {
  static const SOUND_1 = 'sounds/Horagai01-1.mp3';
  static const SOUND_2 = 'sounds/Naruko02-1.mp3';

  final AudioCache _cache = AudioCache(
    fixedPlayer: AudioPlayer(),
  );

  @override
  void initState() {
    _cache.loadAll([SOUND_1, SOUND_2]);
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Audio Test Page'),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          _cache.play(SOUND_1, mode: PlayerMode.LOW_LATENCY);
          print("ok");
        },
        child: const Icon(
          Icons.play_arrow,
        ),
      ),
    );
  }
}
