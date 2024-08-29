using Godot;

namespace Game.Paddle
{

    public abstract class PaddleBase : KinematicBody2D
    {
        // Member variables
        [Export]
        private float _speed = 25000.0f;
        private Vector2 _velocity = Vector2.Zero;
        private Vector2 _startPos = Vector2.Zero;
        private const int PADDLE_SPRITE_SIZE = 16;
        protected int PaddleSize => PADDLE_SPRITE_SIZE;

        // abstract methods
        public abstract float GetPaddleSize();

        public override void _Ready()
        {
            _startPos = Position;
        }

        public void SetDirection(Vector2 newDirection)
        {
            Position = new Vector2(Position.x, _startPos.y);
            _velocity = newDirection;
        }

        public virtual void ResetPaddle()
        {
            _velocity = Vector2.Zero;
            Position = _startPos;
        }

        public override void _PhysicsProcess(float delta)
        {
            MoveAndSlide(_velocity * _speed);
        }
    }
}