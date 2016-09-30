using Match3.Components;
using Match3.Entities;
using Match3.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Core
{
    class Engine{
        public bool ImputIsLocked;
        private List<Entity> entities;
        public RenderSystem renderSystem;
        private List<ISystem> systems;
        private Rectangle viewport; 

        public void addEntity(Entity entity){
            entities.Add(entity);
        }

        public void  addSystem(ISystem system){
            systems.Add(system);
        }

        public Engine(Rectangle viewport){
            ImputIsLocked = false;
            this.viewport = viewport;
            systems = new List<ISystem>();
            entities = new List<Entity>();
            renderSystem = new RenderSystem(this);
        }

        public void update(GameTime gameTime){
            foreach(ISystem system in systems){
                system.update(gameTime);
            }
        }

        public List<Dictionary<Type, object>> getNode(HashSet<Type> components){
            List<Dictionary<Type, object>> nodes = new List<Dictionary<Type, object>>();
            foreach (Entity e in entities){
                Dictionary<Type, object> componentDict = new Dictionary<Type, object>();
                List<Type> buf = e.getComponentsTypes();
                var comp = buf.Intersect(components);
                if (comp.Count() == components.Count()){
                    foreach(Type t in components){
                        componentDict[t] = e.getComponent(t);
                    }
                    nodes.Add(componentDict);
                }                 
            }
            return nodes;
        }

        public void init()
        { }

        public void removeEntity(Entity entity){
            entities.Remove(entity);
        }

        public void removeAll(){
            entities.Clear();
        }
    }
}
