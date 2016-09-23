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
        public Vector2 startPosition;
        public List<Sprite> platforms = new List<Sprite>();
        public List<Item> items = new List<Item>();
        public Texture2D backgroundImage;
        public Sprite Door;

        public Level(List<Sprite> platform, List<Item> items, Texture2D background, Sprite door)
        {
            platforms = platform;
            backgroundImage = background;
            Door = door;
            this.items = items;
        }

        public virtual void Update(Character character)
        {
          /*  if (Mario.HitBox.Intersects(Penguin.HitBox))
            {
                if (enemyisdead == false)
                {
                    if (currentLevel == level3 || currentLevel == level12)
                    {
                        lives -= 10;
                        lostlife = true;

                        if (lives == 0)
                        {

                        }
                    }
                }
            }*/
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height), Color.White);
            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Draw(spriteBatch);
            }
            Door.Draw(spriteBatch);
        }
    }
}
