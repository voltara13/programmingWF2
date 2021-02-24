using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace programmingWF2
{
    class Costs : Cleaning
    {
        internal enum Service
        {
            Labor, 
            Rent,
            Depreciation,
            Utilities,
            Advertising,
            Accounting,
            Purchasing,
            Incidental
        }

        internal Costs(double price, Service service) : base(price, StringService(service), false) {}

        private static string StringService(Service service)
        {
            switch (service)
            {
                case Service.Labor:
                    return "Фонд оплаты труда";
                case Service.Rent:
                    return "Аренда";
                case Service.Depreciation:
                    return "Амортизация";
                case Service.Utilities:
                    return "Коммунальные услуги";
                case Service.Advertising:
                    return "Реклама";
                case Service.Accounting:
                    return "Бухгалтерия";
                case Service.Purchasing:
                    return "Закупка спец. средств";
                case Service.Incidental:
                    return "Непредвиденные расходы";
                default:
                    return "";
            }
        }

        internal override void ChangeBalance()
        {
            MainWindow.balance -= price;
        }
    }
}
