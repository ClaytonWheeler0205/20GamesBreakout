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
        private Vector2 _startingPaddleScale;

        private bool _hasBallHitCeiling = false;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            _paddleWidth = PaddleSize * Transform.Scale.x;
            _startingPaddleScale = Scale;
        }

        public override float GetPaddleSize()
        {
            return _paddleWidth;
        }

        public override void ResetPaddle()
        {
            Scale = _startingPaddleScale;
            _paddleWidth = PaddleSize * Transform.Scale.x;
            _hasBallHitCeiling = false;
            base.ResetPaddle();
        }

        public void OnCeilingHit()
        {
            if (!_hasBallHitCeiling)
            {
                _hasBallHitCeiling = true;
                Scale = new Vector2(Scale.x / 2, Scale.y);
                _paddleWidth = PaddleSize * Transform.Scale.x;
            }
        }
    }
}