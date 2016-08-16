using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Platformer
{ 
    /* To do:  "Map" isn't cooperating w/Level class
            - Lava platforms aren't showing up (Ulevel 3)
            - Moving Platforms
            - Create a buttons list
            - Be able to fall smoothly
            - Door animations??
            - Door scale = Mario scale
            - Teleporting door
            - The penguin should move
            - Rearrange the level menus
            - Make fireballs disappear when intersecting w/penguin hitbox
            - Mario should die after touching lava
            - Slow shift function in ULevels
            - Load & animate underwater level characters
            - Create more enemies
            - Create & animate coins
            - Be able to choose & unlock maps

            */

    public class Game1 : Microsoft.Xna.Framework.Game
    { 
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;
        Texture2D penguinspritesheet;
        Texture2D PatrickSpritesheet;
        Texture2D SpongebobSpritesheet;
        Texture2D currentLevelMap;
        //string levelmap = "Clouds";
        Vector2 speed;
        Vector2 position;
        Vector2 penguinPosition;
        Vector2 coinPosition;
        Texture2D pixel;
        bool hasfirepower = false;
        bool IsTiny = false;
        bool enemyisdead = false;
        bool gameisover = false;
        bool hasJumpBoost;
        bool MoreLives = false;
        bool isUnderwaterLevel = false;
        bool lostlife = false;
        bool canShootEnemy = false;
        Level level0;
        Level level1;
        Level level01;
        Button lvl1button;
        Button lvl2button;
        Button lvl3button;
        Button lvl4button;
        Button lvl5button;
        Button lvl6button;
        Button lvl7button;
        Button lvl8button;
        Button lvl9button;
        Button lvl10button;
        Button lvl11button;
        Button lvl12button;
        Button lvl13button;
        Button lvl01button;
        Button lvl02button;
        Button lvl03button;
        Button lvl04button;
        Button lvl05button;
        Button lvl06button;
        Button lvl07button;
        Button lvl08button;
        Button lvl09button;
        Button lvl010button;
        Button lvl011button;
        Button lvl012button;
        Button lvl013button;
        Button ShopButton;
        Button ExitButton;
        Button MarioButton;
        Button SpongebobButton;
        Button PatrickButton;
        Button PlayButton;
        Button LandLevelsButton;
        Button UnderwaterLevelsButton;
        Level level2;
        Level level3;
        Level level4;
        Level level5;
        Level level6;
        Level level7;
        Level level8;
        Level level9;
        Level level10;
        Level level11;
        Level level12;
        Level level13;
        Level level02;
        Level level03;
        Level level04;
        Level level05;
        Level level06;
        Level level07;
        Level level08;
        Level level09;
        Level level010;
        Level level011;
        Level level012;
        Level level013;
        Sprite bunny;
        Sprite pizza;

        Level currentLevel;
        ULevels currentULevel;

        AnimatedSprite Penguin;

        Character Mario;
        Sprite StartScreenBackground;

        Sprite portal1;
        Sprite portal2;
        Sprite invert;
        Sprite uninvert;
        Sprite flower;
        Button MenuButton;
        Button LevelSelectButton;
        int lives = 100;
        int screen = 8;
        int fireballhitcount = 0;
        string character = "Patrick";
        int maxFireballHits = 1;
        Button restartbutton;

        MouseState ms;

        SpriteFont font;
        SpriteFont font2;
        Sprite GameOverScreen;
        Sprite LevelMenuBg;
        Sprite HealthPowerup;

        Texture2D level1Background;
        Texture2D Map2;
        Texture2D Map3;
        Texture2D Map4;
        Texture2D UnderwaterMap;
        Texture2D Gameoverscreen;

        Dictionary<AnimationType, List<Frame>> marioAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> penguinAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> luigiAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> SpongebobAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> PatrickAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> CoinAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<LevelMaps, Texture2D> maps;

        TimeSpan movingTime;
        TimeSpan timeToMove = new TimeSpan(0,0,0,0,2000);
        TimeSpan ShotDelay = TimeSpan.FromMilliseconds(300);
        TimeSpan TimeSinceLastShot = TimeSpan.Zero;

        LevelMaps levelmaps;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 489;
            graphics.ApplyChanges();
            Global.Screen = new Vector2(1000, 489);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Content.Load<Texture2D>("Mario Sprite Sheet");
            penguinspritesheet = Content.Load<Texture2D>("penguin");
            PatrickSpritesheet = Content.Load<Texture2D>("PatrickSpriteSheet");
            SpongebobSpritesheet = Content.Load<Texture2D>("Spongebob_Spritesheet_2");


            


            font = Content.Load<SpriteFont>("SpriteFont1");
            font2 = Content.Load<SpriteFont>("Font2");

            #region mario Animations
            List<Frame> walking = new List<Frame>();
            walking.Add(new Frame(new Rectangle(304, 18, 32, 47), new Vector2(15, 31)));
            walking.Add(new Frame(new Rectangle(343, 16, 24, 47), new Vector2(12, 31)));
            walking.Add(new Frame(new Rectangle(371, 17, 28, 47), new Vector2(13, 31)));
            walking.Add(new Frame(new Rectangle(343, 16, 24, 47), new Vector2(12, 31)));
            marioAnimations.Add(AnimationType.Walking, walking);

            List<Frame> idle = new List<Frame>();
            idle.Add(new Frame(new Rectangle(6, 16, 30, 47), new Vector2(13, 35)));
            idle.Add(new Frame(new Rectangle(40, 18, 32, 45), new Vector2(18, 33)));
            marioAnimations.Add(AnimationType.Idle, idle);

            List<Frame> jumping = new List<Frame>();
            jumping.Add(new Frame(new Rectangle(128, 18, 35, 45), new Vector2(146 - 128, 50 - 18)));
            marioAnimations.Add(AnimationType.Jumping, jumping);

            List<Frame> falling = new List<Frame>();
            falling.Add(new Frame(new Rectangle(174, 19, 34, 44), new Vector2(11, 31)));
            marioAnimations.Add(AnimationType.Falling, falling);

            //List<Frame> punching = new List<Frame>();
            ////            punching.Add(new Frame(new Rectangle(6, 16, 30, 47), new Vector2(13, 35)));
            //punching.Add(new Frame(new Rectangle(40, 18, 32, 45), new Vector2(18, 33)));
            //punching.Add(new Frame(new Rectangle(76, 22, 46, 41), new Vector2(20, 30)));
            //marioAnimations.Add(AnimationType.Punching, punching);

            List<Frame> crouching = new List<Frame>();
            crouching.Add(new Frame(new Rectangle(6, 92, 35, 32), new Vector2(20, 25)));
            marioAnimations.Add(AnimationType.Crouching, crouching);
            #endregion
            #region penguin Animations

            List<Frame> strolling = new List<Frame>();
            strolling.Add(new Frame(new Rectangle(6, 272, 71, 64), new Vector2(2, 51)));
            strolling.Add(new Frame(new Rectangle(102, 267, 71, 71), new Vector2(103 - 102, 323 - 257)));
            strolling.Add(new Frame(new Rectangle(200, 268, 73, 70), new Vector2(201 - 200, 323 - 268)));
            strolling.Add(new Frame(new Rectangle(299, 271, 72, 67), new Vector2(300 - 299, 327 - 271)));
            penguinAnimations.Add(AnimationType.Walking, strolling);

            List<Frame> still = new List<Frame>();
            still.Add(new Frame(new Rectangle(1, 44, 14, 15), new Vector2(1, 56)));
            penguinAnimations.Add(AnimationType.Idle, still);

            #endregion
            #region Luigi Animations

            List<Frame> lwalking = new List<Frame>();
            lwalking.Add(new Frame(new Rectangle(135, 133, 15, 39), new Vector2(15 / 2, 39 / 2)));
            lwalking.Add(new Frame(new Rectangle(155, 134, 15, 38), new Vector2(15 / 2, 38 / 2)));
            lwalking.Add(new Frame(new Rectangle(175, 135, 16, 37), new Vector2(16 / 2, 37 / 2)));
            lwalking.Add(new Frame(new Rectangle(196, 134, 15, 38), new Vector2(15 / 2, 38 / 2)));
            lwalking.Add(new Frame(new Rectangle(216, 133, 15, 40), new Vector2(15 / 2, 40 / 2)));
            lwalking.Add(new Frame(new Rectangle(236, 134, 15, 37), new Vector2(15 / 2, 37 / 2)));
            lwalking.Add(new Frame(new Rectangle(256, 135, 15, 37), new Vector2(15 / 2, 37 / 2)));
            lwalking.Add(new Frame(new Rectangle(277, 134, 15, 38), new Vector2(15 / 2, 38 / 2)));

            luigiAnimations.Add(AnimationType.Walking, lwalking);

            List<Frame> LuigiIdle = new List<Frame>();
            LuigiIdle.Add(new Frame(new Rectangle(113, 107, 36, 40), new Vector2(36 / 2, 40 / 2))); 
            LuigiIdle.Add(new Frame(new Rectangle(128, 87, 30, 34), new Vector2(30/2, 34/2)));
            LuigiIdle.Add(new Frame(new Rectangle(163, 86, 30, 35), new Vector2(30 / 2, 35 / 2)));

            luigiAnimations.Add(AnimationType.Idle, LuigiIdle);

            #endregion 

            #region Spongebob animations
            List<Frame> SpongebobIdle = new List<Frame>();
            SpongebobIdle.Add(new Frame(new Rectangle(204, 1, 37, 43), new Vector2(37 / 2, 43 / 2)));

            SpongebobAnimations.Add(AnimationType.Idle, SpongebobIdle);

            List<Frame> SpongebobWalking = new List<Frame>();
            SpongebobWalking.Add(new Frame(new Rectangle(1, 95, 47, 48), new Vector2(47 / 2, 48 / 2)));
            SpongebobWalking.Add(new Frame(new Rectangle(52, 98, 47, 45), new Vector2(47 / 2, 45 / 2)));
            SpongebobWalking.Add(new Frame(new Rectangle(102, 100, 45, 43), new Vector2(45 / 2, 43 / 2)));
            SpongebobWalking.Add(new Frame(new Rectangle(150, 97, 38, 45), new Vector2(38 / 2, 45 / 2)));
            SpongebobWalking.Add(new Frame(new Rectangle(191, 96, 34, 47), new Vector2(34 / 2, 47 / 2)));
            SpongebobWalking.Add(new Frame(new Rectangle(228, 97, 34, 47), new Vector2(34 / 2, 47 / 2)));
            SpongebobWalking.Add(new Frame(new Rectangle(266, 100, 32, 43), new Vector2(32 / 2, 43 / 2)));
            SpongebobWalking.Add(new Frame(new Rectangle(302, 99, 35, 44), new Vector2(35 / 2, 44 / 2)));
            SpongebobWalking.Add(new Frame(new Rectangle(340, 97, 40, 46), new Vector2(40 / 2, 46 / 2)));
            
            SpongebobAnimations.Add(AnimationType.Walking, SpongebobWalking);

            List<Frame> SpongebobFalling = new List<Frame>();
            SpongebobFalling.Add(new Frame(new Rectangle(224, 201, 47, 44), new Vector2(47 / 2, 44 / 2))); 

            SpongebobAnimations.Add(AnimationType.Falling, SpongebobFalling);

            List<Frame> SpongebobJumping = new List<Frame>();
            SpongebobJumping.Add(new Frame(new Rectangle(0, 201, 42, 45), new Vector2(42 / 2, 45 / 2)));
            SpongebobJumping.Add(new Frame(new Rectangle(48, 197, 33, 49), new Vector2(33 / 2, 49 / 2)));
            SpongebobJumping.Add(new Frame(new Rectangle(85, 199, 37, 47), new Vector2(37 / 2, 47 / 2)));
            SpongebobJumping.Add(new Frame(new Rectangle(125, 199, 45, 46), new Vector2(45 / 2, 46 / 2)));
            SpongebobJumping.Add(new Frame(new Rectangle(174, 200, 44, 46), new Vector2(44 / 2, 46 / 2)));

            SpongebobAnimations.Add(AnimationType.Jumping, SpongebobJumping);

            List<Frame> SpongebobDucking = new List<Frame>();
            SpongebobDucking.Add(new Frame(new Rectangle(0, 669, 33, 39), new Vector2(33 / 2, 39 / 2)));
            SpongebobDucking.Add(new Frame(new Rectangle(36, 671, 37, 38), new Vector2(37 / 2, 38 / 2)));
            SpongebobDucking.Add(new Frame(new Rectangle(77, 670, 32, 38), new Vector2(32 / 2, 38 / 2)));
            SpongebobDucking.Add(new Frame(new Rectangle(114, 671, 32, 38), new Vector2(32 / 2, 38 / 2)));
            SpongebobDucking.Add(new Frame(new Rectangle(149, 671, 32, 38), new Vector2(32 / 2, 38 / 2)));

            SpongebobAnimations.Add(AnimationType.Crouching, SpongebobDucking);


            #endregion

            #region Patrick Animations
            List<Frame> PatrickIdle = new List<Frame>();
            PatrickIdle.Add(new Frame(new Rectangle(217, 2, 31, 50), new Vector2(31 / 2, 50 / 2)));

            PatrickAnimations.Add(AnimationType.Idle, PatrickIdle);

            List<Frame> PatrickFalling = new List<Frame>();
            PatrickFalling.Add(new Frame(new Rectangle(147, 167, 40, 50), new Vector2(40 / 2, 50 / 2)));

            PatrickAnimations.Add(AnimationType.Falling, PatrickFalling);

            List<Frame> PatrickWalking = new List<Frame>();
            PatrickWalking.Add(new Frame(new Rectangle(0, 57, 26, 49), new Vector2(26 / 2, 49 / 2)));
            PatrickWalking.Add(new Frame(new Rectangle(31, 59, 24, 47), new Vector2(24 / 2, 47 / 2)));
            PatrickWalking.Add(new Frame(new Rectangle(88, 59, 24, 48), new Vector2(24 / 2, 48 / 2)));
            PatrickWalking.Add(new Frame(new Rectangle(116, 56, 28, 50), new Vector2(28 / 2, 50 / 2)));
            PatrickWalking.Add(new Frame(new Rectangle(149, 57, 34, 49), new Vector2(34 / 2, 49 / 2)));
            PatrickWalking.Add(new Frame(new Rectangle(189, 59, 33, 47), new Vector2(33 / 2, 47 / 2)));
            PatrickWalking.Add(new Frame(new Rectangle(228, 59, 30, 47), new Vector2(30 / 2, 47 / 2)));
            PatrickWalking.Add(new Frame(new Rectangle(263, 59, 22, 47), new Vector2(22 / 2, 47 / 2)));
            PatrickWalking.Add(new Frame(new Rectangle(290, 56, 23, 50), new Vector2(23 / 2, 50 / 2)));

            PatrickAnimations.Add(AnimationType.Walking, PatrickWalking);

            List<Frame> PatrickJumping = new List<Frame>();
            PatrickJumping.Add(new Frame(new Rectangle(1, 167, 29, 49), new Vector2(29 / 2, 49 / 2)));
            PatrickJumping.Add(new Frame(new Rectangle(35, 168, 31, 49), new Vector2(31 / 2, 49 / 2)));
            PatrickJumping.Add(new Frame(new Rectangle(71, 168, 30, 49), new Vector2(30 / 2, 49 / 2)));
            PatrickJumping.Add(new Frame(new Rectangle(106, 168, 36, 50), new Vector2(36 / 2, 50 / 2)));
            PatrickJumping.Add(new Frame(new Rectangle(147, 167, 40, 49), new Vector2(40 / 2, 49 / 2)));

            PatrickAnimations.Add(AnimationType.Jumping, PatrickJumping);

            List<Frame> PatrickCrouching = new List<Frame>();
            PatrickCrouching.Add(new Frame(new Rectangle(0, 389, 28, 45), new Vector2(28 / 2, 45 / 2)));
            PatrickCrouching.Add(new Frame(new Rectangle(33, 397, 36, 36), new Vector2(36 / 2, 36 / 2)));
            PatrickCrouching.Add(new Frame(new Rectangle(73, 400, 37, 34), new Vector2(37 / 2, 34 / 2)));

            PatrickAnimations.Add(AnimationType.Crouching, PatrickCrouching);

            #endregion

            #region coinAnimations
            List<Frame> CoinFrames = new List<Frame>();
            CoinFrames.Add(new Frame(new Rectangle(336, 92, 12, 16), new Vector2(12/2, 16/2))); 
            CoinFrames.Add(new Frame(new Rectangle(353, 92, 2, 17), new Vector2(2/2, 17/2))); 
            CoinFrames.Add(new Frame(new Rectangle(359, 92, 12, 16), new Vector2(12/2, 16/2))); 
            CoinFrames.Add(new Frame(new Rectangle(374, 92, 16, 16), new Vector2(16/2, 16/2)));
            
            CoinAnimations.Add(AnimationType.Turning, CoinFrames);

            #endregion



            position = new Vector2(40, 390);
            penguinPosition = new Vector2(500, 272);
            speed = new Vector2(4);

            if (character == "Mario")
            {
                if (!IsTiny)
                {
                    Mario = new Character(spriteSheet, position, marioAnimations, Content.Load<Texture2D>("FireBall_1"));
                }
                else
                {
                    Mario = new Character(spriteSheet, position, marioAnimations, Content.Load<Texture2D>("Copy of Fireball_1"));
                }
            }
            else if (character == "Spongebob")
            {
                if (!IsTiny)
                {
                    Mario = new Character(SpongebobSpritesheet, position, SpongebobAnimations, Content.Load<Texture2D>("FireBall_1"));
                }
                else
                {
                    Mario = new Character(SpongebobSpritesheet, position, SpongebobAnimations, Content.Load<Texture2D>("FireBall_1"));
                }
            }
            else if (character == "Patrick")
            {
                if (!IsTiny)
                {
                    Mario = new Character(PatrickSpritesheet, position, PatrickAnimations, Content.Load<Texture2D>("FireBall_1"));
                }
                else
                {
                    Mario = new Character(PatrickSpritesheet, position, PatrickAnimations, Content.Load<Texture2D>("Copy of Fireball_1"));
                }
            }
          
            Penguin = new AnimatedSprite(penguinspritesheet, penguinPosition, Color.White, strolling);
            
           
            //Spongebob = new Character(SpongebobSpritesheet, position, SpongebobAnimations, Content.Load<Texture2D>("Fireball"));
            pizza = new Sprite(Content.Load<Texture2D>("pizza"), new Vector2(870, 210), Color.White);
            portal2 = new Sprite(Content.Load<Texture2D>("portal"), new Vector2(700, 380), Color.White);
            portal1 = new Sprite(Content.Load<Texture2D>("portal"), new Vector2(95, 380), Color.White);
            invert = new Sprite(Content.Load<Texture2D>("invert"), new Vector2(800, 120), Color.White);
            uninvert = new Sprite(Content.Load<Texture2D>("re-invert"), new Vector2(500, 189), Color.White);
            flower = new Sprite(Content.Load<Texture2D>("Fireball_Flower"), new Vector2(220, 250), Color.White);
            bunny = new Sprite(Content.Load<Texture2D>("bunny"), new Vector2(270, 400), Color.White);
            StartScreenBackground = new Sprite(Content.Load<Texture2D>("PlatformerMap1"), new Vector2(0, 0), Color.White);
            GameOverScreen = new Sprite(Content.Load<Texture2D>("GameOver"), new Vector2(0, 0), Color.White);
            LevelMenuBg = new Sprite(Content.Load<Texture2D>("LevelSelectMenu"), new Vector2(0, 0), Color.White);
            restartbutton = new Button(Content.Load<Texture2D>("RestartButton"), new Vector2(960, 0), Color.White);
            lvl1button = new Button(Content.Load<Texture2D>("lvl1button"), new Vector2(150, 150), Color.White);
            lvl2button = new Button(Content.Load<Texture2D>("lvl2button"), new Vector2(300, 150), Color.White);
            lvl3button = new Button(Content.Load<Texture2D>("lvl3button"), new Vector2(450, 150), Color.White);
            lvl4button = new Button(Content.Load<Texture2D>("lvl4button"), new Vector2(600, 150), Color.White);
            lvl5button = new Button(Content.Load<Texture2D>("lvl5button"), new Vector2(150, 210), Color.White);
            lvl6button = new Button(Content.Load<Texture2D>("lvl6button"), new Vector2(300, 210), Color.White);
            lvl7button = new Button(Content.Load<Texture2D>("lvl7button"), new Vector2(450, 210), Color.White);
            lvl8button = new Button(Content.Load<Texture2D>("lvl8button"), new Vector2(600, 210), Color.White);
            lvl9button = new Button(Content.Load<Texture2D>("lvl9button"), new Vector2(150, 270), Color.White);
            lvl10button = new Button(Content.Load<Texture2D>("lvl10button"), new Vector2(300, 270), Color.White);
            lvl11button = new Button(Content.Load<Texture2D>("lvl11button"), new Vector2(450, 270), Color.White);
            lvl12button = new Button(Content.Load<Texture2D>("lvl12button"), new Vector2(600, 270), Color.White);
            lvl13button = new Button(Content.Load<Texture2D>("lvl13button"), new Vector2(100, 100), Color.White);
            lvl01button = new Button(Content.Load<Texture2D>("lvl1button"), new Vector2(150, 150), Color.White);
            lvl02button = new Button(Content.Load<Texture2D>("lvl2button"), new Vector2(300, 150), Color.White);
            lvl03button = new Button(Content.Load<Texture2D>("lvl3button"), new Vector2(450, 150), Color.White);
            lvl04button = new Button(Content.Load<Texture2D>("lvl4button"), new Vector2(600, 150), Color.White);
            lvl05button = new Button(Content.Load<Texture2D>("lvl5button"), new Vector2(150, 210), Color.White);
            lvl06button = new Button(Content.Load<Texture2D>("lvl6button"), new Vector2(300, 210), Color.White);
            lvl07button = new Button(Content.Load<Texture2D>("lvl7button"), new Vector2(450, 210), Color.White);
            lvl08button = new Button(Content.Load<Texture2D>("lvl8button"), new Vector2(600, 210), Color.White);
            lvl09button = new Button(Content.Load<Texture2D>("lvl9button"), new Vector2(150, 270), Color.White);
            lvl010button = new Button(Content.Load<Texture2D>("lvl10button"), new Vector2(300, 270), Color.White);
            lvl011button = new Button(Content.Load<Texture2D>("lvl11button"), new Vector2(450, 270), Color.White);
            lvl012button = new Button(Content.Load<Texture2D>("lvl12button"), new Vector2(600, 270), Color.White);
            lvl013button = new Button(Content.Load<Texture2D>("lvl13button"), new Vector2(100, 100), Color.White);
            MenuButton = new Button(Content.Load<Texture2D>("MenuButton"), new Vector2(960, 40), Color.White);
            LevelSelectButton = new Button(Content.Load<Texture2D>("LevelSelectButton"), new Vector2(100, 100), Color.White);
            ShopButton = new Button(Content.Load<Texture2D>("ShopButton"), new Vector2(400, 100), Color.White);
            ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(400, 400), Color.White);
            PlayButton = new Button(Content.Load<Texture2D>("PlayButton"), new Vector2(400, 400), Color.White);
            HealthPowerup = new Sprite(Content.Load<Texture2D>("Caduceus"), new Vector2(100, 100), Color.White);
            LandLevelsButton = new Button(Content.Load<Texture2D>("Land Levels_Button"), new Vector2(100, 100), Color.White);
            UnderwaterLevelsButton = new Button(Content.Load<Texture2D>("Underwater Levels_Button"), new Vector2(200, 100), Color.White);
            
            
                MarioButton = new Button(Content.Load<Texture2D>("MarioButton"), new Vector2(100, 100), Color.White);
                SpongebobButton = new Button(Content.Load<Texture2D>("SpongebobButton"), new Vector2(250, 100), Color.White);
                PatrickButton = new Button(Content.Load<Texture2D>("PatrickButton"), new Vector2(400, 100), Color.White);
         
          
            Mario.Origin = new Vector2(15, 33);
            Penguin.Origin = new Vector2(300, 300);
            Texture2D platformImage = Content.Load<Texture2D>("Platform");
            Texture2D lavaPlatformImage = Content.Load<Texture2D>("lava");

            /* level1Background = Content.Load<Texture2D>("PlatformerMap1");
             Map2 = Content.Load<Texture2D>("PlatformerMap2");
             Map3 = Content.Load<Texture2D>("PlatformerMap3");
             Map4 = Content.Load<Texture2D>("PlatformerMap4");
             UnderwaterMap = Content.Load<Texture2D>("UnderwaterMap");
             Gameoverscreen = Content.Load<Texture2D>("GameOver");
             */
            maps = new Dictionary<LevelMaps, Texture2D>();
            maps.Add(LevelMaps.Stars, Content.Load<Texture2D>("PlatformerMap1"));
            maps.Add(LevelMaps.Sunset, Content.Load<Texture2D>("PlatformerMap2"));
            maps.Add(LevelMaps.Clouds, Content.Load<Texture2D>("PlatformerMap3"));
            maps.Add(LevelMaps.Black, Content.Load<Texture2D>("PlatformerMap4"));
            maps.Add(LevelMaps.Kelp, Content.Load<Texture2D>("UnderwaterMap"));
            maps.Add(LevelMaps.Gameover, Content.Load<Texture2D>("GameOver"));
            


            #region level0
            List<Sprite> level0Platforms = new List<Sprite>();
            level0Platforms.Add(new Sprite(platformImage, new Vector2(0, 429), Color.White));
            level0Platforms[0].Size = new Vector2(59, 61);
            level0Platforms.Add(new Sprite(platformImage, new Vector2(104, 349), Color.White));
            level0Platforms[1].Size = new Vector2(20, 20);
            level0Platforms.Add(new Sprite(platformImage, new Vector2(280, 297), Color.White));
            level0Platforms[2].Size = new Vector2(20, 20);
            level0Platforms.Add(new Sprite(platformImage, new Vector2(460, 248), Color.White));
            level0Platforms[3].Size = new Vector2(20, 20);
            level0Platforms.Add(new Sprite(platformImage, new Vector2(635, 207), Color.White));
            level0Platforms[4].Size = new Vector2(20, 20);
            level0Platforms.Add(new Sprite(platformImage, new Vector2(793, 125), Color.White));
            level0Platforms[5].Size = new Vector2(20, 20);

            level0 = new Level(level0Platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion            


            #region level1
            List<Sprite> level1Platforms = new List<Sprite>();
            level1Platforms.Add(new Sprite(platformImage, new Vector2(0, 438), Color.White));
            level1Platforms[0].Size = new Vector2(112, 51);
            
           
        
 
            level1Platforms.Add(new Sprite(platformImage, new Vector2(148, 383), Color.White));
            level1Platforms[1].Size = new Vector2(74, 25);
            level1Platforms.Add(new Sprite(platformImage, new Vector2(262, 437), Color.White));
            level1Platforms[2].Size = new Vector2(165, 52);
            level1Platforms.Add(new Sprite(platformImage, new Vector2(487, 355), Color.White));
            level1Platforms[3].Size = new Vector2(73, 25);
            level1Platforms.Add(new Sprite(platformImage, new Vector2(781, 329), Color.White));
            level1Platforms[4].Size = new Vector2(74, 25);
            level1Platforms.Add(new Sprite(platformImage, new Vector2(873, 439), Color.White));
            level1Platforms[5].Size = new Vector2(127, 50);
            level1Platforms.Add(new Sprite(platformImage, new Vector2(616, 439), Color.White));
            level1Platforms[6].Size = new Vector2(146, 50);
           
            level1Platforms.Add(new Sprite(platformImage, new Vector2(0, 400), Color.White));
            level1Platforms[7].Size = new Vector2(20, 100);

            level1Platforms.Add(new Sprite(platformImage, new Vector2(100, 400), Color.White));
            level1Platforms[8].Size = new Vector2(20, 100);
            #endregion

            level1 = new Level(level1Platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(940, 368), Color.White) { Scale = new Vector2(.75f) });
            


            #region level2
            List<Sprite> level2Platforms = new List<Sprite>();

            level2Platforms.Add(new Sprite(platformImage, new Vector2(1, 447), Color.White));
            level2Platforms[0].Size = new Vector2(101, 41);
            level2Platforms.Add(new Sprite(platformImage, new Vector2(234, 445), Color.White));
            level2Platforms[1].Size = new Vector2(101, 41);
            level2Platforms.Add(new Sprite(platformImage, new Vector2(380, 263), Color.White));
            level2Platforms[2].Size = new Vector2(42, 226);
            level2Platforms.Add(new Sprite(platformImage, new Vector2(593, 183), Color.White));
            level2Platforms[3].Size = new Vector2(40, 306);
            level2Platforms.Add(new Sprite(platformImage, new Vector2(820, 264), Color.White));
            level2Platforms[4].Size = new Vector2(42, 224);
            level2Platforms.Add(new Sprite(platformImage, new Vector2(921, 150), Color.White));
            level2Platforms[5].Size = new Vector2(77, 22);
            level2Platforms.Add(new Sprite(platformImage, new Vector2(540, 363), Color.White));
            level2Platforms[6].Size = new Vector2(156, 53);

            level2 = new Level(level2Platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion

            #region level3platforms
            List<Sprite> level3Platforms = new List<Sprite>();

            level3Platforms.Add(new Sprite(platformImage, new Vector2(1, 465), Color.White));
            level3Platforms[0].Size = new Vector2(995, 23);
            level3Platforms.Add(new Sprite(platformImage, new Vector2(177, 280), Color.White));
            level3Platforms[1].Size = new Vector2(614, 56);
            level3Platforms.Add(new Sprite(platformImage, new Vector2(0, 159), Color.White));
            level3Platforms[2].Size = new Vector2(316, 18);
            level3Platforms.Add(new Sprite(platformImage, new Vector2(775, 153), Color.White));
            level3Platforms[3].Size = new Vector2(221, 18);
            level3Platforms.Add(new Sprite(platformImage, new Vector2(446, 82), Color.White));
            level3Platforms[4].Size = new Vector2(206, 37);
            level3Platforms.Add(new Sprite(platformImage, new Vector2(33, 355), Color.White));
            level3Platforms[5].Size = new Vector2(48, 16);

            level3 = new Level(level3Platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion

            List<Sprite> level4Platforms = new List<Sprite>();

            #region level4
            level4Platforms.Add(new Sprite(platformImage, new Vector2(163, 261), Color.White));
            level4Platforms[0].Size = new Vector2(50, 225);
            level4Platforms.Add(new Sprite(platformImage, new Vector2(307, 479), Color.White));
            level4Platforms[1].Size = new Vector2(27, 7);
            level4Platforms.Add(new Sprite(platformImage, new Vector2(307, 1), Color.White));
            level4Platforms[2].Size = new Vector2(38, 341);
            level4Platforms.Add(new Sprite(platformImage, new Vector2(436, 254), Color.White));
            level4Platforms[3].Size = new Vector2(38, 234);
            level4Platforms.Add(new Sprite(platformImage, new Vector2(550, 0), Color.White));
            level4Platforms[4].Size = new Vector2(39, 325);
            level4Platforms.Add(new Sprite(platformImage, new Vector2(569, 479), Color.White));
            level4Platforms[5].Size = new Vector2(39, 6);
            level4Platforms.Add(new Sprite(platformImage, new Vector2(701, 259), Color.White));
            level4Platforms[6].Size = new Vector2(38, 227);
            level4Platforms.Add(new Sprite(platformImage, new Vector2(834, 173), Color.White));
            level4Platforms[7].Size = new Vector2(166, 44);

            level4 = new Level(level4Platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion

            List<Sprite> level5platforms = new List<Sprite>();

            #region level5
            level5platforms.Add(new Sprite(platformImage, new Vector2(1, 242), Color.White));
            level5platforms[0].Size = new Vector2(858, 41);
            level5platforms.Add(new Sprite(platformImage, new Vector2(257, 297), Color.White));
            level5platforms[1].Size = new Vector2(743, 41);
            level5platforms.Add(new Sprite(platformImage, new Vector2(0, 460), Color.White));
            level5platforms[2].Size = new Vector2(133, 26);
            level5platforms.Add(new Sprite(platformImage, new Vector2(677, 465), Color.White));
            level5platforms[3].Size = new Vector2(321, 23);

            level5 = new Level(level5platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 465), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion

            List<Sprite> level6platforms = new List<Sprite>();
            #region level6
            level6platforms.Add(new Sprite(platformImage, new Vector2(1, 440), Color.White));
            level6platforms[0].Size = new Vector2(280, 45);
            level6platforms.Add(new Sprite(platformImage, new Vector2(357, 349), Color.White));
            level6platforms[1].Size = new Vector2(83, 16);
            level6platforms.Add(new Sprite(platformImage, new Vector2(358, 229), Color.White));
            level6platforms[2].Size = new Vector2(83, 16);
            level6platforms.Add(new Sprite(platformImage, new Vector2(355, 142), Color.White));
            level6platforms[3].Size = new Vector2(235, 12);
            level6platforms.Add(new Sprite(platformImage, new Vector2(494, 229), Color.White));
            level6platforms[4].Size = new Vector2(88, 15);
            level6platforms.Add(new Sprite(platformImage, new Vector2(495, 350), Color.White));
            level6platforms[5].Size = new Vector2(88, 15);
            level6platforms.Add(new Sprite(platformImage, new Vector2(624, 439), Color.White));
            level6platforms[6].Size = new Vector2(374, 46);
            level6platforms.Add(new Sprite(platformImage, new Vector2(439, 141), Color.White));
            level6platforms[7].Size = new Vector2(56, 347);

            level6 = new Level(level6platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 438), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion

            List<Sprite> level7platforms = new List<Sprite>();
            #region level7
            level7platforms.Add(new Sprite(platformImage, new Vector2(2, 186), Color.White));
            level7platforms[0].Size = new Vector2(996, 56);
            level7platforms.Add(new Sprite(platformImage, new Vector2(237, 448), Color.White));
            level7platforms[1].Size = new Vector2(487, 37);

            level7 = new Level(level7platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(500, 447), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion

            List<Sprite> level8platforms = new List<Sprite>();
            #region
            level8platforms.Add(new Sprite(platformImage, new Vector2(1, 452), Color.White));
            level8platforms[0].Size = new Vector2(163, 36);
            level8platforms.Add(new Sprite(platformImage, new Vector2(168, 334), Color.White));
            level8platforms[1].Size = new Vector2(161, 36);
            level8platforms.Add(new Sprite(platformImage, new Vector2(365, 244), Color.White));
            level8platforms[2].Size = new Vector2(161, 36);
            level8platforms.Add(new Sprite(platformImage, new Vector2(593, 177), Color.White));
            level8platforms[3].Size = new Vector2(161, 36);
            level8platforms.Add(new Sprite(platformImage, new Vector2(833, 111), Color.White));
            level8platforms[4].Size = new Vector2(161, 36);

            level8 = new Level(level8platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(940, 110), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion

            List<Sprite> level9platforms = new List<Sprite>();
            #region
            level9platforms.Add(new Sprite(platformImage, new Vector2(208, 135), Color.White));
            level9platforms[0].Size = new Vector2(27, 75);
            level9platforms.Add(new Sprite(platformImage, new Vector2(1, 2), Color.White));
            level9platforms[1].Size = new Vector2(24, 488);
            level9platforms.Add(new Sprite(platformImage, new Vector2(0, 1), Color.White));
            level9platforms[2].Size = new Vector2(1012, 27);
            level9platforms.Add(new Sprite(platformImage, new Vector2(302, 28), Color.White));
            level9platforms[3].Size = new Vector2(47, 285);
            level9platforms.Add(new Sprite(platformImage, new Vector2(25, 136), Color.White));
            level9platforms[4].Size = new Vector2(209, 31);
            level9platforms.Add(new Sprite(platformImage, new Vector2(0, 469), Color.White));
            level9platforms[5].Size = new Vector2(1001, 20);
            level9platforms.Add(new Sprite(platformImage, new Vector2(185, 208), Color.White));
            level9platforms[6].Size = new Vector2(65, 18);
            level9platforms.Add(new Sprite(platformImage, new Vector2(148, 285), Color.White));
            level9platforms[7].Size = new Vector2(829, 28);
            level9platforms.Add(new Sprite(platformImage, new Vector2(457, 127), Color.White));
            level9platforms[8].Size = new Vector2(516, 159);
            level9platforms.Add(new Sprite(platformImage, new Vector2(973, 4), Color.White));
            level9platforms[9].Size = new Vector2(26, 484);

            level9 = new Level(level9platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(910, 130), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion
            List<Sprite> level10platforms = new List<Sprite>();
            #region
            level10platforms.Add(new Sprite(platformImage, new Vector2(0, 230), Color.White));
            level10platforms[0].Size = new Vector2(193, 98);
            level10platforms.Add(new Sprite(platformImage, new Vector2(281, 231), Color.White));
            level10platforms[1].Size = new Vector2(17, 35);
            level10platforms.Add(new Sprite(platformImage, new Vector2(387, 227), Color.White));
            level10platforms[2].Size = new Vector2(17, 35);
            level10platforms.Add(new Sprite(platformImage, new Vector2(500, 226), Color.White));
            level10platforms[3].Size = new Vector2(17, 35);
            level10platforms.Add(new Sprite(platformImage, new Vector2(611, 226), Color.White));
            level10platforms[4].Size = new Vector2(17, 35);
            level10platforms.Add(new Sprite(platformImage, new Vector2(721, 225), Color.White));
            level10platforms[5].Size = new Vector2(17, 35);
            level10platforms.Add(new Sprite(platformImage, new Vector2(814, 219), Color.White));
            level10platforms[6].Size = new Vector2(103, 37);

            level10 = new Level(level10platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(880, 215), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion
            List<Sprite> level11platforms = new List<Sprite>();
            #region
            level11platforms.Add(new Sprite(platformImage, new Vector2(205, 320), Color.White));
            level11platforms[0].Size = new Vector2(14, 14);
            level11platforms.Add(new Sprite(platformImage, new Vector2(95, 213), Color.White));
            level11platforms[1].Size = new Vector2(750, 29);
            level11platforms.Add(new Sprite(platformImage, new Vector2(311, 140), Color.White));
            level11platforms[2].Size = new Vector2(370, 345);

            level11 = new Level(level11platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(700, 440), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion
            List<Sprite> level12platforms = new List<Sprite>();
            #region
            level12platforms.Add(new Sprite(platformImage, new Vector2(1, 249), Color.White));
            level12platforms[0].Size = new Vector2(132, 39);
            level12platforms.Add(new Sprite(platformImage, new Vector2(159, 363), Color.White));
            level12platforms[1].Size = new Vector2(659, 52);
            level12platforms.Add(new Sprite(platformImage, new Vector2(873, 245), Color.White));
            level12platforms[2].Size = new Vector2(132, 39);


            level12 = new Level(level12platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(900, 240), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion
            List<Sprite> level13platforms = new List<Sprite>();
            #region
            level13platforms.Add(new Sprite(platformImage, new Vector2(82, 463), Color.White));
            level13platforms[0].Size = new Vector2(225, 21);
            level13platforms.Add(new Sprite(platformImage, new Vector2(176, 53), Color.White));
            level13platforms[1].Size = new Vector2(31, 431);
            level13platforms.Add(new Sprite(platformImage, new Vector2(83, 313), Color.White));
            level13platforms[2].Size = new Vector2(223, 26);
            level13platforms.Add(new Sprite(platformImage, new Vector2(85, 187), Color.White));
            level13platforms[3].Size = new Vector2(223, 26);
            level13platforms.Add(new Sprite(platformImage, new Vector2(82, 54), Color.White));
            level13platforms[4].Size = new Vector2(223, 26);
            level13platforms.Add(new Sprite(platformImage, new Vector2(610, 0), Color.White));
            level13platforms[5].Size = new Vector2(33, 388);
            level13platforms.Add(new Sprite(platformImage, new Vector2(456, 123), Color.White));
            level13platforms[6].Size = new Vector2(326, 29);
            level13platforms.Add(new Sprite(platformImage, new Vector2(528, 231), Color.White));
            level13platforms[7].Size = new Vector2(204, 37);
            level13platforms.Add(new Sprite(platformImage, new Vector2(575, 369), Color.White));
            level13platforms[8].Size = new Vector2(98, 22);
            level13platforms.Add(new Sprite(platformImage, new Vector2(789, 326), Color.White));
            level13platforms[9].Size = new Vector2(208, 22);
            level13platforms.Add(new Sprite(platformImage, new Vector2(-1, -108), Color.White));
            level13platforms[10].Size = new Vector2(491, 103);
            level13platforms.Add(new Sprite(platformImage, new Vector2(560, 467), Color.White));
            level13platforms[11].Size = new Vector2(138, 20);


            level13 = new Level(level13platforms, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(660, 120), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion
            List<Sprite> level01platforms = new List<Sprite>();
            #region
            level01platforms.Add(new Sprite(platformImage, new Vector2(18, 473), Color.White));
            level01platforms[0].Size = new Vector2(968, 19);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(0, 0), Color.White));
            level01platforms[1].Size = new Vector2(19, 490);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(18, 0), Color.White));
            level01platforms[2].Size = new Vector2(968, 19);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(985, 1), Color.White));
            level01platforms[3].Size = new Vector2(15, 488);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(207, 321), Color.White));
            level01platforms[4].Size = new Vector2(779, 22);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(17, 157), Color.White));
            level01platforms[5].Size = new Vector2(802, 22);


            /*List<Sprite> level01lavaplatforms = new List<Sprite>();
            #region level01
            level01lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(0, 0), Color.White));
            level01lavaplatforms[0].Size = new Vector2(19, 490);
            level01lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(18, 0), Color.White));
            level01lavaplatforms[1].Size = new Vector2(968, 19);
            level01lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(985, 1), Color.White));
            level01lavaplatforms[2].Size = new Vector2(15, 488);
            level01lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(207, 321), Color.White));
            level01lavaplatforms[3].Size = new Vector2(779, 22);
            level01lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(17, 157), Color.White));
            level01lavaplatforms[4].Size = new Vector2(802, 22);

            #endregion
            
    */
            level01 = new Level(level01platforms, maps[LevelMaps.Kelp], new Sprite(Content.Load<Texture2D>("door"), new Vector2(660, 120), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            
            #endregion
            
            List<Sprite> level02platforms = new List<Sprite>();
            #region
            level02platforms.Add(new Sprite(platformImage, new Vector2(0, 435), Color.White));
            level02platforms[0].Size = new Vector2(151, 53);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(227, 0), Color.White));
            level02platforms[1].Size = new Vector2(25, 169);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(227, 251), Color.White));
            level02platforms[2].Size = new Vector2(25, 237);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(353, 0), Color.White));
            level02platforms[3].Size = new Vector2(25, 240);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(354, 327), Color.White));
            level02platforms[4].Size = new Vector2(25, 162);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(481, 0), Color.White));
            level02platforms[5].Size = new Vector2(25, 379);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(483, 461), Color.White));
            level02platforms[6].Size = new Vector2(25, 27);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(594, 0), Color.White));
            level02platforms[7].Size = new Vector2(25, 96);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(596, 228), Color.White));
            level02platforms[8].Size = new Vector2(26, 260);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(709, 0), Color.White));
            level02platforms[9].Size = new Vector2(26, 290);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(709, 392), Color.White));
            level02platforms[10].Size = new Vector2(25, 97);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(803, 0), Color.White));
            level02platforms[11].Size = new Vector2(195, 220);
            level02platforms.Add(new Sprite(lavaPlatformImage, new Vector2(803, 322), Color.White));
            level02platforms[12].Size = new Vector2(196, 167);

            List<Sprite> level02lavaplatforms = new List<Sprite>();
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(227, 0), Color.White));
            level02lavaplatforms[0].Size = new Vector2(25, 169);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(227, 251), Color.White));
            level02lavaplatforms[1].Size = new Vector2(25, 237);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(353, 0), Color.White));
            level02lavaplatforms[2].Size = new Vector2(25, 240);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(354, 327), Color.White));
            level02lavaplatforms[3].Size = new Vector2(25, 162);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(481, 0), Color.White));
            level02lavaplatforms[4].Size = new Vector2(25, 379);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(483, 461), Color.White));
            level02lavaplatforms[5].Size = new Vector2(25, 27);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(594, 0), Color.White));
            level02lavaplatforms[6].Size = new Vector2(25, 96);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(596, 228), Color.White));
            level02lavaplatforms[7].Size = new Vector2(26, 260);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(709, 0), Color.White));
            level02lavaplatforms[8].Size = new Vector2(26, 290);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(709, 392), Color.White));
            level02lavaplatforms[9].Size = new Vector2(25, 97);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(803, 0), Color.White));
            level02lavaplatforms[10].Size = new Vector2(195, 220);
            level02lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(803, 322), Color.White));
            level02lavaplatforms[11].Size = new Vector2(196, 167);

            level02 = new Level(level02platforms, UnderwaterMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(950, 300), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });
            #endregion

            #region ULevel3platforms
            List<Sprite> ULevel3platforms = new List<Sprite>();
            //Regular platforms || X speed = 0 || Y speed = 0
            ULevel3platforms.Add(new Sprite(platformImage, new Vector2(0, 70), Color.White));
            ULevel3platforms[0].Size = new Vector2(143, 28); 
            ULevel3platforms.Add(new Sprite(platformImage, new Vector2(635, 286), Color.White));
            ULevel3platforms[1].Size = new Vector2(143, 28);
            // Horizontally moving platforms || X speed = 5 || Y speed = 0
            ULevel3platforms.Add(new Sprite(platformImage, new Vector2(180, 69), Color.White));
            ULevel3platforms[2].Size = new Vector2(139, 28);
            ULevel3platforms.Add(new Sprite(platformImage, new Vector2(652, 460), Color.White));
            ULevel3platforms[3].Size = new Vector2(139, 28);
            ULevel3platforms.Add(new Sprite(platformImage, new Vector2(125, 251), Color.White));
            ULevel3platforms[4].Size = new Vector2(139, 28);

            // Vertically moving platforms || X speed = 0 || Y speed = 5
            ULevel3platforms.Add(new Sprite(platformImage, new Vector2(862, 83), Color.White));
            ULevel3platforms[5].Size = new Vector2(139, 28);
            ULevel3platforms.Add(new Sprite(platformImage, new Vector2(0, 460), Color.White));
            ULevel3platforms[6].Size = new Vector2(112, 28);

            // Lava platforms
            List<Sprite> ULevel3lavaplatforms = new List<Sprite>();
            ULevel3lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(0, 124), Color.White));
            ULevel3lavaplatforms[0].Size = new Vector2(795, 23);
            ULevel3lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(777, 147), Color.White));
            ULevel3lavaplatforms[1].Size = new Vector2(19, 194);
            ULevel3lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(265, 314), Color.White));
            ULevel3lavaplatforms[2].Size = new Vector2(531, 27);
            ULevel3lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(265, 286), Color.White));
            ULevel3lavaplatforms[3].Size = new Vector2(33, 29);
            ULevel3lavaplatforms.Add(new Sprite(lavaPlatformImage, new Vector2(265, 147), Color.White));
            ULevel3lavaplatforms[4].Size = new Vector2(33, 29);

            level03 = new ULevels(ULevel3platforms, ULevel3lavaplatforms, maps[LevelMaps.Kelp], new Sprite(Content.Load<Texture2D>("door"), new Vector2(950, 300), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) });


            #endregion


            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            currentLevel = level03;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState lastMs = ms;
            ms = Mouse.GetState();

            
            if (ks.IsKeyDown(Keys.L))
            {
                screen = 8;
            }

            if (screen == 1)
            {
                
                
                if (character == "Mario")
                {
                    //set your image to mario

                    //set your animations to mario
                    Mario.Image = spriteSheet;
                    Mario.Animations = marioAnimations;

                }
                else if (character == "Spongebob")
                {
                    Mario.Image = SpongebobSpritesheet;
                    Mario.Animations = SpongebobAnimations;
                }
                else if (character == "Patrick")
                {
                    Mario.Image = PatrickSpritesheet;
                    Mario.Animations = PatrickAnimations;
                }

                currentLevelMap = maps[levelmaps];

                if (Mario.HitBox.Intersects(bunny.HitBox) && currentLevel == level2)
                {
                    hasJumpBoost = true;
                    Mario.jumpPower = 2;
                }

                if (Mario.touchedLava == true)
                {
                    lives--;
                }

                //setting your keyboardstate = what is happening with the keyboard
                if (Mario.Y >= 489)
                {
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y - 50;
                    lives--;
                }
                if (Mario.HitBox.Intersects(currentLevel.Door.HitBox) && ks.IsKeyDown(Keys.O))
                {
                    if (currentLevel == level1)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level2;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;

                    }
                    else if (currentLevel == level2)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level3;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                        enemyisdead = false;
                        penguinPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 1.8f);
                        MoreLives = false;
                    }
                    else if (currentLevel == level3)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level4;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }
                    else if (currentLevel == level4)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level5;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }
                    else if (currentLevel == level5)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level6;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                        MoreLives = false;
                    }
                    else if (currentLevel == level6)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level7;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }
                    else if (currentLevel == level7)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level8;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }
                    else if (currentLevel == level8)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level9;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }
                    else if (currentLevel == level9)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level10;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }
                    else if (currentLevel == level10)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level11;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y - 50;
                        MoreLives = false;
                    }
                    else if (currentLevel == level11)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level12;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                        enemyisdead = false;
                        Penguin.X = 500;
                        Penguin.Y = 350;
                        MoreLives = false;
                        
                    }
                    else if (currentLevel == level12)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level13;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }
                    //else if (currentLevel == level0)
                    //{
                    //    Mario.Scale = Vector2.One;
                    //    currentLevel = level02;
                    //    Mario.X = 10;
                    //    Mario.Y = 0;
                    //}
                    else if(currentLevel == level01)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level02;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }
                    else if(currentLevel == level02)
                    {
                        fireballhitcount = 0;
                        Mario.Scale = Vector2.One;
                        currentLevel = level03;
                        Mario.X = currentLevel.platforms[0].X;
                        Mario.Y = currentLevel.platforms[0].Y;
                    }

                    levelmaps++;
                }
                if (Mario.HitBox.Intersects(Penguin.HitBox))
                {
                    if (enemyisdead == false)
                    {
                        if (currentLevel == level3 || currentLevel == level12)
                        {
                            lives -= 10;
                            lostlife = true;
                            
                            if(lives == 0)
                            {
                                
                            }
                        }
                    }
                }
                
              if (currentLevel == level3 || currentLevel == level12)
                {
                    Penguin.Update(gameTime);
                }
           
                Mario.CheckCollision(currentLevel.platforms);
                //Mario.CheckLavaCollision(currentLevel.lavaplatforms);
                
                
                Mario.Update(gameTime);
                base.Update(gameTime);

                if (ks.IsKeyDown(Keys.R))
                {
                    fireballhitcount = 0;
                    gameisover = false;
                    Mario.Scale = Vector2.One;
                    currentLevel = level1;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                if (ks.IsKeyDown(Keys.D1))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level1;
                    Mario.Origin = new Vector2(15, 33);
                    fireballhitcount = 0;
                }
                if (ks.IsKeyDown(Keys.D2))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level2;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                if (ks.IsKeyDown(Keys.D3))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level3;
                    enemyisdead = false;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    hasfirepower = false;
                    enemyisdead = false;
                    MoreLives = false;
                    penguinPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 1.8f);
                }
                if (ks.IsKeyDown(Keys.D4))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level4;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                if (ks.IsKeyDown(Keys.D5))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level5;
                    IsTiny = false;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    portal2 = new Sprite(Content.Load<Texture2D>("portal"), new Vector2(700, 380), Color.White);
                    portal1 = new Sprite(Content.Load<Texture2D>("portal"), new Vector2(95, 380), Color.White);
                }
                
                if (ks.IsKeyDown(Keys.D6))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level6;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;

                }
                if (ks.IsKeyDown(Keys.D7))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level7;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;

                }
                if (ks.IsKeyDown(Keys.D8))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level8;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;

                }
                if (ks.IsKeyDown(Keys.D9))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level9;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;

                }
                if (ks.IsKeyDown(Keys.D0))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level10;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;

                }
                if (ks.IsKeyDown(Keys.Q))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level11;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y - 50;
                    MoreLives = false;
                }
                if (ks.IsKeyDown(Keys.W))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level12;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    enemyisdead = false;
                    Penguin.X = 500;
                    Penguin.Y = 350;
                    MoreLives = false;
                }
                if (ks.IsKeyDown(Keys.E))
                {
                    fireballhitcount = 0;
                    Mario.Scale = Vector2.One;
                    currentLevel = level13;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                if (ks.IsKeyDown(Keys.Back))
                {
                    Mario.Scale = Vector2.One;
                    currentLevel = level0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                if (ks.IsKeyDown(Keys.Enter))
                {
                    Mario.Scale = Vector2.One;
                    currentLevel = level01;
                    Mario.X = 900;
                    Mario.Y = 448;
                }
                if (ks.IsKeyDown(Keys.B))
                {
                    currentLevel.Door.Scale += new Vector2(0.005f, .01f);
                    currentLevel.Door.Y--;
                }
                if (ks.IsKeyDown(Keys.V))
                {
                    currentLevel.Door.Scale -= new Vector2(0.005f, .01f);
                    currentLevel.Door.Y++;
                }
                if (restartbutton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    //restarts here
                    lives = 100;
                    currentLevel = level1;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    Mario.Scale = Vector2.One;
                    gameisover = false;
                }
                if (Mario.HitBox.Intersects(pizza.HitBox) && currentLevel == level5)
                {
                    if (IsTiny == false)
                    {
                        for (int i = 0; i < 70; i++)
                        {
                            Mario.Scale -= new Vector2(0.005f, .01f);
                        }
                        IsTiny = true;
                        currentLevel.Door.Scale = new Vector2(.25f);
                    }
                    


                }
                if (Mario.HitBox.Intersects(portal1.HitBox) && currentLevel == level5 || Mario.HitBox.Intersects(portal1.HitBox) && currentLevel == level9)
                {
                    movingTime += gameTime.ElapsedGameTime;
                    if (movingTime >= timeToMove)
                    {
                        Mario.X = portal2.X;
                        Mario.Scale -= new Vector2(0.05f, .1f);
                        Mario.Y = portal2.Y;
                        Mario.Scale += new Vector2(0.05f, .1f);
                        movingTime = TimeSpan.Zero;
                    }
                }

                if (Mario.HitBox.Intersects(portal2.HitBox) && currentLevel == level5 || Mario.HitBox.Intersects(portal2.HitBox) && currentLevel == level9)
                {
                    movingTime += gameTime.ElapsedGameTime;
                    if (movingTime >= timeToMove)
                    {
                        Mario.X = portal1.X;
                        Mario.Scale -= new Vector2(0.05f, .1f);
                        Mario.Y = portal1.Y;
                        Mario.Scale += new Vector2(0.05f, .1f);
                        movingTime = TimeSpan.Zero;
                    }
                }
                if (Mario.HitBox.Intersects(invert.HitBox) && currentLevel == level7)
                {
                    Mario.Scale -= new Vector2(1.5f, 3);
                }
                if (Mario.InvertHitBox.Intersects(uninvert.HitBox) && currentLevel == level7)
                {
                    Mario.Scale = Vector2.One;
                    Mario.Y = 300;
                    Mario.X = uninvert.X;
                }
                if (Mario.HitBox.Intersects(flower.HitBox))
                {
                        Mario.canShoot = true;
                }

                if (ks.IsKeyDown(Keys.Space) && hasfirepower == true)
                {
                    hasfirepower = false;
                }
                if (lives == 0)
                {
                    screen = 3;
                    gameisover = true;
                    
                }
                if (gameisover == true)
                {
                    screen = 3;
                    lives = 0;
                    
                }
                
                
                
                //if(penguinPosition.X => )
                //{
                //penguinPosition.X++;
                //}

                //loop through all marios fireballs and check if any of them collide with enemies
                //if collide remove both
                if (Mario.HitBox.Intersects(HealthPowerup.HitBox))
                {
                    if(MoreLives == false)
                    {
                        lives += 10;
                        MoreLives = true;
                    }
                }
                for (int i = 0; i < Mario.Fireballs.Count; i++)
                {
                    if (Mario.Fireballs[i].HitBox.Intersects(Penguin.HitBox) && canShootEnemy == true)
                    {
                        fireballhitcount++;
                        
                    }
                }
                if (fireballhitcount >= maxFireballHits)
                {
                    enemyisdead = true;
                }
                if (currentLevel == level9)
                {
                    portal1 = new Sprite(Content.Load<Texture2D>("portal"), new Vector2(700, 380), Color.White);
                    portal2 = new Sprite(Content.Load<Texture2D>("portal"), new Vector2(375, 175), Color.White);
                }
                if (ks.IsKeyDown(Keys.M))
                {
                    Mario.X = ms.X;
                    Mario.Y = ms.Y;
                }
                

                if (hasJumpBoost == false)
                {
                    Mario.jumpPower = 5;
                }
                else if (hasJumpBoost == true)
                {
                    Mario.jumpPower = 10;
                }
                if (isUnderwaterLevel == true)
                {
                    Mario.jumpPower = 1f;
                    Mario.gravity = 1f;
                    Mario.elapsedJumpTime = TimeSpan.FromMilliseconds(1);
                }

                if (MenuButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 4;
                }
                if (currentLevel == level1)
                {
                    
                }
                if (currentLevel == level2)
                {
                    
                }
                if (currentLevel == level3)
                {
                    hasJumpBoost = false;
                    Penguin.X = 500;
                    Penguin.Y = 272;
                    HealthPowerup.X = 100;
                    HealthPowerup.Y = 100;
                }
                if (currentLevel == level4)
                {
                   
                }
                if (currentLevel == level5)
                {
                    
                }
                if (currentLevel == level6)
                {
                    HealthPowerup.X = 500;
                    HealthPowerup.Y = 290;
                    
                }
                if (currentLevel == level7)
                {
                }
                if (currentLevel == level8)
                {
                }
                if (currentLevel == level9)
                {
                }
                if (currentLevel == level10)
                {
                }
                if (currentLevel == level11)
                {
                    HealthPowerup.X = 100;
                    HealthPowerup.Y = 100;
                    
                }
                if (currentLevel == level12)
                {
                //    Penguin.X = 500;
                //    Penguin.Y = 350;
                    HealthPowerup.X = 100;
                    HealthPowerup.Y = 100;
                    penguinPosition = new Vector2(500, 350);
                    
                }
                if (currentLevel == level13)
                {
                    Mario.isLevel13 = true;
                  
                }
                else
                {
                    Mario.isLevel13 = false;
                }
                if(fireballhitcount <= maxFireballHits)
                {
                    canShootEnemy = true;
                }
                else
                {
                    canShootEnemy = false;
                }


            }
            if (screen == 2)
            {
                ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(0, 420), Color.White);
                
                if (lvl1button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level1;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl1button.Update();

                if (lvl2button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level2;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl2button.Update();

                if (lvl3button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level3;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl3button.Update();
                if (lvl4button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level4;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl4button.Update();
                if (lvl5button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level5;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl5button.Update();
                if (lvl6button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level6;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl6button.Update();
                if (lvl7button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level7;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl7button.Update();
                if (lvl8button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level8;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl8button.Update();
                if (lvl9button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level9;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl9button.Update();
                if (lvl10button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level10;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl10button.Update();
                if (lvl11button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level11;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl11button.Update();
                if (lvl12button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level12;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl12button.Update();
                if (lvl13button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level13;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl13button.Update();
                if (LevelSelectButton.HitBox.Contains(ms.X, ms.Y))
                {
                    LevelSelectButton = new Button(Content.Load<Texture2D>("LevelSelectButton"), new Vector2(100, 100), Color.Black);
                }
                else
                {
                    LevelSelectButton = new Button(Content.Load<Texture2D>("LevelSelectButton"), new Vector2(100, 100), Color.White);
                }
                MenuButton.Update();
                
                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 4;
                }

            }
            if (screen == 3)
            {
                if (restartbutton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    //restarts here
                    lives = 100;
                    currentLevel = level1;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    Mario.Scale = Vector2.One;
                    gameisover = false;
                    screen = 1;
                    MoreLives = false;
                }
            }
            if (screen == 4)
            {
                ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(400, 400), Color.White);
                LevelSelectButton.Update();
                if (LevelSelectButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 7;
                }
                ShopButton.Update();
                if (ShopButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 5;
                }
                ExitButton.Update();
                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                }

            }
            if (screen == 5)
            {
                ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(400, 400), Color.White);
                if (character != "Mario")
                {
                    MarioButton = new Button(Content.Load<Texture2D>("MarioButton"), new Vector2(100, 100), Color.DarkGray);
                }
                else if (character == "Mario")
                {
                    MarioButton = new Button(Content.Load<Texture2D>("MarioButton"), new Vector2(100, 100), Color.White);
                }
                if (character != "Spongebob")
                {
                    SpongebobButton = new Button(Content.Load<Texture2D>("SpongebobButton"), new Vector2(250, 100), Color.DarkGray);
                }
                else if (character == "Spongebob")
                {
                    SpongebobButton = new Button(Content.Load<Texture2D>("SpongebobButton"), new Vector2(250, 100), Color.White);
                }
                if (character != "Patrick")
                {
                    PatrickButton = new Button(Content.Load<Texture2D>("PatrickButton"), new Vector2(400, 100), Color.DarkGray);
                }
                else if (character == "Patrick")
                {
                    PatrickButton = new Button(Content.Load<Texture2D>("PatrickButton"), new Vector2(400, 100), Color.White);
                }
                if (SpongebobButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    character = "Spongebob";
                }
                else if (MarioButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    character = "Mario";

                }
                else if (MarioButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    character = "Mario";

                }
                else if (PatrickButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    character = "Patrick";

                }

                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                }
                PlayButton.Update();
            }
            if (screen == 6)
            {
                if (PlayButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                }
                PlayButton.Update();
            }
            if (screen == 7)
            {
                if (UnderwaterLevelsButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 8;
                }
                UnderwaterLevelsButton.Update();
                if (LandLevelsButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 2;
                }
                LandLevelsButton.Update();
            }
            if (screen == 8)
            {
                

                if (lvl1button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level01;
                    fireballhitcount = 0;
                    Mario.X = 300;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl1button.Update();

                if (lvl2button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level02;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl2button.Update();
                if (lvl3button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level03;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl3button.Update();
                if (lvl4button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level04;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl4button.Update();
                if (lvl5button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level05;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl5button.Update();
                if (lvl6button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level06;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl6button.Update();
                if (lvl7button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level07;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl7button.Update();
                if (lvl8button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level08;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl8button.Update();
                if (lvl9button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level09;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl9button.Update();
                if (lvl10button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level010;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                }
                lvl10button.Update();
                if (lvl11button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level011;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl11button.Update();
                if (lvl12button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level012;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl12button.Update();
                if (lvl13button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 1;
                    currentLevel = level013;
                    fireballhitcount = 0;
                    Mario.X = currentLevel.platforms[0].X;
                    Mario.Y = currentLevel.platforms[0].Y;
                    MoreLives = false;
                }
                lvl13button.Update();
                if (LevelSelectButton.HitBox.Contains(ms.X, ms.Y))
                {
                    LevelSelectButton = new Button(Content.Load<Texture2D>("LevelSelectButton"), new Vector2(100, 100), Color.Black);
                }
                else
                {
                    LevelSelectButton = new Button(Content.Load<Texture2D>("LevelSelectButton"), new Vector2(100, 100), Color.White);
                }
                if (MenuButton.HitBox.Contains(ms.X, ms.Y))
                {
                    MenuButton = new Button(Content.Load<Texture2D>("MenuButton"), new Vector2(960, 40), Color.Black);
                }
                else
                {
                    MenuButton = new Button(Content.Load<Texture2D>("MenuButton"), new Vector2(960, 40), Color.White);
                }

                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = 4;
                }
                if (ExitButton.HitBox.Contains(ms.X, ms.Y))
                {
                    //ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(0, 420), Color.DarkGray);
                }
                else
                {
                    ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(0, 420), Color.White);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //if (screen == 0)
            //{
            //    LevelMenuBg.Draw(spriteBatch);
            //}


            if (screen == 1)
            {
                currentLevel.Draw(spriteBatch);
                //spriteBatch.Draw(bunny, new Vector2(100, 100), Color.White);
                if (gameisover == false)
                {

                    if (currentLevel == level3)
                    {
                        if (MoreLives == false)
                        {
                            HealthPowerup.Draw(spriteBatch);
                        }
                        if (Mario.canShoot == false)
                        {
                            flower.Draw(spriteBatch);
                        }
                    }
                    if (currentLevel == level3 || currentLevel == level12)
                    {
                        if (enemyisdead == false)
                        {
                            Penguin.Draw(spriteBatch);
                        }
                    }
                    if (currentLevel == level5)
                    {
                        if (IsTiny == false)
                        {
                            pizza.Draw(spriteBatch);
                        }
                        portal1.Draw(spriteBatch);
                        portal2.Draw(spriteBatch);

                    }
                    if(currentLevel == level6)
                    {
                        if (MoreLives == false)
                        {
                            HealthPowerup.Draw(spriteBatch);
                        }
                    }
                    if (currentLevel == level7)
                    {
                        invert.Draw(spriteBatch);
                        uninvert.Draw(spriteBatch);
                    }
                    if (currentLevel == level9)
                    {
                        portal1.Draw(spriteBatch);
                        portal2.Draw(spriteBatch);
                    }
                    if (currentLevel == level11)
                    {
                        if (MoreLives == false)
                        {
                            HealthPowerup.Draw(spriteBatch);
                        }
                    }
                    if (currentLevel == level12)
                    {
                        if (MoreLives == false)
                        {
                            HealthPowerup.Draw(spriteBatch);
                        }
                    }
                    if (hasJumpBoost == false && currentLevel == level2)
                    {
                        bunny.Draw(spriteBatch);
                    }

                    Mario.Draw(spriteBatch);


                }
                MenuButton.Draw(spriteBatch);
            }

            if (screen == 2 || screen == 8)
            {
                LevelMenuBg.Draw(spriteBatch);
                lvl1button.Draw(spriteBatch);
                lvl2button.Draw(spriteBatch);
                lvl3button.Draw(spriteBatch);
                lvl4button.Draw(spriteBatch);
                lvl5button.Draw(spriteBatch);
                lvl6button.Draw(spriteBatch);
                lvl7button.Draw(spriteBatch);
                lvl8button.Draw(spriteBatch);
                lvl9button.Draw(spriteBatch);
                lvl10button.Draw(spriteBatch);
                lvl11button.Draw(spriteBatch);
                lvl12button.Draw(spriteBatch);
                lvl13button.Draw(spriteBatch);
                ExitButton.Draw(spriteBatch);
            }
            if (screen == 3)
            {
                GameOverScreen.Draw(spriteBatch);
            }

            if (screen == 1 || screen == 3)
            {
                restartbutton.Draw(spriteBatch);
                spriteBatch.DrawString(font, string.Format("Lives: {0} * {1}", lives, fireballhitcount), Vector2.Zero, Color.White);
            }
            if (screen == 4)
            {
                LevelSelectButton.Draw(spriteBatch);
                ShopButton.Draw(spriteBatch);
                ExitButton.Draw(spriteBatch);
            }
            if (screen == 5)
            {
                SpongebobButton.Draw(spriteBatch);
                MarioButton.Draw(spriteBatch);
                PatrickButton.Draw(spriteBatch);
                PlayButton.Draw(spriteBatch);
            }
            if (screen == 6)
            {
                StartScreenBackground.Draw(spriteBatch);
                PlayButton.Draw(spriteBatch);
                spriteBatch.DrawString(font2, string.Format("Platformer Game"), new Vector2(275, 200), Color.White);
            }
            if (screen == 7)
            {
                UnderwaterLevelsButton.Draw(spriteBatch);
                LandLevelsButton.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}