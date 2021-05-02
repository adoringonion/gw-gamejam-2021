using System;

namespace gw_game_jam.Scripts
{
    public class Score
    {
        public int  Value { get; }


        public Score(int value)
        {
            Value = value;
        }

        public Score Add(int addScore)
        {
            return new Score(Value + addScore);
        }

        public Score Sub(int removeScore)
        {
            return new Score(Value - removeScore);
        }
    }
}