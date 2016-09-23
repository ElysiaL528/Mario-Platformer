using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    public class Sprite
    {
        protected Texture2D _texture;
        protected Vector2 _location;

        public Vector2 Position
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public float X
        {
            get
            {
                return _location.X;
            }
            set
            {
                _location.X = value;
            }
        }
        public float Y
        {
            get
            {
                return _location.Y;
            }
            set
            {
                _location.Y = value;
            }
        }

        protected Rectangle _sourceRectangle;
        protected Color _color;
        protected float _rotation;
        protected Vector2 _origin;
        public Vector2 Origin
        {
            get
            {
                return _origin;
            }
            set
            {
                _origin = value;
            }
        }
        protected Vector2 _scale;
        public Vector2 Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
            }
        }
        protected SpriteEffects _effects;
        public SpriteEffects Effects
        {
            get
            {
                return _effects;
            }
            set
            {
                _effects = value;
            }
        }
        protected float _layerDepth;

        public Rectangle HitBox
        {
            get
            {
                int positionx = (int)(_location.X - _origin.X*_scale.X);
                int positiony = (int)(_location.Y - _origin.Y*_scale.Y);
                int width = (int)(_sourceRectangle.Width *_scale.X);
                int height = (int)(_sourceRectangle.Height * _scale.Y);
                return new Rectangle(positionx, positiony, width, height);
            }
        }
        public Rectangle InvertHitBox
        {
            get
            {
                int positionx = (int)(_location.X - _origin.X * _scale.X);
                int positiony = (int)(_location.Y - _origin.Y * _scale.Y);
                int width = (int)(_sourceRectangle.Width * _scale.X);
                int height = (int)(_sourceRectangle.Height * _scale.Y);
                if (width < 0)
                {
                    positionx += width;
                    width *= -1;
                }
                if (height < 0)
                {
                    positiony += height;
                    height *= -1;
                }
                return new Rectangle(positionx, positiony, width, height);
            }
        }

        public Vector2 Size
        {
            get
            {
                return _scale * new Vector2(_sourceRectangle.Width, _sourceRectangle.Height);
            }
            set
            { 
                _scale.X = value.X / _sourceRectangle.Width;
                _scale.Y = value.Y / _sourceRectangle.Height;
            }
        }
        
        public Sprite(Texture2D img, Vector2 pos, Color color)
            :this(img, pos, new Rectangle(0, 0, img.Width, img.Height), color)
        {}
        
        public Sprite(Texture2D img, Vector2 pos, Rectangle sourceRectangle, Color color)
        {
            _texture = img;
            _location = pos;
            _color = color;
            _sourceRectangle = sourceRectangle;
            _rotation = 0;
            _origin = Vector2.Zero;
            _scale = Vector2.One;
            _effects = SpriteEffects.None;
            _layerDepth = 0f;
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, _location, _sourceRectangle, _color, _rotation, _origin, _scale, _effects, _layerDepth);
            //spritebatch.Draw(_texture, HitBox, Color.Red);
        }
    }
}
