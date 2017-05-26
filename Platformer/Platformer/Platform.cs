using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    public class Platform : Sprite
    {
        /// <summary>
        /// True if platform kills you. False if it's a safe platform.
        /// </summary>
        public bool IsDeadly { get; set; }
        bool isGrounded;
        bool canWalkLeft;
        bool canWalkRight;
        bool canGoUp;

        public Platform(Texture2D texture, Vector2 position)
            :this(texture, position, false)
        {
            //Pass-through constructor
        }

        public Platform(Texture2D texture, Vector2 position, bool isDeadly)
            : base(texture, position)
        {
            IsDeadly = isDeadly;
        }

        public void CheckCollision()
        {
           

            if(PlatformerGame.MainCharacter.groundHitBox.Intersects(HitBox))
            {
                PlatformerGame.MainCharacter.isGrounded = true;
                while (PlatformerGame.MainCharacter.feetHitBox.Intersects(HitBox))
                {
                    PlatformerGame.MainCharacter.Y -= 0.1f;
                    PlatformerGame.MainCharacter.feetHitBox = new Rectangle(PlatformerGame.MainCharacter.HitBox.X, PlatformerGame.MainCharacter.HitBox.Y + PlatformerGame.MainCharacter.HitBox.Height - 1, PlatformerGame.MainCharacter.HitBox.Width, 1);
                }
            }

          else if(PlatformerGame.MainCharacter.leftHitBox.Intersects(HitBox))
            {
                PlatformerGame.MainCharacter.canWalkLeft = false;
                while (PlatformerGame.MainCharacter.leftHitBox.Intersects(HitBox))
                {
                    PlatformerGame.MainCharacter.X += 0.1f;
                    PlatformerGame.MainCharacter.leftHitBox = new Rectangle(PlatformerGame.MainCharacter.HitBox.X - 1, PlatformerGame.MainCharacter.HitBox.Y + 5, 1, PlatformerGame.MainCharacter.HitBox.Height - 10);
                }
            }

            else if(PlatformerGame.MainCharacter.rightHitBox.Intersects(HitBox))
            {
                PlatformerGame.MainCharacter.canWalkRight = false;
                while (PlatformerGame.MainCharacter.rightHitBox.Intersects(HitBox))
                {
                    PlatformerGame.MainCharacter.X -= 0.1f;
                    PlatformerGame.MainCharacter.rightHitBox = new Rectangle(PlatformerGame.MainCharacter.HitBox.X + PlatformerGame.MainCharacter.HitBox.Width, PlatformerGame.MainCharacter.HitBox.Y + 5, 1, PlatformerGame.MainCharacter.HitBox.Height - 10);
                }
            }

            else if(PlatformerGame.MainCharacter.topHitBox.Intersects(HitBox))
            {
                PlatformerGame.MainCharacter.canGoUp = false;
                while(PlatformerGame.MainCharacter.topHitBox.Intersects(HitBox))
                {
                    PlatformerGame.MainCharacter.Y += 0.1f;
                    PlatformerGame.MainCharacter.topHitBox = new Rectangle(PlatformerGame.MainCharacter.HitBox.X , PlatformerGame.MainCharacter.HitBox.Y + 5, PlatformerGame.MainCharacter.HitBox.Width - 10, 1);
                }
            }
        }
    }
}