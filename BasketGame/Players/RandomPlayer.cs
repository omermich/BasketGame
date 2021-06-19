using System;
using System.Collections.Generic;
using System.Text;
using static BasketGame.Program;

namespace BasketGame.Players
{
    class RandomPlayer : Player
    {
        public RandomPlayer(string name) : base(name) { }

        public override int Guess()
        {
            _attempts++;
            return GetRand();
        }
    }
}
