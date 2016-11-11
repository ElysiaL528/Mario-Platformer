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
            //Pass-throgh constructor
        }

        public Platform(Texture2D texture, Vector2 position, bool isDeadly)
            : base(texture, position)
        {
            IsDeadly = isDeadly;
        }
    }
}