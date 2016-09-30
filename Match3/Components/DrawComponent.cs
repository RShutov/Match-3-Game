using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Components
{
    class DrawComponent{
        public Texture2D texture { get; set; }
        public int width;
        public int height;
        public ColorWriteChannels color;

        public DrawComponent(Texture2D texture, int height, int width){
            this.height = height;
            this.width = width;
            this.texture = texture;
        }
    }
}
