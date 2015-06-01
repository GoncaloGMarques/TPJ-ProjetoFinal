using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPJ_ProjetoFinal
{
    public class Cena
    {
        // Variáveis
        public SpriteBatch spriteBatch;
        // Lista sprites
        private List<Sprite> spriteList;
        private List<SlidingBackground> backgrounds;
        public static Vector2 collisionPoint;

        // Construtor
        public Cena(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
            this.spriteList = new List<Sprite>();
            this.backgrounds = new List<SlidingBackground>();
        }

        // Update
        public void Update(GameTime gameTime)
        {
            foreach (var sprite in spriteList.ToList())
                sprite.Update(gameTime);
            
        }

        // Draw
        public void Draw(GameTime gameTime)
        {
            if (spriteList.Count > 0 || backgrounds.Count > 0)
            {
                this.SpriteBatch.Begin();
                // Desenhar os fundos
                foreach (var background in backgrounds)
                    background.Draw(gameTime);

                // Desenhar as sprites
                foreach (var sprite in spriteList)
                    sprite.Draw(gameTime);

                this.SpriteBatch.End();
            }
        }

        // Adiciona uma nova sprite
        public void AddSprite(Sprite sprite)
        {
            this.spriteList.Add(sprite);
            sprite.SetScene(this);
        }

        public void AddBackground(SlidingBackground b)
        {
            this.backgrounds.Add(b);
            b.SetScene(this);
        }

        // Remove uma sprite da cena
        public void RemoveSprite(Sprite sprite)
        {
            this.spriteList.Remove(sprite);
        }

        // Deteção de colisões de todas as sprites da cena
        public bool Collides(Sprite sprite, out Sprite collided, out Vector2 collisionPoint)
        {
            bool collisionExists = false;
            // Parar "calar" o compilador
            collided = sprite;
            collisionPoint = Vector2.Zero;

            foreach (var s in spriteList)
            {
                if (sprite == s) continue;
                if (sprite.CollidesWith(s, out collisionPoint))
                {
                    collisionExists = true;
                    collided = s;
                    break;
                }
            }
            return collisionExists;
        }

        // Dispose
        public void Dispose()
        {
            foreach (var sprite in spriteList)
                sprite.Dispose();

            foreach (var sprite in backgrounds)
                sprite.Dispose();
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            private set { spriteBatch = value; }
        }
    }
}