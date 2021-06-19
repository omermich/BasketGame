using System;
using System.Collections.Generic;
using BasketGame.Players;

namespace BasketGame
{
    class Program
    {
        // Min/Max of basket weight.
        public const int BASKET_WEIGHT_MIN = 40,
                          BASKET_WEIGHT_MAX = 140;

        // Basket weight to be generated.
        private static int basketWeight;

        // List of players that will be participating.
        private static List<Player> players;

        // Game simulation class object.
        private static GameSim game;

        static void Main(string[] args)
        {
            // Instantiate Player list.
            players = new List<Player>();

            // Generate basket weight.
            basketWeight = GetRand();

            // Display initialization message with generated answer.
            Console.WriteLine("GAME CONSOLE INITIATED. " +
                $"\n *** The weight of the basket is: {basketWeight} *** \n" +
                "PRESS ANY KEY TO START GAME SIMULATION. \n");
            Console.ReadLine();

            // Create players with params from user input.
            InitiatePlayers();

            // Create instance of GameSimulation.
            game = new GameSim(players);

            // Run game simulation.
            game.Run();

        }

        /// <summary>
        /// Generates an integer within the given range,
        /// min/max of basket weight contants.
        /// </summary>
        public static int GetRand()
        {
            var rand = new Random();
            return rand.Next(BASKET_WEIGHT_MIN, BASKET_WEIGHT_MAX + 1);
        }

        /// <summary>
        /// Encapsulation Getter.
        /// Returns the generated random basket weight.
        /// </summary>
        public static int GetAnswer() { return basketWeight; }

        /// <summary>
        /// Interacts with the user via console,
        /// to instantiate players using user input.
        /// </summary>
        private static void InitiatePlayers()
        {
            int numOfPlayers;
            try
            {
                // user enters number of players.
                do {
                    Console.Write("Enter number of players (2-8): ");
                    numOfPlayers = Int32.Parse(Console.ReadLine());
                } while (numOfPlayers < 2 || numOfPlayers > 8);

                // loops through given number of players.
                for (int i = 0; i < numOfPlayers; i++)
                {
                    Player player;

                    // user enters name of each player.
                    Console.WriteLine($"Enter name of player {i + 1}: ");
                    string name = Console.ReadLine();

                    // user enters type of each player.
                    Console.WriteLine("Enter type of player (1-5): ");
                    Console.WriteLine(" (1) Random \n (2) Memory");
                    Console.WriteLine(" (3) Thorough \n (4) Cheater \n (5) Thorough-Cheater");
                    int type = Int32.Parse(Console.ReadLine());

                    // switch case to translate given digit to player type.
                    switch (type)
                    {
                        case 1:
                            player = new RandomPlayer(name);
                            break;
                        case 2:
                            player = new MemoryPlayer(name);
                            break;
                        case 3:
                            player = new ThoroughPlayer(name);
                            break;
                        case 4:
                            player = new CheaterPlayer(name);
                            break;
                        case 5:
                            player = new ThoroughCheaterPlayer(name);
                            break;
                        default:
                            throw new Exception();
                    }

                    // add created player to class player list.
                    players.Add(player);
                }
            }

            // exception thrown if user enters invalid input.
            catch (Exception)
            {
                Console.WriteLine("Failed to parse input. Please try again.");
                Environment.Exit(0);
            }
        }
    }
}
