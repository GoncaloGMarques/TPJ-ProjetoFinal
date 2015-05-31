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
using Microsoft.Xna.Framework.Audio;
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
        Texture2D ecraInicial;
        bool pressionarESC = false;
        

        public enum GameStatus
        {
            ecraInicial, jogo
        }
        public GameStatus status;

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
            status = GameStatus.ecraInicial;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ecraInicial = Content.Load<Texture2D>("StartScreen");
            scene = new Cena(spriteBatch);
            scene.AddSprite(new Player(Content, "TimComplete", this));
            scene.AddSprite(new Alunos(Content, "student1", new Vector2(-28f, -10.1f)));
            scene.AddSprite(new Alunos(Content, "student2", new Vector2(-1f, -16.25f)));
            scene.AddSprite(new Alunos(Content, "student3", new Vector2(-16f, 1.18f)));
            scene.AddSprite(new Plataformas(Content));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-70f, -12.76f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-83f, -15.76f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-38f, -15.76f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-21f, -10.91f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-25f, -9.9f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-9f, -11.88f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(0f, -12.9f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(3f, -16.05f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(7f, -5.18f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(0.35f, -1.79f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(1f, -9.13f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-23f, 1.32f), 1.5f, true, this));
            scene.AddSprite(new Inimigo(Content, "TEST-F", new Vector2(-10f, 1.32f), 1.5f, true, this));
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
            if (pressionarESC == true && Keyboard.GetState().IsKeyUp(Keys.Escape))
            { pressionarESC = false; }
            if (status == GameStatus.ecraInicial)
            {
                if (pressionarESC == false && Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    status = GameStatus.jogo;
                }
            }

            if (status == GameStatus.jogo)
            {
                if (pressionarESC == false && Keyboard.GetState().IsKeyDown(Keys.Escape))
                {

                    pressionarESC = true;
                    status = GameStatus.ecraInicial;

                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    status = GameStatus.ecraInicial;
                }
                scene.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (status == GameStatus.ecraInicial)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(ecraInicial, new Vector2(0, 0), Color.White);
                spriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                scene.Draw(gameTime);
                base.Draw(gameTime);
            }
        }

        public void recomecar()
        {
            Initialize();
            status = GameStatus.ecraInicial;
        }
    }
}

