using FunBlackJack.pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunBlackJack.BusinessObjects {
    internal class Shoe {
        public List<Card> Cards;

        public Shoe(int deckCount) {
            Cards = new List<Card>();

            for(int i = 0; i< deckCount; i++) {
                var deck = new Deck();
                Cards.AddRange(deck.Cards);
            }
        }

        public void Shuffle() {
            var rand = new Random();
            Wash(rand.Next(5, 100));
            Riffle(3);
            Strip();
            Riffle(1);
            Split();
        }

        public Card DealCard(bool isFaceUp) {
            var card = Cards[0];
            card.IsFaceUp = isFaceUp;

            Cards.RemoveAt(0);

            return card;
        }

        private void Wash(int count) {
            for(int i = 0; i < count; i++) {
                var rand = new Random();
                var shuffledCards = new List<Card>();

                while (Cards.Any()) {
                    if(Cards.Count > 1) {
                        var index = rand.Next(1, Cards.Count);
                        var card = Cards[index];

                        shuffledCards.Add(card);
                        Cards.RemoveAt(index);
                    }
                    else {
                        shuffledCards.AddRange(Cards);
                        Cards.Clear();
                    }
                }

                Cards = shuffledCards;
            }
        }

        private void Riffle(int count) {
            for (int i = 0; i < count; i++) {
                var rand = new Random();
                var shuffledCards = new List<Card>();
                var packA = new List<Card>();
                var packB = new List<Card>();

                /* split the shoe in approximate halves */
                var halfCount = Cards.Count / 2;
                var splitPointMin = halfCount - rand.Next(1, 5);
                var splitPointMax = halfCount + rand.Next(1, 5);

                var splitPoint = rand.Next(splitPointMin, splitPointMax);
                packA.AddRange(Cards.GetRange(0, splitPoint));
                Cards.RemoveRange(0, splitPoint);
                packB.AddRange(Cards);
                Cards.Clear();

                while(packA.Any() || packB.Any()) {
                    rand = new Random();
                    if (packA.Any()) {
                        var take = rand.Next(1, 5);
                        if(packA.Count > take) {
                            shuffledCards.AddRange(packA.GetRange(0, take));
                            packA.RemoveRange(0, take);
                        }
                        else {
                            shuffledCards.AddRange(packA);
                            packA.Clear();
                        }
                    }

                    if (packB.Any()) {
                        var take = rand.Next(1, 5);
                        if(packB.Count > take) {
                            shuffledCards.AddRange(packB.GetRange(0, take));
                            packB.RemoveRange(0, take);
                        }
                        else {
                            shuffledCards.AddRange(packB);
                            packB.Clear();
                        }
                    }
                }

                Cards = shuffledCards;
            }
        }

        private void Strip() {
            var rand = new Random();
            var shuffledCards = new List<Card>();

            var stripSizeCount = (int)Math.Floor(Cards.Count / 5.0);
            var stripPointMin = stripSizeCount - rand.Next(1, 5);
            var stripPointMax = stripSizeCount + rand.Next(1, 5);
            var stripSize = rand.Next(stripPointMin, stripPointMax);

            while (Cards.Any()) {
                if(Cards.Count >= stripSize) {
                    shuffledCards.AddRange(Cards.GetRange(0, stripSize));
                    Cards.RemoveRange(0, stripSize);
                }
                else {
                    shuffledCards.AddRange(Cards);
                    Cards.Clear();
                }
            }

            Cards = shuffledCards;
        }

        private void Split() {
            var rand = new Random();
            var shuffledCards = new List<Card>();
            var packA = new List<Card>();
            var packB = new List<Card>();

            /* split the shoe in approximate halves */
            var halfCount = Cards.Count / 2;
            var splitPointMin = halfCount - rand.Next(1, 5);
            var splitPointMax = halfCount + rand.Next(1, 5);

            var splitPoint = rand.Next(splitPointMin, splitPointMax);
            packA.AddRange(Cards.GetRange(0, splitPoint));
            Cards.RemoveRange(0, splitPoint);
            packB.AddRange(Cards);
            Cards.Clear();

            Cards.AddRange(packB);
            Cards.AddRange(packA);
        }
    }
}
