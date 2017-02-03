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
        TimeSpan ShotDelay = TimeSpan.FromMilliseconds(500);

        EnemyMovement MovementType;
        AnimationType currentAnimation;
        public List<Fireball> fireballs { get; }
        Dictionary<AnimationType, List<Frame>> _animations;

        int MaxCoord;
        int MinCoord;
        public int Xspeed;
        public int Yspeed;


        public Enemy(Texture2D texture, Vector2 position, Dictionary<AnimationType, List<Frame>> animations, Texture2D fireballImage, EnemyMovement movement, int MaxCoordinate, int MinCoordinate)
            : base(texture, position, Color.White, animations[AnimationType.Idle], false)
        {
            _fireballImage = fireballImage;
            fireballs = new List<Fireball>();
            _animations = animations;
            currentAnimation = AnimationType.Walking;
            MaxCoord = MaxCoordinate;
            MinCoord = MinCoordinate;
        }

        public void EnemyUpdate(GameTime gameTime)
        {
            TimeSinceLastShot += gameTime.ElapsedGameTime;
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
                    newFireball.Speed = new Vector2(10, 0);
                    fireballs.Add(newFireball);
                    //fireballs[fireballs.Count - 1].Speed = new Vector2(10, 0);
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

            if (MovementType == EnemyMovement.SidetoSide)
            {
                if (X >= MaxCoord || X <= MinCoord)
                {
                    Xspeed *= -1;
                    X += Xspeed;

                    if (_effects == SpriteEffects.FlipHorizontally)
                    {
                        _effects = SpriteEffects.None;
                    }
                    else
                    {
                        _effects = SpriteEffects.FlipHorizontally;
                    }
                }
                else
                {
                    X += Xspeed;
                }
            }

            if (MovementType == EnemyMovement.UpDown)
            {
                Y += Yspeed;
                if (Y >= MaxCoord || Y <= MinCoord)
                {
                    Yspeed *= -1;
                    Y += Yspeed;
                }

            }

            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update();
                if (fireballs[i].X > Global.Screen.X)
                {
                    fireballs.RemoveAt(i);
                }
            }
            _animation = _animations[currentAnimation];
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Draw(spritebatch);
            }
        }

        public enum EnemyMovement
        {
            SidetoSide,
            UpDown
        }
    }
    #endregion
}
