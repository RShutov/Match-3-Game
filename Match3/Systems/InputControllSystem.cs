using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Match3.Core;
using Match3.Components;
using Match3.Entities;

namespace Match3.Systems
{
    class InputControllSystem : ISystem
    {
        private MouseState mouseStatePrevious;
        private Engine engine;

        public InputControllSystem(Engine e){
            engine = e;
            mouseStatePrevious = Mouse.GetState();
        }

        public void update(GameTime time){
            var mouseStateCurrent = Mouse.GetState();
            var mousePosition = new Point(mouseStateCurrent.X, mouseStateCurrent.Y);
            if (engine.ImputIsLocked)
                return;
            foreach (var obj in engine.getNode(MouseInteractionNode.components)){
                var position = (PositionComponent)obj[typeof(PositionComponent)];
                var bounds = (MouseInteractionComponent)obj[typeof(MouseInteractionComponent)];
                Rectangle r = new Rectangle(position.x, position.y, bounds.width, bounds.height);
                if (r.Contains(mousePosition)){
                    if (mouseStatePrevious.LeftButton == ButtonState.Pressed && 
                        mouseStateCurrent.LeftButton == ButtonState.Released )
                    {
                        bounds.onClick();
                    }
                }
            }
            mouseStatePrevious = mouseStateCurrent;
        }
    }
}
