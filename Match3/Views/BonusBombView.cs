using Match3.Animations;
using Match3.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.views
{
    class BonusBombView: GemView
    {
        DrawComponent drawComponent;
        //private float intencity;

        public BonusBombView(GemComponent.TYPE gemType, int width, int height) : base(gemType, width, height)
        {
            string textureName;
            switch (gemType)
            {
                case GemComponent.TYPE.RED:
                    textureName = "gem_bonus_bomb_red";
                    break;
                case GemComponent.TYPE.BLUE:
                    textureName = "gem_bonus_bomb_blue";
                    break;
                case GemComponent.TYPE.GREEN:
                    textureName = "gem_bonus_bomb_green";
                    break;
                case GemComponent.TYPE.YELLOW:
                    textureName = "gem_bonus_bomb_yellow";
                    break;
                case GemComponent.TYPE.PURPLE:
                    textureName = "gem_bonus_bomb_purple";
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

    class BonusBombViewFocused : GemFocusedView
    {
        DrawComponent drawComponent;
        public BonusBombViewFocused(GemComponent.TYPE gemType, int width, int height) : base(gemType, width, height)
        {
            string textureName;
            switch (gemType)
            {
                case GemComponent.TYPE.RED:
                    textureName = "gem_bonus_bomb_red";
                    break;
                case GemComponent.TYPE.BLUE:
                    textureName = "gem_bonus_bomb_blue";
                    break;
                case GemComponent.TYPE.GREEN:
                    textureName = "gem_bonus_bomb_green";
                    break;
                case GemComponent.TYPE.YELLOW:
                    textureName = "gem_bonus_bomb_yellow";
                    break;
                case GemComponent.TYPE.PURPLE:
                    textureName = "gem_bonus_bomb_purple";
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

    class BonusBombDestroyedView : BonusBombView, IAnimation
    {
        private double duration;
        private TimeSpan startTime;
        int delta;
        public BonusBombDestroyedView(GemComponent.TYPE gemType, int width, int height, double duration) : base(gemType, width, height)
        {
            delta = 5;
            this.duration = duration;
        }

        public void update(GameTime gameTime)
        {
            if (startTime == TimeSpan.Zero)
                startTime = gameTime.TotalGameTime;
            else
            {
                delta = delta < 0 ? 5 : -5;
            }
        }

        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            PositionComponent p = new PositionComponent(pos.x + delta, pos.y);
            base.draw(s, p);
        }
    }
}
