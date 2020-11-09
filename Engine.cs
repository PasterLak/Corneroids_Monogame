using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Corneroids
{
    public sealed class Engine : Game
    {
        public static GraphicsDeviceManager graphics { get; private set; }
        public static GraphicsDevice graphicsDevice { get; private set; }
        public static GraphicsAdapter graphicsAdapter { get; private set; }
        public static ContentManager contentManager { get; private set; }
        public static Camera camera { get; private set; }
        public static GameWindow window { get; private set; }
        public static GameTime Time { get; private set; }


        public static event Action OnAwake;
        public static event Action OnStart;
        public delegate void Action();

        public static string PlayerName = "PasterLak";

        public static SpriteBatch spriteBatch;

        private GUI.WindowLayer windowLayer;
        private GUI.DescriptionLayer dis;
        private Texture2D mouseCursor;
        

        public static SpriteFont font;
        private FPS fps;
        public static World world;

        private Skybox skybox;
        private SpaceEntity asteroid;

        private SaveManager saveManager;
        public static MouseDevice mouse { get; private set; }
        public static KeyboardDevice Input;

        private bool UseBackFaceCulling = false;
        private bool UseDepthStencil = true;

       


        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            

            Console.Clear();
            Console.WriteLine("-------------------------------------------------");

            fps = new FPS();
            
            Window.Title = "Corneroids";
            
            

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
            

            

        }

        protected override void Initialize()
        {
            graphics.HardwareModeSwitch = false;
            graphicsDevice = graphics.GraphicsDevice;
            graphicsAdapter = new GraphicsAdapter();
           

            Window.Title = "Corneroids";

            mouse = new MouseDevice();
            Input = new KeyboardDevice();
            saveManager = new SaveManager();
            world = new World();

            camera = new Camera(new Vector3(0, 0, -40));
            window = Window;
            AppWindow.SetDefaultWindow();
            Window.AllowUserResizing = true;
            Window.IsBorderless = false;
       
            
            asteroid = new SpaceEntity(Vector3.Zero);
            

            windowLayer = new GUI.WindowLayer(new Rectangle (15,15,500,256));
            windowLayer.Name = "Test";

            
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mouseCursor = Resources.LoadTexture2D("Content/cursor.png");
            Chunk.texture = Resources.LoadTexture2D("Content/blockTextures.png"); 
            font = Content.Load<SpriteFont>("basic");
            skybox = new Skybox(Resources.LoadTexture2D("Content/skybox.jpg"));

            List<string> list = new List<string>() { "Hello", "123" };

            dis = new GUI.DescriptionLayer(list);

            Awake();
            Start();

        }

        private void Awake()
        {
            OnAwake?.Invoke();

            
            Mouse.SetCursor(MouseCursor.FromTexture2D(mouseCursor, 0, 0));


            OnAwake = null;
        }

        private void Start()
        {
            OnStart?.Invoke();

            asteroid.AddChunk(0, 0, 0);
            asteroid.AddChunk(1, 0, 0);
            asteroid.AddChunk(0, 1, 0);
            asteroid.AddChunk(-1, 1, 0);

            asteroid.CreateChunks();

            Chat.Active = true;

            Chat.SendMessage("Welcome to Corneroids!", Color.Yellow, "System");
            Chat.SendMessage("Write /help to get the list of commands", Color.Yellow);

            //Chat.SendMessage("How are you dude?", Color.White, PlayerName);

            Chat.OnStart();
            
            OnStart = null;
        }

        protected override void Update(GameTime gameTime)
        {
            

            Input.UpdateDevice();
            mouse.UpdateDevice();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (Corneroids.Input.GetButton(Keys.Escape))
                Exit();

            if (Corneroids.Input.GetButton(Keys.F12))
            {

                AppWindow.SetFullScreen();
                
                
            }
            if(Input.GetKeyDown(Keys.R))
            {

                asteroid.transform.rotation += new Vector3(0f,45f,0);


            }
            if (Input.GetKeyDown(Keys.O))
            {

                asteroid.transform.scale += new Vector3(-0.1f, -0.1f, -0.1f);


            }
            if (Input.GetKeyDown(Keys.P))
            {

                asteroid.transform.Translate( new Vector3(1f, 0f, 0));


            }
            //if (Input.GetKeyDown(Keys.O))
            //{

            //    asteroid.transform.scale -= new Vector3(-0.1f, -0.1f, -0.1f);


            //}
            //if (Input.GetKeyDown(Keys.P))
            //{

            //    asteroid.transform.scale -= new Vector3(0.1f, 0.1f, 0.1f);


            //}




            if (Input.GetKeyDown(Keys.M))
            {
                Chat.SendMessage("Hello!", Color.White, PlayerName);
             

                //Ray ray = new Ray(camera.Position, Vector3.Forward);
                //ray.Intersects();

                
                
            }


            
            camera.Update(gameTime);
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            

            base.Update(gameTime);
        }

        public Vector2 ScreenToWorldSpace(in Vector2 point ,Matrix TransformMatrix)
        {
            Matrix invertedMatrix = Matrix.Invert(TransformMatrix);
            return Vector2.Transform(point, invertedMatrix);
        }

        protected override void Draw(GameTime gameTime)
        { 

            GraphicsDevice.Clear(Lighting.ClearColor);

            if (UseDepthStencil)
            {
                DepthStencilState dss = new DepthStencilState();

                dss.DepthBufferEnable = true;
                GraphicsDevice.DepthStencilState = dss;
            }

            
            if(UseBackFaceCulling)
            {
                RasterizerState rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                GraphicsDevice.RasterizerState = rasterizerState;
            }

            skybox.Render();
            asteroid.Draw();


            DrawGUI(gameTime);

            


            base.Draw(gameTime);
        }

        public void DrawGUI(GameTime gameTime)
        {

            var osNameAndVersion = System.Environment.OSVersion;

            spriteBatch.Begin();
            
            spriteBatch.DrawString(font, "Corneroids on Monogame Framework v " + Application.GameVersion, new Vector2(5, 30), Color.Yellow);
            spriteBatch.DrawString(font, GraphicsDevice.Adapter.Description, new Vector2(5, 50), Color.White);
            spriteBatch.DrawString(font, osNameAndVersion.VersionString, new Vector2(5, 70), Color.White);

            
            //spriteBatch.DrawString(font, "Asteroid", new Vector2(5, 70), Color.LightGreen);

            spriteBatch.DrawString(font, "+",AppWindow.GetScreenCenter , Color.White);

            Chat.DrawMessages();
            //windowLayer.Render();
            //dis.Render();

            camera.DrawDebug();
            fps.DrawFpsCount(gameTime);


            spriteBatch.End();

        }


        public static void Log(string consoleMessage)
        {
            Console.WriteLine(consoleMessage);
        }


        public static int GetW()
        {
            
            Console.WriteLine(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);
            return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        }
        public static int GetH()
        {
            Console.WriteLine(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

    }
}
