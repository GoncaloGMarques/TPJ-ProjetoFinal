using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace TPJ_ProjetoFinal
{
    class Player : AnimatedSprite
    {
        private float maxDistance, velocity;
        private Vector2 sourcePosition;
        private Vector2 direction;

        Sprite Collided;
        Vector2 CollisionPoint;
        private float fireInterval = 0.5f;
        private float fireCounter = 0f;
        public static Vector2 positionSent;
        public Game1 jogo;
        public static int alunosSalvos;

        // Som
        SoundEffect jogoGanho;
        SoundEffect boo;
        SoundEffect fire;

        // Construtor
        public Player(ContentManager content, String textureName, Game1 game1)
            : base(content, textureName, 5, 27)
        {
            this.isJumping = false;
            this.isFalling = true;
            this.position = new Vector2(-91f, 10.5f);
            this.maxDistance = 5f / 2f;
            this.velocity = .4f;
            this.Scale(2f / 3f);
            this.jogo = game1;
            this.direction = Vector2.Zero;
            alunosSalvos = 0;
            jogoGanho = content.Load<SoundEffect>("aplausos");
            boo = content.Load<SoundEffect>("boo");
            fire = content.Load<SoundEffect>("fire"); 
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
            positionSent = this.position;
            // apenas para debug
            if (state.IsKeyDown(Keys.L))
            { this.position.X += 0.1f; }
            if (state.IsKeyDown(Keys.J))
            { this.position.X -= 0.1f; }
            if (state.IsKeyDown(Keys.I))
            { this.position.Y += 0.1f; }
            if (state.IsKeyDown(Keys.K))
            { this.position.Y -= 0.1f; }

            if (Player.alunosSalvos == 3)// salva os tres alunos, volta ao inicio
            {
                jogoGanho.Play();
                jogo.recomecar();
            }

            if (state.IsKeyDown(Keys.D))
            {
                this.position.X += 0.05f;
                if (this.scene.Collides(this, out this.Collided, out this.CollisionPoint))
                {
                    if (this.Collided.nomedaSprite == "TEST-F")
                    {
                        boo.Play();
                        this.Destroy();
                        jogo.recomecar();
                    }
                    else if (this.Collided.nomedaSprite == "student1" || this.Collided.nomedaSprite == "student2" || this.Collided.nomedaSprite == "student3")
                    {
                        jogoGanho.Play();
                        alunosSalvos++;
                        this.Collided.Destroy();
                    }
                    else
                    {
                        if (CollisionPointLast != positionColision)
                        {
                            this.position.X -= 0.05f;
                        }
                    }
                }
            }

            if (state.IsKeyDown(Keys.A))
            {
                this.position.X -= 0.05f;
                if (this.scene.Collides(this, out this.Collided, out this.CollisionPoint))
                {
                    if (this.Collided.nomedaSprite == "TEST-F")
                    {
                        boo.Play();
                        this.Destroy();
                        jogo.recomecar();
                    }
                    else if (this.Collided.nomedaSprite == "student1" || this.Collided.nomedaSprite == "student2" || this.Collided.nomedaSprite == "student3")
                    {
                        jogoGanho.Play();
                        alunosSalvos++;
                        this.Collided.Destroy();
                    }
                    else
                    {
                        if (CollisionPointLast != positionColision)
                        {
                            this.position.X += 0.05f;
                        }
                    }
                }
            }

            if (state.IsKeyDown(Keys.Space) && isJumping == false && isFalling == false)
            {
                this.isJumping = true;
                this.isFalling = false;
                this.sourcePosition = this.position;
                this.direction = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation));
            }

            mstate = Mouse.GetState();
            Point mpos = mstate.Position;

            Vector2 tpos = Camera.WorldPoint2Pixels(position);
            float a = (float)mpos.Y - tpos.Y;
            float l = (float)mpos.X - tpos.X;
            float rot = (float)Math.Atan2(a, l);
            rot += (float)Math.PI / 2f;

            fireCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (mstate.LeftButton == ButtonState.Pressed && fireCounter >= fireInterval)
            {
                Vector2 pos = this.position
                         + new Vector2((float)Math.Sin(rot) * size.Y,
                                       (float)Math.Cos(rot) * size.Y);
                Bala bullet = new Bala(cManager, "pencil", pos, rot, this.nomedaSprite, jogo);
                scene.AddSprite(bullet);
                fireCounter = 0f;
            }

            Jump(gameTime);
            Camera.SetTarget(this.position);
            CollisionPointLast = positionColision;// guarda a posicao na sprite da ultima colisao
            base.Update(gameTime);
        }

        public void Jump(GameTime gameTime)
        {
            if (isJumping || !isJumping)
            {
                if (!isJumping)
                {
                    // Gravidade 
                    this.position.Y -= 0.05f;

                    if (this.scene.Collides(this, out this.Collided, out this.CollisionPoint))
                    {
                        if (this.Collided.nomedaSprite == "Ground"||this.Collided.nomedaSprite == "student1" || this.Collided.nomedaSprite == "student2" || this.Collided.nomedaSprite == "student3")
                        {
                            isFalling = false;
                            this.position.Y += 0.05f;
                        }
                        if (this.Collided.nomedaSprite == "TEST-F")
                        {
                            boo.Play();
                            this.Destroy();
                            this.jogo.recomecar();
                        }
                    }
                }
                else
                {
                    if (!this.scene.Collides(this, out this.Collided, out this.CollisionPoint))
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
            }
        }
    }
}