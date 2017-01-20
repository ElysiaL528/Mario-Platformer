
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

        Texture2D _fireballImage;
        TimeSpan TimeSinceLastShot = TimeSpan.Zero;
        TimeSpan ShotDelay = TimeSpan.FromMilliseconds(200);

        public bool IsEnemy { get; set; }
        public List<Fireball> fireballs { get; }
        public bool canShoot = true;
        int _currentframe;
        public AnimatedSprite(Texture2D img, Vector2 pos, Color color, List<Frame> animation, bool isEnemy)
            : base(img, pos, animation[0].SourceRectangle, color)
        {
            _animation = animation;
            _currentframe = 0;
            Origin = _animation[_currentframe].Origin;
            _animationtime = TimeSpan.FromMilliseconds(200);
            elapsedAnimateTime = new TimeSpan();
        }

        public virtual void UpdateAnimation(GameTime gameTime)
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

            if (IsEnemy)
            {
                if (TimeSinceLastShot >= ShotDelay)
                {
                    canShoot = true;
                }
                else
                {
                    canShoot = false;
                }

                if (canShoot)
                {
                    Fireball newFireball = new Fireball(_fireballImage, new Vector2(_location.X, _location.Y - HitBox.Height), Color.White, new Vector2(10, 0));
                    newFireball.Scale = Scale;
                    if (_effects == SpriteEffects.None)
                    {
                        fireballs.Add(newFireball);
                    }
                    else
                    {
                        //fireballs.Add(new Fireball(_fireballImage, new Vector2(_location.X, _location.Y - HitBox.Height), Color.White, new Vector2(-10, 0)));
                        fireballs.Add(newFireball);
                        fireballs[fireballs.Count - 1].Effects = SpriteEffects.FlipHorizontally;
                        fireballs[fireballs.Count - 1].Speed = new Vector2(-10, 0);

                    }
                    canShoot = false;
                    TimeSinceLastShot = TimeSpan.Zero;
                }
            }
        }

        }

    }

