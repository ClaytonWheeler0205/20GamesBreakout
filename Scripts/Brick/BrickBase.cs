using Godot;
using System;

namespace Game.Brick
{

    public abstract class BrickBase : StaticBody2D
    {
        // Member variables
        protected Color brickColor;
        protected int pointValue = 1;
        public int PointValue
        {
            get { return pointValue; }
            set
            {
                if (value > 0)
                {
                    pointValue = value;
                }
            }
        }

        public abstract void BrickHit();
        public abstract void SetColor(string color);
    }
}