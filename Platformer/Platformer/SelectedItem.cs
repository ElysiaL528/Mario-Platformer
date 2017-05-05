using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class SelectedItem : Button
    {
        public bool isSelected;
        public bool isLocked = true;
        public bool canAfford = false;
        public Texture2D PriceImage;
        Texture2D PriceTexture;
        public Vector2 PricePosition;
        public Texture2D texture;
        public Color tint;
        public int CollectedCoins;
        public int Price;

        MouseState lastms;
        public int coins;

        public bool beingPurchased;

        public SelectedItem(Texture2D img, Vector2 pos, Color color, Texture2D priceImage, int price, int TotalCoins)
            :base(img, pos, color)
        {
            texture = img;
            tint = color;
            PriceImage = priceImage;
            Price = price;
            coins = TotalCoins;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            PricePosition = new Vector2(X + texture.Width/4, texture.Height + Y);

            if (isLocked)
            {
                spriteBatch.Draw(_texture, _location, _sourceRectangle, Color.Black, _rotation, Origin, Scale, _effects, _layerDepth);
                spriteBatch.Draw(PriceImage, PricePosition, Color.White);
            }
            else
            {
                spriteBatch.Draw(_texture, _location, _sourceRectangle, Color.White, _rotation, Origin, Scale, _effects, _layerDepth);
            }
            
        }

        public void Update()
        {

            lastms = mouseState;
            mouseState = Mouse.GetState();

            beingPurchased = isLocked && IsClicked;

            if (HitBox.Contains(mouseState.X, mouseState.Y))
            {
                _color = Color.DarkGray;
                IsClicked = mouseState.LeftButton == ButtonState.Pressed && lastms.LeftButton == ButtonState.Released;
            }
            else
            {
                IsClicked = false;
                _color = Color.White;
            }
            if (isLocked)
            {
                tint = Color.DarkGray;
            }

            if(isSelected)
            {
                tint = Color.White;
            }
            else
            {
                tint = Color.DarkGray;
            }
            
            if (IsClicked && !isLocked)
            {
                isSelected = true;
            }
            
        }

        public int UnlockItem(int Coins)
        {
            coins = Coins;

            if (coins >= Price)
            {
                canAfford = true;
                isLocked = false;
                isSelected = true;
                
            }
            return coins - Price;
        }

    }
}
