using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BlocksPrototype
{
    static class Input
    {
        static Block oldHoverBlock;
        static Block hoverBlock;
        static MouseState ms = new MouseState();
        static MouseState oldMs = new MouseState();

        public static Vector2 MousePos
        {
            get { return new Vector2(ms.X, ms.Y); }
        }

        public static Vector2 WorldMP
        {
            get { return new Vector2(
                ms.X + World.cam._pos.X - (CM.graphics.Viewport.Width / 2),
                ms.Y + World.cam._pos.Y - (CM.graphics.Viewport.Height / 2));
            }
        }

        public static Block HoverBlock
        {
            get { return oldHoverBlock; }
            set
            {
                if (hoverBlock != null)
                {
                    float depth = (value.Pos.Y - hoverBlock.Pos.Y) + (hoverBlock.Pos.Z - value.Pos.Z) + (hoverBlock.Pos.X - value.Pos.X);
                    if (depth > 0)
                    {
                        hoverBlock = value;
                    }
                }
                else
                {
                    hoverBlock = value;
                }
            }
        }

        public static void Update()
        {
            ms = Mouse.GetState();

            if (ms.LeftButton == ButtonState.Pressed &&
                oldMs.LeftButton == ButtonState.Released)
            {
                if (oldHoverBlock != null)
                {
                    World.AddBlock((int)oldHoverBlock.Pos.X, (int)oldHoverBlock.Pos.Y, (int)oldHoverBlock.Pos.Z - 1);
                }
            }
        }

        public static void LateUpdate()
        {
            oldMs = ms;
            oldHoverBlock = hoverBlock;
            hoverBlock = null;
        }
    }
}
