using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPJ_ProjetoFinal
{
    class Plataformas : Sprite
    {
        public Plataformas(ContentManager content)
            : base(content, "Ground")
        {
            this.position = new Vector2(0f, -5.93f);
            this.Scale(200);
            this.EnableCollisions();
        }
    }
}
