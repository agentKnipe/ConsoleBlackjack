using FunBlackJack.pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunBlackJack.BusinessObjects {
    public class Table {
        internal Shoe TableShoe;
        public List<Player> Players;

        public Table(int player, int decks) {
            Players = new List<Player>();
            TableShoe = new Shoe(decks);

            var isMeIndex = 0;
            if(player > 1) {
                isMeIndex = new Random().Next(0, player - 1);
            }


            for(int i = 0; i < player; i++) {
                var isMe = i == isMeIndex;
                Players.Add(new Player($"Player{i+1}", isMe));
            }

            /* Add the dealer */
            Players.Add(new Player($"Dealer", isDealer: true));
        }

        public void Play() {
            var playing = true;
            var firstRun = true;

            while (playing) {
                var validInput = false;

                /* shuffle if we are low on cards. */
                if(TableShoe.Cards.Count < 5 * Players.Count) {
                    firstRun = true;
                }

                Deal(firstRun);
                ShowTable();
                firstRun = false;

                foreach (var player in Players) {
                    if (player.IsMe) {
                        MePlay(player);
                    }
                    else {
                        DealerAI(player);
                    }
                }


                while (!validInput) {
                    ShowTable(true);
                    GetWinner();
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("Play Again(Y/N)?");
                    var playAgain = Console.ReadKey().KeyChar.ToString().ToUpper();
                    if(playAgain == "N" || playAgain == "Y") {
                        validInput = true;
                    }

                    if(playAgain == "N") {
                        playing = false;
                    }
                    else {
                        ClearTable();
                    }
                }
            }
        }

        public void Deal(bool shuffle = false) {
            if (shuffle) {
                TableShoe.Shuffle();
            }

            for (int i = 0; i < 2; i++) {
                foreach (var player in Players) {
                    var isFaceUp = i > 0;

                    player.Hand.Add(TableShoe.DealCard(isFaceUp));
                }
            }
        }

        public void GetWinner() {
            var winners = new List<Player>();
            var isPush = false;

            var highScore = 0;
            foreach(var player in Players.Where(w => !w.IsBusted)) {
                if((player.TotalValue > highScore && player.TotalValue <= 21) || (player.TotalValueAlt > highScore && player.TotalValueAlt <= 21)) {
                    if(player.TotalValueAlt <= 21 && player.TotalValueAlt > player.TotalValue) {
                        highScore = player.TotalValueAlt;
                    }
                    else {
                        highScore = player.TotalValue;
                    }
                }
            }

            winners = Players.Where(w => w.TotalValue == highScore || w.TotalValueAlt == highScore).ToList();
            if(winners.Count > 1 && winners.Any(w => w.IsDealer)) {
                isPush = true;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Winning Hand: {highScore}");
            if (isPush) {
                Console.WriteLine("Push - No Winners");
            }
            else {
                foreach(var player in winners) {
                    Console.WriteLine($"{player.PlayerName} - Winner!");
                }
            }
        }

        private void ClearTable() {
            foreach(var player in Players) {
                player.Hand.Clear();
                player.IsStand = false;
            }
        }

        private void MePlay(Player player) {
            while (!player.IsBusted && !player.IsStand) {
                Console.Write("H - Hit or S - Stand");
                var choice = Console.ReadKey();

                switch (choice.KeyChar.ToString().ToUpper()) {
                    case "S":
                        player.IsStand = true;
                        break;
                    case "H":
                        player.Hand.Add(TableShoe.DealCard(true));
                        break;
                    default:
                        ShowTable();
                        Console.WriteLine("Invalid Input");
                        MePlay(player);
                        break;
                }

                ShowTable();
            }
        }

        private void DealerAI(Player player) {
            while(!player.IsBusted && !player.IsStand) {
                if (player.TotalValue <= 16 || player.TotalValueAlt <= 16) {
                    player.Hand.Add(TableShoe.DealCard(true));
                }
                else {
                    player.IsStand = true;
                }
            }

            ShowTable();
        }


        private void ShowTable(bool final = false) {
            Console.Clear();
            foreach(var player in Players) {
                if (final) {
                    Console.WriteLine(player.HandDisplayFinal);
                }
                else {
                    Console.WriteLine(player.HandDisplay);
                }
            }
        }

    }
}
