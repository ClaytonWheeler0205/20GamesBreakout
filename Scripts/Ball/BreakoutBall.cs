using System;
using Game.Paddle;
using Godot;
using Util.ExtensionMethods;


namespace Game.Ball
{

    public class BreakoutBall : BallBase
    {
        /// <summary>
        /// When the ball hits certain bricks, the current speed of the ball will be increased by this amount.
        /// </summary>
        [Export]
        private float _speedIncrease = 10.0f;

        /// <summary>
        /// The true speed of the ball. Determined by the base ball's starting speed plus any speed increased added by hitting bricks
        /// </summary>
        private float _currentSpeed;

        /// <summary>
        /// Reference to the sprite node in the breakout ball scene tree
        /// </summary>
        private Sprite _spriteRef;

        // Min and max X values representing the left and right of the arena to give the breakout ball a random starting x position
        [Export]
        private float _minX;
        [Export]
        private float _maxX;

        // Node groups for handling the collision of specific objects in the game
        private const string PADDLE_NODE_GROUP = "Paddle";
        private const string GUTTER_NODE_GROUP = "Gutter";
        private const string BRICK_NODE_GROUP = "Brick";

        //variables for calculating the bounce angle
        private const float MIN_BOUNCE_ANGLE = (float)(Math.PI / 6); // 30 degrees
        private const float MAX_BOUNCE_ANGLE = (float)(5 * Math.PI / 6); // 150 degrees
        private const float RIGHT_ANGLE = (float)(Math.PI / 2);
        // Maximum amount the right angle will change by when determining the bounce angle of the ball hitting the paddle.
        private const float MAX_DELTA_ANGLE = (float)(Math.PI / 3); // 60 degrees 

        /// <summary>
        /// Boolean to make sure handling any collision with the paddle occurs only one time the ball touches the paddle
        /// Ensures that bouncing away from the paddle occurs only once. Set to true when the ball collides with a paddle
        /// and false if the ball isn't colliding with the paddle.
        /// </summary>
        private bool _hasHitPaddle;

        /// <summary>
        /// Signal to let the game know that the ball has hit the gutter in the arena.
        /// </summary>
        [Signal]
        delegate void GutterHit();

        // Called when the node enters the scene tree for the first time.

        public override void _Ready()
        {
            _spriteRef = GetNode<Sprite>("%Sprite");
            if (!_spriteRef.IsValid())
            {
                GD.PrintErr("ERROR: Sprite child node not found!");
            }
            base._Ready();
        }

        public override void ResetBall()
        {
            if (_spriteRef.IsValid())
            {
                _spriteRef.Visible = false;
            }
            base.ResetBall();
        }

        public override void StartBall()
        {
            // set the ball to the center of the arena and give it a random x position. Also resets its angle.
            float yPos = StartPos.y;
            GD.Randomize();
            float xPos = (float)GD.RandRange(_minX, _maxX);
            Position = new Vector2(xPos, yPos);

            float angle = (float)GD.RandRange((-8 * Math.PI / 18), (8 * Math.PI / 18));

            // Set the ball's direction, speed, and visibility
            Direction = new Vector2(0, 1).Rotated(angle);
            _currentSpeed = Speed;
            if (_spriteRef.IsValid())
            {
                _spriteRef.Visible = true;
            }
            base.StartBall();
        }

        protected override void MoveBall(float delta)
        {
            // Move the ball and grab a KinematicCollision2D if we have hit something
            KinematicCollision2D collision = MoveAndCollide(Direction * _currentSpeed * delta);
            if (collision.IsValid())
            {
                HandleCollision(collision);
            }
            else
            {
                // If we didn't hit anything, then we definitely didn't hit the paddle!
                _hasHitPaddle = false;
            }
        }

        protected override void HandleCollision(KinematicCollision2D collision)
        {
            Node collisionNode = collision.Collider as Node;

            // Case 1: The ball hit the gutter.
            if (collisionNode.IsInGroup(GUTTER_NODE_GROUP))
            {
                HandleGutterCollision();
            }
            else if (collisionNode.IsInGroup(PADDLE_NODE_GROUP))
            {
                HandlePaddleCollision(collision);
            }
            // Case 3: The ball hit either a wall or a brick
            else
            {
                // Play a ball hit sound effect
                Direction = Direction.Bounce(collision.Normal);
                _hasHitPaddle = false;
            }
        }

        private void HandleGutterCollision()
        {
            // Play a gutter hit sound effect
            ResetBall();
            _hasHitPaddle = false;
            EmitSignal("GutterHit");
            GD.Print("Gutter hit!");
        }

        private void HandlePaddleCollision(KinematicCollision2D collision)
        {
            if (!_hasHitPaddle)
            {
                _hasHitPaddle = true;
                // play a ball hit sound effect

                // Change the angle of the ball depending on how far from the center of the paddle the ball hit, up to a max angle
                // of 150 degrees
                PaddleBase collidingPaddle = collision.Collider as PaddleBase;
                // If the ball is below the paddle's Y when we hit the paddle, consider that hitting the gutter to avoid odd behavior
                if (collidingPaddle.Position.y < Position.y)
                {
                    HandleGutterCollision();
                }
                else
                {
                    float bounceAngle = GetBouceAngle(collidingPaddle);

                    // Set the direction vector with trig
                    float newX = (float)Math.Cos(bounceAngle);
                    float newY = (float)-Math.Sin(bounceAngle);

                    Direction = new Vector2(newX, newY);
                    Direction = Direction.Normalized();
                }

            }
        }

        private float GetBouceAngle(PaddleBase paddle)
        {
            float relativeIntersectX = paddle.Position.x - Position.x;
            //This should give us a value between -1.0 and 1.0
            float relativeIntersectXNormalized = (relativeIntersectX / (paddle.GetPaddleSize() / 2));
            float bounceAngle = RIGHT_ANGLE + MAX_DELTA_ANGLE * relativeIntersectXNormalized;
            // Clamp to ensure that our angle is between the values we set it to
            bounceAngle = Mathf.Clamp(bounceAngle, MIN_BOUNCE_ANGLE, MAX_BOUNCE_ANGLE);

            return bounceAngle;
        }
    }
}