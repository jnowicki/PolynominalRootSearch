using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polowienie_Sieczne
{
    class Function
    {
        public double val { get; set; }
        public double arg { get; set; }
        public Function(double x)
        {
            this.val = Math.Exp(x) - Math.Tan(x);
            this.arg = x;
        }
        public Function()
        {
            this.val = 0;
            this.arg = 0;
        }
        public double modulus() { if (this.val > 0) return val; else return val * (-1); }
        public double modulusArg() { if (this.arg > 0) return arg; else return arg * (-1); }
    }

}
