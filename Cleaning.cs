namespace programmingWF2
{
    abstract class Cleaning
    {
        internal bool type;
        internal string service;
        internal double price;

        internal Cleaning(double price, string service, bool type)
        {
            if (price < 0) 
                throw new System.FormatException();
            this.type = type;
            this.service = service;
            this.price = price;
        }
        internal abstract void ChangeBalance();
    }
}
