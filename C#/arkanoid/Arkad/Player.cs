using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkad
{
    class Player:GameObj
    {
        private int sizeX, sizeY, width, speedPaddle;

        public Player(int x, int y, int sizeX, int sizeY, int width, int speedPaddle) : base(x, y)
        { this.sizeX = sizeX; this.sizeY = sizeY; this.width = width; this.speedPaddle = speedPaddle; }

        public int SizeX { get { return sizeX; } }
        public int SizeY { get { return sizeY; } }

        public void Move(int horizontal)
        {
            if (horizontal < 0 && this.X >= 0)
                this.X += speedPaddle * horizontal;
            else if (horizontal > 0 && this.X + sizeX <= width)
                this.X += speedPaddle * horizontal;
        }

        public override void Render(Graphics g)
        { 
            SolidBrush p = new SolidBrush(Color.Black);
            g.FillRectangle(p, x, y, sizeX, sizeY);
        }

    }
}
