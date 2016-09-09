using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    public class Level
    {
        public List<Sprite> platforms = new List<Sprite>();
        public Texture2D backgroundImage;
        public Sprite Door;

        public Level(List<Sprite> platform, Texture2D background, Sprite door)
        {
            platforms = platform;
            backgroundImage = background;
            Door = door;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundImage, new Microsoft.Xna.Framework.Rectangle(0, 0, 1000, 489), Color.White);
            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Draw(spriteBatch);
            }
            Door.Draw(spriteBatch);
        }
    }
}
