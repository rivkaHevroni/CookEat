using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat.Data
{
    public class Quantity
    {
        public double Amount { get; set; }

        public Quantity(double amount)
        {
            Amount = amount;
        }

        public override string ToString()
        {
            string res = null;
            if (Amount == 0)
            {
                res += "";
            }
            else
            {
                res = Amount.ToString();
            }

            return res;
        }
    }
}
