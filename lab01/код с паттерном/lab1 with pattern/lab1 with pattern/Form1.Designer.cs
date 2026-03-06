namespace PaymentApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpMethod = new System.Windows.Forms.GroupBox();
            this.rbCard = new System.Windows.Forms.RadioButton();
            this.rbPayPal = new System.Windows.Forms.RadioButton();
            this.rbCrypto = new System.Windows.Forms.RadioButton();
            this.rbCash = new System.Windows.Forms.RadioButton();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlPayPal = new System.Windows.Forms.Panel();
            this.pnlCrypto = new System.Windows.Forms.Panel();
            this.pnlCash = new System.Windows.Forms.Panel();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.lblCVV = new System.Windows.Forms.Label();
            this.txtCVV = new System.Windows.Forms.TextBox();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.txtExpiry = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblWallet = new System.Windows.Forms.Label();
            this.txtWallet = new System.Windows.Forms.TextBox();
            this.lblCoin = new System.Windows.Forms.Label();
            this.cmbCoin = new System.Windows.Forms.ComboBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnPay = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.grpMethod.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlPayPal.SuspendLayout();
            this.pnlCrypto.SuspendLayout();
            this.pnlCash.SuspendLayout();
            this.SuspendLayout();

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 40, 60);
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(420, 30);
            this.lblTitle.Text = "Оформление оплаты";

            this.grpMethod.Controls.Add(this.rbCard);
            this.grpMethod.Controls.Add(this.rbPayPal);
            this.grpMethod.Controls.Add(this.rbCrypto);
            this.grpMethod.Controls.Add(this.rbCash);
            this.grpMethod.Location = new System.Drawing.Point(20, 52);
            this.grpMethod.Name = "grpMethod";
            this.grpMethod.Size = new System.Drawing.Size(420, 54);
            this.grpMethod.TabIndex = 0;
            this.grpMethod.TabStop = false;
            this.grpMethod.Text = "Способ оплаты";

            this.rbCard.Checked = true;
            this.rbCard.Location = new System.Drawing.Point(10, 22);
            this.rbCard.Name = "rbCard";
            this.rbCard.Size = new System.Drawing.Size(88, 22);
            this.rbCard.TabStop = true;
            this.rbCard.Text = "💳 Карта";
            this.rbCard.CheckedChanged += new System.EventHandler(this.rbCard_CheckedChanged);

            this.rbPayPal.Location = new System.Drawing.Point(106, 22);
            this.rbPayPal.Name = "rbPayPal";
            this.rbPayPal.Size = new System.Drawing.Size(88, 22);
            this.rbPayPal.Text = "🅿 PayPal";
            this.rbPayPal.CheckedChanged += new System.EventHandler(this.rbPayPal_CheckedChanged);

            this.rbCrypto.Location = new System.Drawing.Point(202, 22);
            this.rbCrypto.Name = "rbCrypto";
            this.rbCrypto.Size = new System.Drawing.Size(88, 22);
            this.rbCrypto.Text = "₿ Крипта";
            this.rbCrypto.CheckedChanged += new System.EventHandler(this.rbCrypto_CheckedChanged);

            this.rbCash.Location = new System.Drawing.Point(298, 22);
            this.rbCash.Name = "rbCash";
            this.rbCash.Size = new System.Drawing.Size(110, 22);
            this.rbCash.Text = "💵 Наличные";
            this.rbCash.CheckedChanged += new System.EventHandler(this.rbCash_CheckedChanged);

            this.pnlCard.Controls.Add(this.lblCardNumber);
            this.pnlCard.Controls.Add(this.txtCardNumber);
            this.pnlCard.Controls.Add(this.lblCVV);
            this.pnlCard.Controls.Add(this.txtCVV);
            this.pnlCard.Controls.Add(this.lblExpiry);
            this.pnlCard.Controls.Add(this.txtExpiry);
            this.pnlCard.Location = new System.Drawing.Point(20, 118);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(420, 140);

            this.lblCardNumber.AutoSize = false;
            this.lblCardNumber.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblCardNumber.Location = new System.Drawing.Point(0, 0);
            this.lblCardNumber.Size = new System.Drawing.Size(200, 18);
            this.lblCardNumber.Text = "Номер карты";

            this.txtCardNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardNumber.Location = new System.Drawing.Point(0, 20);
            this.txtCardNumber.MaxLength = 16;
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(410, 22);

            this.lblCVV.AutoSize = false;
            this.lblCVV.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblCVV.Location = new System.Drawing.Point(0, 56);
            this.lblCVV.Size = new System.Drawing.Size(60, 18);
            this.lblCVV.Text = "CVV";

            this.txtCVV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCVV.Location = new System.Drawing.Point(0, 76);
            this.txtCVV.MaxLength = 3;
            this.txtCVV.Name = "txtCVV";
            this.txtCVV.Size = new System.Drawing.Size(80, 22);

            this.lblExpiry.AutoSize = false;
            this.lblExpiry.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblExpiry.Location = new System.Drawing.Point(100, 56);
            this.lblExpiry.Size = new System.Drawing.Size(120, 18);
            this.lblExpiry.Text = "Срок действия";

            this.txtExpiry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpiry.Location = new System.Drawing.Point(100, 76);
            this.txtExpiry.MaxLength = 5;
            this.txtExpiry.Name = "txtExpiry";
            this.txtExpiry.Size = new System.Drawing.Size(100, 22);

            this.pnlPayPal.Controls.Add(this.lblEmail);
            this.pnlPayPal.Controls.Add(this.txtEmail);
            this.pnlPayPal.Location = new System.Drawing.Point(20, 118);
            this.pnlPayPal.Name = "pnlPayPal";
            this.pnlPayPal.Size = new System.Drawing.Size(420, 140);
            this.pnlPayPal.Visible = false;

            this.lblEmail.AutoSize = false;
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblEmail.Location = new System.Drawing.Point(0, 0);
            this.lblEmail.Size = new System.Drawing.Size(220, 18);
            this.lblEmail.Text = "Email аккаунта PayPal";

            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Location = new System.Drawing.Point(0, 20);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(410, 22);

            this.pnlCrypto.Controls.Add(this.lblWallet);
            this.pnlCrypto.Controls.Add(this.txtWallet);
            this.pnlCrypto.Controls.Add(this.lblCoin);
            this.pnlCrypto.Controls.Add(this.cmbCoin);
            this.pnlCrypto.Location = new System.Drawing.Point(20, 118);
            this.pnlCrypto.Name = "pnlCrypto";
            this.pnlCrypto.Size = new System.Drawing.Size(420, 140);
            this.pnlCrypto.Visible = false;

            this.lblWallet.AutoSize = false;
            this.lblWallet.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblWallet.Location = new System.Drawing.Point(0, 0);
            this.lblWallet.Size = new System.Drawing.Size(200, 18);
            this.lblWallet.Text = "Адрес кошелька";

            this.txtWallet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWallet.Location = new System.Drawing.Point(0, 20);
            this.txtWallet.Name = "txtWallet";
            this.txtWallet.Size = new System.Drawing.Size(410, 22);

            this.lblCoin.AutoSize = false;
            this.lblCoin.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblCoin.Location = new System.Drawing.Point(0, 56);
            this.lblCoin.Size = new System.Drawing.Size(60, 18);
            this.lblCoin.Text = "Монета";

            this.cmbCoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCoin.Items.AddRange(new object[] { "BTC", "ETH", "USDT" });
            this.cmbCoin.Location = new System.Drawing.Point(0, 76);
            this.cmbCoin.Name = "cmbCoin";
            this.cmbCoin.SelectedIndex = 0;
            this.cmbCoin.Size = new System.Drawing.Size(150, 22);

            this.pnlCash.Controls.Add(this.lblAddress);
            this.pnlCash.Controls.Add(this.txtAddress);
            this.pnlCash.Controls.Add(this.lblPhone);
            this.pnlCash.Controls.Add(this.txtPhone);
            this.pnlCash.Location = new System.Drawing.Point(20, 118);
            this.pnlCash.Name = "pnlCash";
            this.pnlCash.Size = new System.Drawing.Size(420, 140);
            this.pnlCash.Visible = false;

            this.lblAddress.AutoSize = false;
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblAddress.Location = new System.Drawing.Point(0, 0);
            this.lblAddress.Size = new System.Drawing.Size(200, 18);
            this.lblAddress.Text = "Адрес доставки";

            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Location = new System.Drawing.Point(0, 20);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(410, 22);

            this.lblPhone.AutoSize = false;
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblPhone.Location = new System.Drawing.Point(0, 56);
            this.lblPhone.Size = new System.Drawing.Size(60, 18);
            this.lblPhone.Text = "Телефон";

            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Location = new System.Drawing.Point(0, 76);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 22);

            this.lblAmount.AutoSize = false;
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            this.lblAmount.Location = new System.Drawing.Point(20, 272);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(120, 18);
            this.lblAmount.Text = "Сумма (руб.)";

            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Location = new System.Drawing.Point(20, 292);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 22);
            this.txtAmount.Text = "1500";

            this.btnPay.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.FlatAppearance.BorderSize = 0;
            this.btnPay.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(20, 330);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(420, 44);
            this.btnPay.Text = "Оплатить";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPay.Click += new System.EventHandler(this.ButtonPay_Click);

            this.lblLog.AutoSize = false;
            this.lblLog.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLog.Location = new System.Drawing.Point(20, 388);
            this.lblLog.Name = "lblLog";
            this.lblLog.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.lblLog.Size = new System.Drawing.Size(420, 56);
            this.lblLog.Text = "";
            this.lblLog.Visible = false;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(462, 462);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpMethod);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlPayPal);
            this.Controls.Add(this.pnlCrypto);
            this.Controls.Add(this.pnlCash);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.lblLog);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Оформление заказа";
            this.grpMethod.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.pnlPayPal.ResumeLayout(false);
            this.pnlPayPal.PerformLayout();
            this.pnlCrypto.ResumeLayout(false);
            this.pnlCrypto.PerformLayout();
            this.pnlCash.ResumeLayout(false);
            this.pnlCash.PerformLayout();
            this.ResumeLayout(false);
        }


        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpMethod;
        private System.Windows.Forms.RadioButton rbCard;
        private System.Windows.Forms.RadioButton rbPayPal;
        private System.Windows.Forms.RadioButton rbCrypto;
        private System.Windows.Forms.RadioButton rbCash;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlPayPal;
        private System.Windows.Forms.Panel pnlCrypto;
        private System.Windows.Forms.Panel pnlCash;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Label lblCVV;
        private System.Windows.Forms.TextBox txtCVV;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.TextBox txtExpiry;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblWallet;
        private System.Windows.Forms.TextBox txtWallet;
        private System.Windows.Forms.Label lblCoin;
        private System.Windows.Forms.ComboBox cmbCoin;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Label lblLog;
    }
}