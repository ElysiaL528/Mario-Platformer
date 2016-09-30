using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class Character : AnimatedSprite
    {
        CharacterName currentCharacterName;
        public CharacterName getCharacterName
        {
            get
            {
                return currentCharacterName;
            }
        }

        Rectangle feetHitBox;
        public Rectangle FeetHitBox
        {
            get
            {
                return feetHitBox;
            }
        }        
        
        Rectangle groundHitBox;
        public Rectangle GroundHitBox
        {
            get
            {
                return groundHitBox;
            }
        }        
        
        Rectangle rightHitBox;
        public Rectangle RightHitBox
        {
            get
            {
                return rightHitBox;
            }
        }        
        
        Rectangle leftHitBox;
        public Rectangle LeftHitBox
        {
            get
            {
                return leftHitBox;
            }
        }        
        
        Rectangle topHitBox;
        public Rectangle TopHitBox
        {
            get
            {
                return topHitBox;
            }
        }
        public float gravity = 5.18f;
        public float jumpPower = 5;
        public bool isLevel13 = false;
        public bool touchedLava = false;
        Dictionary<AnimationType, List<Frame>> _animations;
        AnimationType currentAnimation;

        Texture2D pixel;
        Texture2D _fireballImage;

        TimeSpan jumpTime = TimeSpan.FromMilliseconds(500);
        public TimeSpan elapsedJumpTime = new TimeSpan();

        public TimeSpan elapsedFallTime = new TimeSpan();

        List<Fireball> fireballs;

        public List<Fireball> Fireballs
        {
            get
            {
                return fireballs;
            }
        }
        public Texture2D Image
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }

        public Dictionary<AnimationType, List<Frame>> Animations
        {
            get
            {
               return _animations;
            }
            set
            {
                _animations = value;
            }
        }
        

        public Character(Texture2D texture, Vector2 position, Dictionary<AnimationType, List<Frame>> animations, Texture2D fireballImage)
            :base(texture, position, Color.White, animations[AnimationType.Idle])
        {
            fireballs = new List<Fireball>();
            pixel = new Texture2D(texture.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            _animations = animations;
            currentAnimation = AnimationType.Idle;
            feetHitBox = new Rectangle(HitBox.X + 5, HitBox.Y + HitBox.Height - 1, HitBox.Width - 10, 1);
            groundHitBox = new Rectangle(HitBox.X + 5, HitBox.Y + HitBox.Height, HitBox.Width - 10, 1);
            topHitBox = new Rectangle(HitBox.X + 5, HitBox.Y, HitBox.Width - 10, 1);
            leftHitBox = new Rectangle(HitBox.X - 1, HitBox.Y + 5, 1, HitBox.Height - 10);
            rightHitBox = new Rectangle(HitBox.X + HitBox.Width, HitBox.Y + 5, 1, HitBox.Height - 10);
            _fireballImage = fireballImage;
        }

        bool isGrounded = false;
        bool canWalkLeft = true;
        bool canWalkRight = true;
        bool canGoUp = true;

        public void CheckCollision(List<Sprite> randomStuff)
        {
            feetHitBox = new Rectangle(HitBox.X + 5, HitBox.Y + HitBox.Height - 1, HitBox.Width - 10, 1);
            groundHitBox = new Rectangle(HitBox.X + 5, HitBox.Y + HitBox.Height, HitBox.Width - 10, 1);
            topHitBox = new Rectangle(HitBox.X + 5, HitBox.Y, HitBox.Width - 10, 1);
            leftHitBox = new Rectangle(HitBox.X - 1, HitBox.Y + 5, 1, HitBox.Height - 10);
            rightHitBox = new Rectangle(HitBox.X + HitBox.Width, HitBox.Y + 5, 1, HitBox.Height - 10);

            isGrounded = false;
            canWalkLeft = true;
            canWalkRight = true;
            canGoUp = true;
            for (int i = 0; i < randomStuff.Count; i++)
            {
                if(groundHitBox.Intersects(randomStuff[i].HitBox))
                {
                    isGrounded = true;
                    while (feetHitBox.Intersects(randomStuff[i].HitBox))
                    {
                        Y--;
                        feetHitBox = new Rectangle(HitBox.X, HitBox.Y + HitBox.Height - 1, HitBox.Width, 1);
                    }
                }

                if (leftHitBox.Intersects(randomStuff[i].HitBox))
                {
                    canWalkLeft = false;
                    while (leftHitBox.Intersects(randomStuff[i].HitBox))
                    {
                        X++;
                        leftHitBox = new Rectangle(HitBox.X - 1, HitBox.Y + 5, 1, HitBox.Height - 10);
                    }
                }

                if (rightHitBox.Intersects(randomStuff[i].HitBox))
                {
                    canWalkRight = false;
                    while (rightHitBox.Intersects(randomStuff[i].HitBox))
                    {
                        X--;
                        rightHitBox = new Rectangle(HitBox.X + HitBox.Width, HitBox.Y + 5, 1, HitBox.Height - 10);
                    }
                }

                if (topHitBox.Intersects(randomStuff[i].HitBox))
                {
                    canGoUp = false;
                    while (topHitBox.Intersects(randomStuff[i].HitBox))
                    {
                        Y++;
                        topHitBox = new Rectangle(HitBox.X + 5, HitBox.Y, HitBox.Width - 10, 1);
                    }
                }
            }
        }
        public void CheckLavaCollision(List<Sprite> randomLavaStuff)
        {
            feetHitBox = new Rectangle(HitBox.X + 5, HitBox.Y + HitBox.Height - 1, HitBox.Width - 10, 1);
            groundHitBox = new Rectangle(HitBox.X + 5, HitBox.Y + HitBox.Height, HitBox.Width - 10, 1);
            topHitBox = new Rectangle(HitBox.X + 5, HitBox.Y, HitBox.Width - 10, 1);
            leftHitBox = new Rectangle(HitBox.X - 1, HitBox.Y + 5, 1, HitBox.Height - 10);
            rightHitBox = new Rectangle(HitBox.X + HitBox.Width, HitBox.Y + 5, 1, HitBox.Height - 10);

            isGrounded = false;
            canWalkLeft = true;
            canWalkRight = true;
            canGoUp = true;
            for (int i = 0; i < randomLavaStuff.Count; i++)
            {
                if (groundHitBox.Intersects(randomLavaStuff[i].HitBox))
                {
                    isGrounded = true;
                    while (feetHitBox.Intersects(randomLavaStuff[i].HitBox))
                    {
                        Y--;
                        feetHitBox = new Rectangle(HitBox.X, HitBox.Y + HitBox.Height - 1, HitBox.Width, 1);
                    }
                }

                if (leftHitBox.Intersects(randomLavaStuff[i].HitBox))
                {
                    canWalkLeft = false;
                    while (leftHitBox.Intersects(randomLavaStuff[i].HitBox))
                    {
                        X++;
                        leftHitBox = new Rectangle(HitBox.X - 1, HitBox.Y + 5, 1, HitBox.Height - 10);
                    }
                }

                if (rightHitBox.Intersects(randomLavaStuff[i].HitBox))
                {
                    canWalkRight = false;
                    while (rightHitBox.Intersects(randomLavaStuff[i].HitBox))
                    {
                        X--;
                        rightHitBox = new Rectangle(HitBox.X + HitBox.Width, HitBox.Y + 5, 1, HitBox.Height - 10);
                    }
                }

                if (topHitBox.Intersects(randomLavaStuff[i].HitBox))
                {
                    canGoUp = false;
                    while (topHitBox.Intersects(randomLavaStuff[i].HitBox))
                    {
                        Y++;
                        topHitBox = new Rectangle(HitBox.X + 5, HitBox.Y, HitBox.Width - 10, 1);
                    }
                }
                if(HitBox.Intersects(randomLavaStuff[i].HitBox))
                {
                    touchedLava = true;
                }
                else
                {
                    touchedLava = false;
                }
            }
        } 


        public bool canShoot = true;
        public TimeSpan ShotDelay = TimeSpan.FromMilliseconds(200);
        public TimeSpan TimeSinceLastShot = TimeSpan.Zero;
        

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            bool isRunning = false;
            _animationtime = TimeSpan.FromMilliseconds(200);
            TimeSinceLastShot += gameTime.ElapsedGameTime;

            currentAnimation = AnimationType.Idle;

            if (TimeSinceLastShot >= ShotDelay)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }

            if (keyboard.IsKeyDown(Keys.Space) && canShoot == true)
            {
                Fireball newFireball = new Fireball(_fireballImage, new Vector2(_location.X, _location.Y - HitBox.Height), Color.White, new Vector2(10, 0));
                newFireball.Scale = _scale;
                if (_effects == SpriteEffects.None)
                {
                    fireballs.Add(newFireball);
                }
                else
                {
                    //fireballs.Add(new Fireball(_fireballImage, new Vector2(_location.X, _location.Y - HitBox.Height), Color.White, new Vector2(-10, 0)));
                    fireballs.Add(newFireball);
                    fireballs[fireballs.Count - 1].Effects = SpriteEffects.FlipHorizontally;
                    fireballs[fireballs.Count - 1].Speed = new Vector2(-10, 0);
                    
                }
                canShoot = false;
                TimeSinceLastShot = TimeSpan.Zero;
            }

            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update();
                if(fireballs[i].X > Global.Screen.X)
                {
                    fireballs.RemoveAt(i);
                }
            }

            

            if (keyboard.IsKeyDown(Keys.LeftShift) && isLevel13 == false|| keyboard.IsKeyDown(Keys.RightShift) && isLevel13 == false)
            {
                    isRunning = true;
            }
            if (keyboard.IsKeyDown(Keys.Right) && canWalkRight == true)
            {
                if (isRunning)
                {
                    X += 5;
                    _animationtime = TimeSpan.FromMilliseconds(200 / 3);
                }
                currentAnimation = AnimationType.Walking;
                X += 1;
                Effects = SpriteEffects.None;
            }
            if (keyboard.IsKeyDown(Keys.Left) && canWalkLeft == true)
            {
                if (isRunning)
                {
                    X -= 5;
                    _animationtime = TimeSpan.FromMilliseconds(200 / 3);
                }
                currentAnimation = AnimationType.Walking;
                X -= 1;
                Effects = SpriteEffects.FlipHorizontally;
            }
            if (keyboard.IsKeyDown(Keys.Down) && isGrounded == true)
            {
                currentAnimation = AnimationType.Crouching;
            }
            if (isGrounded == false)
            {
                currentAnimation = AnimationType.Falling;
            }
            else
            {
                elapsedJumpTime = new TimeSpan();
                elapsedFallTime = new TimeSpan();
            }
            //if(keyboard.IsKeyDown(Keys.P))
            //{
            //    currentAnimation = AnimationType.Punching;
            //}
            if (keyboard.IsKeyDown(Keys.Up) && canGoUp == true)
            {
                elapsedJumpTime += gameTime.ElapsedGameTime;
                if (elapsedJumpTime < jumpTime)
                {
                    Y -= gravity * elapsedFallTime.Milliseconds / 1000 + jumpPower;
                    currentAnimation = AnimationType.Jumping;
                }
                else
                {
                    elapsedFallTime += gameTime.ElapsedGameTime;
                    Y += gravity * elapsedFallTime.Milliseconds / 1000;
                }
            }
            else if (isGrounded == false)
            {
                elapsedFallTime += gameTime.ElapsedGameTime;
                Y += gravity * elapsedFallTime.Milliseconds / 1000;
            }
            if (keyboard.IsKeyDown(Keys.B))
            {
                Scale += new Vector2(0.005f, .01f);
            }
            if (keyboard.IsKeyDown(Keys.V))
            {
                Scale -= new Vector2(0.005f, .01f);
            }


            
            _animation = _animations[currentAnimation];
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
            /*spritebatch.Draw(pixel, HitBox, Color.Lerp(Color.Yellow, Color.Transparent, .5f));
            spritebatch.Draw(pixel, FeetHitBox, Color.Lerp(Color.Green, Color.Transparent, .1f));
            spritebatch.Draw(pixel, GroundHitBox, Color.Lerp(Color.Brown, Color.Transparent, .1f));
            spritebatch.Draw(pixel, TopHitBox, Color.Lerp(Color.Red, Color.Transparent, .1f));
            spritebatch.Draw(pixel, LeftHitBox, Color.Lerp(Color.Red, Color.Transparent, .1f));
            spritebatch.Draw(pixel, RightHitBox, Color.Lerp(Color.Red, Color.Transparent, .1f));*/
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Draw(spritebatch);
            }
        }

        public enum CharacterName
        {
            Mario = 0,
            Spongebob = 1,
            Patrick = 2,
            Luigi = 3,
        }
    }
}
