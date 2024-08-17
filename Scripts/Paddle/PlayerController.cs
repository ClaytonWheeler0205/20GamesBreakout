using Godot;
using System;
using Util.ExtensionMethods;

namespace Game.Paddle
{

    public class PlayerController : PaddleController
    {
        // Member variables
        private Vector2 _direction;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _direction = Vector2.Zero;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            _direction.x = Input.GetAxis("move_left", "move_right");

            if (PaddleToControl.IsValid())
            {
                PaddleToControl.SetDirection(_direction);
            }
        }
    }
}