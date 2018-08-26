using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkad
{
    class Tile : GameObj
    {
        private int sizeX, sizeY;
        private Color color;
        private int visible;

        public Tile(int x, int y, int sizeX, int sizeY, Color color) : base(x, y)
        { this.sizeX = sizeX; this.sizeY = sizeY; this.color = color; visible = 1; }

        public int SizeX {get{return sizeX;} }
        public int SizeY { get { return sizeY; } }
        public int Visible { get { return visible; }  set { visible = value; } }

        public override void Render(Graphics g)
        {
            if (visible==1)
            {
                SolidBrush p = new SolidBrush(color);
                g.FillRectangle(p, x, y, sizeX, sizeY);
            }
        }
    }
}
