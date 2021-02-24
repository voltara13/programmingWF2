using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace programmingWF2
{
    public partial class MainWindow : Form
    {
        private List<Cleaning> ServicesArr = new List<Cleaning>();
        private static Random rand = new Random();
        internal static double balance = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void resetPrice()
        {
            checkBoxPrice1.Checked = false;
            checkBoxPrice2.Checked = false;
            checkBoxPrice3.Checked = false;
            checkBoxPrice4.Checked = false;
            checkBoxPrice5.Checked = false;
            textBoxPrice1.Text = "";
            textBoxPrice2.Text = "";
            textBoxPrice3.Text = "";
            textBoxPrice4.Text = "";
            textBoxPrice5.Text = "";
            resetServicesArr(true);
        }
        private void resetCosts()
        {
            checkBoxCosts1.Checked = false;
            checkBoxCosts2.Checked = false;
            checkBoxCosts3.Checked = false;
            checkBoxCosts4.Checked = false;
            checkBoxCosts5.Checked = false;
            checkBoxCosts6.Checked = false;
            checkBoxCosts7.Checked = false;
            checkBoxCosts8.Checked = false;
            textBoxCosts1.Text = "";
            textBoxCosts2.Text = "";
            textBoxCosts3.Text = "";
            textBoxCosts4.Text = "";
            textBoxCosts5.Text = "";
            textBoxCosts6.Text = "";
            textBoxCosts7.Text = "";
            textBoxCosts8.Text = "";
            resetServicesArr(false);
        }
        private void resetServicesArr(bool flag)
        {
            ServicesArr.RemoveAll(x => x.type == flag);
        }
        private void buttonPriceAccept_Click(object sender, EventArgs e)
        {
            try
            {
                resetServicesArr(true);
                if (checkBoxPrice1.Checked)
                    ServicesArr.Add(
                        new Price(
                            Convert.ToDouble(textBoxPrice1.Text.Replace('.', ',')),
                            Price.Service.Clothes));
                if (checkBoxPrice2.Checked)
                    ServicesArr.Add(
                        new Price(
                            Convert.ToDouble(textBoxPrice2.Text.Replace('.', ',')),
                            Price.Service.Bedding));
                if (checkBoxPrice3.Checked)
                    ServicesArr.Add(
                        new Price(
                            Convert.ToDouble(textBoxPrice3.Text.Replace('.', ',')),
                            Price.Service.Toys));
                if (checkBoxPrice4.Checked)
                    ServicesArr.Add(
                        new Price(
                            Convert.ToDouble(textBoxPrice4.Text.Replace('.', ',')),
                            Price.Service.Bags));
                if (checkBoxPrice5.Checked)
                    ServicesArr.Add(
                        new Price(
                            Convert.ToDouble(textBoxPrice5.Text.Replace('.', ',')),
                            Price.Service.Laundry));
                MessageBox.Show("Изменения успешно внесены");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введены неверные значения");
                resetServicesArr(true);
            }
        }
        private void buttonPriceReset_Click(object sender, EventArgs e)
        {
            resetPrice();
        }
        private void buttonCostsAccept_Click(object sender, EventArgs e)
        {
            try
            {
                resetServicesArr(false);
                if (checkBoxCosts1.Checked)
                    ServicesArr.Add(
                        new Costs(
                            Convert.ToDouble(textBoxCosts1.Text.Replace('.', ',')),
                            Costs.Service.Labor));
                if (checkBoxCosts2.Checked)
                    ServicesArr.Add(
                        new Costs(
                            Convert.ToDouble(textBoxCosts2.Text.Replace('.', ',')),
                            Costs.Service.Rent));
                if (checkBoxCosts3.Checked)
                    ServicesArr.Add(
                        new Costs(
                            Convert.ToDouble(textBoxCosts3.Text.Replace('.', ',')),
                            Costs.Service.Depreciation));
                if (checkBoxCosts4.Checked)
                    ServicesArr.Add(
                        new Costs(
                            Convert.ToDouble(textBoxCosts4.Text.Replace('.', ',')),
                            Costs.Service.Utilities));
                if (checkBoxCosts5.Checked)
                    ServicesArr.Add(
                        new Costs(
                            Convert.ToDouble(textBoxCosts5.Text.Replace('.', ',')),
                            Costs.Service.Advertising));
                if (checkBoxCosts6.Checked)
                    ServicesArr.Add(
                        new Costs(
                            Convert.ToDouble(textBoxCosts6.Text.Replace('.', ',')),
                            Costs.Service.Accounting));
                if (checkBoxCosts7.Checked)
                    ServicesArr.Add(
                        new Costs(
                            Convert.ToDouble(textBoxCosts7.Text.Replace('.', ',')),
                            Costs.Service.Purchasing));
                if (checkBoxCosts8.Checked)
                    ServicesArr.Add(
                        new Costs(
                            Convert.ToDouble(textBoxCosts8.Text.Replace('.', ',')),
                            Costs.Service.Incidental));
                MessageBox.Show("Изменения успешно внесены");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введены неверные значения");
                resetServicesArr(false);
            }
        }
        private void buttonCostsReset_Click(object sender, EventArgs e)
        {
            resetCosts();
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            var listPrice = (ServicesArr.Where(elm => elm.type)).ToList();
            var listCosts = (ServicesArr.Where(elm => !elm.type)).ToList();
            if (listPrice.Count == 0 || listCosts.Count == 0)
            {
                MessageBox.Show("Перед моделированием введите хотя бы по одной позиции прайс-листа и расходов");
                return;
            }

            double day = Convert.ToDouble(numericUpDown.Value);
            double sumPriceAll = 0;
            double sumCostsAll = 0;
            double sumPriceMonth = 0;
            int sumClientAll = 0;
            int sumClientMonth = 0;

            richTextBox1.Text = "СИМУЛЯЦИЯ НАЧАЛАСЬ\n\n";
            for (int i = 1; i <= day; i++)
            {
                int j = 0;
                double sumPriceDay = 0;
                richTextBox1.AppendText($"ДЕНЬ {i}:\n");
                for (; j < rand.Next(0, 10); j++)
                {
                    var selectElm = listPrice[rand.Next(0, listPrice.Count)];
                    richTextBox1.AppendText($"Услуга: {selectElm.service}, цена: {selectElm.price} руб.\n");
                    sumPriceDay += selectElm.price;
                    selectElm.ChangeBalance();
                }
                richTextBox1.AppendText($"Выручка за день: {sumPriceDay}, количество клиентов за день: {j}\n");
                sumPriceMonth += sumPriceDay;
                sumClientMonth += j;
                labelIncomeAll.Text = $"{sumPriceDay} руб.";
                labelClientToday.Text = $"{j} чел.";
                if (i % 30 != 0) continue;

                double sumCostsMonth = 0;
                richTextBox1.AppendText($"КОНЕЦ {i / 30} МЕСЯЦА\nРасходы:\n");
                foreach (var selectElm in listCosts)
                {
                    richTextBox1.AppendText($"{selectElm.service}: {selectElm.price} руб.\n");
                    sumCostsMonth += selectElm.price;
                    selectElm.ChangeBalance();
                }

                richTextBox1.AppendText($"\nСуммарно расходов за месяц: {sumCostsMonth}\n" +
                                        $"Суммарно доходов за месяц: {sumPriceMonth}\n" +
                                        $"Суммарно клиентов за месяц: {sumClientMonth}\n" +
                                        $"Суммарная прибыль за месяц: {sumPriceMonth - sumCostsMonth}\n");

                labelIncomeMonth.Text = $"{sumPriceMonth} руб.";
                labelCostsMonth.Text = $"{sumCostsMonth} руб.";
                labelClientMonth.Text = $"{sumCostsMonth} руб.";

                sumPriceAll += sumPriceMonth;
                sumCostsAll += sumCostsMonth;
                sumClientAll += sumClientMonth;
                sumPriceMonth = 0;
                sumClientMonth = 0;
            }

            richTextBox1.AppendText("\nКОНЕЦ СИМУЛЯЦИИ\n" +
                                    $"Суммарно расходов за всё время: {sumCostsAll}\n" +
                                    $"Суммарно доходов за всё время: {sumPriceAll}\n" +
                                    $"Суммарно клиентов за всё время: {sumClientAll}\n" +
                                    $"Суммарная прибыль за всё время: {sumPriceAll - sumCostsAll}");

            labelIncomeAll.Text = $"{sumCostsAll} руб.";
            labelClientAll.Text = $"{sumClientAll} чел.";
            labelIncomeAvg.Text = $"{Math.Round(sumCostsAll / day)} руб.";
            labelClientAvg.Text = $"{Math.Round(sumClientAll / day)} чел.";
            labelBalance.Text = $"{balance} руб.";
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void checkBoxPrice1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPrice1.Enabled = checkBoxPrice1.Checked;
        }
        private void checkBoxPrice2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPrice2.Enabled = checkBoxPrice2.Checked;
        }
        private void checkBoxPrice3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPrice3.Enabled = checkBoxPrice3.Checked;
        }
        private void checkBoxPrice4_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPrice4.Enabled = checkBoxPrice4.Checked;
        }
        private void checkBoxPrice5_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPrice5.Enabled = checkBoxPrice5.Checked;
        }
        private void checkBoxCosts1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCosts1.Enabled = checkBoxCosts1.Checked;
        }
        private void checkBoxCosts2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCosts2.Enabled = checkBoxCosts2.Checked;
        }
        private void checkBoxCosts3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCosts3.Enabled = checkBoxCosts3.Checked;
        }
        private void checkBoxCosts4_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCosts4.Enabled = checkBoxCosts4.Checked;
        }
        private void checkBoxCosts5_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCosts5.Enabled = checkBoxCosts5.Checked;
        }
        private void checkBoxCosts6_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCosts6.Enabled = checkBoxCosts6.Checked;
        }
        private void checkBoxCosts7_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCosts7.Enabled = checkBoxCosts7.Checked;
        }
        private void checkBoxCosts8_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCosts8.Enabled = checkBoxCosts8.Checked;
        }
    }
}
