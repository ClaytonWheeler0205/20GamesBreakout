using Game.Paddle;
using Godot;
using Util.ExtensionMethods;

namespace Game
{

    public class PaddleManager : Node, IGameManager
    {
        // Node References
        private PaddleBase _paddleRef;
        private PaddleController _paddleController;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _paddleRef = GetNode<PaddleBase>("%Paddle");
            _paddleController = GetNode<PaddleController>("%PaddleController");
            CheckReferences();
        }

        private void CheckReferences()
        {
            if (!_paddleRef.IsValid())
            {
                GD.PrintErr("ERROR: Paddle node is not valid!");
            }
            if (!_paddleController.IsValid())
            {
                GD.PrintErr("ERROR: Paddle controller node is not valid!");
            }
        }

        public bool EndGame()
        {
            if (!_paddleRef.IsValid()) { return false; }
            _paddleRef.ResetPaddle();

            if (!_paddleController.IsValid()) { return false; }
            _paddleController.PaddleToControl = null;

            return true;
        }

        public bool StartGame()
        {
            if (!_paddleRef.IsValid()) { return false; }
            if (!_paddleController.IsValid()) { return false; }
            _paddleController.PaddleToControl = _paddleRef;

            return true;
        }
    }
}