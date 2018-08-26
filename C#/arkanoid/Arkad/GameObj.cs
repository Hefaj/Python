using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkad
{
    public abstract class GameObj
    {
        protected int x, y;

        public int X 
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public GameObj(int x, int y){this.x = x; this.y = y;}
        public abstract void Render(System.Drawing.Graphics g);

    }
}
