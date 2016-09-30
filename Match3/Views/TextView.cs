using Match3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Match3.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Match3.views
{
    class TextView : View
    {
        private string text;
        private string fontName;
        public void textChange(string text)
        {
            this.text = text;
        }

        public TextView(string text, string fontName)
        {
            this.text = text;
            this.fontName = fontName;
        }

        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            s.DrawString(ResourceManager.Instance.getResource<SpriteFont>(fontName), text, new Vector2(pos.x, pos.y), Color.White);
        }
    }
    class TiteledTextView : TextView
    {
        private string title;
        private string fontName;

        public TiteledTextView(string title, string text, string textFontName, string titleFontName) : base(text, textFontName)
        {
            this.title = title;
            fontName = titleFontName;
        }

        public override void draw(SpriteBatch s, PositionComponent pos)
        {
            base.draw(s, pos);
            s.DrawString(ResourceManager.Instance.getResource<SpriteFont>(fontName), title, new Vector2(pos.x, pos.y), Color.White);
        }
    }
}
