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
        public List<Sprite> _lavaPlatform = new List<Sprite>();
        public Texture2D backgroundImage;

      public ULevels(List<Sprite> platform, List<Sprite> lavaPlatforms, Texture2D background, Sprite door)
            :base(platform, background, door)
        {
            _lavaPlatform = lavaPlatforms;
            backgroundImage = background;
            platforms = platform;
        }

     public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundImage, new Microsoft.Xna.Framework.Rectangle(0, 0, 1000, 489), Color.White);

            for (int i = 0; i < _lavaPlatform.Count; i++)
            {
                _lavaPlatform[i].Draw(spriteBatch);
            }
            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Draw(spriteBatch);
            }
            
            
            Door.Draw(spriteBatch);
        }
    }
}
