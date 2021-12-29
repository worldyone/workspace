import 'package:brackjack/playing_card.dart';
import 'package:flutter/material.dart';

import 'deck_controller.dart';

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
  List<PlayingCard> _hands = [];
  DeckController _dc = new DeckController();
  PlayingCard? card;

  void _drawCard() {
    setState(() {
      if (_dc.deck.isNotEmpty) {
        card = _dc.drawCard();
        print(card?.number);
        print(card?.suit);
        _hands.add(card!);
        // _hands.add(_dc.drawCard());
      }
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
