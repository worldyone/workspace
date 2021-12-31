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

class PlayingCard {
  CardSuit suit;
  CardNumber number;
  Image? image;
  bool? opened;

  PlayingCard({
    required this.suit,
    required this.number,
    this.opened,
  });
}
