using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Components
{
    public class PositionComponent{
        public int x { get; set; }
        public int y { get; set; }
        public PositionComponent(int x, int y){
            this.x = x;
            this.y = y;
        }
    }
}
