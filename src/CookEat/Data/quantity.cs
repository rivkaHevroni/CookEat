namespace CookEat
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
