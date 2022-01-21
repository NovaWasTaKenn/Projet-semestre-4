using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_1
{
    class Pixel
    {
        int r;
        int g;
        int b;

        public Pixel(int R, int G, int B)
        {
            this.r = R;
            this.g = G;
            this.b = B;
        }
        public int R
        {
            get { return r; }
            set { r = value; }
        }
        public int G
        {
            get { return g; }
            set { g = value; }
        }
        public int B
        {
            get { return b; }
            set { b = value; }
        }
        public override string ToString()
        {
            return "|R : "+ r+ "| G : " + g + "| B : " + b ;
        }
    }
}
