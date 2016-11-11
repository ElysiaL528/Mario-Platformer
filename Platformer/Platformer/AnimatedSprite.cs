
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    public class AnimatedSprite : Sprite
    {
        public enum AnimationType
        {
            Idle = 0,
            Walking,
            Jumping,
            Falling,
            Crouching,
            Punching,
            Turning
        }

        public List<Frame> _animation;
        protected TimeSpan _animationtime;
        private TimeSpan elapsedAnimateTime;
        int _currentframe;
        public AnimatedSprite(Texture2D img, Vector2 pos, Color color, List<Frame> animation)
            : base(img, pos, animation[0].SourceRectangle, color)
        {
            _animation = animation;
            _currentframe = 0;
            Origin = _animation[_currentframe].Origin;
            _animationtime = TimeSpan.FromMilliseconds(200);
            elapsedAnimateTime = new TimeSpan();
        }

        public virtual void Update(GameTime gameTime)
        {
            elapsedAnimateTime += gameTime.ElapsedGameTime;
            if (elapsedAnimateTime >= _animationtime)
            {
                elapsedAnimateTime = TimeSpan.FromMilliseconds(0);
                _currentframe++;
                if (_currentframe >= _animation.Count)
                {
                    _currentframe = 0;
                }
                _sourceRectangle = _animation[_currentframe].SourceRectangle;
                Origin = _animation[_currentframe].Origin;
            }
        }

    }
}
