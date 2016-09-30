using Match3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Match3.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Match3.views
{
    class BackgroundView : View
    {
        DrawComponent drawComponent;
        public BackgroundView(string textureName, int width, int height)
        {
            drawComponent = new DrawComponent(ResourceManager.Instance.getResource<Texture2D>(textureName), height, width);
        }
        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            s.Draw(drawComponent.texture, new Rectangle(pos.x, pos.y, drawComponent.width, drawComponent.height), Color.White);
        }
    }
}
