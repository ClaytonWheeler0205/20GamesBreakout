using Godot;
using System;

namespace Game.SFX
{

    public class BallSoundPlayer : AudioPlayer
    {
        // Audio Streams
        private AudioStream _ballHitSFX = GD.Load<AudioStream>("res://Audio/Pong_Ball_Hit.wav"); 
        private AudioStream _ballGutterSFX = GD.Load<AudioStream>("res://Audio/Pong_Goal_Hit.wav");

        public override void PlaySound(string audio)
        {
            switch (audio)
            {
                case "ball_hit":
                    SoundToPlay = _ballHitSFX;
                    break;
                case "gutter_hit":
                    SoundToPlay = _ballGutterSFX;
                    break;
            }
            base.PlaySound(audio);
        }
    }
}