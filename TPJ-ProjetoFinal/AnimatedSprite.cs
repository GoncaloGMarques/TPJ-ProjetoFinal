using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TPJ_ProjetoFinal
{
    public class AnimatedSprite : Sprite
    {
        // Variáveis
        public int rows, columns;
        public Point currentFrame;
        private float animationInterval = 1f/35f;
        private float animationTimer = 0f;

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
            if (Player.pressedKey != 0)
            {
                currentFrame.Y = 1;
                if (currentFrame.X < columns - 1)
                    currentFrame.X++;
                else
                    currentFrame.X = 0;
            }
            else
            {
                currentFrame.X = 0;
                currentFrame.Y = 0;
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
        //public override void Update(GameTime gameTime)
        //{
        //    currentFrame++;
        //    if (currentFrame == totalFrames)
        //        currentFrame = 0;

        //    base.Update(gameTime);
        //}
        ////Usar esta função para desenhar (spriteBatch, coordenadas)
        //public void Draw(SpriteBatch spriteBatch, Vector2 location)
        //{
        //    int width = texture.Width / columns;
        //    int height = texture.Height / rows;
        //    int row = (int)((float)currentFrame / (float)columns);
        //    int column = currentFrame % columns;

        //    Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
        //    Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

        //    //spriteBatch.Begin();
        //    spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        //    //spriteBatch.End();
        //}
    }
}
