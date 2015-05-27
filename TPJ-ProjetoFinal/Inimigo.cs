using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TPJ_ProjetoFinal
{
    class Inimigo : AnimatedSprite
    {
        // Variáveis
        ContentManager Content;
        public bool isJumping;
        private float velocity;
        private Vector2 sourcePosition;
        private Vector2 direction;
        Sprite Collided;
        Vector2 CollisionPoint;


        // Construtor
        public Inimigo(ContentManager content, String textureName, Vector2 position)
            : base(content, textureName, 1, 1)
        {
            this.Content = content;
            this.isJumping = false;
            this.position = position;
            this.velocity = 1f;
            this.direction = Vector2.Zero;
            this.Scale(1);
            this.EnableCollisions();
        }

        // Update    
        public override void Update(GameTime gameTime)
        {
            

            base.Update(gameTime);
        }

        // Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
