using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BlocksPrototype
{
    static class CM
    {
        public static GraphicsDevice graphics;
        public static int halfW;
        public static int halfH;

        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D> blockTextures = new Dictionary<string, Texture2D>();

        public static void Init(ContentManager c, GraphicsDevice g)
        {
            graphics = g;
            halfW = g.Viewport.Width / 2;
            halfH = g.Viewport.Height / 2;

            DirectoryInfo dir = new DirectoryInfo(c.RootDirectory + "//" + "blocks");
            foreach (FileInfo f in dir.GetFiles())
            {
                int index = f.Name.IndexOf(f.Extension);
                blockTextures.Add(f.Name.Substring(0, index), c.Load<Texture2D>("blocks" + "//" + f.Name.Substring(0, index)));
            }
            dir = new DirectoryInfo(c.RootDirectory);
            foreach (FileInfo f in dir.GetFiles())
            {
                int index = f.Name.IndexOf(f.Extension);
                textures.Add(f.Name.Substring(0, index), c.Load<Texture2D>(f.Name.Substring(0, index)));
            }

            // fonts
            dir = new DirectoryInfo(c.RootDirectory + "//" + "fonts");
            foreach (FileInfo f in dir.GetFiles())
            {
                int index = f.Name.IndexOf(f.Extension);
                fonts.Add(f.Name.Substring(0, index), c.Load<SpriteFont>("fonts" + "//" + f.Name.Substring(0, index)));
            }
        }
    }
}