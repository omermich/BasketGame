using System;
using System.Collections.Generic;
using System.Text;
using static BasketGame.Program;

namespace BasketGame.Players
{
    class CheaterPlayer : Player
    {
        private List<int> usedGuesses;

        public CheaterPlayer(string name) : base(name)
        {
            usedGuesses = new List<int>();
        }
        public override int Guess()
        {
            _attempts++;
            int guess;
            usedGuesses = GameSim.GetGuesses();
            do guess = GetRand();
            while (usedGuesses.Contains(guess));
            return guess;
        }
    }
}
