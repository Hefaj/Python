using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkad
{
    class Ball : GameObj
    {

        private int vx, vy, radius,
            height, width;

        public Ball(int x, int y, int vx, int vy, int radius, int height, int width) : base(x, y)
        { this.vx = vx; this.vy = vy; this.radius = radius; this.height = height; this.width = width; }

        public int Radius { get { return radius; } }
        public int Vx { get { return vx; } set { vx = value; } }
        public int Vy { get { return vy; } set { vy = value; } }

        public void Move()
        {
            if (this.x - radius / 2 <= 0 || this.x + radius >= width)
                this.vx = -this.vx;

            if (this.y - radius / 2 <= 0)
                this.vy = -this.vy;

            this.x += vx;
            this.y += vy;
        }

        public override void Render(Graphics g)
        {
            SolidBrush p = new SolidBrush(Color.Black);
            g.FillEllipse(p, x, y, radius, radius);
        }
    }
}
