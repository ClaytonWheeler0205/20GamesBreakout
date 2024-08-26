using Godot;
using System;
using Util.ExtensionMethods;

namespace Game.Brick
{

    public class BreakoutBrick : BrickBase
    {
        [Signal]
        delegate void AwardPoints(int pointsToGive);

        public override void BrickHit()
        {
            EmitSignal(nameof(AwardPoints), pointValue);
            this.SafeQueueFree();
        }

        public override void SetColor(string color)
        {
            switch (color)
            {
                case "red":
                    brickColor = new Color(1, 0, 0, 1);
                    break;
                case "orange":
                    brickColor = new Color(1, 0.54902f, 0, 1);
                    break;
                case "green":
                    brickColor = new Color(0, 1, 0, 1);
                    break;
                case "yellow":
                    brickColor = new Color(1, 1, 0, 1);
                    break;
            }
            Modulate = brickColor;
        }
    }
}