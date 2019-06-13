using System;
using System.Collections.Generic;
using System.Text;

namespace FunBlackJack.pocos {
    internal class Card {
        public CardType Type { get; set; }

        public string Suit { get; set; }

        public bool IsFaceUp { get; set; }

        public string CardName {
            get {
                return Type.CardName;
            }
        }

        public string CardNameShort {
            get {
                return Type.CardNameShort;
            }
        }

        public int CardValue {
            get {
                return Type.CardValue;
            }
        }

        public int? CardValueAlt {
            get {
                return Type.CardValueAlt;
            }
        }

        public Card(CardType type, string suit, bool isFaceUp = true) {
            this.Type = type;
            this.Suit = suit;
            this.IsFaceUp = isFaceUp;
        }

    }
}
