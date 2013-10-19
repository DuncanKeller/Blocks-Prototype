using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlocksPrototype
{
    static class World
    {
        static int widthX;
        static int widthY;
        static int height;
        static Block[, ,] tiles;

        public static Camera cam = new Camera();

        public static void Init()
        {
            widthX = 100;
            widthY = 100;
            height = 25;
            tiles = new Block[widthX, widthY, height];

            for (int x = 0; x < widthX; x++)
            {
                for (int y = 0; y < widthY; y++)
                {
                    for (int z = height - 1; z >= height - 20; z--)
                    {
                        tiles[x, y, z] = new Block(x, y, z);
                        if (z == height - 4)
                        {
                            tiles[x, y, z].testColor = Color.LightGreen;
                        }
                        else
                        {
                            tiles[x, y, z].testColor = Color.SaddleBrown;
                        }
                    }
                }
            }

            cam.Pos = new Vector2(200, 300);
        }

        public static void AddBlock(int x, int y, int z)
        {
            tiles[x, y, z] = new Block(x, y, z);
            tiles[x, y, z].testColor = Color.LightBlue;
        }

        public static bool BlockExists(int x, int y, int z)
        {
            return tiles[x, y, z] != null;
        }

        public static bool BlockCulled(int x, int y, int z)
        {
            for (int i = z - 1; i >= 0; i--)
            {
                int x2 = x - (z - i);
                int y2 = y + (z - i);
                if (x2 < 0 || x2 >= widthX)
                { return false; }
                if (y2 < 0 || y2 >= height)
                { return false; }

                if (tiles[x2, y2, i] != null)
                { return true; }
            }
            return false;
        }

        public static void Update()
        {
            return;
            for (int z = height - 1; z >= 0; z--)
            {
                for (int y = 0; y < widthY; y++)
                {
                    for (int x = widthX - 1; x >= 0; x--)
                    {
                        if (tiles[x, y, z] != null)
                        {
                            tiles[x, y, z].Update();
                        }
                    }
                }
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.LinearClamp, DepthStencilState.Default,
                RasterizerState.CullNone, null, cam.get_transformation(CM.graphics));
            
            for (int z = height - 1; z >= 0; z--)
            {
                for (int y = 0; y < widthY; y++)
                {
                    for (int x = widthX - 1; x >= 0; x--)
                    {
                        if (tiles[x, y, z] != null)
                        {
                            tiles[x, y, z].Draw(sb);
                        }
                    }
                }
            }

            sb.End();
        }
    }
}
