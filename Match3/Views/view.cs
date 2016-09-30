using Match3.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Views
{
    public abstract class View
    {
        public abstract void draw(SpriteBatch s, PositionComponent pos);
    }

    public abstract class Animation: View
    {
        public abstract void update(GameTime t);
    }
}
