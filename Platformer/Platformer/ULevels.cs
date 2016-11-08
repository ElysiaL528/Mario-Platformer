using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class ULevels : Level
    {
        public List<Sprite> LavaPlatform = new List<Sprite>();

        public ULevels(List<Sprite> platform, List<Sprite> lavaPlatforms, List<Item> items, Texture2D background, Sprite door)
              : base(platform, items, background, door)
        {
            LavaPlatform = lavaPlatforms;
        }

        public override void Update(Character character)
        {
            base.Update(character);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for (int i = 0; i < LavaPlatform.Count; i++)
            {
                LavaPlatform[i].Draw(spriteBatch);
            }
        }
    }
}
