
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
        public List<Frame> _animation;
        protected TimeSpan _animationtime;
        TimeSpan _ElapsedAnimateTime;
        int _currentframe;
        public AnimatedSprite(Texture2D img, Vector2 pos, Color color, List<Frame> animation)
            : base(img, pos, animation[0].SourceRectangle, color)
        {
            _animation = animation;
            _currentframe = 0;
            _origin = _animation[_currentframe].Origin;
            _animationtime = TimeSpan.FromMilliseconds(200);
            _ElapsedAnimateTime = new TimeSpan();
        }

        public virtual void Update(GameTime gameTime)
        {
            _ElapsedAnimateTime += gameTime.ElapsedGameTime;
            if (_ElapsedAnimateTime >= _animationtime)
            {
                _ElapsedAnimateTime = TimeSpan.FromMilliseconds(0);
                _currentframe++;
                if (_currentframe >= _animation.Count)
                {
                    _currentframe = 0;
                }
                _sourceRectangle = _animation[_currentframe].SourceRectangle;
                _origin = _animation[_currentframe].Origin;
            }
        }

    }
}
