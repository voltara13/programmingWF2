using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace programmingWF2
{
    public partial class MainWindow : Form
    {
        private List<Cleaning> ServicesArr = new List<Cleaning>();
        private static Random rand = new Random();
        internal Data data = new Data();
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
            resetServicesArr(true);
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
            resetServicesArr(false);
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

            int day = Convert.ToInt32(numericUpDown.Value);

            double sumPriceAll = 0;
            double sumPriceMonth = 0;
            double sumPriceToday = 0;
            double sumCostsAll = 0;
            double sumCostsMonth = 0;
            int sumClientAll = 0;
            int sumClientMonth = 0;
            int sumClientToday = 0;

            richTextBox1.Text = "СИМУЛЯЦИЯ НАЧАЛАСЬ\n\n";
            for (int i = 1; i <= day; i++)
            {
                sumClientToday = 0;
                sumPriceToday = 0;

                richTextBox1.AppendText($"ДЕНЬ {i}:\n");
                for (; sumClientToday < rand.Next(0, 10); sumClientToday++)
                {
                    var selectElm = listPrice[rand.Next(0, listPrice.Count)];
                    richTextBox1.AppendText($"Услуга: {selectElm.service}, цена: {selectElm.price} руб.\n");
                    sumPriceToday += selectElm.price;
                    selectElm.ChangeBalance();
                }
                richTextBox1.AppendText($"Выручка за день: {sumPriceToday}, количество клиентов за день: {sumClientToday}\n");


                sumPriceMonth += sumPriceToday;
                sumClientMonth += sumClientToday;
                sumPriceAll += sumPriceToday;
                sumClientAll += sumClientToday;

                if (i % 30 != 0) continue;

                sumCostsMonth = 0;
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

                sumCostsAll += sumCostsMonth;
                sumPriceMonth = 0;
                sumClientMonth = 0;
            }

            richTextBox1.AppendText("\nКОНЕЦ СИМУЛЯЦИИ\n" +
                                    $"Суммарно расходов за всё время: {sumCostsAll}\n" +
                                    $"Суммарно доходов за всё время: {sumPriceAll}\n" +
                                    $"Суммарно клиентов за всё время: {sumClientAll}\n" +
                                    $"Суммарная прибыль за всё время: {sumPriceAll - sumCostsAll}");

            labelIncomeAll.Text = $"{sumCostsAll} руб.";
            labelIncomeMonth.Text = $"{sumPriceMonth} руб.";
            labelIncomeToday.Text = $"{sumPriceToday} руб.";
            labelIncomeAvg.Text = $"{Math.Round(sumCostsAll / day)} руб.";

            labelClientAll.Text = $"{sumClientAll} чел.";
            labelClientMonth.Text = $"{sumCostsMonth} чел.";
            labelClientToday.Text = $"{sumClientToday} чел.";
            labelClientAvg.Text = $"{sumClientAll / day} чел.";

            labelCostsAll.Text = $"{sumCostsAll} руб.";
            labelCostsMonth.Text = $"{sumCostsMonth} руб.";

            labelBalance.Text = $"{balance} руб.";
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            balance = 0;
            labelBalance.Text = "0 руб.";
            richTextBox1.Text = "";

            labelIncomeAll.Text = "0 руб.";
            labelIncomeMonth.Text = "0 руб.";
            labelIncomeToday.Text = "0 руб.";
            labelIncomeAvg.Text = "0 руб.";

            labelClientAll.Text = "0 чел.";
            labelClientMonth.Text = "0 чел.";
            labelClientToday.Text = "0 чел.";
            labelClientAvg.Text = "0 чел.";

            labelCostsAll.Text = "0 руб.";
            labelCostsMonth.Text = "0 руб.";
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            /*Диалоговое окно сохранения файла*/
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "bin files (*.bin)|*.bin"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            /*Сериализация*/

            data.ServicesArr = ServicesArr;
            data.text = richTextBox1.Text;

            data.Balance = balance;

            data.priceAll = labelIncomeAll.Text;
            data.priceMonth = labelIncomeMonth.Text;
            data.priceToday = labelIncomeToday.Text;
            data.priceAvg = labelIncomeAvg.Text;

            data.clientAll = labelClientAll.Text;
            data.clientMonth = labelClientMonth.Text;
            data.clientToday = labelClientToday.Text;
            data.clientAvg = labelClientAvg.Text;

            data.costsAll = labelCostsAll.Text;
            data.costsMonth = labelCostsMonth.Text;

            using (var fs = saveFileDialog.OpenFile())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
                MessageBox.Show("Состояние успешно сохранено.", "Сохранение состояния", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            /*Диалоговое окно открытия файла*/
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "bin files (*.bin)|*.bin"
            };

            while (true)
            {
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    /*Десериализация*/
                    using (var fs = openFileDialog.OpenFile())
                    {
                        var formatter = new BinaryFormatter();
                        data = (Data)formatter.Deserialize(fs);
                    }

                    ServicesArr = data.ServicesArr;
                    richTextBox1.Text = data.text;

                    balance = data.Balance;

                    labelIncomeAll.Text = data.priceAll;
                    labelIncomeMonth.Text = data.priceMonth;
                    labelIncomeToday.Text = data.priceToday;
                    labelIncomeAvg.Text = data.priceAvg;

                    labelClientAll.Text = data.clientAll;
                    labelClientMonth.Text = data.clientMonth;
                    labelClientToday.Text = data.clientToday;
                    labelClientAvg.Text = data.clientAvg;

                    labelCostsAll.Text = data.costsAll;
                    labelCostsMonth.Text = data.costsMonth;

                    resetPrice();
                    resetCosts();

                    foreach (var elm in ServicesArr)
                    {
                        switch (elm.service)
                        {
                            case "Чистка одежды из замши, меха, кожи, текстиля, пуховиков":
                                checkBoxPrice1.Checked = true;
                                textBoxPrice1.Text = elm.price.ToString();
                                break;
                            case "Чистка постельных принадлежностей, ковров":
                                checkBoxPrice2.Checked = true;
                                textBoxPrice2.Text = elm.price.ToString();
                                break;
                            case "Чистка игрушек":
                                checkBoxPrice3.Checked = true;
                                textBoxPrice3.Text = elm.price.ToString();
                                break;
                            case "Ручная чистка сумок и обуви":
                                checkBoxPrice4.Checked = true;
                                textBoxPrice4.Text = elm.price.ToString();
                                break;
                            case "Прачечные услуги":
                                checkBoxPrice5.Checked = true;
                                textBoxPrice5.Text = elm.price.ToString();
                                break;
                            case "Фонд оплаты труда":
                                checkBoxCosts1.Checked = true;
                                textBoxCosts1.Text = elm.price.ToString();
                                break;
                            case "Аренда":
                                checkBoxCosts2.Checked = true;
                                textBoxCosts2.Text = elm.price.ToString();
                                break;
                            case "Амортизация":
                                checkBoxCosts3.Checked = true;
                                textBoxCosts3.Text = elm.price.ToString();
                                break;
                            case "Коммунальные услуги":
                                checkBoxCosts4.Checked = true;
                                textBoxCosts4.Text = elm.price.ToString();
                                break;
                            case "Реклама":
                                checkBoxCosts5.Checked = true;
                                textBoxCosts5.Text = elm.price.ToString();
                                break;
                            case "Бухгалтерия":
                                checkBoxCosts6.Checked = true;
                                textBoxCosts6.Text = elm.price.ToString();
                                break;
                            case "Закупка спец. средств":
                                checkBoxCosts7.Checked = true;
                                textBoxCosts7.Text = elm.price.ToString();
                                break;
                            case "Непредвиденные расходы":
                                checkBoxCosts8.Checked = true;
                                textBoxCosts8.Text = elm.price.ToString();
                                break;
                        }
                    }

                    MessageBox.Show("Состояние успешно загружено.", "Загрузка состояния", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                /*Если произошла ошибка во время десериализации*/
                catch (System.Runtime.Serialization.SerializationException)
                {
                    MessageBox.Show("Неправильный файл сериализации");
                }
            }
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
