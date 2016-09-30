using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Match3
{
    class ResourceManager
    {
        Dictionary<string, Tuple<Type, object>> resources;
        private ContentManager contextContent;
        private static ResourceManager instance;

        public static ResourceManager init(ContentManager Content)
        {
            if (instance == null)
            {
                instance = new ResourceManager(Content);
            }
            return instance;
        }
        public static ResourceManager Instance
        {
            get
            {
                return instance;
            }
        }
        private ResourceManager(ContentManager Content)
        {
            contextContent = Content;
            resources = new Dictionary<string, Tuple<Type, object>>();
        }

        public T getResource<T>(string name)
        {
            if (typeof(T) == resources[name].Item1)
                return (T)resources[name].Item2;
            else
                throw new Exception("Error: wrong type");
        }

        public void LoadResource<T>(string name)
        {
            if (!resources.Keys.Contains(name))
                resources.Add(name, new Tuple<Type, object>(typeof(T), contextContent.Load<T>(name)));

        }
        public void LoadResource(Dictionary<string, Type> objects)
        {
            foreach (KeyValuePair<string, Type> p in objects)
            {
                MethodInfo method = contextContent.GetType().GetMethod("Load").MakeGenericMethod(new Type[] { p.Value });
                object o = method.Invoke(contextContent, new object[] { p.Key });
                if (!resources.Keys.Contains(p.Key))
                    resources.Add(p.Key, new Tuple<Type, object>(p.Value, o));
            }
        }
          
    }
}
