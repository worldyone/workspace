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
  int _handscore = 0;
  final DeckController _dc = DeckController();
  late PlayingCard card;

  void _drawCard() {
    setState(() {
      if (_dc.deck.isNotEmpty) {
        card = _dc.drawCard();
        _hands.add(card);
        _handscore += card.number.decimal!;
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
            Expanded(
              child: ListView.builder(
                  scrollDirection: Axis.horizontal,
                  itemCount: _hands.length,
                  itemBuilder: (context, index) {
                    return _hands[index].image;
                  }),
            ),
            Text(
              '${_handscore}',
            ),
          ],
        ),
      ),
      floatingActionButton: GestureDetector(
        onTap: _drawCard,
        child: Image.asset(
          "assets/cards/cardgame_deck.png",
          width: 160,
        ),
      ),
      bottomNavigationBar: FloatingActionButton(
        onPressed: () {},
        child: Icon(Icons.ac_unit),
      ),
    );
  }
}
