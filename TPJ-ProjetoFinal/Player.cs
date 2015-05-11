using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPJ_ProjetoFinal
{
    class Player : AnimatedSprite
    {
        private float maxDistance, velocity;
        private Vector2 sourcePosition;
        private Vector2 direction;
        
        Sprite Collided;
        // Construtor
        public Player(ContentManager content, String textureName)
            : base(content, textureName, 5, 27)
        {
            this.isJumping = false;
            this.isFalling = true;
            this.position = new Vector2(-45f, -1.4f);
            this.maxDistance = 2f;
            this.velocity = .4f;
            this.Scale(2f/3f);
            this.direction = Vector2.Zero;
            this.EnableCollisions();
        }

        // Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public bool IsFalling
        {
            get { return isFalling; }
            set { isFalling = value; }
        }

        // Update
        public override void Update(GameTime gameTime)
        {

            if (state.IsKeyDown(Keys.D))
            {
                this.position.X += 0.05f;
            }

            if (state.IsKeyDown(Keys.A))
            {
                this.position.X -= 0.05f;
            }
            

            if (state.IsKeyDown(Keys.Space) && isJumping == false && isFalling == false)
            {
                Jump();
            }

            if (!isJumping)
            {
                // Gravidade puxa para baixo
                this.position.Y -= 0.05f;

                if (this.scene.Collides(this, out this.Collided))
                {
                    isFalling = false;
                    this.position.Y += 0.05f;
                }
            }
            else
            {
                if (!this.scene.Collides(this, out this.Collided))
                {
                    if ((position - sourcePosition).Length() < maxDistance)
                    {
                        position = position + direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;
                    }
                    else
                    {
                        if ((position - sourcePosition).Length() >= maxDistance)
                        {
                            position = position + direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;
                            isFalling = true;
                            isJumping = false;
                        }

                    }
                }
                else
                {
                    if (isJumping)
                    {
                        position.Y -= 0.05f;
                        isJumping = false;
                        isFalling = true;
                    }
                }
            }
            Camera.SetTarget(this.position);
            base.Update(gameTime);
        }

        public void Jump()
        {
            this.isJumping = true;
            this.isFalling = false;
            this.sourcePosition = this.position;
            this.direction = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation));
        }


    }
}
