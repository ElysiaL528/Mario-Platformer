using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Enemy : AnimatedSprite
    {
        #region  
        Texture2D _fireballImage;
        TimeSpan TimeSinceLastShot = TimeSpan.Zero;
        TimeSpan ShotDelay = TimeSpan.FromMilliseconds(200);
        public List<Fireball> fireballs { get; }
        Dictionary<AnimationType, List<Frame>> _animations;

        public Enemy(Texture2D texture, Vector2 position, Dictionary<AnimationType, List<Frame>> animations, Texture2D fireballImage)
            : base(texture, position, Color.White, animations[AnimationType.Idle], false)
        {
            _fireballImage = fireballImage;
            fireballs = new List<Fireball>();
            _animations = animations;

        }

        public override void Update(GameTime gameTime)
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
    #endregion
}
