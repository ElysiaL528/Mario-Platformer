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

            PlatformerGame.MainCharacter.feetHitBox = new Rectangle(HitBox.X + 5, HitBox.Y + HitBox.Height - 1, HitBox.Width - 10, 1);
            PlatformerGame.MainCharacter.groundHitBox = new Rectangle(HitBox.X + 5, HitBox.Y + HitBox.Height, HitBox.Width - 10, 1);
            PlatformerGame.MainCharacter.topHitBox = new Rectangle(HitBox.X + 5, HitBox.Y, HitBox.Width - 10, 1);
            PlatformerGame.MainCharacter.leftHitBox = new Rectangle(HitBox.X - 1, HitBox.Y + 5, 1, HitBox.Height - 10);
            PlatformerGame.MainCharacter.rightHitBox = new Rectangle(HitBox.X + HitBox.Width, HitBox.Y + 5, 1, HitBox.Height - 10);

            isGrounded = false;
            canWalkLeft = true;
            canWalkRight = true;
            canGoUp = true;
        }
    }
}