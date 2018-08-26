using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Arkad
{
    class Hp : GameObj
    {

        private int vy, radius,
                    height, width, visible;

        public Hp(int x, int y, int vy, int radius, int height, int width) : base(x, y)
        { this.vy = vy; this.radius = radius; this.height = height; this.width = width; this.visible = 1; }

        public int Visible { get { return visible; } set { visible = value; } }

        public int Radius {
            get {return radius;}    
        }

        public void Move()
        {
            this.y += vy;
        }

        public override void Render(Graphics g)
        {
            if (visible == 1)
            {
                SolidBrush p = new SolidBrush(Color.Yellow);
                g.FillEllipse(p, x, y, radius, radius);
            }
        }
    }
}
