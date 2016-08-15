using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Platformer
{
    public class Frame
    {
        public Rectangle SourceRectangle;
        public Vector2 Origin;

        public Frame(Rectangle sourceRectangle, Vector2 origin)
        {
            SourceRectangle = sourceRectangle;
            Origin = origin;
        }
    }
}
