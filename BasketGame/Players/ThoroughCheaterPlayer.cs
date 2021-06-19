using System;
using System.Collections.Generic;
using System.Text;
using static BasketGame.Program;

namespace BasketGame.Players
{
    class ThoroughCheaterPlayer : Player
    {

        private List<int> usedGuesses;
        private int _currentGuess;

        public ThoroughCheaterPlayer(string name) : base(name)
        {
            usedGuesses = new List<int>();
            _currentGuess = BASKET_WEIGHT_MIN;
        }

        public override int Guess()
        {
            _attempts++;
            usedGuesses = GameSim.GetGuesses();
            do _currentGuess++;
            while (usedGuesses.Contains(_currentGuess - 1));
            return _currentGuess;
        }
    }
}
