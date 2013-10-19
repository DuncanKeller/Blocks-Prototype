using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlocksPrototype
{
    class Block
    {
        public static int width;
        public static int height;

        int xi;
        int yi;
        int zi;
        int x;
        int y;
        Texture2D t;

        public Color testColor;

        public Vector3 Pos
        {
            get { return new Vector3(xi, yi, zi); }
        }

        public Block(int x, int y, int z)
        {
            xi = x;
            yi = y;
            zi = z;
            if (width == 0 || height == 0)
            {
                width = CM.blockTextures["block"].Width;
                height = CM.blockTextures["block"].Height;
            }
            t = CM.blockTextures["block"];

            this.x = (x * (width / 2)) + (y * (width / 2));
            this.y = ((y - x) * (height / 4)) + (z * height / 2);
        }

        public void Update()
        {
            //if (Collides(Input.WorldMP.X, Input.WorldMP.Y))
            //{
            //    Input.HoverBlock = this;
            //}
        }

        bool Collides(float mx, float my)
        {
            mx -= x - (width / 2);
            my -= y - (3 * (height / 4));

            if (mx < 0 || mx > width ||
                my < 0 || my > height)
            { return false; }

            double angle = 0;
            List<Vector2> points = new List<Vector2>();
            points.Add(new Vector2(width / 2, 0));
            points.Add(new Vector2(width, height / 4));
            points.Add(new Vector2(width, height - (height / 4)));
            points.Add(new Vector2(width / 2, height));
            points.Add(new Vector2(0, height - (height / 4)));
            points.Add(new Vector2(0, height / 4));

            for (int i = 0; i < points.Count; i++)
            {
                Vector2 p1 = new Vector2(
                    points[i].X - mx,
                    points[i].Y - my);
                Vector2 p2 = new Vector2(
                    points[(i + 1) % points.Count].X - mx,
                    points[(i + 1) % points.Count].Y - my);

                angle += Angle2D(p1.X, p1.Y, p2.X, p2.Y);
            }

            if (Math.Abs(angle) < Math.PI)
            {
                return false;
            }
            return true;
        }

        double Angle2D(float x1, float y1, float x2, float y2)
        {
            double dtheta, theta1, theta2;

            theta1 = Math.Atan2(y1, x1);
            theta2 = Math.Atan2(y2, x2);
            dtheta = theta2 - theta1;
            while (dtheta > Math.PI)
                dtheta -= Math.PI * 2;
            while (dtheta < -Math.PI)
                dtheta += Math.PI * 2;

            return (dtheta);
        }

        public void Draw(SpriteBatch sb)
        {
            //off-screen
            if (x < World.cam._pos.X - (CM.halfW)) { return; }
            if (y < World.cam._pos.Y - (CM.halfH)) { return; }
            if (x > World.cam._pos.X + (CM.halfW)) { return; }
            if (y > World.cam._pos.Y + (CM.halfH)) { return; }
            // check if block is behind another block / culling
            if (World.BlockCulled(xi, yi, zi)) { return; }

            sb.Draw(t, new Rectangle(x - (width / 2), y - (3 * (height / 4)),
                width, height), testColor);

            if (Input.HoverBlock == this)
            {
                sb.Draw(CM.textures["selector"],
                    new Rectangle(x - (width / 2), y - (3 * (height / 4)),
                width, CM.textures["selector"].Height), Color.White);            
            }

        }
    }
}
