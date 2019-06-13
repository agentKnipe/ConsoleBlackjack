using System;
using System.Collections.Generic;
using System.Text;

namespace FunBlackJack.pocos {
    internal class CardType {
        public string CardName { get; set; }
        public int CardValue { get; set; }
        public int? CardValueAlt { get; set; }

        public string CardNameShort {
            get {
                if(int.TryParse(CardName, out _)) {
                    return CardName;
                }

                return CardName.Substring(0, 1);
            }
        }

        public CardType(string name, int value, int? valueAlt) {
            this.CardName = name;
            this.CardValue = value;
            this.CardValueAlt = valueAlt;
        }
    }
}
