﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    public class MovingPlatform : Platform
    {
        public PlatformMovement Movement;
        public int Speed;
        public int MaxCoord;
        public int MinCoord;

        public bool isTriggered = false;

        public MovingPlatform(Texture2D texture, Vector2 position, PlatformMovement movement, int speed, int MaxCoordinate, int MinCoordinate)
            :base(texture, position)
        {
            movement = Movement;
            Speed = speed;
            MaxCoord = MaxCoordinate;
            MinCoord = MinCoordinate;
        }

        public void Update()
        {
            if(PlatformerGame.MainCharacter.groundHitBox.Intersects(HitBox))
            {
                isTriggered = true;
            }
            else
            {
                isTriggered = false;
            }
            
            if (isTriggered)
            {
                if (Movement == PlatformMovement.Horizontal)
                {
                    if (X >= MaxCoord || X <= MinCoord)
                    {
                        Speed *= -1;
                        X += Speed;

                        if (_effects == SpriteEffects.FlipHorizontally)
                        {
                            _effects = SpriteEffects.None;
                        }
                        else
                        {
                            _effects = SpriteEffects.FlipHorizontally;
                        }
                    }
                    else
                    {
                        X += Speed;
                    }
                }
                else if (Movement == PlatformMovement.Vertical)
                {
                    Y += Speed;
                    if (Y >= MaxCoord || Y <= MinCoord)
                    {
                        Speed *= -1;
                        Y += Speed;
                    }
                }

                if (PlatformerGame.MainCharacter.groundHitBox.Intersects(HitBox))
                {
                    _color = Color.Red;
                    if (Movement == PlatformMovement.Horizontal)
                    {
                        PlatformerGame.MainCharacter.X += Speed;
                    }
                    else if (Movement == PlatformMovement.Vertical)
                    {
                        PlatformerGame.MainCharacter.Y += Speed;
                    }
                }
                else
                {
                    _color = Color.White;
                }
            }
        }
        public enum PlatformMovement
        {
            Horizontal,
            Vertical,
        }
    }
}
