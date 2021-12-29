enum CardSuit {
  spade,
  heart,
  clover,
  dia,
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

  PlayingCard({
    required this.suit,
    required this.number,
  });
}
