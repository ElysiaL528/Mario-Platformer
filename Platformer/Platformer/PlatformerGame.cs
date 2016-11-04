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
    /* To do: Fix Lava Collision
     * - Fix game
            - Some buttons don't highlight
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
            - Title game (& change title screen)
            - Only be able to jump once (not in midair)

        COMPRESS CODE

            */

    public class PlatformerGame : Microsoft.Xna.Framework.Game
    {
        //Instance Variables
        #region
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
        bool setLevelMap = false;
        MouseState ms;
        KeyboardState lastKS;
        SpriteFont font;
        SpriteFont font2;
        Sprite GameOverScreen;
        Sprite LevelMenuBg;
        Dictionary<AnimationType, List<Frame>> marioAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> penguinAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> luigiAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> SpongebobAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> PatrickAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> CoinAnimations = new Dictionary<AnimationType, List<Frame>>();
        LevelMap currentMap;
        Dictionary<LevelMap, Texture2D> maps;
        Dictionary<World, List<Level>> levels;
        Dictionary<World, List<ULevels>> Ulevels;
        AnimatedSprite Penguin;
        Character MainCharacter;
        Sprite StartScreenBackground;
        Sprite flower;
        
        //bool setLevelMap = false;
        #endregion

        //Buttons
        #region 
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
        Button BackgroundButton;
        Button MenuButton;
        Button LevelSelectButton;
        Button restartbutton;
        List<Button> landLevelButtons = new List<Button>();
        List<Button> ULevelButtons = new List<Button>();
        #endregion

        //Run-time variables
        int lives = 100;
        int screen = (int)Gamescreen.UnderwaterLevelMenu;
        int fireballhitcount = 0;
        string character = "Patrick";
        string leveltype = "Land";
        int maxFireballHits = 1;
        TimeSpan timeToMove = new TimeSpan(0, 0, 0, 0, 2000);
        TimeSpan ShotDelay = TimeSpan.FromMilliseconds(300);
        TimeSpan TimeSinceLastShot = TimeSpan.Zero;
        World currentWorld = World.Land;
        int currentLevel = 0;
        int currentULevel = 0;

        public PlatformerGame()
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
            //Instance variables
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Content.Load<Texture2D>("Mario Sprite Sheet");
            penguinspritesheet = Content.Load<Texture2D>("penguin");
            PatrickSpritesheet = Content.Load<Texture2D>("PatrickSpriteSheet");
            SpongebobSpritesheet = Content.Load<Texture2D>("Spongebob_Spritesheet_2");
            font = Content.Load<SpriteFont>("SpriteFont1");
            font2 = Content.Load<SpriteFont>("Font2");

            #region Mario Animations
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

            #region Penguin Animations

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
            LuigiIdle.Add(new Frame(new Rectangle(128, 87, 30, 34), new Vector2(30 / 2, 34 / 2)));
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
            CoinFrames.Add(new Frame(new Rectangle(336, 92, 12, 16), new Vector2(12 / 2, 16 / 2)));
            CoinFrames.Add(new Frame(new Rectangle(353, 92, 2, 17), new Vector2(2 / 2, 17 / 2)));
            CoinFrames.Add(new Frame(new Rectangle(359, 92, 12, 16), new Vector2(12 / 2, 16 / 2)));
            CoinFrames.Add(new Frame(new Rectangle(374, 92, 16, 16), new Vector2(16 / 2, 16 / 2)));

            CoinAnimations.Add(AnimationType.Turning, CoinFrames);

            #endregion

            //Run-time variables
            position = new Vector2(40, 390);
            penguinPosition = new Vector2(500, 272);
            speed = new Vector2(4);
            Penguin = new AnimatedSprite(penguinspritesheet, penguinPosition, Color.White, strolling);

            //Assign main character values and fireballs
            if (!IsTiny)
            {
                MainCharacter = new Character(spriteSheet, position, PatrickAnimations, Content.Load<Texture2D>("FireBall_1"));
            }
            else
            {
                MainCharacter = new Character(spriteSheet, position, PatrickAnimations, Content.Load<Texture2D>("Copy of Fireball_1"));
            }

            //Load Starting Objects
            #region
            flower = new Sprite(Content.Load<Texture2D>("Fireball_Flower"), new Vector2(220, 250), Color.White);
            StartScreenBackground = new Sprite(Content.Load<Texture2D>("PlatformerMap1"), new Vector2(0, 0), Color.White);
            GameOverScreen = new Sprite(Content.Load<Texture2D>("GameOver"), new Vector2(0, 0), Color.White);
            LevelMenuBg = new Sprite(Content.Load<Texture2D>("LevelSelectMenu"), new Vector2(0, 0), Color.White);
            restartbutton = new Button(Content.Load<Texture2D>("RestartButton"), new Vector2(960, 0), Color.White);
            lvl1button = new Button(Content.Load<Texture2D>("lvl1button"), new Vector2(150, 150), Color.White);
            lvl1button.LevelValue = 0;
            landLevelButtons.Add(lvl1button);
            lvl2button = new Button(Content.Load<Texture2D>("lvl2button"), new Vector2(300, 150), Color.White);
            lvl2button.LevelValue = 1;
            landLevelButtons.Add(lvl2button);
            lvl3button = new Button(Content.Load<Texture2D>("lvl3button"), new Vector2(450, 150), Color.White);
            lvl3button.LevelValue = 2;
            landLevelButtons.Add(lvl3button);
            lvl4button = new Button(Content.Load<Texture2D>("lvl4button"), new Vector2(600, 150), Color.White);
            lvl4button.LevelValue = 3;
            landLevelButtons.Add(lvl4button);
            lvl5button = new Button(Content.Load<Texture2D>("lvl5button"), new Vector2(150, 210), Color.White);
            lvl5button.LevelValue = 4;
            landLevelButtons.Add(lvl5button);
            lvl6button = new Button(Content.Load<Texture2D>("lvl6button"), new Vector2(300, 210), Color.White);
            lvl6button.LevelValue = 5;
            landLevelButtons.Add(lvl6button);
            lvl7button = new Button(Content.Load<Texture2D>("lvl7button"), new Vector2(450, 210), Color.White);
            lvl7button.LevelValue = 6;
            landLevelButtons.Add(lvl7button);
            lvl8button = new Button(Content.Load<Texture2D>("lvl8button"), new Vector2(600, 210), Color.White);
            lvl8button.LevelValue = 7;
            landLevelButtons.Add(lvl8button);
            lvl9button = new Button(Content.Load<Texture2D>("lvl9button"), new Vector2(150, 270), Color.White);
            lvl9button.LevelValue = 8;
            landLevelButtons.Add(lvl9button);
            lvl10button = new Button(Content.Load<Texture2D>("lvl10button"), new Vector2(300, 270), Color.White);
            lvl10button.LevelValue = 9;
            landLevelButtons.Add(lvl10button);
            lvl11button = new Button(Content.Load<Texture2D>("lvl11button"), new Vector2(450, 270), Color.White);
            lvl11button.LevelValue = 10;
            landLevelButtons.Add(lvl11button);
            lvl12button = new Button(Content.Load<Texture2D>("lvl12button"), new Vector2(600, 270), Color.White);
            lvl12button.LevelValue = 11;
            landLevelButtons.Add(lvl12button);
            lvl13button = new Button(Content.Load<Texture2D>("lvl13button"), new Vector2(150, 330), Color.White);
            lvl13button.LevelValue = 12;
            landLevelButtons.Add(lvl13button);
            lvl01button = new Button(Content.Load<Texture2D>("lvl1button"), new Vector2(150, 150), Color.White);
            lvl01button.ULevelValue = 0;
            ULevelButtons.Add(lvl01button);
            lvl02button = new Button(Content.Load<Texture2D>("lvl2button"), new Vector2(300, 150), Color.White);
            lvl02button.ULevelValue = 1;
            ULevelButtons.Add(lvl02button);
            lvl03button = new Button(Content.Load<Texture2D>("lvl3button"), new Vector2(450, 150), Color.White);
            lvl03button.ULevelValue = 2;
            ULevelButtons.Add(lvl03button);
            lvl04button = new Button(Content.Load<Texture2D>("lvl4button"), new Vector2(600, 150), Color.White);
            lvl04button.ULevelValue = 3;
            ULevelButtons.Add(lvl04button);
            lvl05button = new Button(Content.Load<Texture2D>("lvl5button"), new Vector2(150, 210), Color.White);
            lvl05button.ULevelValue = 4;
            ULevelButtons.Add(lvl05button);
            lvl06button = new Button(Content.Load<Texture2D>("lvl6button"), new Vector2(300, 210), Color.White);
            lvl6button.ULevelValue = 5;
            ULevelButtons.Add(lvl06button);
            lvl07button = new Button(Content.Load<Texture2D>("lvl7button"), new Vector2(450, 210), Color.White);
            lvl07button.ULevelValue = 6;
            ULevelButtons.Add(lvl07button);
            lvl08button = new Button(Content.Load<Texture2D>("lvl8button"), new Vector2(600, 210), Color.White);
            lvl08button.ULevelValue = 7;
            ULevelButtons.Add(lvl08button);
            lvl09button = new Button(Content.Load<Texture2D>("lvl9button"), new Vector2(150, 270), Color.White);
            lvl09button.ULevelValue = 8;
            ULevelButtons.Add(lvl09button);
            lvl010button = new Button(Content.Load<Texture2D>("lvl10button"), new Vector2(300, 270), Color.White);
            lvl010button.ULevelValue = 9;
            ULevelButtons.Add(lvl010button);
            lvl011button = new Button(Content.Load<Texture2D>("lvl11button"), new Vector2(450, 270), Color.White);
            lvl011button.ULevelValue = 10;
            ULevelButtons.Add(lvl011button);
            lvl012button = new Button(Content.Load<Texture2D>("lvl12button"), new Vector2(600, 270), Color.White);
            lvl012button.ULevelValue = 11;
            ULevelButtons.Add(lvl012button);
            lvl013button = new Button(Content.Load<Texture2D>("lvl13button"), new Vector2(600, 330), Color.White);
            lvl013button.ULevelValue = 12;
            ULevelButtons.Add(lvl013button);
            MenuButton = new Button(Content.Load<Texture2D>("MenuButton"), new Vector2(960, 40), Color.White);
            LevelSelectButton = new Button(Content.Load<Texture2D>("LevelSelectButton"), new Vector2(100, 100), Color.White);
            ShopButton = new Button(Content.Load<Texture2D>("ShopButton"), new Vector2(400, 100), Color.White);
            ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(400, 400), Color.White);
            PlayButton = new Button(Content.Load<Texture2D>("PlayButton"), new Vector2(400, 400), Color.White);
            LandLevelsButton = new Button(Content.Load<Texture2D>("Land Levels_Button"), new Vector2(100, 100), Color.White);
            UnderwaterLevelsButton = new Button(Content.Load<Texture2D>("Underwater Levels_Button"), new Vector2(200, 100), Color.White);
            BackgroundButton = new Button(Content.Load<Texture2D>("ChooseBackgroundButton"), new Vector2(50, 300), Color.White);
            MarioButton = new Button(Content.Load<Texture2D>("MarioButton"), new Vector2(100, 100), Color.White);
            SpongebobButton = new Button(Content.Load<Texture2D>("SpongebobButton"), new Vector2(250, 100), Color.White);
            PatrickButton = new Button(Content.Load<Texture2D>("PatrickButton"), new Vector2(400, 100), Color.White);
            #endregion

            //Instance Variables
            MainCharacter.Origin = new Vector2(15, 33);
            Penguin.Origin = new Vector2(300, 300);
            Texture2D platformImage = Content.Load<Texture2D>("Platform");
            Texture2D lavaPlatformImage = Content.Load<Texture2D>("lava");
            maps = new Dictionary<LevelMap, Texture2D>();
            maps.Add(LevelMap.Stars, Content.Load<Texture2D>("PlatformerMap1"));
            maps.Add(LevelMap.Sunset, Content.Load<Texture2D>("PlatformerMap2"));
            maps.Add(LevelMap.Clouds, Content.Load<Texture2D>("PlatformerMap3"));
            maps.Add(LevelMap.Black, Content.Load<Texture2D>("PlatformerMap4"));
            maps.Add(LevelMap.Gameover, Content.Load<Texture2D>("GameOver"));
            maps.Add(LevelMap.Kelp, Content.Load<Texture2D>("UnderwaterMap"));
            levels = new Dictionary<World, List<Level>>();
            levels.Add(World.Land, new List<Level>());
            levels.Add(World.Underwater, new List<Level>());
            //Ulevels.Add(World.Underwater, new List<ULevels>());

            //Sets the level background to the map selected
            currentLevelMap = maps[currentMap];

            //If background is changed, change map
            if (setLevelMap)
            {
                currentLevelMap = maps[currentMap];
            }

            //Draws the platforms for each level. 0-1 means on land and level 1, 1-2 means underwater. First number can go from 0 to 1, second number can go from 0 to 13.
            #region level 0-0

            var level0_0Platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(0, 429)) { Size = new Vector2(59, 61) },
                new Sprite(platformImage, new Vector2(104, 349)) { Size = new Vector2(20, 20) },
                new Sprite(platformImage, new Vector2(280, 297)) { Size = new Vector2(20, 20) },
                new Sprite(platformImage, new Vector2(460, 248)) { Size =  new Vector2(20, 20) },
                new Sprite(platformImage, new Vector2(635, 207)) { Size = new Vector2(20, 20)},
                new Sprite(platformImage, new Vector2(793, 125)) {Size =  new Vector2(20, 20)}
            };

            levels[World.Land].Add(new Level(level0_0Platforms, new List<Item>(), currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion            
            #region level 0-1

            var level0_1Platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(0, 438)) { Size = new Vector2(112, 51) },
                new Sprite(platformImage, new Vector2(148, 383)) { Size = new Vector2(74, 25) },
                new Sprite(platformImage, new Vector2(262, 437)) {Size =  new Vector2(165, 52) },
                new Sprite(platformImage, new Vector2(487, 355)) { Size = new Vector2(73, 25) },
                new Sprite(platformImage, new Vector2(781, 329)) {Size =  new Vector2(74, 25) },
                new Sprite(platformImage, new Vector2(873, 439)) {Size =  new Vector2(127, 50) },
                new Sprite(platformImage, new Vector2(616, 439)) {Size =  new Vector2(146, 50) },
                new Sprite(platformImage, new Vector2(0, 400)) {Size =  new Vector2(20, 100) },
                new Sprite(platformImage, new Vector2(100, 400)) {Size =  new Vector2(20, 100) },
            };

            levels[World.Land].Add(new Level(level0_1Platforms, new List<Item>(), currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(940, 368), Color.White) { Scale = new Vector2(.75f) }));

            #endregion
            #region level 0-2

            var level0_2Platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(1, 447)) {Size =  new Vector2(101, 41)},
                new Sprite(platformImage, new Vector2(234, 445)) { Size = new Vector2(101, 41)},
                new Sprite(platformImage, new Vector2(380, 263)) {Size =  new Vector2(42, 226)},
                new Sprite(platformImage, new Vector2(593, 183)) {Size =  new Vector2(40, 306)},
                new Sprite(platformImage, new Vector2(820, 264)) {Size =  new Vector2(42, 224)},
                new Sprite(platformImage, new Vector2(921, 150)) {Size =  new Vector2(77, 22)},
                new Sprite(platformImage, new Vector2(540, 363)) {Size =  new Vector2(156, 53)}
            };

            List<Item> items0_2 = new List<Item>();
            items0_2.Add(new Item(Content.Load<Texture2D>("bunny"), new Vector2(270, 400), Color.White));

            levels[World.Land].Add(new Level(level0_2Platforms, items0_2, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-3

            var level0_3Platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(1, 465)) {Size =  new Vector2(995, 23)},
                new Sprite(platformImage, new Vector2(177, 280)) {Size =  new Vector2(614, 56)},
                new Sprite(platformImage, new Vector2(0, 159)) {Size =  new Vector2(316, 18)},
                new Sprite(platformImage, new Vector2(775, 153)) {Size =  new Vector2(221, 18)},
                new Sprite(platformImage, new Vector2(446, 82)) {Size =  new Vector2(206, 37)},
                new Sprite(platformImage, new Vector2(33, 355)) {Size =  new Vector2(48, 16)}
            };

            var items0_3 = new List<Item>();
            items0_3.Add(new Item(Content.Load<Texture2D>("Caduceus"), new Vector2(100, 100), Color.White));

            levels[World.Land].Add(new Level(level0_3Platforms, items0_3, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-4

           var level0_4Platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(163, 261)) {Size =  new Vector2(50, 225)},
                new Sprite(platformImage, new Vector2(307, 479)) {Size =  new Vector2(27, 7)},
                new Sprite(platformImage, new Vector2(307, 1)) {Size =  new Vector2(38, 341)},
                new Sprite(platformImage, new Vector2(436, 254)) {Size =  new Vector2(38, 234)},
                new Sprite(platformImage, new Vector2(550, 0)) {Size =  new Vector2(39, 325)},
                new Sprite(platformImage, new Vector2(569, 479)) {Size = new Vector2(39, 6)},
                new Sprite(platformImage, new Vector2(701, 259)) {Size =  new Vector2(38, 227)},
                new Sprite(platformImage, new Vector2(834, 173)) {Size =  new Vector2(166, 44)}
            };

            levels[World.Land].Add(new Level(level0_4Platforms, new List<Item>(), currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-5

            var level0_5platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(1, 242)) {Size =  new Vector2(858, 41)},
                new Sprite(platformImage, new Vector2(257, 297)) {Size =  new Vector2(743, 41)},
                new Sprite(platformImage, new Vector2(0, 460)) {Size =  new Vector2(133, 26)},
                new Sprite(platformImage, new Vector2(677, 465)) {Size =  new Vector2(321, 23)}
            };

            var items0_5 = new List<Item>();


            items0_5.Add(new Item(Content.Load<Texture2D>("pizza"), new Vector2(870, 210), Color.White));
            items0_5.Add(new Item(Content.Load<Texture2D>("portal"), new Vector2(700, 380), Color.White));
            items0_5.Add(new Item(Content.Load<Texture2D>("portal"), new Vector2(95, 380), Color.White));


            levels[World.Land].Add(new Level(level0_5platforms, items0_5, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 465), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-6

            var level0_6platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(1, 440)) {Size =  new Vector2(280, 45)},
                new Sprite(platformImage, new Vector2(357, 349)) {Size =  new Vector2(83, 16)},
                new Sprite(platformImage, new Vector2(358, 229)) {Size = new Vector2(83, 16)},
                new Sprite(platformImage, new Vector2(355, 142)) {Size =  new Vector2(235, 12)},
                new Sprite(platformImage, new Vector2(494, 229)) {Size =  new Vector2(88, 15)},
                new Sprite(platformImage, new Vector2(495, 350)) {Size =  new Vector2(88, 15)},
                new Sprite(platformImage, new Vector2(624, 439)) {Size =  new Vector2(374, 46)},
                new Sprite(platformImage, new Vector2(439, 141)) {Size =  new Vector2(56, 347)}
            };
            var items0_6 = new List<Item>();
            items0_6.Add(new Platformer.Item(Content.Load<Texture2D>("Caduceus"), new Vector2(500, 290), Color.White));

            levels[World.Land].Add(new Level(level0_6platforms, items0_6, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 438), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-7
            var level0_7platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(2, 186)) {Size =  new Vector2(996, 56)},
                new Sprite(platformImage, new Vector2(237, 448)) {Size =  new Vector2(487, 37)}
            };

            var items0_7 = new List<Item>();
            items0_7.Add(new Item(Content.Load<Texture2D>("invert"), new Vector2(800, 120), Color.White));
            items0_7.Add(new Item(Content.Load<Texture2D>("re-invert"), new Vector2(500, 189), Color.White));

            levels[World.Land].Add(new Level(level0_7platforms, items0_7, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(500, 447), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-8
            var level0_8platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(1, 452)) {Size =  new Vector2(163, 36)},
                new Sprite(platformImage, new Vector2(168, 334)) {Size =  new Vector2(161, 36)},
                new Sprite(platformImage, new Vector2(365, 244)) {Size =  new Vector2(161, 36)},
                new Sprite(platformImage, new Vector2(593, 177)) {Size =  new Vector2(161, 36)},
                new Sprite(platformImage, new Vector2(833, 111)) {Size =  new Vector2(161, 36)}
            };

            levels[World.Land].Add(new Level(level0_8platforms, new List<Item>(), currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(940, 110), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-9

            var level0_9platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(208, 135)) {Size =  new Vector2(27, 75)},
                new Sprite(platformImage, new Vector2(1, 2)) {Size = new Vector2(24, 488)},
                new Sprite(platformImage, new Vector2(0, 1)) {Size = new Vector2(1012, 27)},
                new Sprite(platformImage, new Vector2(302, 28)) {Size =  new Vector2(47, 285)},
                new Sprite(platformImage, new Vector2(25, 136)) {Size =  new Vector2(209, 31)},
                new Sprite(platformImage, new Vector2(0, 469)) {Size =  new Vector2(1001, 20)},
                new Sprite(platformImage, new Vector2(185, 208)) {Size =  new Vector2(65, 18)},
                new Sprite(platformImage, new Vector2(148, 285)) {Size =  new Vector2(829, 28)},
                new Sprite(platformImage, new Vector2(457, 127)) {Size = new Vector2(516, 159)},
                new Sprite(platformImage, new Vector2(973, 4)) {Size =  new Vector2(26, 484)}

            };

            var items0_9 = new List<Item>();
            items0_9.Add(new Item(Content.Load<Texture2D>("portal"), new Vector2(700, 380), Color.White));
            items0_9.Add(new Item(Content.Load<Texture2D>("portal"), new Vector2(375, 175), Color.White));


            levels[World.Land].Add(new Level(level0_9platforms, items0_9, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(910, 130), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-10
           
           var level0_10platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(0, 230)) {Size =  new Vector2(193, 98)},
                new Sprite(platformImage, new Vector2(281, 231)) {Size =  new Vector2(17, 35)},
                new Sprite(platformImage, new Vector2(387, 227)) {Size =  new Vector2(17, 35)},
                new Sprite(platformImage, new Vector2(500, 226)) {Size =  new Vector2(17, 35)},
                new Sprite(platformImage, new Vector2(611, 226)) {Size =  new Vector2(17, 35)},
                new Sprite(platformImage, new Vector2(721, 225)) {Size =  new Vector2(17, 35)},
                new Sprite(platformImage, new Vector2(814, 219)) {Size =  new Vector2(103, 37)}
            };

            levels[World.Land].Add(new Level(level0_10platforms, new List<Item>(), currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(880, 215), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-11

           var level0_11platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(205, 320)) {Size =  new Vector2(14, 14)},
                new Sprite(platformImage, new Vector2(95, 213)) {Size =  new Vector2(750, 29)},
                new Sprite(platformImage, new Vector2(311, 140)) {Size =  new Vector2(370, 345)}
            };

            var items0_11 = new List<Item>();
            items0_11.Add(new Item(Content.Load<Texture2D>("Caduceus"), new Vector2(100, 100), Color.White));

            levels[World.Land].Add(new Level(level0_11platforms, items0_11, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(700, 440), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-12

            var level0_12platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(1, 249)) {Size =  new Vector2(132, 39)},
                new Sprite(platformImage, new Vector2(159, 363)) {Size =  new Vector2(659, 52)},
                new Sprite(platformImage, new Vector2(873, 245)) {Size =  new Vector2(132, 39)}
            };


            var items0_12 = new List<Item>();
            items0_12.Add(new Item(Content.Load<Texture2D>("Caduceus"), new Vector2(100, 100), Color.White));

            levels[World.Land].Add(new Level(level0_12platforms, items0_12, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(900, 240), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 0-13

            var level0_13platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(82, 463)) {Size =  new Vector2(225, 21)},
                new Sprite(platformImage, new Vector2(176, 53)) {Size =  new Vector2(31, 431)},
                new Sprite(platformImage, new Vector2(83, 313)) {Size =  new Vector2(223, 26)},
                new Sprite(platformImage, new Vector2(85, 187)) {Size =  new Vector2(223, 26)},
                new Sprite(platformImage, new Vector2(82, 54)) {Size =  new Vector2(223, 26)},
                new Sprite(platformImage, new Vector2(610, 0)) {Size =  new Vector2(33, 388)},
                new Sprite(platformImage, new Vector2(456, 123)) {Size =  new Vector2(326, 29)},
                new Sprite(platformImage, new Vector2(528, 231)) {Size =  new Vector2(204, 37)},
                new Sprite(platformImage, new Vector2(575, 369)) {Size =  new Vector2(98, 22)},
                new Sprite(platformImage, new Vector2(789, 326)) {Size =  new Vector2(208, 22)},
                new Sprite(platformImage, new Vector2(-1, -108)) {Size =  new Vector2(491, 103)},
                new Sprite(platformImage, new Vector2(560, 467)) {Size =  new Vector2(138, 20)}
            };

            levels[World.Land].Add(new Level(level0_13platforms, new List<Item>(), currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(660, 120), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region level 1-0
           var level1_0platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(18, 473)) {Size =  new Vector2(968, 19)}
            };
            /*level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(0, 0), Color.White));
            level01platforms[1].Size = new Vector2(19, 490);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(18, 0), Color.White));
            level01platforms[2].Size = new Vector2(968, 19);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(985, 1), Color.White));
            level01platforms[3].Size = new Vector2(15, 488);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(207, 321), Color.White));
            level01platforms[4].Size = new Vector2(779, 22);
            level01platforms.Add(new Sprite(lavaPlatformImage, new Vector2(17, 157), Color.White));
            level01platforms[5].Size = new Vector2(802, 22);
            */



           var level1_0lavaplatforms = new List<Sprite>()
            {
                new Sprite(lavaPlatformImage, new Vector2(0, 0)) {Size =  new Vector2(19, 490)},
                new Sprite(lavaPlatformImage, new Vector2(18, 0)) {Size =  new Vector2(968, 19)},
                new Sprite(lavaPlatformImage, new Vector2(985, 1)) {Size =  new Vector2(15, 488)},
                new Sprite(lavaPlatformImage, new Vector2(207, 321)) {Size =  new Vector2(779, 22)},
                new Sprite(lavaPlatformImage, new Vector2(17, 157)) {Size =  new Vector2(802, 22)}
            };


            levels[World.Underwater].Add(new ULevels(level1_0platforms, level1_0lavaplatforms, new List<Item>(), maps[LevelMap.Kelp], new Sprite(Content.Load<Texture2D>("door"), new Vector2(660, 120), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));


            #endregion
            #region level 1-1
            var level1_1platforms = new List<Sprite>()
            {
                new Sprite(platformImage, new Vector2(0, 435)) { Size = new Vector2(151, 53) }
            };
            var level1_1lavaplatforms = new List<Sprite>()
            {
                new Sprite(lavaPlatformImage, new Vector2(227, 0)) { Size = new Vector2(25, 169) },
                new Sprite(lavaPlatformImage, new Vector2(227, 251)) { Size = new Vector2(25, 237) },
                new Sprite(lavaPlatformImage, new Vector2(353, 0)) { Size = new Vector2(25, 240) },
                new Sprite(lavaPlatformImage, new Vector2(354, 327)) { Size = new Vector2(25, 162) },
                new Sprite(lavaPlatformImage, new Vector2(481, 0)) { Size = new Vector2(25, 379) },
                new Sprite(lavaPlatformImage, new Vector2(483, 461)) { Size = new Vector2(25, 27) },
                new Sprite(lavaPlatformImage, new Vector2(594, 0)) { Size = new Vector2(25, 96) },
                new Sprite(lavaPlatformImage, new Vector2(596, 228)) { Size = new Vector2(26, 260) },
                new Sprite(lavaPlatformImage, new Vector2(709, 0)) { Size = new Vector2(26, 290) },
                new Sprite(lavaPlatformImage, new Vector2(709, 392)) { Size = new Vector2(25, 97) },
                new Sprite(lavaPlatformImage, new Vector2(803, 0)) { Size = new Vector2(195, 220) },
                new Sprite(lavaPlatformImage, new Vector2(803, 322)) { Size = new Vector2(196, 167) }

            };

            levels[World.Underwater].Add(new ULevels(level1_1platforms, level1_1lavaplatforms, new List<Item>(), maps[LevelMap.Kelp], new Sprite(Content.Load<Texture2D>("door"), new Vector2(950, 300), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));
            #endregion
            #region Level 1-2
            // Lava platforms
            var Level1_2lavaplatforms = new List<Sprite>()
            {
                new Sprite(lavaPlatformImage, new Vector2(0, 124)) { Size = new Vector2(795, 23) },
                new Sprite(lavaPlatformImage, new Vector2(777, 147)) { Size = new Vector2(19, 194) },
                new Sprite(lavaPlatformImage, new Vector2(265, 314)) { Size = new Vector2(531, 27) },
                new Sprite(lavaPlatformImage, new Vector2(265, 286)) { Size = new Vector2(33, 29) },
                new Sprite(lavaPlatformImage, new Vector2(265, 147)) { Size = new Vector2(33, 29) }
            };
            

           var Level1_2platforms = new List<Sprite>()
            {
                //Regular platforms || X speed = 0 || Y speed = 0
                new Sprite(platformImage, new Vector2(0, 70)) { Size = new Vector2(143, 28) },
                new Sprite(platformImage, new Vector2(635, 286)) { Size = new Vector2(143, 28) },
                // Horizontally moving platforms || X speed = 5 || Y speed = 0
                new Sprite(platformImage, new Vector2(180, 69)) { Size = new Vector2(139, 28) },
                new Sprite(platformImage, new Vector2(652, 460)) { Size = new Vector2(139, 28) },
                new Sprite(platformImage, new Vector2(125, 251)) { Size = new Vector2(139, 28) },
                // Vertically moving platforms || X speed = 0 || Y speed = 5
                new Sprite(platformImage, new Vector2(862, 83)) { Size = new Vector2(139, 28) },
                new Sprite(platformImage, new Vector2(0, 460)) { Size = new Vector2(112, 28) }
            };

            levels[World.Underwater].Add(new ULevels(Level1_2platforms, Level1_2lavaplatforms, new List<Item>(), maps[LevelMap.Kelp], new Sprite(Content.Load<Texture2D>("door"), new Vector2(950, 300), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }));


            #endregion

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState lastMs = ms;
            ms = Mouse.GetState();

            //Shortcut to Underwater Level Menu
            if (ks.IsKeyDown(Keys.L))
            {
                screen = (int)Gamescreen.UnderwaterLevelMenu;
            }

            //If we're playing the game, then...
            if (screen == (int)Gamescreen.Maingame)
            {
                MainCharacter.Update(gameTime);
                levels[currentWorld][currentLevel].Update(MainCharacter);
                MainCharacter.CheckCollision(levels[currentWorld][currentLevel].platforms);
                //MainCharacter.CheckLavaCollision(levels[World.Underwater][currentLevel]._lavaPlatform);

                //Assign character traits>
                if (character == "Mario")
                {
                    MainCharacter.Image = spriteSheet;
                    MainCharacter.Animations = marioAnimations;
                    
                }
                else if (character == "Spongebob")
                {
                    MainCharacter.Image = SpongebobSpritesheet;
                    MainCharacter.Animations = SpongebobAnimations;
                }
                else if (character == "Patrick")
                {
                    MainCharacter.Image = PatrickSpritesheet;
                    MainCharacter.Animations = PatrickAnimations;
                }


                if (currentLevelMap == maps[currentMap])
                {
                    leveltype = "Land";
                }
                //else if(currentLevelMap == maps[c])
                //{
                //    leveltype = "Underwater";
                //}
                if (leveltype == "Underwater")
                {
                    currentLevel = 0;
                }
                

                if (MainCharacter.touchedLava == true)
                {
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition - Vector2.UnitY * 50;
                    enemyisdead = false;
                    hasfirepower = false;
                    enemyisdead = false;
                    MoreLives = false;
                    lives--;
                }

                //setting your keyboardstate = what is happening with the keyboard
                if (MainCharacter.Y >= 489)
                {
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition - Vector2.UnitY * 50;
                    enemyisdead = false;
                    hasfirepower = false;
                    enemyisdead = false;
                    MoreLives = false;
                    lives--;
                }

                if (MainCharacter.HitBox.Intersects(levels[currentWorld][currentLevel].Door.HitBox) && ks.IsKeyDown(Keys.O) && currentLevel != 13)
                {
                    if (currentLevel < levels[currentWorld].Count)
                    {
                        currentLevel++;
                        fireballhitcount = 0;
                        MainCharacter.Scale = Vector2.One;
                        MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                        enemyisdead = false;
                        hasfirepower = false;
                        enemyisdead = false;
                        MoreLives = false;
                    }
                    else
                    {
                        //finished world
                        //make them go to main menu
                    }
                }


                base.Update(gameTime);

                if (ks.IsKeyDown(Keys.R))
                {
                    fireballhitcount = 0;
                    gameisover = false;
                    MainCharacter.Scale = Vector2.One;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    enemyisdead = false;
                    hasfirepower = false;
                    enemyisdead = false;
                    MoreLives = false;
                }
                /*if (ks.IsKeyDown(Keys.B))
                {
                    currentLevel.Door.Scale += new Vector2(0.005f, .01f);
                    currentLevel.Door.Y--;
                }
                if (ks.IsKeyDown(Keys.V))
                {
                    currentLevel.Door.Scale -= new Vector2(0.005f, .01f);
                    currentLevel.Door.Y++;
                }*/

                
                if (restartbutton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    //restarts here
                    lives = 100;
                    fireballhitcount = 0;
                    MainCharacter.Scale = Vector2.One;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    enemyisdead = false;
                    hasfirepower = false;
                    enemyisdead = false;
                    MoreLives = false;
                }


                if (ks.IsKeyDown(Keys.Space) && hasfirepower == true)
                {
                    hasfirepower = false;
                }
                if (lives <= 0)
                {
                    screen = (int)Gamescreen.GameOver;
                    gameisover = true;

                }




                //if(penguinPosition.X => )
                //{
                //penguinPosition.X++;
                //}

                //loop through all marios fireballs and check if any of them collide with enemies
                //if collide remove both

                for (int i = 0; i < MainCharacter.fireballs.Count; i++)
                {
                    if (MainCharacter.fireballs[i].HitBox.Intersects(Penguin.HitBox) && canShootEnemy == true)
                    {
                        fireballhitcount++;

                    }
                }
                if (fireballhitcount >= maxFireballHits)
                {
                    enemyisdead = true;
                }

                if (ks.IsKeyDown(Keys.M))
                {
                    MainCharacter.X = ms.X;
                    MainCharacter.Y = ms.Y;
                }


                if (hasJumpBoost == false)
                {
                    MainCharacter.jumpPower = 5;
                }
                else if (hasJumpBoost == true)
                {
                    MainCharacter.jumpPower = 10;
                }
                if (isUnderwaterLevel == true)
                {
                    MainCharacter.jumpPower = 1f;
                    MainCharacter.gravity = 1f;
                    MainCharacter.elapsedJumpTime = TimeSpan.FromMilliseconds(1);
                }

                if (MenuButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.MainMenu;
                }

                if (fireballhitcount <= maxFireballHits)
                {
                    canShootEnemy = true;
                }
                else
                {
                    canShootEnemy = false;
                }


            }
            if (screen == (int)Gamescreen.LandLevelMenu)
            {
                ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(0, 420), Color.White);
                currentWorld = World.Land;
               foreach(Button button in landLevelButtons)
                {
                    if(button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                    {
                        screen = (int)Gamescreen.Maingame;
                        currentLevel = button.LevelValue;
                        fireballhitcount = 0;
                        MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    }
                    button.Update();
                }
                #region old level select
                /*
                if (lvl1button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 0;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                }
                lvl1button.Update();

                if (lvl2button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 1;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                }
                lvl2button.Update();

                if (lvl3button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 2;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    MoreLives = false;
                }
                lvl3button.Update();
                if (lvl4button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 3;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                }
                lvl4button.Update();
                if (lvl5button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 4;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                }
                lvl5button.Update();
                if (lvl6button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 5;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    MoreLives = false;
                }
                lvl6button.Update();
                if (lvl7button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 6;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                }
                lvl7button.Update();
                if (lvl8button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 7;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                }
                lvl8button.Update();
                if (lvl9button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 8;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                }
                lvl9button.Update();
                if (lvl10button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 9;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                }
                lvl10button.Update();
                if (lvl11button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 10;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    MoreLives = false;
                }
                lvl11button.Update();
                if (lvl12button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 11;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    MoreLives = false;
                }
                lvl12button.Update();
                if (lvl13button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                    currentLevel = 12;
                    fireballhitcount = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
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
                */
                #endregion
                MenuButton.Update();

                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.MainMenu;
                }

            }
            if (screen == (int)Gamescreen.GameOver)
            {
                if (restartbutton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    //restarts here
                    lives = 100;
                    currentLevel = 0;
                    MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    MainCharacter.Scale = Vector2.One;
                    gameisover = false;
                    screen = (int)Gamescreen.Maingame;
                    MoreLives = false;
                }
            }
            if (screen == (int)Gamescreen.MainMenu)
            {
                ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(400, 400), Color.White);
                LevelSelectButton.Update();
                if (LevelSelectButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.KindOfLevelMenu;
                }
                ShopButton.Update();
                if (ShopButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Shop;
                }
                ExitButton.Update();
                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                }

            }
            if (screen == (int)Gamescreen.Shop)
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
                    screen = (int)Gamescreen.Maingame;
                }
                PlayButton.Update();
                if (BackgroundButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.ChooseBackgroundMenu;
                }
            }
            if (screen == (int)Gamescreen.StartScreen)
            {
                if (PlayButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.Maingame;
                }
                PlayButton.Update();
            }
            if (screen == (int)Gamescreen.KindOfLevelMenu)
            {
                if (UnderwaterLevelsButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.UnderwaterLevelMenu;
                }
                UnderwaterLevelsButton.Update();
                if (LandLevelsButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.LandLevelMenu;
                }
                LandLevelsButton.Update();
            }
            if (screen == (int)Gamescreen.UnderwaterLevelMenu)
            {
                currentWorld = World.Underwater;
                foreach(Button button in ULevelButtons)
                {
                    if (button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                    {
                        screen = (int)Gamescreen.Maingame;
                        currentLevel = button.ULevelValue;
                        fireballhitcount = 0;
                        MainCharacter.Position = levels[currentWorld][currentLevel].startPosition;
                    }
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
                    screen = (int)Gamescreen.MainMenu;
                }
            }

            if (screen == (int)Gamescreen.ChooseBackgroundMenu)
            {
                if (ks.IsKeyDown(Keys.Right) && lastKS.IsKeyUp(Keys.Right))
                {
                    if (currentMap == LevelMap.Black)
                    {
                        currentMap = LevelMap.Stars;

                    }
                    else
                    {
                        currentMap++;
                    }
                    currentLevelMap = maps[currentMap];
                    setLevelMap = true;
                }
                else if (ks.IsKeyDown(Keys.Left) && lastKS.IsKeyUp(Keys.Left))
                {
                    if (currentMap == LevelMap.Stars)
                    {
                        currentMap = LevelMap.Black;
                    }
                    else
                    {
                        currentMap--;
                    }
                    currentLevelMap = maps[currentMap];
                    setLevelMap = true;
                }
                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = (int)Gamescreen.MainMenu;
                    levels[currentWorld][currentLevel].backgroundImage = currentLevelMap;
                }

                lastKS = ks;
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


            if (screen == (int)Gamescreen.Maingame)
            {
                levels[currentWorld][currentLevel].Draw(spriteBatch);
                MainCharacter.Draw(spriteBatch);
                /*
                currentLevel.backgroundImage = maps[currentMap];
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
                    if (currentLevel == level6)
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


                }*/
                MenuButton.Draw(spriteBatch);
            }

            if (screen == (int)Gamescreen.LandLevelMenu || screen == (int)Gamescreen.UnderwaterLevelMenu)
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
            if (screen == (int)Gamescreen.GameOver)
            {
                GameOverScreen.Draw(spriteBatch);
            }

            if (screen == (int)Gamescreen.Maingame || screen == (int)Gamescreen.GameOver)
            {
                restartbutton.Draw(spriteBatch);
                spriteBatch.DrawString(font, string.Format("Lives: {0} * {1}", lives, fireballhitcount), Vector2.Zero, Color.White);
            }
            if (screen == (int)Gamescreen.MainMenu)
            {
                LevelSelectButton.Draw(spriteBatch);
                ShopButton.Draw(spriteBatch);
                ExitButton.Draw(spriteBatch);
            }
            if (screen == (int)Gamescreen.Shop)
            {
                SpongebobButton.Draw(spriteBatch);
                MarioButton.Draw(spriteBatch);
                PatrickButton.Draw(spriteBatch);
                PlayButton.Draw(spriteBatch);
                BackgroundButton.Draw(spriteBatch);
            }
            if (screen == (int)Gamescreen.StartScreen)
            {
                StartScreenBackground.Draw(spriteBatch);
                PlayButton.Draw(spriteBatch);
                spriteBatch.DrawString(font2, string.Format("Platformer Game"), new Vector2(275, 200), Color.White);
            }
            if (screen == (int)Gamescreen.KindOfLevelMenu)
            {
                UnderwaterLevelsButton.Draw(spriteBatch);
                LandLevelsButton.Draw(spriteBatch);
            }
            if (screen == (int)Gamescreen.ChooseBackgroundMenu)
            {

                spriteBatch.Draw(currentLevelMap, new Microsoft.Xna.Framework.Rectangle(0, 0, 1000, 489), Color.White);
                ExitButton.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}