#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

#endregion

namespace TPJ_ProjetoFinal
{
    public class Game1 : Game
    {
        // Variáveis
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Texture2D ground;
        Cena scene;



        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Definição do tamanho da janela
            graphics.PreferredBackBufferHeight = 520;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
            Camera.SetGraphicsDeviceManager(graphics);
            Camera.SetTarget(Vector2.Zero);
            Camera.SetWorldWidth(10);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Cena(spriteBatch);
            scene.AddSprite(new Player(Content, "TimComplete"));
            scene.AddSprite(new Plataformas (Content));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-90f, -3.15f)));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-89f, -3.15f)));
            //SlidingBackground sand = new SlidingBackground(Content, "city");
            //scene.AddBackground(sand);
        }

        protected override void UnloadContent()
        {
            spriteBatch.Dispose();
            scene.Dispose();
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            scene.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            scene.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}

