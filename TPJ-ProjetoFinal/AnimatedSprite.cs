using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TPJ_ProjetoFinal
{
    public class AnimatedSprite : Sprite
    {
        // Variáveis
        public int rows, columns;
        public Point currentFrame;
        private float animationInterval = 1f / 35f;
        private float animationTimer = 0f;
        bool loopJump;
        public bool isFalling;
        public bool isJumping;

        public bool Loop { get; set; }
        // Construtor
        public AnimatedSprite(ContentManager content, String textureName, int rows, int columns)
            : base(content, textureName)
        {
            this.rows = rows;
            this.columns = columns;
            this.rows = rows;
            this.pixelSize.X = this.pixelSize.X / columns;
            this.pixelSize.Y = this.pixelSize.Y / rows;
            this.size = new Vector2(1f, (float)pixelSize.Y / (float)pixelSize.X);
            Loop = true;
        }

        // Update
        public override void Update(GameTime gameTime)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Controla o tempo de cada frame
            if (animationTimer > animationInterval)
            {
                animationTimer = 0f;
                NextFrame();
            }
            base.Update(gameTime);
        }

        // Draw
        public override void Draw(GameTime gameTime)
        {
            source = new Rectangle((int)(currentFrame.X * pixelSize.X), (int)(currentFrame.Y * pixelSize.Y), (int)pixelSize.X, (int)pixelSize.Y);
            base.Draw(gameTime);
        }

        // Passa para a próxima frame da spritesheet
        private void NextFrame()
        {
            if (this.nomedaSprite == "explosion")
            {
                currentFrame.X++;
            }
            if (this.nomedaSprite == "TimComplete")
            {
                if (loopJump == true)
                {
                    if (isJumping)
                    {
                        if (state.IsKeyDown(Keys.D))
                        {
                            currentFrame.Y = 3;
                        }
                        if (state.IsKeyDown(Keys.A))
                        {
                            currentFrame.Y = 4;
                        }
                        if (currentFrame.X < 9)
                        {
                            this.animationInterval = 1f / 30f;
                            currentFrame.X++;
                        }
                    }
                    else
                        if (!isFalling)
                        {
                            loopJump = false;
                            animationInterval = 1f / 35f;
                        }
                        else
                        {
                            currentFrame.X = 9;
                        }
                }
                else
                {
                    if ((state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.D)) && state.IsKeyDown(Keys.Space))
                    {
                        currentFrame.X = 0;
                        loopJump = true;

                    }

                    else
                    {
                        if (state.IsKeyDown(Keys.D))
                        {
                            currentFrame.Y = 1;
                            if (currentFrame.X < columns - 1)
                                currentFrame.X++;
                            else
                                currentFrame.X = 0;
                        }
                        else
                        {
                            if (state.IsKeyDown(Keys.A))
                            {
                                currentFrame.Y = 2;
                                if (currentFrame.X < columns - 1)
                                    currentFrame.X++;
                                else
                                    currentFrame.X = 0;
                            }
                            else
                            {
                                if (state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D) && state.IsKeyUp(Keys.Space))
                                {
                                    currentFrame.X = 0;
                                    currentFrame.Y = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void EnableCollisions()
        {
            this.HasCollisions = true;

            this.radius = (float)Math.Sqrt(Math.Pow(size.X / 2, 2) +
                                           Math.Pow(size.Y / 2, 2));

            pixels = new Color[(int)(pixelSize.X * pixelSize.Y)];
            image.GetData<Color>(0, new Rectangle(
                    (int)(currentFrame.X * pixelSize.X),
                    (int)(currentFrame.Y * pixelSize.Y),
                    (int)pixelSize.X,
                    (int)pixelSize.Y),
                 pixels, 0,
                (int)(pixelSize.X * pixelSize.Y));
        }
    }
}