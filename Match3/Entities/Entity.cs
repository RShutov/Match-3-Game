using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Match3.Components;

namespace Match3.Entities
{

    public class Entity
    {
        private Dictionary<Type, object> components;
        

        public void removeComponent(Type t){
            components.Remove(t);
        }

        public void removeComponent<T>(){
            components.Remove(typeof(T));
        }
        public void addComponent(Type t, object o){
            components[t] = o;
        }

        public void addComponent<T>(object o){
            components[typeof(T)] = o;
        }

        public Entity(){
            components = new Dictionary<Type, object>() ;
        }
        
        public bool haveComponent(Type t){
            return components.ContainsKey(t);
        }

        public T getComponent<T>(){
            return (T)components[typeof(T)];
        }

        public object getComponent(Type t){
            return components[t];
        }

        public List<Type> getComponentsTypes(){
            return components.Keys.ToList();
        }
    }


    public class EntityState{
        public string name;
        public Dictionary<Type, object>  components;

        public EntityState(string name){
           this.name = name;
            components = new Dictionary<Type, object>();
        }

        public bool hasComponent(Type t){
            return components.ContainsKey(t);
        }
        public object component(Type t){
            if (!components.ContainsKey(t))
                throw new Exception("Error: Entity does not contain component of type " + t.ToString());
            return components[t];
        }

        public EntityState add(Type t, object component){
            components[t] = component;
            return this;
        }
        public EntityState add<T>(object component){
            components[typeof(T)] = component;
            return this;
        }
    }
}
