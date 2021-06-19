using System;
using System.Collections.Generic;
using System.Text;

namespace BasketGame
{
    abstract class Player
    {
        protected string _name;
        protected int _attempts;

        public Player(string name)
        {
            _name = name;
            _attempts = 0;
        }
        
        public string GetName() { return _name; }

        public int GetAttempts() { return _attempts; }

        public abstract int Guess();
    }
}
