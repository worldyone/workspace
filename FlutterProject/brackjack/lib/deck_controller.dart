import 'playing_card.dart';

class DeckController {
  List<PlayingCard> deck = [];

  DeckController() {
    initianize();
    shuffle();
  }

  void initianize() {
    CardSuit.values.forEach((suit) {
      CardNumber.values.forEach((number) {
        deck.add(PlayingCard(
          suit: suit,
          number: number,
        ));
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
