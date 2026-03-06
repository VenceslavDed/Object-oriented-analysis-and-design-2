using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PaymentAppNoPattern
{
    public partial class Form1 : Form
    {
        private string type;
        private string cvvRealValue = "";

        public Form1()
        {
            InitializeComponent();
            type = "card";

            this.ActiveControl = null;

            SetPlaceholder(txtCardNumber, "#### #### #### ####");
            SetPlaceholder(txtExpiry, "MM/YY");
            SetPlaceholder(txtEmail, "example@mail.com");
            SetPlaceholder(txtWallet, "bc1qxy2kgdygjrsqtzq2n0yrf...");
            SetPlaceholder(txtAddress, "ул. Примерная, д. 1, кв. 1");
            SetPlaceholder(txtPhone, "+7 (999) 000-00-00");
            SetPlaceholderCVV(txtCVV, "123");

            ShowPaymentFields("card");
        }

        private void ButtonPay_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                ShowLog("Введите корректную сумму.", success: false);
                return;
            }

            if (type == "card")
            {
                string cardNumber = GetFieldValue(txtCardNumber);
                string cvv = cvvRealValue;
                string expiry = GetFieldValue(txtExpiry);

                if (cardNumber.Replace(" ", "").Length != 16)
                {
                    ShowLog("Проверьте номер карты: должно быть 16 цифр.", success: false);
                    return;
                }
                if (cvv.Length != 3)
                {
                    ShowLog("Проверьте CVV: должно быть 3 цифры.", success: false);
                    return;
                }
                if (string.IsNullOrWhiteSpace(expiry))
                {
                    ShowLog("Укажите срок действия карты.", success: false);
                    return;
                }

                string savedCardText = txtCardNumber.Text;
                Color savedCardColor = txtCardNumber.ForeColor;
                string savedExpiryText = txtExpiry.Text;
                Color savedExpiryColor = txtExpiry.ForeColor;
                string savedCvv = cvvRealValue;

                SetUIEnabled(false);
                ShowLog("Обрабатываем оплату через Банковская карта...", processing: true);

                var thread = new Thread(() =>
                {
                    Thread.Sleep(1500);
                    bool result = true;

                    Invoke((Action)(() =>
                    {
                        txtCardNumber.Text = savedCardText;
                        txtCardNumber.ForeColor = savedCardColor;
                        txtExpiry.Text = savedExpiryText;
                        txtExpiry.ForeColor = savedExpiryColor;
                        cvvRealValue = savedCvv;
                        txtCVV.Text = savedCvv.Length > 0
                                                    ? new string('●', savedCvv.Length)
                                                    : "123";
                        txtCVV.ForeColor = savedCvv.Length > 0 ? Color.Black : Color.Gray;

                        if (result)
                            ShowLog($"Оплата через Банковская карта на сумму {amount:F2} руб. прошла успешно!", success: true);
                        else
                            ShowLog("Оплата через Банковская карта отклонена. Попробуйте ещё раз.", success: false);

                        SetUIEnabled(true);
                    }));
                });
                thread.IsBackground = true;
                thread.Start();
            }

            else if (type == "paypal")
            {
                string email = GetFieldValue(txtEmail);

                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                {
                    ShowLog("Проверьте email для PayPal.", success: false);
                    return;
                }

                SetUIEnabled(false);
                ShowLog("Обрабатываем оплату через PayPal...", processing: true);

                var thread = new Thread(() =>
                {
                    Thread.Sleep(2000);
                    bool result = new Random().Next(0, 10) != 0;

                    Invoke((Action)(() =>
                    {
                        if (result)
                            ShowLog($"Оплата через PayPal на сумму {amount:F2} руб. прошла успешно!", success: true);
                        else
                            ShowLog("Оплата через PayPal отклонена. Попробуйте ещё раз.", success: false);

                        SetUIEnabled(true);
                    }));
                });
                thread.IsBackground = true;
                thread.Start();
            }

            else if (type == "crypto")
            {
                string wallet = GetFieldValue(txtWallet);
                string coin = cmbCoin.Text.Trim();

                if (string.IsNullOrWhiteSpace(wallet) || wallet.Length < 26)
                {
                    ShowLog("Проверьте адрес кошелька: минимум 26 символов.", success: false);
                    return;
                }
                if (string.IsNullOrWhiteSpace(coin))
                {
                    ShowLog("Выберите монету.", success: false);
                    return;
                }

                SetUIEnabled(false);
                ShowLog($"Обрабатываем оплату через Криптовалюта ({coin})...", processing: true);

                var thread = new Thread(() =>
                {
                    Thread.Sleep(3000);
                    bool result = new Random().Next(0, 7) != 0;

                    Invoke((Action)(() =>
                    {
                        if (result)
                            ShowLog($"Оплата через Криптовалюта ({coin}) на сумму {amount:F2} руб. прошла успешно!", success: true);
                        else
                            ShowLog($"Оплата через Криптовалюта ({coin}) отклонена. Попробуйте ещё раз.", success: false);

                        SetUIEnabled(true);
                    }));
                });
                thread.IsBackground = true;
                thread.Start();
            }

            else if (type == "cash")
            {
                string address = GetFieldValue(txtAddress);
                string phone = GetFieldValue(txtPhone);

                if (string.IsNullOrWhiteSpace(address))
                {
                    ShowLog("Укажите адрес доставки.", success: false);
                    return;
                }
                if (string.IsNullOrWhiteSpace(phone))
                {
                    ShowLog("Укажите телефон.", success: false);
                    return;
                }

                SetUIEnabled(false);
                ShowLog("Обрабатываем оплату через Наличными при получении...", processing: true);

                var thread = new Thread(() =>
                {
                    Thread.Sleep(800);

                    Invoke((Action)(() =>
                    {
                        ShowLog($"Заказ с оплатой наличными на сумму {amount:F2} руб. успешно зарегистрирован!", success: true);
                        SetUIEnabled(true);
                    }));
                });
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void ShowPaymentFields(string paymentType)
        {
            pnlCard.Visible = false;
            pnlPayPal.Visible = false;
            pnlCrypto.Visible = false;
            pnlCash.Visible = false;

            switch (paymentType)
            {
                case "card": pnlCard.Visible = true; break;
                case "paypal": pnlPayPal.Visible = true; break;
                case "crypto": pnlCrypto.Visible = true; break;
                case "cash": pnlCash.Visible = true; break;
            }
        }

        private void rbCard_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCard.Checked) { type = "card"; ShowPaymentFields("card"); }
        }
        private void rbPayPal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPayPal.Checked) { type = "paypal"; ShowPaymentFields("paypal"); }
        }
        private void rbCrypto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCrypto.Checked) { type = "crypto"; ShowPaymentFields("crypto"); }
        }
        private void rbCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCash.Checked) { type = "cash"; ShowPaymentFields("cash"); }
        }

        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.Gray;
            tb.Tag = placeholder;

            tb.Enter += (s, e) =>
            {
                if (tb.ForeColor == Color.Gray)
                {
                    tb.Text = "";
                    tb.ForeColor = Color.Black;
                }
            };

            tb.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = placeholder;
                    tb.ForeColor = Color.Gray;
                }
                else
                {
                    tb.ForeColor = Color.Black;
                }
            };
        }

        private void SetPlaceholderCVV(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.Gray;

            tb.Enter += (s, e) =>
            {
                if (tb.ForeColor == Color.Gray)
                {
                    cvvRealValue = "";
                    tb.Text = "";
                    tb.ForeColor = Color.Black;
                }
            };

            tb.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(cvvRealValue))
                {
                    cvvRealValue = "";
                    tb.Text = placeholder;
                    tb.ForeColor = Color.Gray;
                }
            };

            tb.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = true;

                if (e.KeyChar == (char)Keys.Back)
                {
                    if (cvvRealValue.Length > 0)
                        cvvRealValue = cvvRealValue.Substring(0, cvvRealValue.Length - 1);
                }
                else if (cvvRealValue.Length < 3)
                {
                    cvvRealValue += e.KeyChar;
                }

                tb.Text = new string('●', cvvRealValue.Length);
                tb.SelectionStart = tb.Text.Length;
            };
        }

        private string GetFieldValue(TextBox tb)
        {
            string placeholder = tb.Tag as string ?? "";
            if (tb.ForeColor == Color.Gray && tb.Text == placeholder)
                return "";
            return tb.Text.Trim();
        }

        private void ShowLog(string message, bool success = false, bool processing = false)
        {
            if (processing)
            {
                lblLog.ForeColor = Color.FromArgb(133, 77, 14);
                lblLog.BackColor = Color.FromArgb(254, 252, 232);
            }
            else if (success)
            {
                lblLog.ForeColor = Color.FromArgb(22, 101, 52);
                lblLog.BackColor = Color.FromArgb(240, 253, 244);
            }
            else
            {
                lblLog.ForeColor = Color.FromArgb(153, 27, 27);
                lblLog.BackColor = Color.FromArgb(254, 242, 242);
            }

            lblLog.Text = message;
            lblLog.Visible = true;
        }

        private void SetUIEnabled(bool enabled)
        {
            btnPay.Enabled = enabled;
            grpMethod.Enabled = enabled;
            txtAmount.Enabled = enabled;
        }
    }
}