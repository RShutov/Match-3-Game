using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Match3.Core;
using Match3.Entities;
using Match3.Components;
using Match3.Views;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.Systems
{
    class RenderSystem
    {
        private Engine engine;

        public RenderSystem(Engine engine)
        {
            this.engine = engine;
        }

        public void Draw(SpriteBatch spriteBatch){
            foreach (var elem in engine.getNode(RenderNode.components)){
                var position = (PositionComponent)elem[typeof(PositionComponent)];
                var view = (View)elem[typeof(View)];
                view.draw(spriteBatch, position);
            };
        }
        public void update(GameTime gameTime) { }
    }
}
