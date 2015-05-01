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
        
        // Construtor
        public Player(ContentManager content, String textureName)
            : base(content, textureName, 2, 27)
        {
        }

        // Draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        // Update
        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D))
            {
                pressedKey = 4;
                this.position.X += 0.1f; 
            }
            if (state.IsKeyDown(Keys.A))
            {
                pressedKey = 1;
                this.position.X -= 0.1f;
            }
            if (state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D))
            {
                pressedKey = 0;
            }
            base.Update(gameTime);
        }

        public static int returnKey()
        {
            return pressedKey;
        }
    }
}
