using Match3.Animations;
using Match3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Match3.Components
{
    class Delay: IDelay{
        public delegate void OnTimeout(Entity e);
        public TimeSpan startTime;
        private int duration;
        private Entity entity;

        public event OnTimeout onEndListeners;

        public void end(){   
            onEndListeners(entity);
            entity.removeComponent<IDelay>();
        }
        public Delay(int duration, Entity e){
            this.duration = duration;
            entity = e;
        }
        public void update(GameTime gameTime){
            if (startTime == TimeSpan.Zero)
                startTime = gameTime.TotalGameTime;
            if ((gameTime.TotalGameTime - startTime).TotalMilliseconds >= duration )
                end();
        }
    }
}
