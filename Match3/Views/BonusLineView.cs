using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Match3.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Match3.views
{
    class BonusLineView : GemView
    {
        DrawComponent drawComponent;

        public BonusLine.Orientation orientation;

        public BonusLineView(GemComponent.TYPE gemType, int width, int height, BonusLine.Orientation orientation) : base(gemType, width, height)
        {
            string textureName;
            switch (gemType)
            {
                case GemComponent.TYPE.RED:
                    textureName = "gem_bonus_line_red";
                    break;
                case GemComponent.TYPE.BLUE:
                    textureName = "gem_bonus_line_blue";
                    break;
                case GemComponent.TYPE.GREEN:
                    textureName = "gem_bonus_line_green";
                    break;
                case GemComponent.TYPE.YELLOW:
                    textureName = "gem_bonus_line_yellow";
                    break;
                case GemComponent.TYPE.PURPLE:
                    textureName = "gem_bonus_line_purple";
                    break;
                default:
                    throw new Exception("Error: there is no texture for this gem type");
            }
            this.orientation = orientation;
            drawComponent = new DrawComponent(ResourceManager.Instance.getResource<Texture2D>(textureName), height, width);
        }
        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            base.draw(s, pos);
            if (orientation == BonusLine.Orientation.HORIZONTAL)
                s.Draw(drawComponent.texture, new Rectangle(pos.x, pos.y, drawComponent.width, drawComponent.height), Color.White);
            else
                s.Draw(
                    drawComponent.texture,
                    new Rectangle(pos.x + drawComponent.width, pos.y, drawComponent.width, drawComponent.height),
                    null,
                    Color.White,
                    (float)Math.PI / 2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0);

        }
    }

    class BonusLineViewFocused : BonusLineView
    {
        DrawComponent drawComponent;
        public BonusLineViewFocused(GemComponent.TYPE gemType, int width, int height, BonusLine.Orientation orientation) : base(gemType, width, height, orientation)
        {
            string textureName;
            switch (gemType)
            {
                case GemComponent.TYPE.RED:
                    textureName = "gem_focus_effect_red";
                    break;
                case GemComponent.TYPE.BLUE:
                    textureName = "gem_focus_effect_blue";
                    break;
                case GemComponent.TYPE.GREEN:
                    textureName = "gem_focus_effect_green";
                    break;
                case GemComponent.TYPE.YELLOW:
                    textureName = "gem_focus_effect_yellow";
                    break;
                case GemComponent.TYPE.PURPLE:
                    textureName = "gem_focus_effect_purple";
                    break;
                default:
                    throw new Exception("Error: there is no texture for this gem type");
            }
            drawComponent = new DrawComponent(ResourceManager.Instance.getResource<Texture2D>(textureName), height, width);
        }
        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            base.draw(s, pos);
            s.Draw(drawComponent.texture, new Rectangle(pos.x, pos.y, drawComponent.width, drawComponent.height), Color.White);
        }
    }
}
