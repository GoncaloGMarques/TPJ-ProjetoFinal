using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace TPJ_ProjetoFinal
{
    class Bala: AnimatedSprite
    {
        public float maxDistance = 2;
        public float velocity = 4;
        private Vector2 sourcePosition;
        private Vector2 direction;
        Sprite Collided;
        Vector2 CollisionPoint;
        String SpriteChamada;
        Game1 jogo;

        public Bala(ContentManager cManager, String textureName, 
                      Vector2 sourcePosition, float rotation, String spriteQueChamou, Game1 game1)
            : base(cManager, textureName,1,1)
        {
            this.position = sourcePosition;
            this.sourcePosition = sourcePosition;
            this.rotation = rotation;
            this.Scale(0.6f);
            this.jogo = game1;
            this.SpriteChamada = spriteQueChamou;
            this.direction = new Vector2((float)Math.Sin(rotation),
                                         (float)Math.Cos(rotation));
            this.EnableCollisions();
        }

        public override void Update(GameTime gameTime)
        {
            if (SpriteChamada == "TEST-F")
            {
                position = position + direction * velocity *
                  (float)gameTime.ElapsedGameTime.TotalSeconds;

                if ((position - sourcePosition).Length() > maxDistance)
                {
                    this.Destroy();
                }

                if (this.scene.Collides(this, out this.Collided, out this.CollisionPoint))
                {
                    if (this.Collided.nomedaSprite == "TimComplete")
                    {
                        this.Destroy();
                        this.Collided.Destroy();
                        this.jogo.recomecar();
                    }
                }
                this.rotation += (float)Math.PI / 12;
            }
            else
            {
                position = position + direction * velocity *
                      (float)gameTime.ElapsedGameTime.TotalSeconds;

                if ((position - sourcePosition).Length() > maxDistance)
                {
                    this.Destroy();
                }

                if (this.scene.Collides(this, out this.Collided, out this.CollisionPoint))
                {
                    if (this.Collided.nomedaSprite == "Ground")
                    {
                        this.Destroy();
                    }
                    if (this.Collided.nomedaSprite == "TEST-F")
                    {
                        this.Destroy();
                        this.Collided.Destroy();
                    }
                }
                this.rotation += (float)Math.PI / 5;
            }
            base.Update(gameTime);
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}