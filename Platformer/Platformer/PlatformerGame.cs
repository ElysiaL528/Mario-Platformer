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
using static Platformer.AnimatedSprite;
using static Platformer.Item;

namespace Platformer
{
    /* To do: Fix the MC's intersection with the mplatforms; the MC doesn't move at the same pace as the mplatform; problem with CheckCollision function?
     * - 
            - Moving Platforms
            - Slow shift function in ULevels
            - Load & animate underwater level characters
            - Create more enemies
            - Be able to unlock maps
            - Add more platforms (i.e. trampoline, moving, disappearing, triggering)
            - Add music
            - Comment code
            - Multiplayer?
            - Make fireballs spin
            - Minigames
            - Don't be able to change speed in midair
            - Fix glitch in level 4
            - Be able to buy items in the shop
            - Disable any powerup functions every time the level resets
            - No matter what level you choose, it always goes to the third ULevel (somewhere, there's a line of code that sets the level repeatedly to ULevel 3)
            - Loop back to the title screen when all the levels are finished
            - Animate the fireballs in Mario's sprite sheet for the loading
            


            */

    public class PlatformerGame : Microsoft.Xna.Framework.Game
    {
        //Instance Variables
        #region
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;
        Texture2D enemySpriteSheet;
        Texture2D penguinSpritesheet;
        Texture2D PatrickSpritesheet;
        Texture2D SpongebobSpritesheet;
        Texture2D PrincessPSpritesheet;
        Texture2D PenguinCharacterSpritesheet;
        Texture2D currentLevelMap;
        Texture2D LandLevelMenuBackground;
        Texture2D ULevelMenuBackground;
        Texture2D TitleScreen;
        Texture2D CoinImage;
        Vector2 speed;
        Vector2 position;
        Vector2 coinPosition;
        Texture2D pixel;
        bool hasfirepower = false;
        bool IsTiny = false;
        bool gameisover = false;
        bool hasJumpBoost;
        bool MoreLives = false;
        bool isUnderwaterLevel = true;
        bool lostlife = false;
        bool canShootEnemy = false;
        bool hasDied = false;
        bool intersectingFireball = false;
        bool restarted = false;
        bool isOnMPlatform = false;
        int CollectedCoins = 0;
        int TotalCollectedCoins = 1000;
        MouseState ms;
        KeyboardState lastKS;
        SpriteFont font;
        SpriteFont font2;
        SpriteFont coinCounter;
        Sprite GameOverScreen;
        Sprite LevelMenuBg;
        Item pizza;
        Item portal1;
        Item portal2;
        Item bunnyPowerup;
        Item invert;
        Item reInvert;
        Item HealthPowerup;
        Dictionary<AnimationType, List<Frame>> marioAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> penguinAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> luigiAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> SpongebobAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> PatrickAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> CoinAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> PrincessPAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> TurtleAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<AnimationType, List<Frame>> PenguinCharAnimations = new Dictionary<AnimationType, List<Frame>>();
        Dictionary<Level, List<Item>> LevelPowerups = new Dictionary<Level, List<Item>>();
        Keys[] LevelSkipKeys = { Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0, Keys.Q, Keys.W, Keys.E, Keys.T };
        LevelMap currentMap;
        Dictionary<LevelMap, Texture2D> maps;
        Dictionary<World, List<Level>> levels;
        List<AnimatedSprite> Coins = new List<AnimatedSprite>();
        Enemy enemy;
        public static Character MainCharacter;
        AnimatedSprite PrincessP;
        AnimatedSprite FlyingTurtle;
        AnimatedSprite MarioSprite;
        Sprite StartScreenBackground;
        Sprite flower;
        MouseState lastMs;

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
        Button lvl14button;
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
        SelectedItem MarioButton;
        SelectedItem SpongebobButton;
        SelectedItem PatrickButton;
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
        #region
        int MCLives = 10000000;
        Gamescreen screen = Gamescreen.StartScreen;
        int fireballhitcount = 0;
        string character = "Patrick";
        string leveltype = "Land";
        int maxFireballHits = 5;
        TimeSpan timeToMove = new TimeSpan(0, 0, 0, 0, 2000);
        TimeSpan movingTime = new TimeSpan(0, 0, 0, 0, 2000);
        TimeSpan TimeSinceLastShot = TimeSpan.Zero;
        World currentWorld = World.Land;
        int currentLevel = 0;
        #endregion
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
        
        public void Reset()
        {
            fireballhitcount = 0;
            gameisover = false;
            MainCharacter.Scale = Vector2.One;
            MainCharacter.Position = levels[currentWorld][currentLevel].StartPosition;
            enemy.isDead = false;
            hasfirepower = false;
            enemy.isDead = false;
            MoreLives = false;
            foreach(AnimatedSprite coin in levels[currentWorld][currentLevel].CoinList)
            {
                coin.collected = false;
            }
            foreach(Item powerup in levels[currentWorld][currentLevel].Items)
            {
                powerup.isSelected = false;
            }
            if(restarted)
            {
                TotalCollectedCoins = TotalCollectedCoins - CollectedCoins;
            }
            restarted = false;
            CollectedCoins = 0;
        }

        protected override void LoadContent()
        {
            //Instance variables
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Content.Load<Texture2D>("Mario Sprite Sheet");
            penguinSpritesheet = Content.Load<Texture2D>("penguin");
            PatrickSpritesheet = Content.Load<Texture2D>("PatrickSpriteSheet");
            SpongebobSpritesheet = Content.Load<Texture2D>("Spongebob_Spritesheet_2");
            PrincessPSpritesheet = Content.Load<Texture2D>("Mario Sprite Sheet");
            PenguinCharacterSpritesheet = Content.Load<Texture2D>("Penguin_Spritesheet_1");
            font = Content.Load<SpriteFont>("SpriteFont1");
            font2 = Content.Load<SpriteFont>("Font2");
            coinCounter = Content.Load<SpriteFont>("coinCounter");
            TitleScreen = Content.Load<Texture2D>("PlatformerTitleScreen");

            #region Mario Animations
            var walking = new List<Frame>();
            
            /*walking.Add(new Frame(new Rectangle(304, 18, 32, 47), new Vector2(15, 31)));
            walking.Add(new Frame(new Rectangle(343, 16, 24, 47), new Vector2(12, 31)));
            walking.Add(new Frame(new Rectangle(371, 17, 28, 47), new Vector2(13, 31)));
            walking.Add(new Frame(new Rectangle(343, 16, 24, 47), new Vector2(12, 31)));*/

            walking.Add(new Frame(new Rectangle(304, 18, 32, 47), new Vector2(16, (float)23.5)));
            walking.Add(new Frame(new Rectangle(343, 16, 24, 47), new Vector2(12, (float)23.5)));
            walking.Add(new Frame(new Rectangle(371, 17, 28, 47), new Vector2(13, (float)23.5)));
            walking.Add(new Frame(new Rectangle(343, 16, 24, 47), new Vector2(12, (float)23.5)));
            
            marioAnimations.Add(AnimationType.Walking, walking);

            var idle = new List<Frame>();
            idle.Add(new Frame(new Rectangle(6, 16, 30, 47), new Vector2(/*13, 35*/15, (float)23.5)));
            marioAnimations.Add(AnimationType.Idle, idle);

            var jumping = new List<Frame>();
            jumping.Add(new Frame(new Rectangle(128, 18, 35, 45), new Vector2(/*18, 32*/(float)17.5, (float)22.5)));
            marioAnimations.Add(AnimationType.Jumping, jumping);

            var falling = new List<Frame>();
            falling.Add(new Frame(new Rectangle(174, 19, 34, 44), new Vector2(/*11, 31*/17, 22)));
            marioAnimations.Add(AnimationType.Falling, falling);

            var flamethrower = new List<Frame>();
            flamethrower.Add(new Frame(new Rectangle(218, 20, 33, 45), new Vector2(20, (float)22.5)));
            flamethrower.Add(new Frame(new Rectangle(257, 18, 40, 45), new Vector2(20, (float)22.5)));
            marioAnimations.Add(AnimationType.ThrowingFireball, flamethrower);

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
            SpongebobAnimations.Add(AnimationType.ThrowingFireball, SpongebobIdle);

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
            PatrickAnimations.Add(AnimationType.ThrowingFireball, PatrickIdle);

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

            #region Princess Peach Animations
            List<Frame> PrincessPFrames = new List<Frame>();
            PrincessPFrames.Add(new Frame(new Rectangle(123, 262, 36, 51), new Vector2(36 / 2, 51 / 2)));
            PrincessPFrames.Add(new Frame(new Rectangle(169, 261, 31, 52), new Vector2(31 / 2, 52 / 2)));
            PrincessPFrames.Add(new Frame(new Rectangle(204, 262, 30, 53), new Vector2(30 / 2, 53 / 2)));

            PrincessPAnimations.Add(AnimationType.Idle, PrincessPFrames);

            #endregion

            #region Flying Turtle Animations
            List<Frame> TurtleFrames = new List<Frame>();
            TurtleFrames.Add(new Frame(new Rectangle(242, 257, 35, 38), new Vector2(35 / 2, 38 / 2)));
            TurtleFrames.Add(new Frame(new Rectangle(314, 261, 30, 33), new Vector2(30 / 2, 33 / 2)));

            TurtleAnimations.Add(AnimationType.Idle, TurtleFrames);

            #endregion

            #region Penguin Character Animations

            var PenguinCharIdleFrames = new List<Frame>();

            PenguinCharIdleFrames.Add(new Frame(new Rectangle(10, 34, 43, 46), new Vector2(43 / 2, 46 / 2)));

            PenguinCharAnimations.Add(AnimationType.Idle, PenguinCharIdleFrames);
            

            var PenguinCharacterFramesWalking = new List<Frame>();
            PenguinCharacterFramesWalking.Add(new Frame(new Rectangle(10, 34, 43, 46), new Vector2(43 / 2, 46 / 2)));
            PenguinCharacterFramesWalking.Add(new Frame(new Rectangle(58, 34, 41, 46), new Vector2(41 / 2, 46 / 2)));
            PenguinCharacterFramesWalking.Add(new Frame(new Rectangle(104, 34, 40, 46), new Vector2(20, 23)));
            PenguinCharacterFramesWalking.Add(new Frame(new Rectangle(150, 34, 40, 46), new Vector2(20, 23)));

            PenguinCharAnimations.Add(AnimationType.Walking, PenguinCharacterFramesWalking);

            var PenguinCharacterFramesJumping = new List<Frame>();
            PenguinCharacterFramesJumping.Add(new Frame(new Rectangle(11, 93, 40, 47), new Vector2(40 / 2, 47 / 2)));
            PenguinCharacterFramesJumping.Add(new Frame(new Rectangle(56, 84, 45, 45), new Vector2(45 / 2, 45 / 2)));
            PenguinCharacterFramesJumping.Add(new Frame(new Rectangle(103, 88, 45, 45), new Vector2(45 / 2, 45 / 2)));

            PenguinCharAnimations.Add(AnimationType.Jumping, PenguinCharacterFramesJumping);

            var PenguinCharDucking = new List<Frame>();
            PenguinCharDucking.Add(new Frame(new Rectangle(7, 146, 44, 40), new Vector2(44 / 2, 40 / 2)));
            PenguinCharDucking.Add(new Frame(new Rectangle(61, 156, 51, 32), new Vector2(51 / 2, 32 / 2)));

            PenguinCharAnimations.Add(AnimationType.Crouching, PenguinCharDucking);
            

            #endregion

            //Run-time variables
            #region
            position = new Vector2(40, 390);
            speed = new Vector2(4);
            enemySpriteSheet = penguinSpritesheet;
            enemy = new Enemy(enemySpriteSheet, new Vector2(500, 270), penguinAnimations, Content.Load<Texture2D>("GreenFireball"), Enemy.EnemyMovement.SidetoSide, 700, 200);
            #endregion

            //Assign MC values
            MainCharacter = new Character(spriteSheet, position, PatrickAnimations, Content.Load<Texture2D>("Fireball_1"));


            PrincessP = new AnimatedSprite(PrincessPSpritesheet, new Vector2(100, 300), Color.White, PrincessPFrames);
            FlyingTurtle = new AnimatedSprite(spriteSheet, new Vector2(800, -20), Color.White, TurtleFrames);
            MarioSprite = new AnimatedSprite(spriteSheet, new Vector2(-20, 315), Color.White, walking);

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
            lvl14button = new Button(Content.Load<Texture2D>("lvl14button"), new Vector2(300, 330), Color.White);
            lvl14button.LevelValue = 13;
            landLevelButtons.Add(lvl14button);
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
            lvl013button = new Button(Content.Load<Texture2D>("lvl13button"), new Vector2(150, 330), Color.White);
            lvl013button.ULevelValue = 12;
            ULevelButtons.Add(lvl013button);
            MenuButton = new Button(Content.Load<Texture2D>("MenuButton"), new Vector2(960, 40), Color.White);
            LevelSelectButton = new Button(Content.Load<Texture2D>("LevelSelectButton"), new Vector2(100, 100), Color.White);
            ShopButton = new Button(Content.Load<Texture2D>("ShopButton"), new Vector2(400, 100), Color.White);
            ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(400, 400), Color.White);
            PlayButton = new Button(Content.Load<Texture2D>("PlayButton"), new Vector2(400, 400), Color.White);
            LandLevelsButton = new Button(Content.Load<Texture2D>("Land Levels_Button"), new Vector2(100, 100), Color.White);
            UnderwaterLevelsButton = new Button(Content.Load<Texture2D>("Underwater Levels_Button"), new Vector2(400, 100), Color.White);
            BackgroundButton = new Button(Content.Load<Texture2D>("ChooseBackgroundButton"), new Vector2(50, 300), Color.White);
            MarioButton = new SelectedItem(Content.Load<Texture2D>("MarioButton"), new Vector2(100, 100), Color.White, Content.Load<Texture2D>("Price_100"), 100, TotalCollectedCoins);
            SpongebobButton = new SelectedItem(Content.Load<Texture2D>("SpongebobButton"), new Vector2(250, 100), Color.White, Content.Load<Texture2D>("Price_100"), 100, TotalCollectedCoins);
            PatrickButton = new SelectedItem(Content.Load<Texture2D>("PatrickButton"), new Vector2(400, 100), Color.White, Content.Load<Texture2D>("Price_100"), 100, TotalCollectedCoins);
            #endregion

            //Instance Variables
            MainCharacter.Origin = new Vector2(15, 33);
            enemy.Origin = new Vector2(300, 300);
            enemy.Xspeed = 1;
            Texture2D platformImage = Content.Load<Texture2D>("Platform");
            Texture2D lavaPlatformImage = Content.Load<Texture2D>("lava");
            CoinImage = Content.Load<Texture2D>("CoinImage");
            ULevelMenuBackground = Content.Load<Texture2D>("ULevelMenu_Title");
            LandLevelMenuBackground = Content.Load<Texture2D>("LandLevelMenu_Title");

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

            pizza = new Item(Content.Load<Texture2D>("pizza"), new Vector2(870, 210), Color.White, PowerupType.Shrink);
            portal1 = new Item(Content.Load<Texture2D>("portal"), new Vector2(700, 380), Color.White, PowerupType.Portal1);
            portal2 = new Item(Content.Load<Texture2D>("portal"), new Vector2(95, 380), Color.White, PowerupType.Portal2);
            bunnyPowerup = new Item(Content.Load<Texture2D>("bunny"), new Vector2(270, 400), Color.White, PowerupType.JumpBoost);
            invert = new Item(Content.Load<Texture2D>("invert"), new Vector2(800, 120), Color.White, PowerupType.Invert);
            reInvert = new Item(Content.Load<Texture2D>("re-invert"), new Vector2(500, 189), Color.White, PowerupType.ReInvert);
            HealthPowerup = new Item(Content.Load<Texture2D>("Caduceus"), new Vector2(500, 290), Color.White, PowerupType.Health);

            SpongebobButton.isLocked = true;
            PatrickButton.isLocked = true;
            MarioButton.isLocked = false;
            MarioButton.isSelected = true;

            //Draws the platforms for each level. 0-1 means on land and level 1, 1-2 means underwater. First number can go from 0 to 1, second number can go from 0 to 13.

            #region level 0-0

            var level0_0Platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(0, 429)) { Size = new Vector2(59, 61) },
                new Platform(platformImage, new Vector2(104, 349)) { Size = new Vector2(20, 20) },
                new Platform(platformImage, new Vector2(280, 297)) { Size = new Vector2(20, 20) },
                new Platform(platformImage, new Vector2(460, 248)) { Size =  new Vector2(20, 20) },
                new Platform(platformImage, new Vector2(635, 207)) { Size = new Vector2(20, 20)},
                new Platform(platformImage, new Vector2(793, 125)) {Size =  new Vector2(20, 20)}
            };

            var lvl0coins = new List<AnimatedSprite>()
            {
                new AnimatedSprite(spriteSheet, new Vector2(113, 330), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(290, 277), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(470, 230), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(645, 190), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(800, 105), Color.White, CoinFrames)
        };
                    

            levels[World.Land].Add(new Level(level0_0Platforms, new List<Item>(), lvl0coins, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(921, 150), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            levels[currentWorld][0].hasPowerup = false;
            levels[currentWorld][0].hasMovingPlatforms = false;
            #endregion
            #region level 0-1



            var level0_1Platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(0, 438)) { Size = new Vector2(112, 51) },
                new Platform(platformImage, new Vector2(148, 383)) { Size = new Vector2(74, 25) },
                new Platform(platformImage, new Vector2(262, 437)) {Size =  new Vector2(165, 52) },
                new Platform(platformImage, new Vector2(487, 355)) { Size = new Vector2(73, 25) },
                new Platform(platformImage, new Vector2(781, 329)) {Size =  new Vector2(74, 25) },
                new Platform(platformImage, new Vector2(873, 439)) {Size =  new Vector2(127, 50) },
                new Platform(platformImage, new Vector2(616, 439)) {Size =  new Vector2(146, 50) },
                new Platform(platformImage, new Vector2(0, 400)) {Size =  new Vector2(20, 100) },
                new Platform(platformImage, new Vector2(100, 400)) {Size =  new Vector2(20, 100) },
            };

            List<AnimatedSprite> lvl1coins = new List<AnimatedSprite>()
            {
                new AnimatedSprite(spriteSheet, new Vector2(180, 363), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(500, 335), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(520, 335), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(790, 310), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(810, 310), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(830, 310), Color.White, CoinFrames)
        };

            levels[World.Land].Add(new Level(level0_1Platforms, new List<Item>(), lvl1coins, currentLevelMap, new Sprite(Content.Load<Texture2D>("door"), new Vector2(940, 368), Color.White) { Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            levels[World.Land][1].hasPowerup = false;
            levels[World.Land][1].hasMovingPlatforms = false;
            #endregion
            #region level 0-2

            var level0_2Platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(1, 447)) {Size =  new Vector2(101, 41)},
                new Platform(platformImage, new Vector2(234, 445)) { Size = new Vector2(101, 41)},
                new Platform(platformImage, new Vector2(380, 263)) {Size =  new Vector2(42, 226)},
                new Platform(platformImage, new Vector2(593, 183)) {Size =  new Vector2(40, 306)},
                new Platform(platformImage, new Vector2(820, 264)) {Size =  new Vector2(42, 224)},
                new Platform(platformImage, new Vector2(921, 150)) {Size =  new Vector2(77, 22)},
                new Platform(platformImage, new Vector2(540, 363)) {Size =  new Vector2(156, 53)}
            };

            bunnyPowerup.Position = new Vector2(270, 400);

            List<Item> items0_2 = new List<Item>();
            items0_2.Add(bunnyPowerup);

            List<AnimatedSprite> lvl2coins = new List<AnimatedSprite>()
            {
                new AnimatedSprite(spriteSheet, new Vector2(565, 350), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(670, 350), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(840, 245), Color.White, CoinFrames),
            };

            enemy.isDead = false;

            levels[World.Land].Add(new Level(level0_2Platforms, items0_2, lvl2coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(921, 150)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            LevelPowerups.Add(levels[World.Land][2], items0_2);
            levels[World.Land][2].hasPowerup = true;
            levels[World.Land][2].hasMovingPlatforms = false;
            #endregion
            #region level 0-3

            var level0_3Platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(1, 465)) {Size =  new Vector2(995, 23)},
                new Platform(platformImage, new Vector2(177, 280)) {Size =  new Vector2(614, 56)},
                new Platform(platformImage, new Vector2(0, 159)) {Size =  new Vector2(316, 18)},
                new Platform(platformImage, new Vector2(775, 153)) {Size =  new Vector2(221, 18)},
                new Platform(platformImage, new Vector2(446, 82)) {Size =  new Vector2(206, 37)},
                new Platform(platformImage, new Vector2(33, 355)) {Size =  new Vector2(48, 16)}
            };

            HealthPowerup.Position = new Vector2(100, 100);


            var lvl3coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(550, 30), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(550, 140), Color.White, CoinFrames)
                };
            var items0_3 = new List<Item>();
            enemy.Position = new Vector2(500, 270);
            items0_3.Add(HealthPowerup);
            levels[World.Land].Add(new Level(level0_3Platforms, items0_3, lvl3coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(921, 150)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            LevelPowerups.Add(levels[World.Land][3], items0_3);
            levels[World.Land][3].hasPowerup = true;
            levels[World.Land][3].hasMovingPlatforms = false;
            #endregion
            #region level 0-4

            var level0_4Platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(163, 261)) {Size =  new Vector2(50, 225)},
                new Platform(platformImage, new Vector2(307, 479)) {Size =  new Vector2(27, 7)},
                new Platform(platformImage, new Vector2(307, 1)) {Size =  new Vector2(38, 341)},
                new Platform(platformImage, new Vector2(436, 254)) {Size =  new Vector2(38, 234)},
                new Platform(platformImage, new Vector2(550, 0)) {Size =  new Vector2(39, 325)},
                new Platform(platformImage, new Vector2(569, 479)) {Size = new Vector2(39, 6)},
                new Platform(platformImage, new Vector2(701, 259)) {Size =  new Vector2(38, 227)},
                new Platform(platformImage, new Vector2(834, 173)) {Size =  new Vector2(166, 44)}
            };

            var lvl4coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(320, 460), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(590, 460), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(455, 235), Color.White, CoinFrames)
                };

            levels[World.Land].Add(new Level(level0_4Platforms, new List<Item>(), lvl4coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(921, 150)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            levels[World.Land][4].hasPowerup = false;
            levels[World.Land][4].hasMovingPlatforms = false;
            #endregion
            #region level 0-5

            var level0_5platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(1, 242)) {Size =  new Vector2(858, 41)},
                new Platform(platformImage, new Vector2(257, 297)) {Size =  new Vector2(743, 41)},
                new Platform(platformImage, new Vector2(0, 460)) {Size =  new Vector2(133, 26)},
                new Platform(platformImage, new Vector2(677, 465)) {Size =  new Vector2(321, 23)}
            };

            pizza.Position = new Vector2(870, 210);
            portal1.Position = new Vector2(700, 380);
            portal2.Position = new Vector2(95, 380);

            var items0_5 = new List<Item>();

            items0_5.Add(pizza);
            items0_5.Add(portal1);
            items0_5.Add(portal2);

            var lvl5coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(200, 150), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(400, 150), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(600, 150), Color.White, CoinFrames)
                };

            levels[World.Land].Add(new Level(level0_5platforms, items0_5, lvl5coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(921, 465)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            LevelPowerups.Add(levels[World.Land][5], items0_5);
            levels[World.Land][5].hasPowerup = true;
            levels[World.Land][5].hasPowerup = false;

            #endregion
            #region level 0-6

            var level0_6platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(1, 440)) {Size =  new Vector2(280, 45)},
                new Platform(platformImage, new Vector2(357, 349)) {Size =  new Vector2(235, 16)},
                new Platform(platformImage, new Vector2(358, 229)) {Size = new Vector2(235, 16)},
                new Platform(platformImage, new Vector2(355, 142)) {Size =  new Vector2(235, 12)},
                new Platform(platformImage, new Vector2(624, 439)) {Size =  new Vector2(374, 46)},
                new Platform(platformImage, new Vector2(439, 141)) {Size =  new Vector2(56, 347)}
            };

            HealthPowerup.Position = new Vector2(500, 290);
            var items0_6 = new List<Item>();
            items0_6.Add(HealthPowerup);

            var lvl6coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(400, 330), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(400, 210), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(470, 120), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(550, 210), Color.White, CoinFrames)
                };

            levels[World.Land].Add(new Level(level0_6platforms, items0_6, lvl6coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(921, 438)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            LevelPowerups.Add(levels[World.Land][6], items0_6);
            levels[World.Land][6].hasPowerup = true;
            levels[World.Land][6].hasMovingPlatforms = false;
            #endregion
            #region level 0-7
            var level0_7platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(2, 186)) {Size =  new Vector2(996, 56)},
                new Platform(platformImage, new Vector2(237, 448)) {Size =  new Vector2(487, 37)}
            };

            invert.Position = new Vector2(800, 120);
            reInvert.Position = new Vector2(500, 190);
            var items0_7 = new List<Item>();
            items0_7.Add(invert);
            items0_7.Add(reInvert);

            var lvl7coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(300, 300), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(300, 350), Color.White, CoinFrames)
                };

            levels[World.Land].Add(new Level(level0_7platforms, items0_7, lvl7coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(500, 447)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            LevelPowerups.Add(levels[World.Land][7], items0_7);
            levels[World.Land][7].hasPowerup = true;
            levels[World.Land][7].hasMovingPlatforms = false;
            #endregion
            #region level 0-8
            var level0_8platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(1, 452)) {Size =  new Vector2(163, 36)},
                new Platform(platformImage, new Vector2(168, 334)) {Size =  new Vector2(161, 36)},
                new Platform(platformImage, new Vector2(365, 244)) {Size =  new Vector2(161, 36)},
                new Platform(platformImage, new Vector2(593, 177)) {Size =  new Vector2(161, 36)},
                new Platform(platformImage, new Vector2(833, 111)) {Size =  new Vector2(161, 36)}
            };

            var lvl8coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(300, 320), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(500, 220), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(750, 150), Color.White, CoinFrames)
                };
            levels[World.Land].Add(new Level(level0_8platforms, new List<Item>(), lvl8coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(940, 110)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            levels[World.Land][8].hasPowerup = false;
            levels[World.Land][8].hasMovingPlatforms = false;
            #endregion
            #region level 0-9

            var level0_9platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(208, 135)) {Size =  new Vector2(27, 75)},
                new Platform(platformImage, new Vector2(1, 2)) {Size = new Vector2(24, 488)},
                new Platform(platformImage, new Vector2(0, 1)) {Size = new Vector2(1012, 27)},
                new Platform(platformImage, new Vector2(302, 28)) {Size =  new Vector2(47, 285)},
                new Platform(platformImage, new Vector2(25, 136)) {Size =  new Vector2(209, 31)},
                new Platform(platformImage, new Vector2(0, 469)) {Size =  new Vector2(1001, 20)},
                new Platform(platformImage, new Vector2(185, 208)) {Size =  new Vector2(65, 18)},
                new Platform(platformImage, new Vector2(148, 285)) {Size =  new Vector2(829, 28)},
                new Platform(platformImage, new Vector2(457, 127)) {Size = new Vector2(516, 159)},
                new Platform(platformImage, new Vector2(973, 4)) {Size =  new Vector2(26, 484)}

            };

            portal1.Position = new Vector2(700, 380);
            portal2.Position = new Vector2(375, 175);

            var items0_9 = new List<Item>();
            items0_9.Add(portal1);
            items0_9.Add(portal2);

            var lvl9coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(470, 100), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(550, 100), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(630, 100), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(710, 100), Color.White, CoinFrames)
                };


            levels[World.Land].Add(new Level(level0_9platforms, items0_9, lvl9coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(910, 130)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            LevelPowerups.Add(levels[World.Land][9], items0_9);
            levels[World.Land][9].hasPowerup = true;
            levels[World.Land][9].hasMovingPlatforms = false;
            #endregion
            #region level 0-10

            var level0_10platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(0, 230)) {Size =  new Vector2(193, 98)},
                new Platform(platformImage, new Vector2(281, 231)) {Size =  new Vector2(17, 35)},
                new Platform(platformImage, new Vector2(387, 227)) {Size =  new Vector2(17, 35)},
                new Platform(platformImage, new Vector2(500, 226)) {Size =  new Vector2(17, 35)},
                new Platform(platformImage, new Vector2(611, 226)) {Size =  new Vector2(17, 35)},
                new Platform(platformImage, new Vector2(721, 225)) {Size =  new Vector2(17, 35)},
                new Platform(platformImage, new Vector2(814, 219)) {Size =  new Vector2(103, 37)}
            };

            var lvl10coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(290, 210), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(397, 210), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(510, 210), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(620, 210), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(730, 210), Color.White, CoinFrames),

                };

            levels[World.Land].Add(new Level(level0_10platforms, new List<Item>(), lvl10coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(880, 215)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            levels[World.Land][10].hasPowerup = false;
            levels[World.Land][10].hasMovingPlatforms = false;
            #endregion
            #region level 0-11

            var level0_11platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(205, 320)) {Size =  new Vector2(14, 14)},
                new Platform(platformImage, new Vector2(95, 213)) {Size =  new Vector2(750, 29)},
                new Platform(platformImage, new Vector2(311, 140)) {Size =  new Vector2(370, 345)}
            };

            //HealthPowerup = new Item(Content.Load<Texture2D>("Caduceus"), new Vector2(100, 100), Color.White));

            HealthPowerup.Position = new Vector2(100, 100);

            var items0_11 = new List<Item>();
            items0_11.Add(HealthPowerup);

            var lvl11coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(750, 400), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(800, 350), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(850, 300), Color.White, CoinFrames)
                };

            levels[World.Land].Add(new Level(level0_11platforms, items0_11, lvl11coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(700, 440)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            LevelPowerups.Add(levels[World.Land][11], items0_11);
            levels[World.Land][11].hasPowerup = true;
            levels[World.Land][11].hasMovingPlatforms= false;
            #endregion
            #region level 0-12

            var level0_12platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(1, 249)) {Size =  new Vector2(132, 39)},
                new Platform(platformImage, new Vector2(159, 363)) {Size =  new Vector2(659, 52)},
                new Platform(platformImage, new Vector2(873, 245)) {Size =  new Vector2(132, 39)}
            };

            enemy.Position = new Vector2(500, 350);
            HealthPowerup.Position = new Vector2(100, 100);
            var items0_12 = new List<Item>();
            items0_12.Add(HealthPowerup);

            var lvl12coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(180, 340), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(300, 340), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(420, 340), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(540, 340), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(660, 340), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(780, 340), Color.White, CoinFrames)
                };

            levels[World.Land].Add(new Level(level0_12platforms, items0_12, lvl12coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(900, 240)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            LevelPowerups.Add(levels[World.Land][12], items0_12);
            levels[World.Land][12].hasPowerup = true;
            #endregion
            #region level 0-13

            var level0_13platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(82, 463)) {Size =  new Vector2(225, 21)},
                new Platform(platformImage, new Vector2(176, 53)) {Size =  new Vector2(31, 431)},
                new Platform(platformImage, new Vector2(83, 313)) {Size =  new Vector2(223, 26)},
                new Platform(platformImage, new Vector2(85, 187)) {Size =  new Vector2(223, 26)},
                new Platform(platformImage, new Vector2(82, 54)) {Size =  new Vector2(223, 26)},
                new Platform(platformImage, new Vector2(610, 0)) {Size =  new Vector2(33, 388)},
                new Platform(platformImage, new Vector2(456, 123)) {Size =  new Vector2(326, 29)},
                new Platform(platformImage, new Vector2(528, 231)) {Size =  new Vector2(204, 37)},
                new Platform(platformImage, new Vector2(575, 369)) {Size =  new Vector2(98, 22)},
                new Platform(platformImage, new Vector2(789, 326)) {Size =  new Vector2(208, 22)},
                new Platform(platformImage, new Vector2(-1, -108)) {Size =  new Vector2(491, 103)},
                new Platform(platformImage, new Vector2(560, 467)) {Size =  new Vector2(138, 20)}
            };

            var lvl13coins = new List<AnimatedSprite> {
                new AnimatedSprite(spriteSheet, new Vector2(100, 290), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(100, 160), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(280, 160), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(280, 290), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(630, 445), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(750, 110), Color.White, CoinFrames),
                new AnimatedSprite(spriteSheet, new Vector2(700, 219), Color.White, CoinFrames)
                };

            levels[World.Land].Add(new Level(level0_13platforms, new List<Item>(), lvl13coins, currentLevelMap, new Platform(Content.Load<Texture2D>("door"), new Vector2(660, 120)) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            levels[currentWorld][13].hasPowerup = false;
            #endregion
            #region level 1-0
            var level1_0platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(18, 473)) {Size =  new Vector2(968, 19)}
            };

            var level1_0lavaplatforms = new List<Platform>()
            {
                new Platform(lavaPlatformImage, new Vector2(0, 0), true) {Size =  new Vector2(19, 490)},
                new Platform(lavaPlatformImage, new Vector2(18, 0), true) {Size =  new Vector2(968, 19)},
                new Platform(lavaPlatformImage, new Vector2(985, 1), true) {Size =  new Vector2(15, 488)},
                new Platform(lavaPlatformImage, new Vector2(207, 321), true) {Size =  new Vector2(779, 22)},
                new Platform(lavaPlatformImage, new Vector2(17, 157), true) {Size =  new Vector2(802, 22)}
            };
            level1_0platforms.AddRange(level1_0lavaplatforms);

            levels[World.Underwater].Add(new Level(level1_0platforms, new List<Item>(), new List<AnimatedSprite>(), maps[LevelMap.Kelp], new Sprite(Content.Load<Texture2D>("door"), new Vector2(660, 120), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));


            #endregion
            #region level 1-1
            var level1_1platforms = new List<Platform>()
            {
                new Platform(platformImage, new Vector2(0, 435)) { Size = new Vector2(151, 53) }
            };
            var level1_1lavaplatforms = new List<Platform>()
            {
                new Platform(lavaPlatformImage, new Vector2(227, 0), true) { Size = new Vector2(25, 160) },
                new Platform(lavaPlatformImage, new Vector2(227, 251), true) { Size = new Vector2(25, 237) },
                new Platform(lavaPlatformImage, new Vector2(353, 0), true) { Size = new Vector2(25, 240) },
                new Platform(lavaPlatformImage, new Vector2(354, 327), true) { Size = new Vector2(25, 162) },
                new Platform(lavaPlatformImage, new Vector2(481, 0), true) { Size = new Vector2(25, 370) },
                new Platform(lavaPlatformImage, new Vector2(483, 461), true) { Size = new Vector2(25, 27) },
                new Platform(lavaPlatformImage, new Vector2(594, 0), true) { Size = new Vector2(25, 96) },
                new Platform(lavaPlatformImage, new Vector2(596, 228), true) { Size = new Vector2(26, 260) },
                new Platform(lavaPlatformImage, new Vector2(709, 0), true) { Size = new Vector2(26, 290) },
                new Platform(lavaPlatformImage, new Vector2(709, 392), true) { Size = new Vector2(25, 97) },
                new Platform(lavaPlatformImage, new Vector2(803, 0), true) { Size = new Vector2(195, 220) },
                new Platform(lavaPlatformImage, new Vector2(803, 322), true) { Size = new Vector2(196, 167) }
            };
            level1_1platforms.AddRange(level1_1lavaplatforms);

            levels[World.Underwater].Add(new Level(level1_1platforms, new List<Item>(), new List<AnimatedSprite>(), maps[LevelMap.Kelp], new Sprite(Content.Load<Texture2D>("door"), new Vector2(950, 300), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, new List<MovingPlatform>()));
            #endregion
            #region Level 1-2
            // Lava platforms
            var Level1_2lavaplatforms = new List<Platform>()
            {
                new Platform(lavaPlatformImage, new Vector2(0, 124), true) { Size = new Vector2(795, 23) },
                new Platform(lavaPlatformImage, new Vector2(777, 147), true) { Size = new Vector2(19, 194) },
                new Platform(lavaPlatformImage, new Vector2(265, 314), true) { Size = new Vector2(531, 27) },
                new Platform(lavaPlatformImage, new Vector2(265, 286), true) { Size = new Vector2(33, 29) },
                new Platform(lavaPlatformImage, new Vector2(265, 147), true) { Size = new Vector2(33, 29) }
            };


            var Level1_2platforms = new List<Platform>()
            {
                //Regular platforms || X speed = 0 || Y speed = 0
                new Platform(platformImage, new Vector2(0, 70), false) { Size = new Vector2(143, 28) },
                new Platform(platformImage, new Vector2(635, 286), false) { Size = new Vector2(143, 28) },
                // Horizontally moving platforms || X speed = 5 || Y speed = 0
                new Platform(platformImage, new Vector2(180, 69), false) { Size = new Vector2(139, 28) }, 
                new Platform(platformImage, new Vector2(652, 460), false) { Size = new Vector2(139, 28) },
                new Platform(platformImage, new Vector2(125, 251), false) { Size = new Vector2(139, 28) },
                // Vertically moving platforms || X speed = 0 || Y speed = 5
                new Platform(platformImage, new Vector2(862, 83), false) { Size = new Vector2(139, 28) },
                new Platform(platformImage, new Vector2(0, 460), false) { Size = new Vector2(112, 28) }
            };
            Level1_2platforms.AddRange(Level1_2lavaplatforms);

            var Level1_2MovingPlatforms = new List<MovingPlatform>()
            {
                new MovingPlatform(platformImage, new Vector2(0, 0), MovingPlatform.PlatformMovement.Horizontal, 1, 900, 100),
                new MovingPlatform(platformImage, new Vector2(180, 69), MovingPlatform.PlatformMovement.Horizontal, 1, 900, 100),
                new MovingPlatform(platformImage, new Vector2(652, 460), MovingPlatform.PlatformMovement.Horizontal, 1, 900, 100),
                new MovingPlatform(platformImage, new Vector2(125, 251), MovingPlatform.PlatformMovement.Horizontal, 1, 900, 100),
                new MovingPlatform(platformImage, new Vector2(862, 83), MovingPlatform.PlatformMovement.Vertical, 1, 400, 10),
                new MovingPlatform(platformImage, new Vector2(0, 460), MovingPlatform.PlatformMovement.Vertical, 1, 400, 10)
            };
            

            levels[World.Underwater].Add(new Level(Level1_2platforms, new List<Item>(), new List<AnimatedSprite>(), maps[LevelMap.Kelp], new Sprite(Content.Load<Texture2D>("door"), new Vector2(700, 280), Color.White) { Origin = new Vector2(0, Content.Load<Texture2D>("door").Height), Scale = new Vector2(.75f) }, Level1_2MovingPlatforms));
            levels[World.Underwater][2].hasMovingPlatforms = true;

            #endregion

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            List<List<Item>> allItems = new List<List<Item>>();
            allItems.Add(items0_2);
            allItems.Add(items0_3);
            allItems.Add(items0_5);
            allItems.Add(items0_6);
            allItems.Add(items0_7);
            allItems.Add(items0_9);
            allItems.Add(items0_11);
            allItems.Add(items0_12);
        }

        protected override void UnloadContent()
        {

        }

        TimeSpan ElapsedTurtleDescending = TimeSpan.Zero;
        TimeSpan TurtleDescending = TimeSpan.FromMilliseconds(5600);
        
        protected override void Update(GameTime gameTime)
        {
            /*currentWorld = World.Underwater;
            currentLevel = 2;*/
            KeyboardState ks = Keyboard.GetState();

            ms = Mouse.GetState();
            ElapsedTurtleDescending += gameTime.ElapsedGameTime;

            //Shortcut to Underwater Level Menu
            if (ks.IsKeyDown(Keys.L))
            {
                screen = Gamescreen.UnderwaterLevelMenu;
            }

            SpongebobButton.CollectedCoins = TotalCollectedCoins;
            if(screen == Gamescreen.StartScreen)
            {
                FlyingTurtle.UpdateAnimation(gameTime);
                PrincessP.UpdateAnimation(gameTime);
                MarioSprite.UpdateAnimation(gameTime);
                

                if (PrincessP.X <= 800)
                {
                    PrincessP.X += 3;
                }
                else
                {
                    if(ElapsedTurtleDescending <= TurtleDescending)
                    {
                        FlyingTurtle.Y += 3;
                    }
                    else
                    {
                        if(PrincessP.Y >= -100)
                        {
                            FlyingTurtle.Y -= 3;
                            PrincessP.Y -= 3;
                        }
                        else
                        {
                           if(MarioSprite.X <= 1500)
                            {
                                MarioSprite.X += 4;
                            }
                        }
                    }
                }

            }

            //If we're playing the game, then...
            if (screen == Gamescreen.Maingame)
            {
                MainCharacter.UpdateAnimation(gameTime);

                    enemy.UpdateAnimation(gameTime);
                    enemy.EnemyUpdate(gameTime);

                //MainCharacter.CheckCollision(levels[currentWorld][currentLevel].Platforms);

                MainCharacter.isGrounded = false;
                MainCharacter.canWalkLeft = true;
                MainCharacter.canWalkRight = true;
                MainCharacter.canGoUp = true;

                foreach (Platform platform in levels[currentWorld][currentLevel].Platforms)
                {
                    platform.CheckCollision();
                }

                foreach (MovingPlatform mplatform in levels[currentWorld][currentLevel].mPlatforms)
                {
                    mplatform.CheckCollision();
                }
                
                foreach (AnimatedSprite coin in levels[currentWorld][currentLevel].CoinList)
                {
                    coin.UpdateAnimation(gameTime);
                    if(MainCharacter.HitBox.Intersects(coin.HitBox))
                    {
                        if (!coin.collected)
                        {
                            coin.collected = true;
                            CollectedCoins += 10;
                            TotalCollectedCoins += 10;
                        }
                    }
                }

                //Assign character traits>

                switch (MainCharacter.currentCharacterName)
                {
                    case Character.CharacterName.Mario:
                        MainCharacter.Image = spriteSheet;
                        MainCharacter.Animations = marioAnimations;
                        break;

                    case Character.CharacterName.Spongebob:
                        MainCharacter.Image = SpongebobSpritesheet;
                        MainCharacter.Animations = SpongebobAnimations;
                        break;

                    case Character.CharacterName.Patrick:
                        MainCharacter.Image = PatrickSpritesheet;
                        MainCharacter.Animations = PatrickAnimations;
                        break;
                }

                //Set start positions
                #region
                levels[World.Land][0].StartPosition = new Vector2(27, 404);
                levels[World.Land][1].StartPosition = new Vector2(50, 413);
                levels[World.Land][2].StartPosition = new Vector2(45, 422);
                levels[World.Land][3].StartPosition = new Vector2(51, 440);
                levels[World.Land][4].StartPosition = new Vector2(180, 236);
                levels[World.Land][5].StartPosition = new Vector2(26, 217);
                levels[World.Land][6].StartPosition = new Vector2(44, 415);
                levels[World.Land][7].StartPosition = new Vector2(32, 161);
                levels[World.Land][8].StartPosition = new Vector2(19, 427);
                levels[World.Land][9].StartPosition = new Vector2(95, 111);
                levels[World.Land][10].StartPosition = new Vector2(29, 205);
                levels[World.Land][11].StartPosition = new Vector2(209, 295);
                levels[World.Land][12].StartPosition = new Vector2(30, 224);
                levels[World.Land][13].StartPosition = new Vector2(111, 438);

                levels[World.Underwater][0].StartPosition = new Vector2(913, 448);
                levels[World.Underwater][1].StartPosition = new Vector2(27, 410);
                levels[World.Underwater][2].StartPosition = new Vector2(34, 45);
                #endregion


                for (int i = 0; i <= 13 /*number of levels - 1*/; i++)
                {
                   if(ks.IsKeyDown(LevelSkipKeys[i]))
                    {
                        currentLevel = i;
                        Reset();
                    }
                }


                if (currentLevelMap == maps[currentMap])
                {
                    leveltype = "Land";
                }
                
                if (currentWorld == World.Underwater)
                {
                    isUnderwaterLevel = true;
                }
                else if (currentWorld == World.Land)
                {
                    isUnderwaterLevel = false;
                }

                if (MainCharacter.isHealthPowerup)
                {
                    MCLives += 10;
                    MainCharacter.isHealthPowerup = false;
                }

                if (MainCharacter.isJumpBoostPowerup)
                {
                    hasJumpBoost = true;
                }

                if (MainCharacter.isPortal1Powerup)
                {
                    movingTime += gameTime.ElapsedGameTime;
                    if (movingTime >= timeToMove)
                    {
                        MainCharacter.X = portal2.X;
                        MainCharacter.Y = portal2.Y;
                        movingTime = TimeSpan.Zero;   
                    }
                    MainCharacter.isPortal1Powerup = false;
                }
                if (MainCharacter.isPortal2Powerup)
                {
                    movingTime += gameTime.ElapsedGameTime;
                    if (movingTime >= timeToMove)
                    {
                        MainCharacter.X = portal1.X;
                        MainCharacter.Y = portal1.Y;
                        movingTime = TimeSpan.Zero;
                    }
                    MainCharacter.isPortal2Powerup = false;
                }


                

                if (MainCharacter.Y >= 489 || MainCharacter.Died)
                {
                    MainCharacter.Position = levels[currentWorld][currentLevel].StartPosition;
                    enemy.isDead = false;
                    hasfirepower = false;
                    enemy.isDead = false;
                    restarted = true;
                    Reset();
                    MCLives--;
                }

                if (MainCharacter.HitBox.Intersects(levels[currentWorld][currentLevel].Door.HitBox))
                {
                    if (currentLevel < levels[currentWorld].Count)
                    {
                        currentLevel++;
                        Reset();
                        //Have all the level's item return to non-selected status
                        if (levels[currentWorld][currentLevel].hasPowerup)
                        {
                            foreach (Item powerup in LevelPowerups[levels[currentWorld][currentLevel]])
                            {
                                powerup.isSelected = false;
                            }
                        }
                        if (currentWorld != World.Underwater)
                        {
                            levels[currentWorld][currentLevel].BackgroundImage = currentLevelMap;
                        }
                        else
                        {
                            levels[World.Underwater][currentLevel].BackgroundImage = maps[LevelMap.Kelp];
                        }
                    }
                    else
                    {
                        screen = Gamescreen.KindOfLevelMenu;
                    }
                }


                base.Update(gameTime);

                if (ks.IsKeyDown(Keys.R))
                {
                    Reset();
                    restarted = true;
                }
            
                if (restartbutton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    Reset();
                    restarted = true;

                    if (levels[currentWorld][currentLevel].hasPowerup)
                    {
                        foreach (Item powerup in LevelPowerups[levels[currentWorld][currentLevel]])
                        {
                                powerup.isSelected = false;
                            
                        }
                    }
                }


                if (ks.IsKeyDown(Keys.Space) && hasfirepower == true)
                {
                    hasfirepower = false;
                }
                if (MCLives <= 0)
                {
                    screen = Gamescreen.GameOver;
                    gameisover = true;

                }




                //if(penguinPosition.X => )
                //{
                //penguinPosition.X++;
                //}

                //loop through all marios fireballs and check if any of them collide with enemies
                //if collide remove both

                if (levels[currentWorld][currentLevel] == levels[World.Land][3] || levels[currentWorld][currentLevel] == levels[World.Land][12])
                {
                    for (int i = 0; i < MainCharacter.fireballs.Count; i++)
                    {
                        MainCharacter.fireballs[i].UpdateHitbox(MainCharacter.fireballs[i].HitBox);
                        if (MainCharacter.fireballs[i].HitBox.Intersects(enemy.HitBox) && canShootEnemy == true)
                        {
                            fireballhitcount++;
                            MainCharacter.fireballs.Remove(MainCharacter.fireballs[i]);
                        }
                    }
                    for (int i = 0; i < enemy.fireballs.Count; i++)
                    {
                        enemy.fireballs[i].UpdateHitbox(enemy.fireballs[i].HitBox);
                        if (enemy.fireballs[i].HitBox.Intersects(MainCharacter.HitBox))
                        {
                            MCLives--;
                            enemy.fireballs.Remove(enemy.fireballs[i]);
                        }

                    }
                    if(MainCharacter.HitBox.Intersects(enemy.HitBox))
                    {
                        enemy.isDead = true;
                    }
                }
                if (fireballhitcount >= maxFireballHits)
                {
                    enemy.isDead = true;
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
                    screen = Gamescreen.MainMenu;
                }

                if (fireballhitcount <= maxFireballHits)
                {
                    canShootEnemy = true;
                }
                else
                {
                    canShootEnemy = false;
                }

                if (currentWorld == World.Land)
                {
                    switch (currentLevel)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            enemy.Y = 270;
                            break;
                        case 4:
                            HealthPowerup.Position = new Vector2(100, 100);
                            break;
                        case 5:
                            portal1.Position = new Vector2(700, 380);
                            portal2.Position = new Vector2(95, 380);
                            break;
                        case 6:
                            HealthPowerup.Position = new Vector2(500, 290);
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            portal1.Position = new Vector2(700, 380);
                            portal2.Position = new Vector2(375, 175);
                            break;
                        case 10:
                            break;
                        case 11:
                            HealthPowerup.Position = new Vector2(100, 100);
                            break;
                        case 12:
                            enemy.Y = 350;
                            HealthPowerup.Position = new Vector2(100, 100);
                            break;
                        case 13:
                            break;
                    }
                }

                if (levels[currentWorld][currentLevel].hasPowerup)
                {
                    foreach (Item item in LevelPowerups[levels[currentWorld][currentLevel]])
                    {
                        MainCharacter.CheckPowerup(item);
                    }
                }
                
                foreach(MovingPlatform mplatform in levels[currentWorld][currentLevel].mPlatforms)
                {
                    if (levels[currentWorld][currentLevel].hasMovingPlatforms)
                    {
                        mplatform.Update();
                    }
                }

                if(ks.IsKeyDown(Keys.Delete) && lastKS.IsKeyUp(Keys.Delete))
                {
                    currentLevel = 0;
                    if (currentWorld == World.Land)
                    {
                        currentWorld = World.Underwater;
                    }
                    else if(currentWorld == World.Underwater)
                    {
                        currentWorld = World.Land;
                    }
                    Reset();
                }
                MenuButton.Update();
                restartbutton.Update();
            }
            if (screen == Gamescreen.LandLevelMenu)
            {
                //ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(0, 420), Color.White);
                currentWorld = World.Land;
                foreach (Button button in landLevelButtons)
                {
                    button.Update();
                    if (button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                    {
                        screen = Gamescreen.Maingame;
                        currentLevel = button.LevelValue;
                        fireballhitcount = 0;
                        MainCharacter.Position = levels[currentWorld][currentLevel].StartPosition;
                        levels[currentWorld][currentLevel].BackgroundImage = currentLevelMap;
                    }
                 
                }
                MenuButton.Update();

                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.KindOfLevelMenu;
                }
                ExitButton.Update();

            }
            if (screen == Gamescreen.UnderwaterLevelMenu)
            {
                currentWorld = World.Underwater;
                foreach (Button button in ULevelButtons)
                {
                    button.Update();
                    if (button.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                    {
                        screen = Gamescreen.Maingame;
                        currentLevel = button.ULevelValue;
                        fireballhitcount = 0;
                        MainCharacter.Position = levels[currentWorld][currentLevel].StartPosition;
                    }
                   
                }
                MenuButton.Update();
                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.KindOfLevelMenu;
                }
                ExitButton.Update();
            }
            if (screen == Gamescreen.GameOver)
            {
                if (restartbutton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    //restarts here
                    MCLives = 100;
                    currentLevel = 0;
                    Reset();
                    screen = Gamescreen.Maingame;
                    
                }
            }
            if (screen == Gamescreen.MainMenu)
            {
                ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(400, 400), Color.White);
                LevelSelectButton.Update();
                if (LevelSelectButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.KindOfLevelMenu;
                }
                ShopButton.Update();
                if (ShopButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.Shop;
                }
                ExitButton.Update();
                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.Maingame;
                }

            }
            if (screen == Gamescreen.Shop)
            {
                SpongebobButton.Update();
                PatrickButton.Update();
                MarioButton.Update();
                ExitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(400, 400), Color.White);

                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.Maingame;
                }
                PlayButton.Update();
                if (BackgroundButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.ChooseBackgroundMenu;
                }
                if(SpongebobButton.IsClicked && SpongebobButton.isLocked)
                {
                    TotalCollectedCoins = SpongebobButton.UnlockItem(TotalCollectedCoins);
                    MainCharacter.currentCharacterName = Character.CharacterName.Spongebob;
                }
                
                else if(PatrickButton.beingPurchased)
                {
                    TotalCollectedCoins = PatrickButton.UnlockItem(TotalCollectedCoins);
                    MainCharacter.currentCharacterName = Character.CharacterName.Patrick;
                }

                
            }
            if (screen == Gamescreen.StartScreen)
            {
                if (PlayButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.Maingame;
                }
                PlayButton.Update();
            }
            if (screen == Gamescreen.KindOfLevelMenu)
            {
                UnderwaterLevelsButton.Update();
                if (UnderwaterLevelsButton.IsClicked)
                {
                    screen = Gamescreen.UnderwaterLevelMenu;
                }
                LandLevelsButton.Update();
                if (LandLevelsButton.IsClicked)
                {
                    screen = Gamescreen.LandLevelMenu;
                }

                ExitButton.Update();
                if(ExitButton.IsClicked)
                {
                    screen = Gamescreen.MainMenu;
                }

            }
            

            if (screen == Gamescreen.ChooseBackgroundMenu)
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
                }
                if (ExitButton.HitBox.Contains(ms.X, ms.Y) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
                {
                    screen = Gamescreen.MainMenu;
                    levels[currentWorld][currentLevel].BackgroundImage = currentLevelMap;
                }


            }
            lastKS = ks;
            lastMs = ms;

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //if (screen == 0)
            //{
            //    LevelMenuBg.Draw(spriteBatch);
            //}

            if (screen == Gamescreen.Maingame)
            {
                levels[currentWorld][currentLevel].Draw(spriteBatch);
                if (levels[currentWorld][currentLevel] == levels[World.Land][3] && !enemy.isDead || levels[currentWorld][currentLevel] == levels[World.Land][12] && !enemy.isDead)
                {
                    enemy.Draw(spriteBatch);
                }
                MainCharacter.Draw(spriteBatch);

                MenuButton.Draw(spriteBatch);

                foreach (AnimatedSprite coin in levels[currentWorld][currentLevel].CoinList)
                {
                    if (!coin.collected)
                    {
                        coin.Draw(spriteBatch);
                    }
                }
                
                spriteBatch.DrawString(coinCounter, string.Format("Coins: {0}", TotalCollectedCoins), new Vector2(500, 0), Color.White);
                
            }

            if (screen == Gamescreen.LandLevelMenu)
            {
                spriteBatch.Draw(LandLevelMenuBackground, new Microsoft.Xna.Framework.Rectangle(0, 0, 1000, 489), Color.White);
                foreach (Button button in landLevelButtons)
                {
                    button.Draw(spriteBatch);
                }
                ExitButton.Draw(spriteBatch);
            }

            if(screen == Gamescreen.UnderwaterLevelMenu)
            {
                spriteBatch.Draw(ULevelMenuBackground, new Microsoft.Xna.Framework.Rectangle(0, 0, 1000, 489), Color.White);
                foreach (Button button in ULevelButtons)
                {
                    button.Draw(spriteBatch);
                }
                ExitButton.Draw(spriteBatch);
            }

            if (screen == Gamescreen.GameOver)
            {
                GameOverScreen.Draw(spriteBatch);
            }

            if (screen == Gamescreen.Maingame || screen == Gamescreen.GameOver)
            {
                restartbutton.Draw(spriteBatch);
                spriteBatch.DrawString(font, string.Format("Lives: {0}", MCLives), Vector2.Zero, Color.White);
            }
            if (screen == Gamescreen.MainMenu)
            {
                LevelSelectButton.Draw(spriteBatch);
                ShopButton.Draw(spriteBatch);
                ExitButton.Draw(spriteBatch);
            }
            if (screen == Gamescreen.Shop)
            {
                SpongebobButton.Draw(spriteBatch);
                MarioButton.Draw(spriteBatch);
                PatrickButton.Draw(spriteBatch);
                PlayButton.Draw(spriteBatch);
                BackgroundButton.Draw(spriteBatch);
                spriteBatch.DrawString(coinCounter, string.Format("Coins: {0}", TotalCollectedCoins), new Vector2(500, 0), Color.White);
            }
            if (screen == Gamescreen.StartScreen)
            {
                spriteBatch.Draw(TitleScreen, Vector2.Zero, Color.White);
                PlayButton.Draw(spriteBatch);
                //spriteBatch.DrawString(font2, string.Format("Mario Platformer"), new Vector2(275, 200), Color.White);
                PrincessP.Draw(spriteBatch);
                FlyingTurtle.Draw(spriteBatch);
                MarioSprite.Draw(spriteBatch);
            }
            if (screen == Gamescreen.KindOfLevelMenu)
            {
                UnderwaterLevelsButton.Draw(spriteBatch);
                LandLevelsButton.Draw(spriteBatch);
                ExitButton.Draw(spriteBatch);
            }
            if (screen == Gamescreen.ChooseBackgroundMenu)
            {

                spriteBatch.Draw(currentLevelMap, new Microsoft.Xna.Framework.Rectangle(0, 0, 1000, 489), Color.White);
                ExitButton.Draw(spriteBatch);
            }
            
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
