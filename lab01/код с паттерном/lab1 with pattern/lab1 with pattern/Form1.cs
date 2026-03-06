using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PaymentApp
{
    public partial class Form1 : Form
    {
        private PaymentFactory factory;
        private string type;

        private readonly HashSet<TextBox> editedFields = new HashSet<TextBox>();

        private string cvvRealValue = "";

        public Form1()
        {
            InitializeComponent();
            factory = new PaymentFactory();
            type = "card";

            SetPlaceholder(txtCardNumber, "1234567890123456");
            SetPlaceholder(txtExpiry, "MM/YY");
            SetPlaceholder(txtEmail, "example@mail.com");
            SetPlaceholder(txtWallet, "bc1qxy2kgdygjrsqtzq2n0yrf...");
            SetPlaceholder(txtAddress, "ул. Примерная, д. 1, кв. 1");
            SetPlaceholder(txtPhone, "+7 (999) 000-00-00");
            SetPlaceholderCVV(txtCVV, "123");

            ShowPaymentFields("card");
        }

        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.Gray;

            tb.KeyDown += (s, e) =>
            {
                if (!editedFields.Contains(tb))
                {
                    editedFields.Add(tb);
                    tb.Text = "";
                    tb.ForeColor = Color.Black;
                }
            };

            tb.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    editedFields.Remove(tb);
                    tb.Text = placeholder;
                    tb.ForeColor = Color.Gray;
                }
            };
        }

        private string GetFieldValue(TextBox tb)
        {
            return editedFields.Contains(tb) ? tb.Text.Trim() : "";
        }

        private void SetPlaceholderCVV(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.Gray;

            tb.Enter += (s, e) =>
            {
                if (!editedFields.Contains(tb))
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
                    editedFields.Remove(tb);
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
                editedFields.Add(tb);

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

        private void ButtonPay_Click(object sender, EventArgs e)
        {
            var data = new PaymentData
            {
                CardNumber = GetFieldValue(txtCardNumber),
                CVV = cvvRealValue,
                Expiry = GetFieldValue(txtExpiry),
                Email = GetFieldValue(txtEmail),
                WalletAddress = GetFieldValue(txtWallet),
                Coin = cmbCoin.Text.Trim(),
                Address = GetFieldValue(txtAddress),
                Phone = GetFieldValue(txtPhone),
            };

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                ShowLog("Введите корректную сумму.", success: false);
                return;
            }

            IPaymentProcessor payment = factory.Create(type, data);

            if (!payment.Check())
            {
                ShowLog($"Проверьте данные для {payment.GetDisplayName()}.", success: false);
                return;
            }

            SetUIEnabled(false);
            ShowLog($"Обрабатываем оплату через {payment.GetDisplayName()}...", processing: true);

            string savedCardText = txtCardNumber.Text;
            bool cardWasEdited = editedFields.Contains(txtCardNumber);
            string savedExpiryText = txtExpiry.Text;
            bool expiryWasEdited = editedFields.Contains(txtExpiry);
            string savedCvv = cvvRealValue;

            var thread = new Thread(() =>
            {
                bool result = payment.Process(amount);

                Invoke((Action)(() =>
                {
                    txtCardNumber.Text = savedCardText;
                    txtCardNumber.ForeColor = cardWasEdited ? Color.Black : Color.Gray;
                    if (cardWasEdited) editedFields.Add(txtCardNumber);

                    txtExpiry.Text = savedExpiryText;
                    txtExpiry.ForeColor = expiryWasEdited ? Color.Black : Color.Gray;
                    if (expiryWasEdited) editedFields.Add(txtExpiry);

                    cvvRealValue = savedCvv;
                    txtCVV.Text = savedCvv.Length > 0 ? new string('●', savedCvv.Length) : "123";
                    txtCVV.ForeColor = savedCvv.Length > 0 ? Color.Black : Color.Gray;
                    if (savedCvv.Length > 0) editedFields.Add(txtCVV);

                    if (result)
                        ShowLog($"Оплата через {payment.GetDisplayName()} " +
                                $"на сумму {amount:F2} руб. прошла успешно!", success: true);
                    else
                        ShowLog($"Оплата через {payment.GetDisplayName()} отклонена. " +
                                 "Попробуйте ещё раз.", success: false);

                    SetUIEnabled(true);
                }));
            });
            thread.IsBackground = true;
            thread.Start();
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