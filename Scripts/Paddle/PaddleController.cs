using Godot;
using Util.ExtensionMethods;

namespace Game.Paddle
{
    /// <summary>
    /// Abstract base class of the two paddle controllers in pong. This class stores the information about the paddle it is controlling.
    /// Concrete implementations will have their own logic on how exactly they make the paddle move.
    /// </summary>
    public abstract class PaddleController : Node
    {
        // Member Variables
        private PaddleBase _paddleToControl;
        public PaddleBase PaddleToControl
        {
            get { return _paddleToControl; }
            set { _paddleToControl = value; }
        }
    }
}