using Match3.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Components
{
    class MouseInteractionComponent
    {
        public int width;
        public int height;
        public Entity entity;
        public delegate void OnClick(Entity e);

        public event OnClick onCLickListeners;

        public void onClick(){
            onCLickListeners?.Invoke(entity);
        }
        public MouseInteractionComponent(int width, int height, Entity e){
            this.height = height;
            this.width = width;
            entity = e;
        }
    }
}
