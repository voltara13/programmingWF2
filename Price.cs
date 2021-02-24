using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programmingWF2
{
    class Price : Cleaning
    {
        internal enum Service
        {
            Clothes,
            Bedding,
            Toys,
            Bags,
            Laundry
        }
        internal Price(double price, Service service) : base(price, StringService(service), true) {}
        private static string StringService(Service service)
        {
            switch (service)
            {
                case Service.Clothes:
                    return "Чистка одежды из замши, меха, кожи, текстиля, пуховиков";
                case Service.Bedding:
                    return "Чистка постельных принадлежностей, ковров";
                case Service.Toys:
                    return "Чистка игрушек";
                case Service.Bags:
                    return "Ручная чистка сумок и обуви";
                case Service.Laundry:
                    return "Прачечные услуги";
                default: 
                    return "";
            }
        }
        internal override void ChangeBalance()
        {
            MainWindow.balance += price;
        }
    }
}
