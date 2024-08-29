using Godot;
using System;
using Util.ExtensionMethods;

namespace Game.UI
{

    public class BreakoutScore : Control, IGameScore
    {
        // Label node references
        private Label _scoreLabelReference;
        private Label _highScoreLabelReference;

        private int _score = 0;
        private int _highScore;

        private ConfigFile _config = new ConfigFile();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _scoreLabelReference = GetNode<Label>("%ScoreLabel");
            _highScoreLabelReference = GetNode<Label>("%HighScoreLabel");
            // Get the high score value
            if (!_scoreLabelReference.IsValid())
            {
                GD.PrintErr("ERROR: Score label reference is not valid!");
            }
            if (!_highScoreLabelReference.IsValid())
            {
                GD.PrintErr("ERROR: High score label reference is not valid!");
            }
            LoadHighScore();
        }

        private void LoadHighScore()
        {
            Error err = _config.Load("user://highscore.cfg");

            if (err != Error.Ok)
            {
                _highScore = 0;
                return;
            }

            _highScore = (int)_config.GetValue("PlayerScore", "high_score");
            if (_highScoreLabelReference.IsValid())
            {
                if (_highScore < 10)
                {
                    _highScoreLabelReference.Text = $"High: 00{_highScore}";
                }
                else if (_highScore < 100)
                {
                    _highScoreLabelReference.Text = $"High: 0{_highScore}";
                }
                else
                {
                    _highScoreLabelReference.Text = $"High: {_highScore}";
                }
            }
        }

        private void SaveHighScore()
        {
            _config.SetValue("PlayerScore", "high_score", _highScore);

            _config.Save("user://highscore.cfg");
        }

        public void ResetScore()
        {
            if (_score > _highScore)
            {
                UpdateHighScore();
            }
            _score = 0;
            if (_scoreLabelReference.IsValid())
            {
                _scoreLabelReference.Text = "Score: 000";
            }
        }

        private void UpdateScore()
        {
            if (_scoreLabelReference.IsValid())
            {
                if (_score < 10)
                {
                    _scoreLabelReference.Text = $"Score: 00{_score}";
                }
                else if (_score < 100)
                {
                    _scoreLabelReference.Text = $"Score: 0{_score}";
                }
                else
                {
                    _scoreLabelReference.Text = $"Score: {_score}";
                }
            }
        }

        private void UpdateHighScore()
        {
            _highScore = _score;
            // save the new high score
            SaveHighScore();
            // update the high score UI
            if (_highScoreLabelReference.IsValid())
            {
                if (_highScore < 10)
                {
                    _highScoreLabelReference.Text = $"High: 00{_highScore}";
                }
                else if (_highScore < 100)
                {
                    _highScoreLabelReference.Text = $"High: 0{_highScore}";
                }
                else
                {
                    _highScoreLabelReference.Text = $"High: {_highScore}";
                }
            }
        }

        public void OnPointsScored(int pointValue)
        {
            _score += pointValue;
            _score = Mathf.Clamp(_score, 0, 999);
            UpdateScore();
        }
    }
}