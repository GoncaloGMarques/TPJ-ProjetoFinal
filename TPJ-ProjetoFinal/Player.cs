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
        private bool isJumping;
        private float maxDistance, velocity;
        private Vector2 sourcePosition;
        private Vector2 direction;
        bool isFalling;
        Sprite Collided;
        Vector2 CollisionPoint;
        // Construtor
        public Player(ContentManager content, String textureName)
            : base(content, textureName, 3, 27)
        {
            this.isJumping = false;
            this.isFalling = true;
            this.position = new Vector2(0, 3);
            this.maxDistance = 2f;
            this.velocity = .7f;
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
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D))
            {
                pressedKey = 4;
                this.position.X += 0.01f; 
            }
            if (state.IsKeyDown(Keys.A))
            {
                pressedKey = 1;
                this.position.X -= 0.01f;
            }
            if (state.IsKeyDown(Keys.Space))
            {
                pressedKey = 5;
            }
            if (state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D)&& state.IsKeyUp(Keys.D))
            {
                pressedKey = 0;
            }

            if (!this.scene.Collides(this, out this.Collided, out this.CollisionPoint))
            {

                this.position.Y -= 0.05f;
                // this.isFalling = false;

            }

            if (state.IsKeyDown(Keys.Space) && isJumping == false)
                Jump();

            
            if (isJumping)
            {
                this.isFalling = false;
                if ((position - sourcePosition).Length() <= maxDistance)
                {
                    position = position + direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;
                    this.isFalling = true;
                }
                else
                {
                    position = position - direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;
                    if (position.Y <= sourcePosition.Y)
                    {
                        position.Y = 0f;
                        isJumping = false;
                        this.isFalling = true;
                    }
                }

            }
            Camera.SetTarget(this.position);
            base.Update(gameTime);
        }

        public void Jump()
        {
            this.isJumping = true;
            this.sourcePosition = position;
            this.direction = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation));
        }

        public static int returnKey()
        {
            return pressedKey;
        }
    }
}
