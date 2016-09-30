using Match3.Components;
using Match3.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Animations
{
    interface IAnimation{
        void update(GameTime gameTime);
    }

    interface IDelay{
        void update(GameTime gameTime);
        void end();
    }

}
