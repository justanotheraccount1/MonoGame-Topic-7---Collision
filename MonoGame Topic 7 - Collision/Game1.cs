using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MonoGame_Topic_7___Collision
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyboardState;
        MouseState mouseState;
        Texture2D pacLeftTexture, pacRightTexture, pacUpTexture, pacDownTexture, pacCurrentTexture, exitTexture, barrierTexture, coinTexture;
        Rectangle pacRect, barrierRect1, barrierRect2, exitRect, window;
        Vector2 pacSpeed;
        Rectangle coinRect; List<Rectangle> coins;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here

            base.Initialize();
            pacSpeed = Vector2.Zero;
            pacRect = new Rectangle(10, 10, 60, 60);
            barrierRect1 = new Rectangle(0, 250, 350, 75);
            barrierRect2 = new Rectangle(450, 250, 350, 75);
            coins = new List<Rectangle>();
            coins.Add(new Rectangle(400, 50, coinTexture.Width, coinTexture.Height)); 
            coins.Add(new Rectangle(475, 50, coinTexture.Width, coinTexture.Height)); 
            coins.Add(new Rectangle(200, 300, coinTexture.Width, coinTexture.Height)); 
            coins.Add(new Rectangle(400, 300, coinTexture.Width, coinTexture.Height));
            exitRect = new Rectangle(700, 400, 100, 100);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pacDownTexture = Content.Load<Texture2D>("pac_down");
            pacUpTexture = Content.Load<Texture2D>("pac_up");
            pacLeftTexture = Content.Load<Texture2D>("pac_left");
            pacRightTexture = Content.Load<Texture2D>("pac_right");
            pacCurrentTexture = pacRightTexture;
            barrierTexture = Content.Load<Texture2D>("rock_barrier");
            coinTexture = Content.Load<Texture2D>("coin");
            exitTexture = Content.Load<Texture2D>("hobbit_door");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            pacSpeed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacSpeed.X += 2;
                pacCurrentTexture = pacRightTexture;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacSpeed.X -= 2;
                pacCurrentTexture = pacLeftTexture;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacSpeed.Y -= 2;
                pacCurrentTexture = pacUpTexture;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacSpeed.Y += 2;
                pacCurrentTexture = pacDownTexture;
            }
            pacRect.Offset(pacSpeed);
            for (int i = 0; i < coins.Count; i++)
            {
                if (pacRect.Intersects(coins[i]))
                {
                    coins.RemoveAt(i);
                    i--;
                }

            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkViolet);
            _spriteBatch.Begin();

            _spriteBatch.Draw(barrierTexture, barrierRect1, Color.White);
            _spriteBatch.Draw(barrierTexture, barrierRect2, Color.White);
            _spriteBatch.Draw(pacCurrentTexture, pacRect, Color.White);
            _spriteBatch.Draw(exitTexture, exitRect, Color.White);
            foreach (Rectangle coin in coins)
                _spriteBatch.Draw(coinTexture, coin, Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
