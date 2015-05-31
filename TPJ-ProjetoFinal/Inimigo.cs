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
        private Vector2 direction;
        private float fireInterval = 2f;
        private float fireCounter = 0f;
        float rot;
        bool fire;
        Game1 jogo;

        // Construtor
        public Inimigo(ContentManager content, String textureName, Vector2 position, float rotation, bool canFire, Game1 game1)
            : base(content, textureName, 1, 1)
        {
            this.Content = content;
            this.isJumping = false;
            this.position = position;
            this.direction = Vector2.Zero;
            this.rot = rotation;
            this.fire = canFire;
            this.jogo = game1;
            this.Scale(1);
            this.EnableCollisions();
        }

        // Update    
        public override void Update(GameTime gameTime)
        {
            if (this.fire == true)
            {
                if (Math.Abs(this.position.X - Player.positionSent.X) < 5)
                {
                    Console.WriteLine(this.position.X - Player.positionSent.X);
                    if (this.position.X - Player.positionSent.X > 0)
                    {
                        fireCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (fireCounter >= fireInterval)
                        {
                            Vector2 pos = this.position
                                     + new Vector2((float)Math.Sin(rot) * size.Y,
                                                   (float)Math.Cos(rot) * size.Y);
                            Console.WriteLine(this.nomedaSprite);
                            Bala bullet = new Bala(cManager, "bulletTestes", new Vector2(pos.X-2f, pos.Y), -rot, this.nomedaSprite, jogo);

                            scene.AddSprite(bullet);
                            fireCounter = 0f;
                        }

                    }
                    if (this.position.X - Player.positionSent.X < 0)
                    {
                        fireCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (fireCounter >= fireInterval)
                        {
                            Vector2 pos = this.position
                                     + new Vector2((float)Math.Sin(rot) * size.Y,
                                                   (float)Math.Cos(rot) * size.Y);
                            Console.WriteLine(this.nomedaSprite);
                            Bala bullet = new Bala(cManager, "bulletTestes", pos, rot, this.nomedaSprite,jogo);

                            scene.AddSprite(bullet);
                            fireCounter = 0f;
                        }

                    }
                }
            }
            base.Update(gameTime);
        }

        // Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
