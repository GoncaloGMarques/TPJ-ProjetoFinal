using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Bala(ContentManager cManager,
                      Vector2 sourcePosition, float rotation)
            : base(cManager, "pencil",1,1)
        {
            this.position = sourcePosition;
            this.sourcePosition = sourcePosition;
            this.rotation = rotation;
            this.Scale(0.6f);
            this.direction = new Vector2((float)Math.Sin(rotation),
                                         (float)Math.Cos(rotation));
            this.EnableCollisions();
        }

        public override void Update(GameTime gameTime)
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
            

            this.rotation += (float)Math.PI/5;


            base.Update(gameTime);
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}