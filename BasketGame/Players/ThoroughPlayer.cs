using System;
using System.Collections.Generic;
using System.Text;
using static BasketGame.Program;

namespace BasketGame.Players
{
    class ThoroughPlayer : Player
    {
        private int _currentGuess;

        public ThoroughPlayer(string name) : base(name) {
            _currentGuess = BASKET_WEIGHT_MIN;
        }
        public override int Guess()
        {
            _attempts++;
            _currentGuess++;
            return _currentGuess - 1;
        }
    }
}
