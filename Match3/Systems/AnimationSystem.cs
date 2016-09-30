using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Match3.Core;
using Match3.Entities;
using Match3.Animations;

namespace Match3.Systems
{
    class AnimationSystem : ISystem
    {
        private Engine engine;

        public AnimationSystem(Engine e){
            engine = e;
        }

        public void update(GameTime time){
            foreach(var a in engine.getNode(AnimationNode.components))
            {
                ((IAnimation)a[typeof(IAnimation)]).update(time);
            }
        }
    }
}
