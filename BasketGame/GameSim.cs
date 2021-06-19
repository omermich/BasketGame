using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static BasketGame.Program;

namespace BasketGame
{
    class GameSim
    {
        private readonly List<Player> _players;
        private static List<int> _guesses;

        private int _totalAttempts;
        private bool timesUp = false;

        System.Timers.Timer timer;
        readonly CancellationTokenSource cts;
        readonly ParallelOptions po;

        private const int MAX_ATTEMPTS = 100;
        private const int TIMER_END = 1500;

        public GameSim(List<Player> players)
        {
            _players = players;
            _guesses = new List<int>();
            _totalAttempts = 0;

            cts = new CancellationTokenSource();
            po = new ParallelOptions();
            po.CancellationToken = cts.Token;
        }

        public static List<int> GetGuesses() { return _guesses; }

        /// <summary>
        /// 
        /// Runs the coarse of the game given the list of players.
        /// 
        /// Parallel continuous tasks are given for each player,
        /// in which the they will attempt a guess.
        /// 
        /// In case the player guessed incorrectly, their task will
        /// be delayed for the delta between their guess and the correct answer
        /// in miliseconds.
        /// 
        /// The game will be over once:
        /// 1. A player has guessed correctly.
        /// 2. 100 attempts have been made.
        /// 3. 1500ms have passed since the game began.
        /// 
        /// </summary>
        public void Run()
        {
            int answer = GetAnswer();
            bool gameOver = false, maxAttemptsReached = false;
            Player winner = null;
            timer = new System.Timers.Timer(TIMER_END);
            timer.Elapsed += TimesUp;
            timer.AutoReset = false;
            timer.Enabled = true;

            Console.WriteLine("\n *** GAME STARTED! *** \n");
            
            // As long as the game isn't over, 
            // continue cycling through players for guesses.
            while (!gameOver) {
                try
                {
                    // Parallel task for each player.
                    Parallel.ForEach(_players, po, (player) =>
                    {
                        // Catch operation-canceled exception in case game is over.
                        try
                        {
                            po.CancellationToken.ThrowIfCancellationRequested();
                        }
                        catch (OperationCanceledException)
                        {
                        }

                        // Increment total player attempts.
                        _totalAttempts++;

                        // Activate player's Guessing function!
                        int guess = player.Guess();

                        
                        if (guess == answer)
                        {
                            // player won!
                            gameOver = true;
                            winner = player;
                            cts.Cancel();
                        }
                        else if (_totalAttempts == MAX_ATTEMPTS)
                        {
                            // max attempts reached
                            maxAttemptsReached = true;
                            gameOver = true;
                            cts.Cancel();
                        }
                        else
                        {
                            // wrong guess - 
                            // wait for delta between guess and answer (in miliseconds).
                            int sleepTime = Math.Abs(guess - answer);
                            Console.WriteLine($"{player.GetName()} is sleeping for {sleepTime}ms");
                            System.Threading.Thread.Sleep(sleepTime);
                        }
                        
                    });
                }
                catch (OperationCanceledException)
                {
                    if (maxAttemptsReached)
                    {
                        Console.WriteLine("\n\n *** Maximum Attempts Reached ***");
                    }
                    else if (timesUp)
                    {
                        gameOver = true;
                        Console.WriteLine($"\n\n *** Time's up! {TIMER_END}ms passed. ***");
                    }
                    else
                    {
                        Console.WriteLine
                            ($"\n\n *** WINNER IS: {winner.GetName()} " +
                            $"| ATTEMPTS: {winner.GetAttempts()} ***");
                    }
                }
            }
        }

        // Timer elapsed.
        private void TimesUp(Object source, ElapsedEventArgs e)
        {
            timesUp = true;
            cts.Cancel();
        }
    }
}
