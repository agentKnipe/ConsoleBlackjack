using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunBlackJack.pocos {
    public class Player {
        public string PlayerName;
        internal List<Card> Hand;
        public bool IsStand;
        public bool IsMe;
        public bool IsDealer;

        public int ShownValue {
            get {
                return Hand.Where(w => w.IsFaceUp).Sum(s => s.CardValue);
            }
        }

        public int ShownValueAlt {
            get {
                return Hand.Where(w => w.IsFaceUp).Sum(s => s.CardValueAlt ?? s.CardValue);
            }
        }

        public int TotalValue {
            get {
                return Hand.Sum(s => s.CardValue);
            }
        }

        public int TotalValueAlt {
            get {
                return Hand.Sum(s => s.CardValueAlt ?? s.CardValue);
            }
        }

        public Player(string name, bool isMe = false, bool isDealer = false) {
            PlayerName = isMe ? "Me" : name;
            Hand = new List<Card>();
            IsDealer = isDealer;
            IsMe = isMe;
        }

        public bool IsBusted {
            get {
                if(TotalValue > 21 && TotalValueAlt > 21) {
                    return true;
                }

                return false;
            }
        }

        public string ShowCards {
            get {
                var showing = new StringBuilder();
                foreach (var card in Hand) {
                    if (card.IsFaceUp) {
                        if (showing.Length > 0) {
                            showing.Append(",");
                        }
                        showing.Append($"{card.CardNameShort} {card.Suit}");
                    }
                }

                return showing.ToString();
            }
        }

        public string AllCards {
            get {
                var cards = new StringBuilder();
                foreach (var card in Hand) {
                    if (cards.Length > 0) {
                        cards.Append(",");
                    }
                    cards.Append($"{card.CardNameShort} {card.Suit}");
                }

                return cards.ToString();
            }
        }

        public string HandDisplay {
            get {
                var playerHand = string.Empty;
                playerHand = $"{PlayerName} {Hand.Count} cards.";

                if (IsMe) {
                    playerHand = $"{playerHand}. {AllCards}. Total Value:{TotalValue}";

                    if (TotalValue != TotalValueAlt) {
                        playerHand = $"{playerHand}({TotalValueAlt})";
                    }
                }
                else {
                    if (IsBusted) {
                        playerHand = $"{playerHand}. {AllCards} showing. Showing Value:{TotalValue}";
                        if (TotalValue != TotalValue) {
                            playerHand = $"{playerHand}({TotalValueAlt})";
                        }
                    }
                    else {
                        playerHand = $"{playerHand}. {ShowCards} showing. Showing Value:{ShownValue}";
                        if (ShownValue != ShownValueAlt) {
                            playerHand = $"{playerHand}({ShownValueAlt})";
                        }
                    }
                }

                if (IsBusted) {
                    playerHand = $"{playerHand} - BUSTED!";
                }

                if (IsStand) {
                    playerHand = $"{playerHand} - Stand";
                }

                return playerHand;
            }
        }

        public string HandDisplayFinal {
            get {
                var playerHand = string.Empty;
                playerHand = $"{PlayerName} {Hand.Count} cards.";

                playerHand = $"{playerHand}. {AllCards}. Total Value:{TotalValue}";

                if (TotalValue != TotalValueAlt) {
                    playerHand = $"{playerHand}({TotalValueAlt})";
                }

                if (IsBusted) {
                    playerHand = $"{playerHand} - BUSTED!";
                }

                if (IsStand) {
                    playerHand = $"{playerHand} - Stand";
                }

                return playerHand;
            }
        }
    }
}
