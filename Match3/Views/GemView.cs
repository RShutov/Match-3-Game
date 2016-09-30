using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Match3.Components;
using Microsoft.Xna.Framework;
using Match3.Views;
using Match3.Animations;

namespace Match3.views
{
    class GemView : View
    {
        DrawComponent drawComponent;
        public GemView(GemComponent.TYPE gemType, int width, int height)
        {
            string textureName;
            switch (gemType)
            {
                case GemComponent.TYPE.RED:
                    textureName = "gem_red";
                    break;
                case GemComponent.TYPE.BLUE:
                    textureName = "gem_blue";
                    break;
                case GemComponent.TYPE.GREEN:
                    textureName = "gem_green";
                    break;
                case GemComponent.TYPE.YELLOW:
                    textureName = "gem_yellow";
                    break;
                case GemComponent.TYPE.PURPLE:
                    textureName = "gem_purple";
                    break;
                default:
                    throw new Exception("Error: there is no texture for this gem type");
            }
            drawComponent = new DrawComponent(ResourceManager.Instance.getResource<Texture2D>(textureName), height, width);
        }

        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            s.Draw(drawComponent.texture, new Rectangle(pos.x, pos.y, drawComponent.width, drawComponent.height), Color.White);
        }
    }

    class GemFocusedView : GemView
    {
        DrawComponent drawComponent;
        public GemFocusedView(GemComponent.TYPE gemType, int width, int height) : base(gemType, width, height)
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


    class GemDestoroyedView : GemView, IAnimation
    {
        DrawComponent drawComponent;
        public double intencity;
        private double duration;
        private TimeSpan startTime;

        public GemDestoroyedView(GemComponent.TYPE gemType, int width, int height, double duration) : base(gemType, width, height)
        {
            string textureName;
            switch (gemType)
            {
                case GemComponent.TYPE.RED:
                    textureName = "gem_red";
                    break;
                case GemComponent.TYPE.BLUE:
                    textureName = "gem_blue";
                    break;
                case GemComponent.TYPE.GREEN:
                    textureName = "gem_green";
                    break;
                case GemComponent.TYPE.YELLOW:
                    textureName = "gem_yellow";
                    break;
                case GemComponent.TYPE.PURPLE:
                    textureName = "gem_purple";
                    break;
                default:
                    throw new Exception("Error: there is no texture for this gem type");
            }
            this.duration = duration * 1000;
            intencity = 1;
            drawComponent = new DrawComponent(ResourceManager.Instance.getResource<Texture2D>(textureName), height, width);
        }

        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            s.Draw(drawComponent.texture, new Rectangle(pos.x, pos.y, drawComponent.width, drawComponent.height), Color.White * (float)intencity);
        }

        public void update(GameTime gameTime)
        {
            if (startTime == TimeSpan.Zero)
                startTime = gameTime.TotalGameTime;
            else
            {
                var deltaTime = (gameTime.TotalGameTime - startTime).TotalMilliseconds;
                intencity = (duration  - deltaTime ) / duration; 
            }
            
        }
    }
}
