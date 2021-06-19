using System;
using System.Collections.Generic;
using System.Text;
using static BasketGame.Program;

namespace BasketGame.Players
{
    class MemoryPlayer : Player
    {
        private List<int> usedGuesses;

        public MemoryPlayer(string name) : base(name)
        {
            usedGuesses = new List<int>();
        }

        public override int Guess()
        {
            _attempts++;
            int guess;
            do guess = GetRand(); 
            while (usedGuesses.Contains(guess));
            usedGuesses.Add(guess);
            return guess;
        }
    }
}
