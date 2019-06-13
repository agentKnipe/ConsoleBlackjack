using FunBlackJack.pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunBlackJack.BusinessObjects {
    internal class Deck {
        private List<CardType> CardTypes;
        private List<string> Suits;

        internal List<Card> Cards;

        public Deck() {
            Suits = InitializeSuits();
            CardTypes = InitializeCardTypes();

            CreateDeck();
        }

        private void CreateDeck() {
            var cards = new List<Card>();

            foreach(var suit in Suits) {
                foreach(var cardType in CardTypes) {
                    cards.Add(new Card(cardType, suit));
                }
            }

            Cards = cards;
        }


        private List<string> InitializeSuits() {
            var suits = new List<string>() {
                "Heart", "Club", "Spade", "Diamond"
            };

            return suits;
        }

        private List<CardType> InitializeCardTypes() {
            var cardTypes = new List<CardType>();
            cardTypes.Add(new CardType("Ace", 11, 1));
            cardTypes.Add(new CardType("King", 10, null));
            cardTypes.Add(new CardType("Queen", 10, null));
            cardTypes.Add(new CardType("Jack", 10, null));
            cardTypes.Add(new CardType("10", 10, null));
            cardTypes.Add(new CardType("9", 9, null));
            cardTypes.Add(new CardType("8", 8, null));
            cardTypes.Add(new CardType("7", 7, null));
            cardTypes.Add(new CardType("6", 6, null));
            cardTypes.Add(new CardType("5", 5, null));
            cardTypes.Add(new CardType("4", 4, null));
            cardTypes.Add(new CardType("3", 3, null));
            cardTypes.Add(new CardType("2", 2, null));

            return cardTypes;
        }
    }
}
