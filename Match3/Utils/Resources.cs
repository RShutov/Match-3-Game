using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3
{
    class Resources
    {
        public static Dictionary<string, Type> getResources()
        {
            return new Dictionary<string, Type>() {
                    { "gem_red",                  typeof(Texture2D)},
                    { "gem_blue",                 typeof(Texture2D)},
                    { "gem_green",                typeof(Texture2D)},
                    { "gem_yellow",               typeof(Texture2D)},
                    { "gem_purple",               typeof(Texture2D)},
                    { "background",               typeof(Texture2D)},
                    { "gem_focus_effect_red",     typeof(Texture2D)},
                    { "gem_focus_effect_blue",    typeof(Texture2D)},
                    { "gem_focus_effect_green",   typeof(Texture2D)},
                    { "gem_focus_effect_yellow",  typeof(Texture2D)},
                    { "gem_focus_effect_purple",  typeof(Texture2D)},
                    { "MainFont",                 typeof(SpriteFont)},
                    { "ScoreFont",                typeof(SpriteFont)},
                    { "OkButtonFont",                typeof(SpriteFont)},
                    { "gem_bonus_line_red",       typeof(Texture2D)},
                    { "gem_bonus_line_blue",      typeof(Texture2D)},
                    { "gem_bonus_line_yellow",    typeof(Texture2D)},
                    { "gem_bonus_line_green",     typeof(Texture2D)},
                    { "gem_bonus_line_purple",    typeof(Texture2D)},
                    { "gem_bonus_bomb_red",       typeof(Texture2D)},
                    { "gem_bonus_bomb_blue",      typeof(Texture2D)},
                    { "gem_bonus_bomb_yellow",    typeof(Texture2D)},
                    { "gem_bonus_bomb_green",     typeof(Texture2D)},
                    { "gem_bonus_bomb_purple",    typeof(Texture2D)},
                    { "destroyer",                typeof(Texture2D)},
            };
        }
    }
}
