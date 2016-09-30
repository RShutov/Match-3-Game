using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Match3.Core;
using Match3.Entities;
using Match3.Components;

namespace Match3.Systems
{
    class MotionSystem : ISystem
    {
        private Engine engine;

        public MotionSystem(Engine e){
            engine = e;
        }

        public void update(GameTime time){
            foreach (var node in engine.getNode(MotionNode.components)){
                var position = (PositionComponent)node[typeof(PositionComponent)];
                var motion = (MotionComponent)node[typeof(MotionComponent)];
                if (motion.target != null){
                    if (
                        Math.Abs(position.x - motion.target.x) < GameConstants.POSITION_DELTA && 
                        Math.Abs(position.y - motion.target.y) < GameConstants.POSITION_DELTA  
                    )
                    {
                        position.x = motion.target.x;
                        position.y = motion.target.y;
                        motion.onEnd();
                        continue;
                    }
                    if (motion.motionType == MotionComponent.MotionType.CUVILINEAR){     
                        int rotation = motion.direction == MotionComponent.RotationDirection.CLOCKWISE ? 1 : -1;
                        float angle = (float)(rotation * Math.PI * motion.rotation / 180.0);
                        Point p = new Point(
                             position.x - motion.center.x,
                             position.y - motion.center.y
                        );
                        position.x = motion.center.x + (int)Math.Round(p.X * Math.Cos(angle) - p.Y * Math.Sin(angle));
                        position.y = motion.center.y + (int)Math.Round(p.Y * Math.Cos(angle) + p.X * Math.Sin(angle));
                    }else{
                        position.x += motion.velocityX;
                        position.y += motion.velocityY;
                    }
                }
            }
        }
    }
}