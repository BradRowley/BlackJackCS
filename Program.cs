using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackCS
{

    class Deck
    {
        public List<Card> CardsInDeck = new List<Card>();
        public Card DealCard()
        {
            var topCard = CardsInDeck[0];
            CardsInDeck.Remove(topCard);

            return topCard;
        }
        public void MakesNewShuffledCards()
        {
            //   Suits = "Club", "Diamond", "Heart", or "Spade"
            var suits = new List<string>() { "Club", "Diamond", "Heart", "Spade" };

            //   Faces = 2, 3, 4 etc
            var faces = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            foreach (var suit in suits)
            {

                foreach (var face in faces)
                //current face
                {
                    //make a new card
                    var ourCard = new Card()
                    {
                        Face = face,
                        Suit = suit,
                    };


                    CardsInDeck.Add(ourCard);
                }
            }


            var n = CardsInDeck.Count();



            for (var rightIndex = n - 1; rightIndex >= 1; rightIndex--)
            {
                var randomNumberGenerator = new Random();
                var leftIndex = randomNumberGenerator.Next(rightIndex);
                var leftCard = CardsInDeck[rightIndex];
                var rightCard = CardsInDeck[leftIndex];
                CardsInDeck[rightIndex] = rightCard;
                CardsInDeck[leftIndex] = leftCard;
            }
        }
    }

    class Hand
    {
        public List<Card> Cards = new List<Card>();

        public int TotalValue()
        {
            var total = 0;

            foreach (var card in Cards)
            {
                total = total + card.Value();
            }

            return total;
        }

        public bool Busted()
        {
            if (TotalValue() > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Display()
        {
            foreach (var card in Cards)
            {
                Console.WriteLine($"The {card.Face} of {card.Suit}");
            }
            Console.WriteLine($"The total is: {TotalValue()}");
            Console.WriteLine();
        }

        public void AddCardToHand(Card cardToAdd)
        {
            Cards.Add(cardToAdd);
        }
    }

    class Card
    {
        // PROPERTY     Face
        public string Face { get; set; }

        // PROPERTY     Suit
        public string Suit { get; set; }

        public int Value()
        {
            var answer = 0;
            //THIS CREATES VALUES/CAN USE IF METHOD AS WELL
            switch (Face)
            {
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "10":
                    answer = int.Parse(Face);
                    break;

                case "Jack":
                case "Queen":
                case "King":
                    answer = 10;
                    break;

                case "Ace":
                    answer = 11;
                    break;
            }

            return answer;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //LANG if they want to play again....at bottom is where loop ends
            var playAgain = "YES";

            while (playAgain.ToUpper() == "YES")
            {
                var deck = new Deck();
                deck.MakesNewShuffledCards();
                //Player hand
                var player = new Hand();

                // dealer hand
                var dealer = new Hand();

                for (var count = 0; count < 2; count++)
                {
                    player.AddCardToHand(deck.DealCard());
                }

                for (var count = 0; count < 2; count++)
                {
                    dealer.AddCardToHand(deck.DealCard());
                }

                var choice = "";
                while (choice != "STAND" && !player.Busted())
                {
                    Console.WriteLine("------- PLAYER 1 ------");
                    player.Display();

                    //HIT or STAND
                    Console.WriteLine();
                    Console.Write("HIT or STAND? ");
                    choice = Console.ReadLine();
                    // If HIT
                    if (choice == "HIT")
                    {
                        player.AddCardToHand(deck.DealCard());
                    }

                }

                Console.WriteLine("------- PLAYER 1 ------");
                player.Display();

                // While the player isn't busted AND dealer has < 17
                while (!player.Busted() && dealer.TotalValue() < 17)
                {
                    //     - Add a card to the dealer hand 
                    dealer.AddCardToHand(deck.DealCard());
                }

                Console.WriteLine("------- DEALER ------");
                dealer.Display();


                if (player.Busted())
                {
                    Console.WriteLine("Dealer wins!");
                }
                else
                {

                    if (dealer.Busted())
                    {
                        Console.WriteLine("Player wins");
                    }
                    else
                    {

                        if (dealer.TotalValue() > player.TotalValue())
                        {
                            Console.WriteLine("Dealer Wins!");
                        }
                        else
                        {
                            if (player.TotalValue() > dealer.TotalValue())
                            {
                                Console.WriteLine("Player wins");
                            }
                            else
                            {
                                Console.WriteLine("Tie,  Dealer wins");

                            }
                        }
                    }
                }

                Console.WriteLine();
                Console.Write("Play again? YES or NO? ");
                playAgain = Console.ReadLine();
            }
        }
    }
}
