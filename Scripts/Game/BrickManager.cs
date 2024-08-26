using Game.Brick;
using Godot;
using Godot.Collections;
using Util.ExtensionMethods;

namespace Game
{

    public class BrickManager : Node, IGameManager
    {
        // Member variables
        [Export]
        private Vector2 _startPos;
        [Export]
        private float _deltaX;
        [Export]
        private float _deltaY;

        private int _brickCount = 0;

        private PackedScene _brickScene = GD.Load<PackedScene>("res://Scenes/Brick.tscn");

        [Signal]
        delegate void BricksCleared();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
        }

        public bool StartGame()
        {
            BuildBricks();
            return true;
        }


        public bool EndGame()
        {
            Array bricks = GetChildren();
            foreach (var brick in bricks)
            {
                Node brickNode = brick as Node;
                brickNode.SafeQueueFree();
            }
            return true;
        }

        private void BuildBricks()
        {
            // Red bricks: 4 points
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Vector2 brickPos = new Vector2(_startPos.x + _deltaX * j, _startPos.y + _deltaY * i);
                    BrickBase redBrick = _brickScene.Instance<BreakoutBrick>();
                    redBrick.GlobalPosition = brickPos;
                    redBrick.SetColor("red");
                    redBrick.PointValue = 4;
                    AddChild(redBrick);
                    redBrick.Connect("AwardPoints", this, "OnAwardPoints");
                    _brickCount++;
                }
            }

            // Orange bricks: 3 points
            for (int i = 2; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Vector2 brickPos = new Vector2(_startPos.x + _deltaX * j, _startPos.y + _deltaY * i);
                    BrickBase orangeBrick = _brickScene.Instance<BreakoutBrick>();
                    orangeBrick.GlobalPosition = brickPos;
                    orangeBrick.SetColor("orange");
                    orangeBrick.PointValue = 3;
                    AddChild(orangeBrick);
                    orangeBrick.Connect("AwardPoints", this, "OnAwardPoints");
                    _brickCount++;
                }
            }

            // Green bricks: 2 points
            for (int i = 4; i < 6; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Vector2 brickPos = new Vector2(_startPos.x + _deltaX * j, _startPos.y + _deltaY * i);
                    BrickBase greenBrick = _brickScene.Instance<BreakoutBrick>();
                    greenBrick.GlobalPosition = brickPos;
                    greenBrick.SetColor("green");
                    greenBrick.PointValue = 2;
                    AddChild(greenBrick);
                    greenBrick.Connect("AwardPoints", this, "OnAwardPoints");
                    _brickCount++;
                }
            }

            // Yellow bricks: 1 point
            for (int i = 6; i < 8; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Vector2 brickPos = new Vector2(_startPos.x + _deltaX * j, _startPos.y + _deltaY * i);
                    BrickBase yellowBrick = _brickScene.Instance<BreakoutBrick>();
                    yellowBrick.GlobalPosition = brickPos;
                    yellowBrick.SetColor("yellow");
                    yellowBrick.PointValue = 1;
                    AddChild(yellowBrick);
                    yellowBrick.Connect("AwardPoints", this, "OnAwardPoints");
                    _brickCount++;
                }
            }
        }

        public void OnAwardPoints(int pointValue)
        {
            GD.Print(pointValue);
            _brickCount--;
            if (_brickCount <= 0)
            {
                EmitSignal(nameof(BricksCleared));
            }
        }

        //  // Called every frame. 'delta' is the elapsed time since the previous frame.
        //  public override void _Process(float delta)
        //  {
        //      
        //  }

    }
}