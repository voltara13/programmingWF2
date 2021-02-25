using System;
using System.Collections.Generic;

namespace programmingWF2
{
    /*Специальный класс, созданный для сериализации*/
    [Serializable]
    public class Data
    {
        internal List<Cleaning> ServicesArr = new List<Cleaning>();
        internal string text;
        internal double Balance { get; set; }
        internal string priceAll { get; set; }
        internal string priceMonth { get; set; }
        internal string priceToday { get; set; }
        internal string priceAvg { get; set; }
        internal string clientAll { get; set; }
        internal string clientMonth { get; set; }
        internal string clientToday { get; set; }
        internal string clientAvg { get; set; }
        internal string costsAll { get; set; }
        internal string costsMonth { get; set; }
    }
}