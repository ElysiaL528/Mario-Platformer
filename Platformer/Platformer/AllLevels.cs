using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class AllLevels : Level
    {
        public AllLevels(List<Sprite> platform, List<Item> items, Texture2D background, Sprite door)
              : base(platform, items, background, door)
        {
            
        }
    }
}
