using Game.UI;
using Godot;
using Util.ExtensionMethods;

namespace Game
{

    public class BreakoutGame : Node
    {
        // Managers
        IGameManager _paddleManager;
        IGameManager _ballManager;
        IGameManager _brickManager;
        IGameScore _scoreUI;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            SetNodeReferences();
            StartGame();
        }

        private void SetNodeReferences()
        {
            _paddleManager = GetNode<IGameManager>("%PaddleManager");
            if (!(_paddleManager is Node paddleNode))
            {
                GD.PrintErr("ERROR: Breakout paddle manager is not a node!");
                GetTree().Quit();
            }
            else if (!paddleNode.IsValid())
            {
                GD.PrintErr("ERROR: Breakout paddle manager is not valid");
                GetTree().Quit();
            }

            _ballManager = GetNode<IGameManager>("%BallManager");
            if (!(_ballManager is Node ballNode))
            {
                GD.PrintErr("ERROR: Breakout ball manager is not a node!");
                GetTree().Quit();
            }
            else if (!ballNode.IsValid())
            {
                GD.PrintErr("ERROR: Breakout ball manager is not valid!");
                GetTree().Quit();
            }
            _brickManager = GetNode<IGameManager>("%BrickManager");
            if (!(_brickManager is Node brickNode))
            {
                GD.PrintErr("ERROR: Breakout brick manager is not a node!");
                GetTree().Quit();
            }
            else if (!brickNode.IsValid())
            {
                GD.PrintErr("ERROR: Breakout brick manager is not valid!");
                GetTree().Quit();
            }
            _scoreUI = GetNode<IGameScore>("%ScoreUI");
            if (!(_scoreUI is Node scoreNode))
            {
                GD.PrintErr("ERROR: Score UI is not a node!");
                GetTree().Quit();
            }
            else if (!scoreNode.IsValid())
            {
                GD.PrintErr("ERROR: Score UI is not valid!");
                GetTree().Quit();
            }
        }

        private void StartGame()
        {
            if (!_paddleManager.StartGame())
            {
                GetTree().Quit();
            }
            if (!_ballManager.StartGame())
            {
                GetTree().Quit();
            }
            if (!_brickManager.StartGame())
            {
                GetTree().Quit();
            }
        }

        private void EndGame()
        {
            if (!_paddleManager.EndGame())
            {
                GetTree().Quit();
            }
            if (!_ballManager.EndGame())
            {
                GetTree().Quit();
            }
            if (!_brickManager.EndGame())
            {
                GetTree().Quit();
            }
            _scoreUI.ResetScore();
        }

        public void OnBricksCleared()
        {
            EndGame();
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event.IsActionPressed("reset_game"))
            {
                EndGame();
            }
            else if (@event.IsActionPressed("test_play"))
            {
                StartGame();
            }
        }
    }
}