using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Button : Sprite
    {
        public bool isclicked;
        MouseState lastms;
        public MouseState mouseState;
         int levelValue = 0;
        int UlevelValue = 0;
        public int LevelValue
        {
            get
            {
                return levelValue;
            }
            set
            {
                levelValue = value;
            }
        }
        public int ULevelValue
        {
            get
            {
                return UlevelValue;
            }
            set
            {
                UlevelValue = value;
            }
        }
        public Button(Texture2D img, Vector2 pos, Color color)
            : base(img, pos, color)
        {

        }

        public void Update()
        {
            lastms = mouseState;
            mouseState = Mouse.GetState();

            if (HitBox.Contains(mouseState.X, mouseState.Y))
            {
                _color = Color.DarkGray;
                if (mouseState.LeftButton == ButtonState.Pressed && lastms.LeftButton == ButtonState.Released)
                {
                    isclicked = true;
                }
                else
                {
                    isclicked = false;
                }
            }
            else
            {
                isclicked = false;
                _color = Color.White;
            }

        }


    }
}
