//using Awesomium.Windows.Forms;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using CefSharp;
using CefSharp.WinForms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace web
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser Browser;
        StringBuilder buf = new StringBuilder();
        V83.COMConnector con;
        dynamic Connection;
        TableLayoutPanel Baz = new TableLayoutPanel();
        Label Bazlabel;

        void CreateBazbyPanel()
        {
            Baz.ColumnCount = 1;
            Baz.RowCount = 2;

            Baz.RowStyles.Add(new RowStyle { SizeType = SizeType.Percent, Height = 10 });
            Baz.RowStyles.Add(new RowStyle { SizeType = SizeType.Percent, Height = 90 });
            Baz.Dock = DockStyle.Fill;
            //Baz.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            Bazlabel = new Label()
            {
                Dock = DockStyle.Fill,
                ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
                Text = "Поднесите штрихкод товара к сканеру",
                TextAlign = ContentAlignment.MiddleCenter
            };

            PictureBox BazbyPicture = new PictureBox
            {
                Image = Properties.Resources.bazby,
                InitialImage = Properties.Resources.bazby,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Bazlabel.PreviewKeyDown += TableLayoutPanel1_PreviewKeyDown;
            //Baz.PreviewKeyDown += TableLayoutPanel1_PreviewKeyDown;

            Baz.Controls.Add(Bazlabel, 0, 0);
            Baz.Controls.Add(BazbyPicture, 0, 1);
            panel1.Controls.Add(Baz);
            //Bazlabel.Focus();
        }

        public Form1()
        {
            InitializeComponent();

            //MessageBox.Show("Start");
            splitContainer1.Panel1MinSize = pictureBox1.Height;
            splitContainer1.SplitterDistance = flowLayoutPanel1.Height;

            pictureBox4.Visible = !Program.LicenseFlag;

            timer1.Interval = Properties.Settings.Default.Duration;
            timer1.Stop();
            try
            {
                Browser = new ChromiumWebBrowser(Properties.Settings.Default.Site)
                {
                    BackColor = Color.White,
                    Name = "Br",
                    TabIndex = 1,
                    Dock = DockStyle.Fill
                };
                con = new V83.COMConnector
                {
                    PoolCapacity = 10,
                    PoolTimeout = 60,
                    MaxConnections = 2
                };
                CreateBazbyPanel();
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
            }
            panel1.Controls.Add(Browser);
            Browser.Visible = false;
            panel1.Controls.Add(Baz);
            Baz.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                PictureBox2_Click(sender, e);
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Browser.Visible = true;
            tableLayoutPanel1.Visible = false;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Bazlabel.Text = "Поднесите штрихкод товара к сканеру";
            Bazlabel.Focus();

            Baz.Visible = true;
            tableLayoutPanel1.Visible = false;
            DiscountLabel.Visible = false;

            //label1.Text = "Поднесите штрихкод товара к сканеру";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label2.Text = string.Empty;
            label3.Text = string.Empty;
            DiscountLabel.Text = string.Empty;
            pictureBox3.Image = Properties.Resources.bazby;

            try
            {
                timer1.Stop();
                Browser.Load(Properties.Settings.Default.Site);
                Browser.Visible = false;
                //tableLayoutPanel1.Visible = true;
                //label1.Focus();
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            if (Browser != null)
                Browser.Dispose();
            Cef.Shutdown();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    FindByBarcode(buf.ToString());

            //    buf = new StringBuilder();
            //}
            //else
            //    buf.Append(e.KeyChar);
            //e.Handled = true;
        }

        private void Display(Label L, string v)
        {
            if (L.InvokeRequired)
            {
                L.Invoke(new Action<string>((s) => L.Text = s), v);
            }
            else
            {
                L.Text = v;
            }
        }

        private void TableLayoutPanel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (buf.Length > 0))
            {
                Display(sender as Label, buf.ToString());
                FindByBarcode(buf.ToString());

                buf = new StringBuilder();
            }
            else
            {
                if (char.IsDigit((char)e.KeyCode))
                {
                    buf.Append((char)e.KeyCode);
                    Display(sender as Label, buf.ToString());

                }
            }
            //e.Handled = true;
        }

        private void FindByBarcode(string Code)
        {
            int QQ = 0;
            Bazlabel.Text = "Соединяемся с сервером";
            while (QQ == 0)
            {
                dynamic r;
                if (Connection == null)
                {
                    label1.Text = "Соединяемся с сервером";
                    Bazlabel.Text = "Соединяемся с сервером";
                    Connection = ConnectToServer();
                }

                if (Connection != null)
                {
                    // Сервер жив?
                    try
                    {
                        r = Connection.ПолучитьСоединенияИнформационнойБазы();
                        QQ = r.Количество;
                    }
                    catch (Exception /*err*/)
                    {
                        //MessageBox.Show("reconnect");
                        label1.Text = "Соединение с сервером";
                        Bazlabel.Text = "Соединение с сервером";
                        Connection = null;
                        con = null;
                        GC.Collect();
                        con = new V83.COMConnector
                        {
                            PoolCapacity = 10,
                            PoolTimeout = 60,
                            MaxConnections = 2
                        };
                    }
                    //    StringBuilder b = new StringBuilder();
                    //    foreach (var rr in r)
                    //    {
                    //        b.AppendLine($"{rr.ComputerName} {rr.ApplicationName} {rr.ConnectionStarted} {rr.SessionNumber} {rr.ConnectionNumber} {rr.User.Name}");
                    //    }
                    //    //MessageBox.Show($"{b}");
                }
            }

            if (Code != string.Empty)
            {
                label1.Text = $"Ищем {Code}";
                Bazlabel.Text = $"Ищем {Code}";
            }
            else
            {
                label1.Text = "Поднесите штрихкод товара к сканеру";
                Bazlabel.Text = "Поднесите штрихкод товара к сканеру";
            }

            try
            {
                // розничная цена
                dynamic TP = Connection.Справочники.ТипыЦенНоменклатуры.НайтиПоНаименованию(Properties.Settings.Default.PriceType);


                Cursor = Cursors.WaitCursor;
                // Штрихкод
                dynamic Q = Connection.NewObject("Query");
                Q.Текст = "ВЫБРАТЬ Штрихкоды.Владелец КАК CсылкаНаНоменклатуру, Штрихкоды.Штрихкод как Код " +
                          "ИЗ РегистрСведений.Штрихкоды КАК Штрихкоды " +
                          "ГДЕ Штрихкоды.Штрихкод = &Code и Штрихкоды.Владелец ССЫЛКА Справочник.Номенклатура;";
                if (Code != string.Empty)
                {
                    Q.УстановитьПараметр("Code", Code);
                    label1.Text = $"Ищем {Code}";
                    Bazlabel.Text = $"Ищем {Code}";
                }
                else
                {
                    label1.Text = "Поднесите штрихкод товара к сканеру";
                    Bazlabel.Text = "Поднесите штрихкод товара к сканеру";
                    return;
                }

                dynamic res = Q.Выполнить();
                dynamic l = res.Выбрать();
                if (l.Количество > 0)
                {
                    while (l.Следующий())
                    {
                        //Title
                        label1.Text = l.CсылкаНаНоменклатуру.Наименование;
                        label1.TextAlign = ContentAlignment.TopLeft;

                        // Picture
                        // картинка
                        dynamic pp = null;
                        try
                        {
                            dynamic r = Connection.Обработки.APPLIX_RU_ХДВИ_ЗМ.Создать();
                            pp = r.ХранилищеДополнительнойИнформации_ПолучитьДанныеФайла(l.CсылкаНаНоменклатуру.ОсновноеИзображение);
                        }
                        catch (Exception)
                        {
                            pp = l.CсылкаНаНоменклатуру.ОсновноеИзображение.Хранилище.Get();
                        }

                        if (pp != null)
                        {
                            Bitmap pp1 = PictureFrom1C(Connection, pp);

                            pictureBox3.Image = pp1;
                        }
                        else
                        {
                            pictureBox3.Image = Properties.Resources.bazby;
                            tableLayoutPanel1.SetColumnSpan(pictureBox3, 2);
                            tableLayoutPanel1.SetRowSpan(pictureBox3, 2);
                        }
                        label2.Text = $"{l.CсылкаНаНоменклатуру.ДополнительноеОписаниеНоменклатуры}";

                        // Цена
                        dynamic P = Connection.NewObject("Query");
                        P.Text = "select Цена " +
                                 "from РегистрСведений.ЦеныНоменклатуры.СрезПоследних(&ДатаПолучения) " +
                                 "where Номенклатура = &Goods and ТипЦен = &TP;";
                        P.УстановитьПараметр("ДатаПолучения", DateTime.Today);
                        P.УстановитьПараметр("Goods", l.CсылкаНаНоменклатуру);
                        P.УстановитьПараметр("TP", TP);
                        dynamic PRes = P.Выполнить().Выбрать();
                        while (PRes.Следующий())
                        {
                            label3.Text = $"Цена: {PRes.Цена:C}";
                        }
                        // Скидки
                        dynamic DiscountQuery = Connection.NewObject("Query");
                        DiscountQuery.Text =
                        "select ЕстьNull(sum(ПроцентСкидкиНаценки),0) as Dis " + 
                        "from  РегистрСведений.СкидкиНаценкиНоменклатуры.СрезПоследних " + 
                        "where Номенклатура.Ссылка = &Goods " +
                        "    and Активность ";
                        DiscountQuery.УстановитьПараметр("Goods", l.CсылкаНаНоменклатуру);
                        dynamic DiscountRes = DiscountQuery.Execute().Select();

                        decimal Discount = 0;
                        if (DiscountRes.Next())
                        {
                            Discount = DiscountRes.Dis;
                            DiscountLabel.Text = "Цена со скидкой:"+Environment.NewLine+$"{((100 - Discount) / 100) * PRes.Цена:C2}";
                            DiscountLabel.Visible = true;
                        }

                        Baz.Visible = false;
                        tableLayoutPanel1.Visible = true;
                        timer1.Start();
                    }
                }
                else
                {
                    timer1.Stop();
                    label1.Text = "Поднесите штрихкод товара к сканеру";
                    Bazlabel.Text = "Поднесите штрихкод товара к сканеру";
                    label2.Text = string.Empty;
                    label3.Text = string.Empty;
                    pictureBox3.Image = Properties.Resources.bazby;
                    label1.TextAlign = ContentAlignment.MiddleCenter;
                    label3.Text = string.Empty;
                    tableLayoutPanel1.Visible = false;
                    Baz.Visible = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"FindByBarcode: {err.Message}");
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
                //if (Connection != null)
                //{
                //    Connection = null;
                //    //con = null;
                //}
            }
        }

        private dynamic ConnectToServer()
        {
            dynamic Connection = null;
            try
            {
                Connection = con.Connect($"{Properties.Settings.Default.CS}");
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
                Connection = null;
            }
            return Connection;
        }

        private Image PictureFrom1C(dynamic Connection, dynamic Picture)
        {
            dynamic data;
            try
            {
                data = Picture.ПолучитьДвоичныеДанные();
                if (data != null)
                {
                    var Q1 = Convert.FromBase64String(Connection.Base64String(data));
                    using (MemoryStream ms = new MemoryStream(Q1))
                    {
                        return Bitmap.FromStream(ms);
                    }
                }
                else
                    return null;
            }
            catch (Exception err)
            {
                MessageBox.Show($"FictureFrom1C: {err}");
                return null;
            }


        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            PictureBox2_Click(sender, e);
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            if (aboutBox1.ShowDialog() == DialogResult.OK)
            {
                aboutBox1.Close();
                label1.Focus();
            }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Hide();
            MessageBox.Show($"Для получения лицензии сообщите код {Licensing.CPU()} в службу поддержки");
            Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
