using Match3.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3
{
    class TestField
    {
        private const GemComponent.TYPE red = GemComponent.TYPE.RED;
        private const GemComponent.TYPE blue = GemComponent.TYPE.BLUE;
        private const GemComponent.TYPE green = GemComponent.TYPE.GREEN;
        private const GemComponent.TYPE yellow = GemComponent.TYPE.YELLOW;
        private const GemComponent.TYPE purple = GemComponent.TYPE.PURPLE;
        private const TYPE primal = TYPE.PRIMAL;
        private const TYPE bomb = TYPE.BOMB;
        private const TYPE line = TYPE.LINE;

        public enum TYPE { PRIMAL, BOMB, LINE};
        public static Tuple<GemComponent.TYPE[,], TYPE[,]> LineTestField
        {
            get
            {
               
                var elem1 =  new GemComponent.TYPE[8, 8]
                {
                   {red,  blue,  red,  blue,   red,    blue,   red,    blue},
                   {blue, red,   blue, red,    blue,   green,    blue,   red },
                   {red,  blue,  red,  blue,   red,    green,   red, blue},
                   {blue, red,   blue, red,    yellow, yellow, blue,    red },
                   {red,  blue,  red,  yellow, red,    green,   red,    blue},
                   {blue, red,   blue, red,    blue,   green,    blue,   red },
                   {red,  blue,  red,  blue,   red,    blue,   red,    blue},
                   {blue, red,   blue, red,    blue,   red,    blue,   red },
                };
                var elem2 = new TYPE[8, 8]
                {
                   {primal, primal, primal, primal, primal, primal, primal, primal},
                   {line, primal, primal, primal, primal, bomb, primal, line},
                   {primal, primal, primal, line, bomb, primal, primal, primal},
                   {primal, primal, line, primal, primal, primal, bomb, primal},
                   {primal, bomb, primal, primal, primal, primal, primal, primal},
                   {primal, line, primal, primal, primal, line, primal, primal},
                   {primal, primal, primal, primal, primal, bomb, primal, primal},
                   {primal, primal, primal, primal, primal, primal, primal, line }
                };
                return new Tuple<GemComponent.TYPE[,], TYPE[,]>(elem1, elem2);
            }
        }

        public static GemComponent.TYPE[,] BombTestField
        {
            get
            {
                GemComponent.TYPE red = GemComponent.TYPE.RED;
                GemComponent.TYPE blue = GemComponent.TYPE.BLUE;
                GemComponent.TYPE green = GemComponent.TYPE.GREEN;
                GemComponent.TYPE yellow = GemComponent.TYPE.YELLOW;
                GemComponent.TYPE purple = GemComponent.TYPE.PURPLE;
                return new GemComponent.TYPE[8, 8]
                {
                   {yellow,  blue,  red,  blue,   red,     blue,red,blue},
                   {yellow, red,   blue, red,    blue,    red,blue,red},
                   {yellow,  blue,  red,  blue,   red,     blue,yellow,blue},
                   {blue, yellow,   blue, yellow, yellow,  yellow,red,red},
                   {red,  blue,  red,  blue,   red,     yellow,red,blue},
                   {blue, red,   blue, red,    blue,    red,blue,red},
                   {red,  blue,  red,  blue,   red,     blue,red,blue},
                   {blue, red,   blue, red,    blue,    red,blue,red},
                };
            }
        }

        public static GemComponent.TYPE[,] BombTestField2
        {
            get
            {
                GemComponent.TYPE red = GemComponent.TYPE.RED;
                GemComponent.TYPE blue = GemComponent.TYPE.BLUE;
                GemComponent.TYPE green = GemComponent.TYPE.GREEN;
                GemComponent.TYPE yellow = GemComponent.TYPE.YELLOW;
                GemComponent.TYPE purple = GemComponent.TYPE.PURPLE;
                return new GemComponent.TYPE[8, 8]
                {
                   {red,  blue,  red,  blue,   red,     blue,red,blue},
                   {blue, red,   blue, red,    blue,    red,blue,red},
                   {red,  blue,  red,  yellow,   red,     blue,yellow,blue},
                   {blue, red,   blue, yellow, yellow,  yellow,red,red},
                   {red,  blue,  red,  yellow,   red,     yellow,red,blue},
                   {blue, red,   blue, yellow,    blue,    red,blue,red},
                   {red,  blue,  red,  blue,   red,     blue,red,blue},
                   {blue, red,   blue, red,    blue,    red,blue,red},
                };
            }
        }

    }
}
