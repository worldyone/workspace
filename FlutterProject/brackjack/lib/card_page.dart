import 'dart:io';

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
  List<PlayingCard> hands = [];
  int handScore = 0;
  List<PlayingCard> dealerHands = [];
  int dealerHandScore = 0;
  final DeckController _dc = DeckController();
  late PlayingCard card;
  int bet = 100;
  int possessionMoney = 10000;
  String statusMessage = "";
  bool inGame = true;

  void _drawCard() {
    setState(() {
      if (_dc.deck.isNotEmpty) {
        card = _dc.drawCard();
        hands.add(card);
        handScore = calculateHandScore(hands);
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
            Text(
              'You posses $possessionMoney'
              '\n'
              '$statusMessage',
            ),
            Expanded(
              child: ListView.builder(
                  scrollDirection: Axis.horizontal,
                  itemCount: dealerHands.length,
                  itemBuilder: (context, index) {
                    return dealerHands[index].opened
                        ? dealerHands[index].image
                        : Image.asset(
                            "assets/cards/card_back.png",
                            width: 96,
                            height: 192,
                          );
                  }),
            ),
            const Text('Dealer\'s hands'),
            Expanded(
              // todo: 4枚目以上が画面外にはみ出してしまうので、下に並べられるようにしたい
              child: ListView.builder(
                  scrollDirection: Axis.horizontal,
                  itemCount: hands.length,
                  itemBuilder: (context, index) {
                    return hands[index].image;
                  }),
            ),
            const Text(
              'Yours hands',
            ),
            Text(
              'Your hands score is $handScore.'
              '    '
              '${(handScore > 21) ? "burst" : ""}',
            ),
          ],
        ),
      ),
      floatingActionButton: GestureDetector(
        onTap: () {
          // 試合中であればカードを引くことができる
          if (inGame) {
            _drawCard();

            if (calculateHandScore(hands) > 21) {
              setState(() {
                statusMessage = 'バーストしました';
                possessionMoney -= bet;
                inGame = false;
              });
            }
          }
        },
        child: Image.asset(
          "assets/cards/cardgame_deck.png",
          width: 160,
        ),
      ),
      bottomNavigationBar: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              FloatingActionButton(
                onPressed: () {
                  // デッキの初期化
                  _dc.initialize();
                  _dc.shuffle();

                  setState(() {
                    // ディーラーの初期化
                    dealerHands = [];
                    dealerHands.add(_dc.drawCard());
                    dealerHands.add(_dc.drawCard());
                    dealerHands[1].opened = false; // 2枚目を裏向きにする

                    // 手札を初期化
                    hands = [];
                    handScore = 0;

                    // 掛け金を初期化
                    // todo: 初期化しない方がいいか？
                    bet = 100;

                    // ゲーム中である
                    inGame = true;
                  });
                },
                child: const Icon(Icons.arrow_right, size: 48),
              ),
              const Text('リセット')
            ],
          ),
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              FloatingActionButton(
                onPressed: () {
                  // バーストしていた場合は、勝負できない
                  if (calculateHandScore(hands) > 21) return;
                  // ゲームが終了している場合は、勝負できない
                  if (!inGame) return;

                  // ディーラーの処理
                  // 裏向きのカードを表向きにする
                  setState(() {
                    dealerHands[1].opened = true;
                  });
                  // 17以上になるまでカードを引き続ける
                  while (calculateHandScore(dealerHands) < 16) {
                    setState(() {
                      dealerHands.add(_dc.drawCard());
                    });
                    sleep(const Duration(seconds: 1));
                  }

                  // 勝敗処理
                  int myScore = calculateHandScore(hands);
                  int dealerScore = calculateHandScore(dealerHands);
                  // 勝利
                  if (myScore > dealerScore || dealerScore > 21) {
                    setState(() {
                      possessionMoney += bet;
                      statusMessage = 'ディーラーに勝ちました!!';
                    });
                    // 敗北
                  } else if (myScore < dealerScore) {
                    setState(() {
                      possessionMoney -= bet;
                      statusMessage = 'ディーラーに負けました…';
                    });
                    // 引き分け
                  } else {
                    setState(() {
                      statusMessage = 'ディーラーと引き分けました';
                    });
                  }

                  // ゲーム終了 リセットが掛かるまで勝負ができず、カードも引けない
                  inGame = false;
                },
                child: const Icon(Icons.gavel),
              ),
              const Text('勝負!')
            ],
          ),
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              FloatingActionButton(
                onPressed: () {
                  setState(() {
                    bet += 100;
                  });
                },
                child: const Icon(Icons.attach_money),
              ),
              Text('掛け金: $bet'),
            ],
          ),
        ],
      ),
    );
  }
}

// 手札の合計点数を計算して返す
int calculateHandScore(List<PlayingCard> hands) {
  int score = 0;
  List<int> cardDecimals = [];
  int aceCount = 0;

  for (PlayingCard card in hands) {
    cardDecimals.add(card.number.decimal!);
  }

  // todo: debugprint
  print(cardDecimals);

  for (int num in cardDecimals) {
    if (num >= 11) score += 10;
    if (num >= 1 && num <= 10) score += num;
  }

  aceCount =
      cardDecimals.where((int cardDecimal) => cardDecimal == 1).toList().length;

  for (int i = 0; i < aceCount; i++) {
    if (score + 10 <= 21) {
      score += 10;
    }
  }

  return score;
}

// todo: スタート画面の実装
// todo: 掛け金をより簡単に設定できるようにする
