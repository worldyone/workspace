import 'package:flutter/material.dart';

enum CardSuit {
  spade,
  heart,
  club,
  diamond,
}

enum CardNumber {
  ace,
  two,
  three,
  four,
  five,
  six,
  seven,
  eight,
  nine,
  ten,
  jack,
  queen,
  king,
}

extension CardNumberEx on CardNumber {
  static Map<CardNumber, int> cardNumber2decimal = {
    CardNumber.ace: 1,
    CardNumber.two: 2,
    CardNumber.three: 3,
    CardNumber.four: 4,
    CardNumber.five: 5,
    CardNumber.six: 6,
    CardNumber.seven: 7,
    CardNumber.eight: 8,
    CardNumber.nine: 9,
    CardNumber.ten: 10,
    CardNumber.jack: 11,
    CardNumber.queen: 12,
    CardNumber.king: 13,
  };
  int? get decimal => cardNumber2decimal[this];
  String get zfillDecimal =>
      cardNumber2decimal[this].toString().padLeft(2, '0');
}

class PlayingCard {
  CardSuit suit;
  CardNumber number;
  Image image;
  bool opened;

  PlayingCard({
    required this.suit,
    required this.number,
    required this.image,
    required this.opened,
  });
}
