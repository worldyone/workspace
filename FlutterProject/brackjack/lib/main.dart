import 'dart:math' as math;

import 'package:flutter/material.dart';

import 'playing_card.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
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
  List<String> _hands = [];
  static const List _cardsNumbers = [
    "A",
    "2",
    "3",
    "4",
    "5",
    "6",
    "7",
    "8",
    "9",
    "10",
    "J",
    "Q",
    "K"
  ];
  static const List _cardsSuits = ["spade", "heart", "clover", "dia"];
  List<PlayingCard> _deck = [];
  var rand = new math.Random();
  late int lottery;

  void _drawCard() {
    setState(() {
      lottery = rand.nextInt(_cardsNumbers.length);
      _hands.add(_cardsNumbers[lottery]);
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
              'Your hands',
            ),
            Text(
              '$_hands',
              style: Theme.of(context).textTheme.headline4,
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _drawCard,
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ),
    );
  }
}
