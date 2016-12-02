using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    public class Item : Sprite
    {
        public Item(Texture2D img, Vector2 pos, Color color, PowerupType powerupType)
            :base(img, pos, color)
        {

            /*if (Mario.HitBox.Intersects(bunny.HitBox) && currentLevel == level2)
            {
                hasJumpBoost = true;
                Mario.jumpPower = 2;
            }

            if (Mario.HitBox.Intersects(pizza.HitBox) && currentLevel == level5)
                {
                    if (IsTiny == false)
                    {
                        for (int i = 0; i < 70; i++)
                        {
                            Mario.Scale -= new Vector2(0.005f, .01f);
                        }
                        IsTiny = true;
                        currentLevel.Door.Scale = new Vector2(.25f);
                    }
                     if (Mario.HitBox.Intersects(portal1.HitBox) && currentLevel == level5 || Mario.HitBox.Intersects(portal1.HitBox) && currentLevel == level9)
                {
                    movingTime += gameTime.ElapsedGameTime;
                    if (movingTime >= timeToMove)
                    {
                        Mario.X = portal2.X;
                        Mario.Scale -= new Vector2(0.05f, .1f);
                        Mario.Y = portal2.Y;
                        Mario.Scale += new Vector2(0.05f, .1f);
                        movingTime = TimeSpan.Zero;
                    }
                }

                if (Mario.HitBox.Intersects(portal2.HitBox) && currentLevel == level5 || Mario.HitBox.Intersects(portal2.HitBox) && currentLevel == level9)
                {
                    movingTime += gameTime.ElapsedGameTime;
                    if (movingTime >= timeToMove)
                    {
                        Mario.X = portal1.X;
                        Mario.Scale -= new Vector2(0.05f, .1f);
                        Mario.Y = portal1.Y;
                        Mario.Scale += new Vector2(0.05f, .1f);
                        movingTime = TimeSpan.Zero;
                    }
                }
                if (Mario.HitBox.Intersects(invert.HitBox) && currentLevel == level7)
                {
                    Mario.Scale -= new Vector2(1.5f, 3);
                }
                if (Mario.InvertHitBox.Intersects(uninvert.HitBox) && currentLevel == level7)
                {
                    Mario.Scale = Vector2.One;
                    Mario.Y = 300;
                    Mario.X = uninvert.X;
                }


                }
                f (Mario.HitBox.Intersects(HealthPowerup.HitBox))
                {
                    if (MoreLives == false)
                    {
                        lives += 10;
                        MoreLives = true;
                    }
                }
                  if (Mario.HitBox.Intersects(flower.HitBox))
                {
                    Mario.canShoot = true;
                }

                */
        }
        public enum PowerupType
        {
            Health,
            Shrink,
            JumpBoost,
            Portal1,
            Portal2,
            Invert,
            ReInvert,
        }
}
}
