using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TPJ_ProjetoFinal
{
    class Alunos : AnimatedSprite
    {
        ContentManager Content;

        public Alunos(ContentManager content, String textureName, Vector2 position)
            : base(content, textureName, 1, 1)
        {
            this.Content = content;
            this.position = position;
            this.Scale(0.7f);
            this.EnableCollisions();
        }
    }
}
