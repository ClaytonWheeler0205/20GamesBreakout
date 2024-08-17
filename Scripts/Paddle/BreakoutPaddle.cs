using Godot;
using System;

namespace Game.Paddle
{

    public class BreakoutPaddle : PaddleBase
    {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";
        private float _paddleWidth;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            _paddleWidth = PaddleSize * Transform.Scale.x;
        }

        public override float GetPaddleSize()
        {
            return _paddleWidth;
        }
    }
}