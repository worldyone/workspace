import 'package:flutter/material.dart';

import 'playing_card.dart';

class DeckController {
  List<PlayingCard> deck = [];

  DeckController() {
    initialize();
    shuffle();
  }

  void initialize() {
    deck = [];
    CardSuit.values.forEach((suit) {
      CardNumber.values.forEach((number) {
        deck.add(
          PlayingCard(
            suit: suit,
            number: number,
            image: Image.asset(
              "assets/cards/card_" +
                  suit.name +
                  "_" +
                  number.zfillDecimal +
                  ".png",
              width: 96,
              height: 192,
            ),
          ),
        );
      });
    });
  }

  void shuffle() {
    deck.shuffle();
  }

  // デッキを引く側がデッキがないかを確認する
  PlayingCard drawCard() {
    return deck.removeAt(0);
  }
}
