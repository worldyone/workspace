import 'package:brackjack/animation_card.dart';
import 'package:brackjack/playing_card.dart';
import 'package:flutter/material.dart';

import 'deck_controller.dart';

class CardPage extends StatefulWidget {
  const CardPage({Key? key, required this.title}) : super(key: key);
  final String title;

  @override
  State<CardPage> createState() => _CardPageState();
}

class _CardPageState extends State<CardPage> {
  final List<PlayingCard> _hands = [];
  final DeckController _dc = DeckController();
  PlayingCard? card;

  void _drawCard() {
    setState(() {
      if (_dc.deck.isNotEmpty) {
        _hands.add(_dc.drawCard());
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
            AnimationCard(Image.asset("assets/cards/card_club_01.png"))
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
