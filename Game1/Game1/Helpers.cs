using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers
{
    public enum ActiveScreenState { OPENING, PLAY, PAUSE, ENDING };
    public static class Helper
    {
        
        public static SpriteFont GameFont;
        public static GraphicsDevice graphicsDevice;
        public static Vector2 WorldBounds;
    }
}
