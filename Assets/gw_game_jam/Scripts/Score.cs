using System;

namespace gw_game_jam.Scripts
{
    public class Score
    {
        private readonly int _value;

        public int  Value => this._value;


        public Score(int value)
        {
            this._value = value;
        }

        public Score add(int addScore)
        {
            return new Score(_value + addScore);
        }

        public Score remove(int removeScore)
        {
            return new Score(_value - removeScore);
        }
    }
}