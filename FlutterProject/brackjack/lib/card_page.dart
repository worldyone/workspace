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
  List<PlayingCard> _hands = [];
  int _handscore = 0;
  DeckController _dc = DeckController();
  late PlayingCard card;

  void _drawCard() {
    setState(() {
      if (_dc.deck.isNotEmpty) {
        card = _dc.drawCard();
        _hands.add(card);
        _handscore = calculateHandScore(_hands);
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
              // todo: 4枚目以上が画面外にはみ出してしまうので、下に並べられるようにしたい
              child: ListView.builder(
                  scrollDirection: Axis.horizontal,
                  itemCount: _hands.length,
                  itemBuilder: (context, index) {
                    return _hands[index].image;
                  }),
            ),
            Text(
              'Your hands score is ${_handscore}.'
              '    '
              '${(_handscore > 21) ? "burst" : ""}',
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
        onPressed: () {
          _dc.initialize();
          _dc.shuffle();

          setState(() {
            _hands = [];
            _handscore = 0;
          });
        },
        child: Icon(Icons.ac_unit),
      ),
    );
  }
}

// 手札の合計点数を計算して返す
int calculateHandScore(List<PlayingCard> hands) {
  int score = 0;
  List<int> cardDecimals = [];

  for (PlayingCard card in hands) {
    cardDecimals.add(card.number.decimal!);
  }

  // 逆順にソート
  // aceを一番最後に処理したいため
  cardDecimals.sort(((a, b) => -1 * a.compareTo(b)));

  // todo: debugprint
  print(cardDecimals);

  for (int num in cardDecimals) {
    if (num >= 11) score += 10;
    if (num >= 2 && num <= 10) score += num;
    if (num == 1) {
      if (score <= 10) {
        score += 11;
      } else {
        score += 1;
      }
    }
  }

  return score;
}

// todo: ディーラーの実装
// todo: チェック?や勝負?の実装
// todo: 掛け金の実装
