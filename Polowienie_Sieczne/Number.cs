using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polowienie_Sieczne
{
    class Number
    {
        public Number(double nominator, double denominator)
        {
            this._nominator = nominator;
            this._denominator = denominator;
            this._value = nominator / denominator;
        }

        private double _nominator;
        private double _denominator;
        private double _value;
        public double value { get { return _value; } }

        public double nominator
        {
            get { return _nominator; }
            set
            {
                _nominator = nominator;
                value = nominator / denominator;
            }
        }
        public double denominator
        {
            get { return _denominator; }
            set
            {
                _denominator = denominator;
                value = nominator / denominator;
            }
        }
    }
}
