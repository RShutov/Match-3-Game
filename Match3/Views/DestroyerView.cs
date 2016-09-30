using Match3.Components;
using Match3.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.views
{
    class DestroyerView: View
    {

        DrawComponent drawComponent;
        Destroyer.Direction direction;

        public DestroyerView(int width, int height, Destroyer.Direction direction) 
        {
            drawComponent = new DrawComponent(ResourceManager.Instance.getResource<Texture2D>("destroyer"), height, width);
            this.direction = direction;
        }

        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            switch(direction)
            {
                case Destroyer.Direction.LEFT:
                    s.Draw(
                    drawComponent.texture,
                    new Rectangle(pos.x, pos.y, drawComponent.width, drawComponent.height),
                    null,
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0);
                    break;
                case Destroyer.Direction.BOTTOM:
                    s.Draw(
                    drawComponent.texture,
                    new Rectangle(pos.x, pos.y + drawComponent.width, drawComponent.width, drawComponent.height),
                    null,
                    Color.White,
                    -(float)Math.PI / 2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0);
                    break;
                case Destroyer.Direction.RIGHT:
                    s.Draw(
                    drawComponent.texture,
                    new Rectangle(pos.x + drawComponent.width, pos.y + drawComponent.height, drawComponent.width, drawComponent.height),
                    null,
                    Color.White,
                    (float)Math.PI,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0);
                    break;
                case Destroyer.Direction.TOP:
                    s.Draw(
                    drawComponent.texture,
                    new Rectangle(pos.x + drawComponent.width, pos.y, drawComponent.width, drawComponent.height),
                    null,
                    Color.White,
                    (float)Math.PI / 2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0);
                    break;
            }
        }
    }
}
