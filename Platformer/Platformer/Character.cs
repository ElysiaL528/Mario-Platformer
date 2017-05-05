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
        public CharacterName currentCharacterName;

        public Rectangle feetHitBox { get; set; }

        public Rectangle groundHitBox { get; set; }

        Rectangle rightHitBox { get; set; }

        Rectangle leftHitBox { get; set; }

        Rectangle topHitBox { get; set; }

        public float gravity = 5.18f;
        public float jumpPower = 5;
        public bool isLevel13 = false;
        public bool Died = false;
        public bool isHealthPowerup = false;
        public bool isShrinkPowerup = false;
        public bool isJumpBoostPowerup = false;
        public bool isPortal1Powerup = false;
        public bool isPortal2Powerup = false;
        public bool isInvertPowerup = false;
        public bool isReInvertPowerup = false;
        public bool isJumping = false;


        Dictionary<AnimationType, List<Frame>> _animations;
        AnimationType currentAnimation;

        Texture2D pixel;
        Texture2D _fireballImage;

        TimeSpan jumpTime = TimeSpan.FromMilliseconds(500);
        public TimeSpan elapsedJumpTime = new TimeSpan();

        public TimeSpan elapsedFallTime = new TimeSpan();

        public List<Fireball> fireballs { get; }

        /* public List<Fireball> Fireballs
         {
             get
             {
                 return fireballs;
             }
         }*/

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
            : base(texture, position, Color.White, animations[AnimationType.Idle])
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

        public void CheckCollision(List<Platform> platforms)
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

            for (int i = 0; i < platforms.Count; i++)
            {
                //Did we die?
                if (platforms[i].IsDeadly)
                {
                    Died = (platforms[i].HitBox.Intersects(new Rectangle(HitBox.X - 2, HitBox.Y + 1, HitBox.Width, HitBox.Height)) 
                                    || platforms[i].HitBox.Intersects(new Rectangle(HitBox.X + 2, HitBox.Y - 1, HitBox.Width, HitBox.Height)));

                    if (Died)
                    {
                        break;
                    }
                }
                
                if (groundHitBox.Intersects(platforms[i].HitBox))
                {
                    isGrounded = true;
                    while (feetHitBox.Intersects(platforms[i].HitBox))
                    {
                        Y--;
                        feetHitBox = new Rectangle(HitBox.X, HitBox.Y + HitBox.Height - 1, HitBox.Width, 1);
                    }
                }

                if (leftHitBox.Intersects(platforms[i].HitBox))
                {
                    canWalkLeft = false;
                    while (leftHitBox.Intersects(platforms[i].HitBox))
                    {
                        X++;
                        leftHitBox = new Rectangle(HitBox.X - 1, HitBox.Y + 5, 1, HitBox.Height - 10);
                    }
                }

                if (rightHitBox.Intersects(platforms[i].HitBox))
                {
                    canWalkRight = false;
                    while (rightHitBox.Intersects(platforms[i].HitBox))
                    {
                        X--;
                        rightHitBox = new Rectangle(HitBox.X + HitBox.Width, HitBox.Y + 5, 1, HitBox.Height - 10);
                    }
                }

                if (topHitBox.Intersects(platforms[i].HitBox))
                {
                    canGoUp = false;
                    while (topHitBox.Intersects(platforms[i].HitBox))
                    {
                        Y++;
                        topHitBox = new Rectangle(HitBox.X + 5, HitBox.Y, HitBox.Width - 10, 1);
                    }
                }
            }
        }


        public bool canShoot = true;
        public TimeSpan ShotDelay = TimeSpan.FromMilliseconds(200);
        public TimeSpan TimeSinceLastShot = TimeSpan.Zero;


        public override void UpdateAnimation(GameTime gameTime)
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
                if(currentCharacterName == CharacterName.Mario)
                {
                    currentAnimation = AnimationType.ThrowingFireball;
                    _animationtime = TimeSpan.FromMilliseconds(200 / 3);
                }
                Fireball newFireball = new Fireball(_fireballImage, new Vector2(_location.X, _location.Y - HitBox.Height), Color.White, new Vector2(10, 0));
                newFireball.Scale = Scale;
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
                if (fireballs[i].X > Global.Screen.X)
                {
                    fireballs.RemoveAt(i);
                }
            }


            if (keyboard.IsKeyDown(Keys.LeftShift) && isLevel13 == false || keyboard.IsKeyDown(Keys.RightShift))
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
            if (keyboard.IsKeyDown(Keys.Up) && canGoUp == true && !isJumping)
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
                Y += gravity * elapsedFallTime.Milliseconds / 500;
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
            base.UpdateAnimation(gameTime);
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

        public void CheckPowerup(Item item)
        {
            if (HitBox.Intersects(item.HitBox) && !item.isSelected)
            {
                switch (item.Type)
                {
                    case Item.PowerupType.JumpBoost:
                        jumpPower = 10;
                        isJumpBoostPowerup = true;
                        item.isSelected = true;
                        break;

                    case Item.PowerupType.Health:
                        isHealthPowerup = true;
                        item.isSelected = true;
                        break;

                    case Item.PowerupType.Invert:
                        Scale -= new Vector2(1.5f, 3);
                        isInvertPowerup = true;
                        break;

                    case Item.PowerupType.Portal1:
                        isPortal1Powerup = true;
                        break;

                    case Item.PowerupType.Portal2:
                        isPortal2Powerup = true;
                        break;

                    case Item.PowerupType.Shrink:
                        isShrinkPowerup = true;
                        for (int i = 0; i < 70; i++)
                        {
                            Scale -= new Vector2(0.005f, .01f);
                        }
                        item.isSelected = true;
                        break;
                }
            }
            else if (InvertHitBox.Intersects(item.HitBox))
            {
                if (item.Type == Item.PowerupType.ReInvert)
                {
                    Scale = Vector2.One;
                    Y = 300;
                    X = item.X;
                    isReInvertPowerup = true;
                }
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
