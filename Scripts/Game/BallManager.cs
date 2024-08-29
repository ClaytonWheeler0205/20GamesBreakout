using Game.Ball;
using Godot;
using System;
using Util.ExtensionMethods;

namespace Game
{

    public class BallManager : Node, IGameManager
    {

        private BallBase _ballRef;
        private Timer _timerRef;

        /// <summary>
        /// How long the timer should run until the ball should move once again.
        /// </summary>
        [Export]
        private float _pauseTime = 3.0f;

        private bool _isActive = false;

        [Signal]
        delegate void LifeLost();

        public override void _Ready()
        {
            _ballRef = GetNode<BallBase>("Ball");
            _timerRef = GetNode<Timer>("%PauseTimer");
            CheckReferences();
        }

        private void CheckReferences()
        {
            if (!_ballRef.IsValid())
            {
                GD.PrintErr("ERROR: Ball node reference is not valid!");
            }
            if (!_timerRef.IsValid())
            {
                GD.PrintErr("ERROR: Timer node reference is not valid!");
            }
        }

        public bool StartGame()
        {
            if (!_ballRef.IsValid()) { return false; }

            _ballRef.StartBall();
            _isActive = true;
            return true;
        }

        public bool EndGame()
        {
            if (!_ballRef.IsValid()) { return false; }

            _ballRef.ResetBall();
            _timerRef.Stop();
            _isActive = false;
            return true;
        }

        public void OnBallGutterHit()
        {
            if (!_timerRef.IsValid()) { return; }
            _timerRef.Start(_pauseTime);
            EmitSignal("LifeLost");
        }

        public void OnPauseTimerTimeout()
        {
            if (!_isActive) { return; }
            if (!_ballRef.IsValid()) { return; }

            _ballRef.StartBall();
        }
    }
}