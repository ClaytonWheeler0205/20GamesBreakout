using Godot;
using Util.ExtensionMethods;

namespace Game
{

    public class BreakoutGame : Node
    {
        // Managers
        IGameManager _paddleManager;

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
        }

        private void StartGame()
        {
            if (!_paddleManager.StartGame())
            {
                GetTree().Quit();
            }
        }
    }
}