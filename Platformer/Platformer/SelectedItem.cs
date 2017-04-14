using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class SelectedItem : Button
    {
        public bool isSelected;
        public bool isLocked;
        public Texture2D PriceImage;
        public Vector2 PricePosition;
        public Texture2D texture;
        public Color tint;       

        public SelectedItem(Texture2D img, Vector2 pos, Color color, Texture2D priceImage)
            :base(img, pos, color)
        {
            texture = img;
            tint = color;
            priceImage = PriceImage;
            PricePosition = new Vector2(0, texture.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch) 
            :base(spriteBatch)
        {
            if (isLocked)
            {
                spriteBatch.Draw(PriceImage, PricePosition, Color.White);
            }
        }

        public void Update()
        {
            if(isSelected)
            {
                tint = Color.White;
            }
            else
            {
                tint = Color.DarkGray;
            }

            if(IsClicked && !isLocked)
            {
                isSelected = true;
            }

        }
    }
}
