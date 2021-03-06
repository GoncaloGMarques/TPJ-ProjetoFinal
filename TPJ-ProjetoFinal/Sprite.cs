﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;

namespace TPJ_ProjetoFinal
{
    public class Sprite
    {
        public bool HasCollisions { protected set; get; }
        public string nomedaSprite; // distinguir sprites
        protected Texture2D image;
        public Vector2 position;
        protected float radius; // raio da "bounding box"
        public Vector2 size;
        protected float rotation;
        protected Cena scene;
        protected Vector2 pixelSize;
        protected Rectangle? source = null;
        protected Color[] pixels;
        protected ContentManager cManager;
        public KeyboardState state;
        public Vector2 CollidePoint;
        public Vector2 positionColision;
        public Vector2 CollisionPointLast;
        protected MouseState mstate;

        public Sprite(ContentManager contents, String assetName)
        {
            this.cManager = contents;
            this.HasCollisions = false;
            this.rotation = 0f;
            this.position = Vector2.Zero;
            this.image = contents.Load<Texture2D>(assetName);
            this.pixelSize = new Vector2(image.Width, image.Height);
            this.size = new Vector2(1f, (float)image.Height / (float)image.Width);
            this.nomedaSprite = assetName;
        }

        // Se houver colisao, collisionPoint é o ponto de colisão
        // se não houver, collisionPoint deve ser ignorado!
        public bool CollidesWith(Sprite other, out Vector2 collisionPoint)
        {
            collisionPoint = position; // Calar o compilador

            if (!this.HasCollisions) return false;
            if (!other.HasCollisions) return false;

            float distance = (this.position - other.position).Length();

            if (distance > this.radius + other.radius) return false;

            return this.PixelTouches(other, out collisionPoint);
        }

        public virtual void EnableCollisions()
        {
            this.HasCollisions = true;
            this.radius = (float)Math.Sqrt(Math.Pow(size.X / 2, 2) +
                                             Math.Pow(size.Y / 2, 2));

            pixels = new Color[(int)(pixelSize.X * pixelSize.Y)];
            image.GetData<Color>(pixels);
        }

        public Color GetColorAt(int x, int y)
        {
            // Se nao houver collider, da erro!!!
            return pixels[x + y * (int)pixelSize.X];
        }

        private Vector2 ImagePixelToVirtualWorld(int i, int j)
        {
            float x = i * size.X / (float)pixelSize.X;
            float y = j * size.Y / (float)pixelSize.Y;
            return new Vector2(position.X + x - (size.X * 0.5f),
                               position.Y - y + (size.Y * 0.5f));
        }

        private Vector2 VirtualWorldPointToImagePixel(Vector2 p)
        {
            Vector2 delta = p - position;
            float i = delta.X * pixelSize.X / size.X;
            float j = delta.Y * pixelSize.Y / size.Y;

            i += pixelSize.X * 0.5f;
            j = pixelSize.Y * 0.5f - j;

            return new Vector2(i, j);
        }

        public bool PixelTouches(Sprite other, out Vector2 collisionPoint)
        {
            // Se nao houver colisao, o ponto de colisao retornado e'
            // a posicao da Sprite (podia ser outro valor qualquer)
            collisionPoint = position;
            bool touches = false;

            int i = 0;
            while (touches == false && i < pixelSize.X)
            {
                int j = 0;
                while (touches == false && j < pixelSize.Y)
                {
                    if (GetColorAt(i, j).A > 0)
                    {
                        Vector2 otherPixel = other.VirtualWorldPointToImagePixel(CollidePoint);
                        CollidePoint = ImagePixelToVirtualWorld(i, j);
                        if (otherPixel.X >= 0 && otherPixel.Y >= 0 &&
                            otherPixel.X < other.pixelSize.X &&
                            otherPixel.Y < other.pixelSize.Y)
                        {
                            if (other.GetColorAt((int)otherPixel.X, (int)otherPixel.Y).A > 0)
                            {
                                touches = true;
                                positionColision = new Vector2(i, j);// posicao da colisao na imagem(pixeis)
                                collisionPoint = CollidePoint;
                            }
                        }
                    }
                    j++;
                }
                i++;
            }
            return touches;
        }

        public virtual void Scale(float scale)
        {
            this.size *= scale;
        }

        public virtual void SetScene(Cena s)
        {
            this.scene = s;
        }
        public Sprite Scl(float scale)
        {
            this.Scale(scale);
            return this;
        }


        public virtual void Draw(GameTime gameTime)
        {
            Rectangle pos = Camera.WorldSize2PixelRectangle(this.position, this.size);
            // scene.SpriteBatch.Draw(this.image, pos, Color.White);

            scene.SpriteBatch.Draw(this.image, pos, source, Color.White,
                this.rotation, new Vector2(pixelSize.X / 2, pixelSize.Y / 2),
                SpriteEffects.None, 0);

        }

        public virtual void SetRotation(float r)
        {
            this.rotation = r;
        }

        public virtual void Update(GameTime gameTime) { state = Keyboard.GetState(); }

        public virtual void Dispose()
        {
            this.image.Dispose();
        }

        public virtual void Destroy()
        {
            if (this.nomedaSprite == "pencil" || this.nomedaSprite == "bulletTestes")
            {
                AnimatedSprite explosion;
                explosion = new AnimatedSprite(cManager, "explosion", 1, 12);
                scene.AddSprite(explosion);
                explosion.SetPosition(this.position);
                explosion.Scale(.3f);
                explosion.Loop = false;
                this.scene.RemoveSprite(this);
            }
            if (this.nomedaSprite == "TEST-F")
            {
                AnimatedSprite explosion;
                explosion = new AnimatedSprite(cManager, "explosion", 1, 12);
                scene.AddSprite(explosion);
                explosion.SetPosition(this.position);
                explosion.Scale(.9f);
                explosion.Loop = false;
                this.scene.RemoveSprite(this);
            }
            if (this.nomedaSprite == "TimComplete")
            {
                AnimatedSprite explosion;
                explosion = new AnimatedSprite(cManager, "explosion", 1, 12);
                scene.AddSprite(explosion);
                explosion.SetPosition(this.position);
                explosion.Scale(.9f);
                explosion.Loop = false;
                this.scene.RemoveSprite(this);
            }
            if (this.nomedaSprite == "student1" || this.nomedaSprite == "student2" || this.nomedaSprite == "student3")
            {
                this.scene.RemoveSprite(this);
            }
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
        public Sprite At(Vector2 p)
        {
            this.SetPosition(p);
            return this;
        }

        public KeyboardState returnKey()
        {
            return state;
        }
    }
}
