using Match3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Components
{
    class MotionComponent{

        public delegate void OnEnd(Entity e);

        public event OnEnd onEndListeners;

        public void onEnd(){
            entity.removeComponent<MotionComponent>();
            onEndListeners?.Invoke(entity);
        }

        public int velocityX;
        public int velocityY;

        public int rotation;
        public enum MotionType { LINEAR, CUVILINEAR}
        public MotionType motionType;
        public readonly PositionComponent target;
        public PositionComponent center;
        public RotationDirection direction;
        public enum RotationDirection { CLOCKWISE, COUNTERCLOCK_WISE }
        public Entity entity;

        public MotionComponent(
            Entity e,
            int velocityX, 
            int velocityY, 
            int rotation, 
            PositionComponent target = null
            )
        {
            entity = e;
            this.rotation = rotation;
            this.velocityX = velocityX;
            this.velocityY = velocityY;
            this.target = target;
            motionType = MotionType.LINEAR;
        }
        public MotionComponent(
            Entity e,
            int velocityX,
            int velocityY,
            int rotation,
            RotationDirection direction,
            PositionComponent center,
            PositionComponent target = null
            )
        {
            entity = e;
            this.rotation = rotation;
            this.velocityX = velocityX;
            this.velocityY = velocityY;
            this.direction = direction;
            this.target = target;
            this.center = center;
            motionType = MotionType.CUVILINEAR;
        }

    }
}
