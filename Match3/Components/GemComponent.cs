using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Components
{
    public class GemComponent
    {
        private TYPE _type;
        public TYPE type { get { return _type; } }
        public enum TYPE{
            NONE,
            RED,
            BLUE,
            GREEN,
            YELLOW,
            PURPLE
        }
        public GemComponent(TYPE t){
            _type = t;
        }
    }
}
