using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Entities
{
    public class EntityStateManager
    {
        private Dictionary<string, EntityState> states;
        private EntityState currState;
        private string currStateName;
        private Entity entity;

        public string getCurrStateName(){
            return currStateName;
        }
        public EntityState getCurrState(){
            return currState; 
        }

        public EntityStateManager(Entity e){
            entity = e;
            states = new Dictionary<string, EntityState>();
        }

        public void changeState(string name){
            if (!states.ContainsKey(name))
                throw new Exception("Error: Wrong state name");
            if (currState == null)
            {
                currState = states[name];
                currStateName = name;
                foreach (var elem in currState.components){
                    entity.addComponent(elem.Key, elem.Value);
                }
                return;
            }
           
            EntityState newState = states[name];
            var componentTypes = newState.components.Keys.Intersect(currState.components.Keys);
            List<Type> toDelete = currState.components.Keys.Except(newState.components.Keys).ToList();
            List<Type> toAdd = newState.components.Keys.Except(currState.components.Keys).ToList();

            foreach(Type t in toDelete){
                entity.removeComponent(t);
            }
            foreach (Type t in componentTypes){
                entity.removeComponent(t);
            }

            foreach (Type t in toAdd){
                entity.addComponent(t, newState.components[t]);
            }
            foreach (Type t in componentTypes){
                entity.addComponent(t, newState.components[t]);
            }
            currState = newState;
            currStateName = name;
        }

        public EntityState createState(string name){          
            EntityState es = new EntityState(name);
            states[name] = es;
            return es;
        }
    }
}
