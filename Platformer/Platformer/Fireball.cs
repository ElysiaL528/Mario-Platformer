using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    public class Fireball : Sprite
    {
        Vector2 _speed;
        Rectangle Hitbox;
        public Vector2 Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }

        }
        public Fireball(Texture2D image, Vector2 pos, Rectangle sourceRectangle, Color tint, Vector2 speed) :
            base(image, pos, sourceRectangle, tint)
        {
            _speed = speed;
            Hitbox.X = (int)X;
            Hitbox.Y = (int)Y;
        }
        public Fireball(Texture2D image, Vector2 pos, Color tint, Vector2 speed) :
            base(image, pos, tint)
        {
            _speed = speed;
        }

        public void Update()
        {
            Hitbox.X = (int)X;
            Hitbox.Y = (int)Y;
            _location += _speed;

            
            
        }
        

    }
}
