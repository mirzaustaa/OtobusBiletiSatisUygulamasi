namespace OtobusBiletUygulamasi
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAdminPanel;
        private System.Windows.Forms.Button btnRezervasyonIptal;
        private System.Windows.Forms.Button btnOtobusYonetimi;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblRota;
        private System.Windows.Forms.ComboBox cmbRota;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.DateTimePicker dtpSeyahatTarihi;
        private System.Windows.Forms.Button btnAra;
        private System.Windows.Forms.Label lblKoltukBaslik;
        private System.Windows.Forms.Panel panelKoltuklar;
        private System.Windows.Forms.Label lblYolcuBaslik;
        private System.Windows.Forms.Label lblAd;
        private System.Windows.Forms.TextBox txtAd;
        private System.Windows.Forms.Label lblTC;
        private System.Windows.Forms.TextBox txtTC;
        private System.Windows.Forms.Label lblTelefon;
        private System.Windows.Forms.TextBox txtTelefon;
        private System.Windows.Forms.Label lblCinsiyet;
        private System.Windows.Forms.RadioButton rbErkek;
        private System.Windows.Forms.RadioButton rbKadin;
        private System.Windows.Forms.Label lblKoltukBilgi;
        private System.Windows.Forms.Label lblFiyat;
        private System.Windows.Forms.Button btnBiletiSatinAl;

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
            this.btnAdminPanel = new System.Windows.Forms.Button();
            this.btnRezervasyonIptal = new System.Windows.Forms.Button();
            this.btnOtobusYonetimi = new System.Windows.Forms.Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblRota = new System.Windows.Forms.Label();
            this.cmbRota = new System.Windows.Forms.ComboBox();
            this.lblTarih = new System.Windows.Forms.Label();
            this.dtpSeyahatTarihi = new System.Windows.Forms.DateTimePicker();
            this.btnAra = new System.Windows.Forms.Button();
            this.lblKoltukBaslik = new System.Windows.Forms.Label();
            this.panelKoltuklar = new System.Windows.Forms.Panel();
            this.lblYolcuBaslik = new System.Windows.Forms.Label();
            this.lblAd = new System.Windows.Forms.Label();
            this.txtAd = new System.Windows.Forms.TextBox();
            this.lblTC = new System.Windows.Forms.Label();
            this.txtTC = new System.Windows.Forms.TextBox();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.txtTelefon = new System.Windows.Forms.TextBox();
            this.lblCinsiyet = new System.Windows.Forms.Label();
            this.rbErkek = new System.Windows.Forms.RadioButton();
            this.rbKadin = new System.Windows.Forms.RadioButton();
            this.lblKoltukBilgi = new System.Windows.Forms.Label();
            this.lblFiyat = new System.Windows.Forms.Label();
            this.btnBiletiSatinAl = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // btnAdminPanel
            this.btnAdminPanel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnAdminPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminPanel.ForeColor = System.Drawing.Color.White;
            this.btnAdminPanel.Location = new System.Drawing.Point(20, 45);
            this.btnAdminPanel.Name = "btnAdminPanel";
            this.btnAdminPanel.Size = new System.Drawing.Size(100, 30);
            this.btnAdminPanel.TabIndex = 0;
            this.btnAdminPanel.Text = "Admin Panel";
            this.btnAdminPanel.UseVisualStyleBackColor = false;
            this.btnAdminPanel.Click += new System.EventHandler(this.btnAdminPanel_Click);

            // btnRezervasyonIptal
            this.btnRezervasyonIptal.BackColor = System.Drawing.Color.DarkOrange;
            this.btnRezervasyonIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRezervasyonIptal.ForeColor = System.Drawing.Color.White;
            this.btnRezervasyonIptal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnRezervasyonIptal.Location = new System.Drawing.Point(130, 45);
            this.btnRezervasyonIptal.Name = "btnRezervasyonIptal";
            this.btnRezervasyonIptal.Size = new System.Drawing.Size(120, 30);
            this.btnRezervasyonIptal.TabIndex = 1;
            this.btnRezervasyonIptal.Text = "Rezervasyon İptal";
            this.btnRezervasyonIptal.UseVisualStyleBackColor = false;
            this.btnRezervasyonIptal.Click += new System.EventHandler(this.BtnRezervasyonIptal_Click);

            // btnOtobusYonetimi
            this.btnOtobusYonetimi.BackColor = System.Drawing.Color.Purple;
            this.btnOtobusYonetimi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtobusYonetimi.ForeColor = System.Drawing.Color.White;
            this.btnOtobusYonetimi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnOtobusYonetimi.Location = new System.Drawing.Point(260, 45);
            this.btnOtobusYonetimi.Name = "btnOtobusYonetimi";
            this.btnOtobusYonetimi.Size = new System.Drawing.Size(120, 30);
            this.btnOtobusYonetimi.TabIndex = 2;
            this.btnOtobusYonetimi.Text = "Rota Yönetimi";
            this.btnOtobusYonetimi.UseVisualStyleBackColor = false;
            this.btnOtobusYonetimi.Click += new System.EventHandler(this.BtnOtobusYonetimi_Click);

            // lblBaslik
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblBaslik.Location = new System.Drawing.Point(400, 20);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(296, 29);
            this.lblBaslik.TabIndex = 3;
            this.lblBaslik.Text = "OTOBÜS BİLET SİSTEMİ";

            // lblRota
            this.lblRota.AutoSize = true;
            this.lblRota.Location = new System.Drawing.Point(30, 100);
            this.lblRota.Name = "lblRota";
            this.lblRota.Size = new System.Drawing.Size(33, 13);
            this.lblRota.TabIndex = 4;
            this.lblRota.Text = "Rota:";

            // cmbRota
            this.cmbRota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRota.Location = new System.Drawing.Point(100, 97);
            this.cmbRota.Name = "cmbRota";
            this.cmbRota.Size = new System.Drawing.Size(250, 21);
            this.cmbRota.TabIndex = 5;
            this.cmbRota.SelectedIndexChanged += new System.EventHandler(this.cmbRota_SelectedIndexChanged);

            // lblTarih
            this.lblTarih.AutoSize = true;
            this.lblTarih.Location = new System.Drawing.Point(370, 100);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(34, 13);
            this.lblTarih.TabIndex = 6;
            this.lblTarih.Text = "Tarih:";

            // dtpSeyahatTarihi
            this.dtpSeyahatTarihi.Location = new System.Drawing.Point(430, 97);
            this.dtpSeyahatTarihi.MinDate = System.DateTime.Today;
            this.dtpSeyahatTarihi.Name = "dtpSeyahatTarihi";
            this.dtpSeyahatTarihi.Size = new System.Drawing.Size(120, 20);
            this.dtpSeyahatTarihi.TabIndex = 7;
            this.dtpSeyahatTarihi.Value = System.DateTime.Today;
            this.dtpSeyahatTarihi.ValueChanged += new System.EventHandler(this.dtpSeyahatTarihi_ValueChanged);

            // btnAra
            this.btnAra.BackColor = System.Drawing.Color.Orange;
            this.btnAra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAra.ForeColor = System.Drawing.Color.White;
            this.btnAra.Location = new System.Drawing.Point(560, 95);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(80, 23);
            this.btnAra.TabIndex = 8;
            this.btnAra.Text = "Ara";
            this.btnAra.UseVisualStyleBackColor = false;
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);

            // lblKoltukBaslik
            this.lblKoltukBaslik.AutoSize = true;
            this.lblKoltukBaslik.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblKoltukBaslik.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblKoltukBaslik.Location = new System.Drawing.Point(30, 140);
            this.lblKoltukBaslik.Name = "lblKoltukBaslik";
            this.lblKoltukBaslik.Size = new System.Drawing.Size(120, 19);
            this.lblKoltukBaslik.TabIndex = 9;
            this.lblKoltukBaslik.Text = "Koltuk Seçimi:";

            // panelKoltuklar
            this.panelKoltuklar.AutoScroll = true;
            this.panelKoltuklar.BackColor = System.Drawing.Color.White;
            this.panelKoltuklar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKoltuklar.Location = new System.Drawing.Point(30, 170);
            this.panelKoltuklar.Name = "panelKoltuklar";
            this.panelKoltuklar.Size = new System.Drawing.Size(500, 550);
            this.panelKoltuklar.TabIndex = 10;

            // lblYolcuBaslik
            this.lblYolcuBaslik.AutoSize = true;
            this.lblYolcuBaslik.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblYolcuBaslik.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblYolcuBaslik.Location = new System.Drawing.Point(550, 140);
            this.lblYolcuBaslik.Name = "lblYolcuBaslik";
            this.lblYolcuBaslik.Size = new System.Drawing.Size(118, 19);
            this.lblYolcuBaslik.TabIndex = 11;
            this.lblYolcuBaslik.Text = "Yolcu Bilgileri:";

            // lblAd
            this.lblAd.AutoSize = true;
            this.lblAd.Location = new System.Drawing.Point(550, 180);
            this.lblAd.Name = "lblAd";
            this.lblAd.Size = new System.Drawing.Size(56, 13);
            this.lblAd.TabIndex = 12;
            this.lblAd.Text = "Ad Soyad:";

            // txtAd
            this.txtAd.Location = new System.Drawing.Point(660, 177);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(250, 20);
            this.txtAd.TabIndex = 13;

            // lblTC
            this.lblTC.AutoSize = true;
            this.lblTC.Location = new System.Drawing.Point(550, 220);
            this.lblTC.Name = "lblTC";
            this.lblTC.Size = new System.Drawing.Size(54, 13);
            this.lblTC.TabIndex = 14;
            this.lblTC.Text = "TC Kimlik:";

            // txtTC
            this.txtTC.Location = new System.Drawing.Point(660, 217);
            this.txtTC.MaxLength = 11;
            this.txtTC.Name = "txtTC";
            this.txtTC.Size = new System.Drawing.Size(250, 20);
            this.txtTC.TabIndex = 15;
            this.txtTC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTC_KeyPress);

            // lblTelefon
            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Location = new System.Drawing.Point(550, 260);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(46, 13);
            this.lblTelefon.TabIndex = 16;
            this.lblTelefon.Text = "Telefon:";

            // txtTelefon
            this.txtTelefon.Location = new System.Drawing.Point(660, 257);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(250, 20);
            this.txtTelefon.TabIndex = 17;

            // lblCinsiyet
            this.lblCinsiyet.AutoSize = true;
            this.lblCinsiyet.Location = new System.Drawing.Point(550, 300);
            this.lblCinsiyet.Name = "lblCinsiyet";
            this.lblCinsiyet.Size = new System.Drawing.Size(46, 13);
            this.lblCinsiyet.TabIndex = 18;
            this.lblCinsiyet.Text = "Cinsiyet:";

            // rbErkek
            this.rbErkek.AutoSize = true;
            this.rbErkek.Location = new System.Drawing.Point(660, 300);
            this.rbErkek.Name = "rbErkek";
            this.rbErkek.Size = new System.Drawing.Size(53, 17);
            this.rbErkek.TabIndex = 19;
            this.rbErkek.Text = "Erkek";
            this.rbErkek.UseVisualStyleBackColor = true;

            // rbKadin
            this.rbKadin.AutoSize = true;
            this.rbKadin.Checked = true;
            this.rbKadin.Location = new System.Drawing.Point(760, 300);
            this.rbKadin.Name = "rbKadin";
            this.rbKadin.Size = new System.Drawing.Size(52, 17);
            this.rbKadin.TabIndex = 20;
            this.rbKadin.TabStop = true;
            this.rbKadin.Text = "Kadın";
            this.rbKadin.UseVisualStyleBackColor = true;

            // lblKoltukBilgi
            this.lblKoltukBilgi.AutoSize = true;
            this.lblKoltukBilgi.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblKoltukBilgi.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblKoltukBilgi.Location = new System.Drawing.Point(550, 350);
            this.lblKoltukBilgi.Name = "lblKoltukBilgi";
            this.lblKoltukBilgi.Size = new System.Drawing.Size(124, 18);
            this.lblKoltukBilgi.TabIndex = 21;
            this.lblKoltukBilgi.Text = "Seçilen Koltuk: -";

            // lblFiyat
            this.lblFiyat.AutoSize = true;
            this.lblFiyat.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblFiyat.ForeColor = System.Drawing.Color.DarkRed;
            this.lblFiyat.Location = new System.Drawing.Point(550, 380);
            this.lblFiyat.Name = "lblFiyat";
            this.lblFiyat.Size = new System.Drawing.Size(124, 18);
            this.lblFiyat.TabIndex = 22;
            this.lblFiyat.Text = "Bilet Ücreti: - TL";

            // btnBiletiSatinAl
            this.btnBiletiSatinAl.BackColor = System.Drawing.Color.Green;
            this.btnBiletiSatinAl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBiletiSatinAl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnBiletiSatinAl.ForeColor = System.Drawing.Color.White;
            this.btnBiletiSatinAl.Location = new System.Drawing.Point(660, 430);
            this.btnBiletiSatinAl.Name = "btnBiletiSatinAl";
            this.btnBiletiSatinAl.Size = new System.Drawing.Size(200, 50);
            this.btnBiletiSatinAl.TabIndex = 23;
            this.btnBiletiSatinAl.Text = "Bileti Satın Al";
            this.btnBiletiSatinAl.UseVisualStyleBackColor = false;
            this.btnBiletiSatinAl.Click += new System.EventHandler(this.btnBiletiSatinAl_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Controls.Add(this.btnBiletiSatinAl);
            this.Controls.Add(this.lblFiyat);
            this.Controls.Add(this.lblKoltukBilgi);
            this.Controls.Add(this.rbKadin);
            this.Controls.Add(this.rbErkek);
            this.Controls.Add(this.lblCinsiyet);
            this.Controls.Add(this.txtTelefon);
            this.Controls.Add(this.lblTelefon);
            this.Controls.Add(this.txtTC);
            this.Controls.Add(this.lblTC);
            this.Controls.Add(this.txtAd);
            this.Controls.Add(this.lblAd);
            this.Controls.Add(this.lblYolcuBaslik);
            this.Controls.Add(this.panelKoltuklar);
            this.Controls.Add(this.lblKoltukBaslik);
            this.Controls.Add(this.btnAra);
            this.Controls.Add(this.dtpSeyahatTarihi);
            this.Controls.Add(this.lblTarih);
            this.Controls.Add(this.cmbRota);
            this.Controls.Add(this.lblRota);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.btnOtobusYonetimi);
            this.Controls.Add(this.btnRezervasyonIptal);
            this.Controls.Add(this.btnAdminPanel);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🚌 Otobüs Bilet Sistemi";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
