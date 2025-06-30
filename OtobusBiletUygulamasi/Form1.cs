using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OtobusBiletUygulamasi
{
    public partial class Form1 : Form
    {
        private List<Rota> rotalar;
        private List<Bilet> biletler;
        private Button[] koltukButonlari;
        private int secilenKoltukNo = -1;
        private Rota secilenRota;
        private DateTime secilenTarih = DateTime.Today;

        public Form1()
        {
            InitializeComponent();

            try
            {
                DatabaseHelper.InitializeDatabase();
                VeriYukle();
                KoltukDuzeniOlustur();
                RotalariYukle();
                TarihAyarla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TarihAyarla()
        {
            if (dtpSeyahatTarihi != null)
            {
                dtpSeyahatTarihi.MinDate = DateTime.Today;
                dtpSeyahatTarihi.Value = DateTime.Today;
                secilenTarih = DateTime.Today;
            }
        }

        private void VeriYukle()
        {
            rotalar = DatabaseHelper.GetRotalar();
            biletler = DatabaseHelper.GetBiletler();
        }

        private void RotalariYukle()
        {
            if (cmbRota != null)
            {
                cmbRota.Items.Clear();
                foreach (var rota in rotalar)
                    cmbRota.Items.Add($"{rota.Nereden} - {rota.Nereye} ({rota.Saat})");
                if (cmbRota.Items.Count > 0) cmbRota.SelectedIndex = 0;
            }
        }

        private void KoltukDuzeniOlustur()
        {
            if (panelKoltuklar == null)
            {
                MessageBox.Show("Panel kontrolü bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            panelKoltuklar.Controls.Clear();
            koltukButonlari = new Button[48];

            for (int i = 0; i < 48; i++)
            {
                int koltukNo = i + 1;
                int satir = i / 4, sutun = i % 4;
                koltukButonlari[i] = new Button
                {
                    Text = $"👤\n{koltukNo}",
                    Size = new Size(55, 60),
                    Location = new Point(20 + sutun * 60 + (sutun >= 2 ? 30 : 0), 20 + satir * 65),
                    BackColor = Color.LightGray,
                    Tag = koltukNo,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.DarkGray,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                koltukButonlari[i].FlatAppearance.BorderSize = 2;
                koltukButonlari[i].FlatAppearance.BorderColor = Color.Gray;
                koltukButonlari[i].Click += (s, e) => {
                    Button btn = (Button)s;
                    int no = (int)btn.Tag;
                    if (btn.BackColor == Color.CornflowerBlue || btn.BackColor == Color.LightPink) return;

                    if (secilenKoltukNo != -1 && secilenKoltukNo != no)
                    {
                        Button onceki = koltukButonlari[secilenKoltukNo - 1];
                        if (onceki.BackColor != Color.CornflowerBlue && onceki.BackColor != Color.LightPink)
                        {
                            onceki.BackColor = Color.LightGray;
                            onceki.ForeColor = Color.DarkGray;
                        }
                    }

                    if (secilenKoltukNo != no)
                    {
                        secilenKoltukNo = no;
                        btn.BackColor = Color.Gold;
                        btn.ForeColor = Color.DarkBlue;
                        if (lblKoltukBilgi != null)
                            lblKoltukBilgi.Text = $"Seçilen Koltuk: {secilenKoltukNo}";
                    }
                    else
                    {
                        secilenKoltukNo = -1;
                        btn.BackColor = Color.LightGray;
                        btn.ForeColor = Color.DarkGray;
                        if (lblKoltukBilgi != null)
                            lblKoltukBilgi.Text = "Seçilen Koltuk: -";
                    }
                };
                panelKoltuklar.Controls.Add(koltukButonlari[i]);
            }
        }

        private void cmbRota_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRota != null && cmbRota.SelectedIndex >= 0 && lblFiyat != null)
            {
                secilenRota = rotalar[cmbRota.SelectedIndex];
                lblFiyat.Text = $"Bilet Ücreti: {secilenRota.Fiyat} TL";
            }
        }

        private void dtpSeyahatTarihi_ValueChanged(object sender, EventArgs e)
        {
            if (dtpSeyahatTarihi != null)
                secilenTarih = dtpSeyahatTarihi.Value;
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (secilenRota == null)
            {
                MessageBox.Show("Lütfen önce bir rota seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Button buton in koltukButonlari)
            {
                buton.BackColor = Color.LightGray;
                buton.ForeColor = Color.DarkGray;
                buton.Enabled = true;
            }

            secilenKoltukNo = -1;
            if (lblKoltukBilgi != null)
                lblKoltukBilgi.Text = "Seçilen Koltuk: -";

            foreach (var bilet in biletler.Where(b => b.RotaId == secilenRota.RotaId && b.SeyahatTarihi.Date == secilenTarih.Date))
            {
                int idx = bilet.KoltukNo - 1;
                if (idx >= 0 && idx < koltukButonlari.Length)
                {
                    if (bilet.Cinsiyet == "Erkek")
                    {
                        koltukButonlari[idx].BackColor = Color.CornflowerBlue;
                        koltukButonlari[idx].ForeColor = Color.White;
                    }
                    else
                    {
                        koltukButonlari[idx].BackColor = Color.LightPink;
                        koltukButonlari[idx].ForeColor = Color.DarkRed;
                    }
                    koltukButonlari[idx].Enabled = false;
                }
            }

            int doluKoltuk = biletler.Count(b => b.RotaId == secilenRota.RotaId && b.SeyahatTarihi.Date == secilenTarih.Date);
            MessageBox.Show($"Sefer bilgileri yüklendi.\n{doluKoltuk} koltuk dolu, {48 - doluKoltuk} koltuk boş.",
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBiletiSatinAl_Click(object sender, EventArgs e)
        {
            if (secilenKoltukNo == -1)
            {
                MessageBox.Show("Lütfen bir koltuk seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (secilenRota == null)
            {
                MessageBox.Show("Lütfen bir rota seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Lütfen ad soyad giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAd?.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTC.Text) || txtTC.Text.Length != 11)
            {
                MessageBox.Show("TC Kimlik numarası 11 haneli olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTC?.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTelefon?.Text))
            {
                MessageBox.Show("Lütfen telefon numarası giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefon?.Focus();
                return;
            }

            Bilet bilet = new Bilet
            {
                YolcuAdi = txtAd.Text.Trim(),
                TC = txtTC.Text.Trim(),
                Telefon = txtTelefon?.Text?.Trim() ?? "",
                Cinsiyet = rbErkek?.Checked == true ? "Erkek" : "Kadın",
                KoltukNo = secilenKoltukNo,
                RotaId = secilenRota.RotaId,
                SeyahatTarihi = secilenTarih,
                Fiyat = secilenRota.Fiyat
            };

            if (DatabaseHelper.AddBilet(bilet))
            {
                biletler.Add(bilet);
                if (rbErkek?.Checked == true)
                {
                    koltukButonlari[secilenKoltukNo - 1].BackColor = Color.CornflowerBlue;
                    koltukButonlari[secilenKoltukNo - 1].ForeColor = Color.White;
                }
                else
                {
                    koltukButonlari[secilenKoltukNo - 1].BackColor = Color.LightPink;
                    koltukButonlari[secilenKoltukNo - 1].ForeColor = Color.DarkRed;
                }
                koltukButonlari[secilenKoltukNo - 1].Enabled = false;

                MessageBox.Show($"Bilet başarıyla oluşturuldu!\n\n" +
                               $"Yolcu: {bilet.YolcuAdi}\n" +
                               $"TC: {bilet.TC}\n" +
                               $"Telefon: {bilet.Telefon}\n" +
                               $"Cinsiyet: {bilet.Cinsiyet}\n" +
                               $"Rota: {secilenRota.Nereden} - {secilenRota.Nereye}\n" +
                               $"Tarih: {bilet.SeyahatTarihi.ToShortDateString()}\n" +
                               $"Saat: {secilenRota.Saat}\n" +
                               $"Koltuk: {bilet.KoltukNo}\n" +
                               $"Ücret: {bilet.Fiyat} TL", "Bilet Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                secilenKoltukNo = -1;
                if (lblKoltukBilgi != null)
                    lblKoltukBilgi.Text = "Seçilen Koltuk: -";
                if (txtAd != null) txtAd.Text = "";
                if (txtTC != null) txtTC.Text = "";
                if (txtTelefon != null) txtTelefon.Text = "";
                if (rbKadin != null) rbKadin.Checked = true;

                btnAra_Click(sender, e);
            }
        }

        private void txtTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            AdminPanelForm adminForm = new AdminPanelForm();
            if (adminForm.ShowDialog() == DialogResult.OK)
            {
                VeriYukle();
                RotalariYukle();
                MessageBox.Show("Değişiklikler başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnRezervasyonIptal_Click(object sender, EventArgs e)
        {
            RezervasyonIptalForm iptalForm = new RezervasyonIptalForm();
            if (iptalForm.ShowDialog() == DialogResult.OK)
            {
                VeriYukle();
                if (secilenRota != null)
                {
                    btnAra_Click(sender, e);
                }
                MessageBox.Show("Rezervasyon başarıyla iptal edildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnOtobusYonetimi_Click(object sender, EventArgs e)
        {
            OtobusYonetimiForm otobusForm = new OtobusYonetimiForm();
            if (otobusForm.ShowDialog() == DialogResult.OK)
            {
                VeriYukle();
                RotalariYukle();
                MessageBox.Show("Rota bilgileri güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    
    public partial class RezervasyonIptalForm : Form
    {
        private TextBox txtTCIptal;
        private DataGridView dgvRezervasyonlar;
        private Button btnSorgula, btnIptalEt;
        private List<Bilet> bulunanBiletler;

        public RezervasyonIptalForm()
        {
            InitializeRezervasyonComponents();
        }

        private void InitializeRezervasyonComponents()
        {
            this.Text = "Rezervasyon İptal";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            Label lblBaslik = new Label
            {
                Text = "REZERVASYON İPTAL İŞLEMLERİ",
                Location = new Point(200, 20),
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };

            Label lblTC = new Label
            {
                Text = "TC Kimlik No:",
                Location = new Point(20, 70),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            txtTCIptal = new TextBox
            {
                Location = new Point(120, 67),
                Size = new Size(150, 25),
                MaxLength = 11
            };
            txtTCIptal.KeyPress += (s, e) => {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            };

            btnSorgula = new Button
            {
                Text = "Sorgula",
                Location = new Point(290, 65),
                Size = new Size(80, 30),
                BackColor = Color.Blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSorgula.Click += BtnSorgula_Click;

            dgvRezervasyonlar = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(640, 280),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };

            btnIptalEt = new Button
            {
                Text = "Seçili Rezervasyonu İptal Et",
                Location = new Point(250, 410),
                Size = new Size(180, 40),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnIptalEt.Click += BtnIptalEt_Click;

            dgvRezervasyonlar.SelectionChanged += (s, e) => {
                btnIptalEt.Enabled = dgvRezervasyonlar.SelectedRows.Count > 0;
            };

            this.Controls.AddRange(new Control[] {
                lblBaslik, lblTC, txtTCIptal, btnSorgula, dgvRezervasyonlar, btnIptalEt
            });
        }

        private void BtnSorgula_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTCIptal.Text) || txtTCIptal.Text.Length != 11)
            {
                MessageBox.Show("Geçerli bir TC Kimlik numarası girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bulunanBiletler = DatabaseHelper.GetBiletlerByTC(txtTCIptal.Text.Trim());

            if (bulunanBiletler.Count == 0)
            {
                MessageBox.Show("Bu TC kimlik numarasına ait gelecek tarihli rezervasyon bulunamadı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvRezervasyonlar.DataSource = null;
                return;
            }

            var rotalar = DatabaseHelper.GetRotalar();
            var rezervasyonListesi = bulunanBiletler.Select(b => {
                var rota = rotalar.FirstOrDefault(r => r.RotaId == b.RotaId);
                return new
                {
                    BiletId = b.BiletId,
                    YolcuAdi = b.YolcuAdi,
                    Rota = rota != null ? $"{rota.Nereden} - {rota.Nereye}" : "Bilinmiyor",
                    Saat = rota?.Saat ?? "Bilinmiyor",
                    SeyahatTarihi = b.SeyahatTarihi.ToShortDateString(),
                    KoltukNo = b.KoltukNo,
                    Fiyat = b.Fiyat + " TL"
                };
            }).ToList();

            dgvRezervasyonlar.DataSource = rezervasyonListesi;
        }

        private void BtnIptalEt_Click(object sender, EventArgs e)
        {
            if (dgvRezervasyonlar.SelectedRows.Count == 0) return;

            var selectedRow = dgvRezervasyonlar.SelectedRows[0];
            int biletId = (int)selectedRow.Cells["BiletId"].Value;
            string yolcuAdi = selectedRow.Cells["YolcuAdi"].Value.ToString();
            string rota = selectedRow.Cells["Rota"].Value.ToString();

            var result = MessageBox.Show($"'{yolcuAdi}' adlı yolcunun '{rota}' rotasındaki rezervasyonunu iptal etmek istediğinizden emin misiniz?",
                "Rezervasyon İptal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (DatabaseHelper.DeleteBilet(biletId))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }

    public partial class OtobusYonetimiForm : Form
    {
        private DataGridView dgvRotalar;
        private TextBox txtRotaNereden, txtRotaNereye, txtRotaSaat, txtRotaFiyat;
        private Button btnRotaEkle, btnRotaDuzenle, btnRotaSil;
        private int secilenRotaId = -1;

        public OtobusYonetimiForm()
        {
            InitializeOtobusComponents();
            VeriYukle();
        }

        private void InitializeOtobusComponents()
        {
            this.Text = "Rota Yönetimi";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            
            Label lblRotaBaslik = new Label
            {
                Text = "ROTA YÖNETİMİ",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            dgvRotalar = new DataGridView
            {
                Location = new Point(20, 50),
                Size = new Size(540, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };
            dgvRotalar.SelectionChanged += DgvRotalar_SelectionChanged;

            Label lblNereden = new Label { Text = "Nereden:", Location = new Point(20, 370), AutoSize = true };
            txtRotaNereden = new TextBox { Location = new Point(90, 367), Size = new Size(120, 25) };

            Label lblNereye = new Label { Text = "Nereye:", Location = new Point(230, 370), AutoSize = true };
            txtRotaNereye = new TextBox { Location = new Point(290, 367), Size = new Size(120, 25) };

            Label lblSaat = new Label { Text = "Saat:", Location = new Point(20, 410), AutoSize = true };
            txtRotaSaat = new TextBox { Location = new Point(60, 407), Size = new Size(80, 25) };

            Label lblFiyat = new Label { Text = "Fiyat:", Location = new Point(160, 410), AutoSize = true };
            txtRotaFiyat = new TextBox { Location = new Point(200, 407), Size = new Size(80, 25) };

            btnRotaEkle = new Button
            {
                Text = "Rota Ekle",
                Location = new Point(300, 405),
                Size = new Size(80, 30),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRotaEkle.Click += BtnRotaEkle_Click;

            btnRotaDuzenle = new Button
            {
                Text = "Düzenle",
                Location = new Point(390, 405),
                Size = new Size(80, 30),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnRotaDuzenle.Click += BtnRotaDuzenle_Click;

            btnRotaSil = new Button
            {
                Text = "Sil",
                Location = new Point(480, 405),
                Size = new Size(80, 30),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnRotaSil.Click += BtnRotaSil_Click;

            this.Controls.AddRange(new Control[] {
                lblRotaBaslik, dgvRotalar, lblNereden, txtRotaNereden, lblNereye, txtRotaNereye,
                lblSaat, txtRotaSaat, lblFiyat, txtRotaFiyat, btnRotaEkle, btnRotaDuzenle, btnRotaSil
            });
        }

        private void VeriYukle()
        {
            var rotalar = DatabaseHelper.GetRotalar();
            dgvRotalar.DataSource = rotalar;
        }

        private void DgvRotalar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRotalar.SelectedRows.Count > 0)
            {
                var selectedRow = dgvRotalar.SelectedRows[0];
                secilenRotaId = (int)selectedRow.Cells["RotaId"].Value;
                txtRotaNereden.Text = selectedRow.Cells["Nereden"].Value.ToString();
                txtRotaNereye.Text = selectedRow.Cells["Nereye"].Value.ToString();
                txtRotaSaat.Text = selectedRow.Cells["Saat"].Value.ToString();
                txtRotaFiyat.Text = selectedRow.Cells["Fiyat"].Value.ToString();

                btnRotaDuzenle.Enabled = true;
                btnRotaSil.Enabled = true;
            }
            else
            {
                secilenRotaId = -1;
                btnRotaDuzenle.Enabled = false;
                btnRotaSil.Enabled = false;
            }
        }

        private void BtnRotaEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRotaNereden.Text) || string.IsNullOrWhiteSpace(txtRotaNereye.Text) ||
                string.IsNullOrWhiteSpace(txtRotaSaat.Text) || string.IsNullOrWhiteSpace(txtRotaFiyat.Text))
            {
                MessageBox.Show("Tüm alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtRotaFiyat.Text, out int fiyat))
            {
                MessageBox.Show("Geçerli bir fiyat giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rota = new Rota
            {
                Nereden = txtRotaNereden.Text.Trim(),
                Nereye = txtRotaNereye.Text.Trim(),
                Saat = txtRotaSaat.Text.Trim(),
                Fiyat = fiyat
            };

            if (DatabaseHelper.AddRota(rota))
            {
                VeriYukle();
                TemizleRotaAlanlari();
                MessageBox.Show("Rota başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnRotaDuzenle_Click(object sender, EventArgs e)
        {
            if (secilenRotaId == -1) return;

            if (string.IsNullOrWhiteSpace(txtRotaNereden.Text) || string.IsNullOrWhiteSpace(txtRotaNereye.Text) ||
                string.IsNullOrWhiteSpace(txtRotaSaat.Text) || string.IsNullOrWhiteSpace(txtRotaFiyat.Text))
            {
                MessageBox.Show("Tüm alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtRotaFiyat.Text, out int fiyat))
            {
                MessageBox.Show("Geçerli bir fiyat giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rota = new Rota
            {
                RotaId = secilenRotaId,
                Nereden = txtRotaNereden.Text.Trim(),
                Nereye = txtRotaNereye.Text.Trim(),
                Saat = txtRotaSaat.Text.Trim(),
                Fiyat = fiyat
            };

            if (DatabaseHelper.UpdateRota(rota))
            {
                VeriYukle();
                TemizleRotaAlanlari();
                MessageBox.Show("Rota başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnRotaSil_Click(object sender, EventArgs e)
        {
            if (secilenRotaId == -1) return;

            var result = MessageBox.Show("Bu rotayı silmek istediğinizden emin misiniz?\nBu rotaya ait tüm biletler de silinecektir!",
                "Rota Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (DatabaseHelper.DeleteRota(secilenRotaId))
                {
                    VeriYukle();
                    TemizleRotaAlanlari();
                    MessageBox.Show("Rota başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void TemizleRotaAlanlari()
        {
            txtRotaNereden.Text = "";
            txtRotaNereye.Text = "";
            txtRotaSaat.Text = "";
            txtRotaFiyat.Text = "";
            secilenRotaId = -1;
            btnRotaDuzenle.Enabled = false;
            btnRotaSil.Enabled = false;
        }
    }

    
    public class Rota
    {
        public int RotaId { get; set; }
        public string Nereden { get; set; }
        public string Nereye { get; set; }
        public string Saat { get; set; }
        public int Fiyat { get; set; }
    }

    public class Bilet
    {
        public int BiletId { get; set; }
        public string YolcuAdi { get; set; }
        public string TC { get; set; }
        public string Telefon { get; set; }
        public string Cinsiyet { get; set; }
        public int KoltukNo { get; set; }
        public int RotaId { get; set; }
        public DateTime SeyahatTarihi { get; set; }
        public decimal Fiyat { get; set; }
    }

    
    public static class DatabaseHelper
    {
        private static string connectionString = "Server=localhost;Database=otobus_bilet;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();

                    string createRotalarTable = @"
                        CREATE TABLE IF NOT EXISTS rotalar (
                            RotaId INT AUTO_INCREMENT PRIMARY KEY,
                            Nereden VARCHAR(100) NOT NULL,
                            Nereye VARCHAR(100) NOT NULL,
                            Saat VARCHAR(10) NOT NULL,
                            Fiyat INT NOT NULL
                        )";

                    string createBiletlerTable = @"
                        CREATE TABLE IF NOT EXISTS biletler (
                            BiletId INT AUTO_INCREMENT PRIMARY KEY,
                            YolcuAdi VARCHAR(100) NOT NULL,
                            TC VARCHAR(11) NOT NULL,
                            Telefon VARCHAR(20) NOT NULL,
                            Cinsiyet VARCHAR(10) NOT NULL,
                            KoltukNo INT NOT NULL,
                            RotaId INT NOT NULL,
                            SeyahatTarihi DATE NOT NULL,
                            Fiyat DECIMAL(10,2) NOT NULL,
                            FOREIGN KEY (RotaId) REFERENCES rotalar(RotaId)
                        )";

                    string createOtobuslerTable = @"
                        CREATE TABLE IF NOT EXISTS otobusler (
                            OtobusId INT AUTO_INCREMENT PRIMARY KEY,
                            Plaka VARCHAR(20) UNIQUE NOT NULL,
                            Kapasite INT NOT NULL DEFAULT 48
                        )";

                    using (var cmd = new MySqlCommand(createRotalarTable, conn))
                        cmd.ExecuteNonQuery();

                    using (var cmd = new MySqlCommand(createBiletlerTable, conn))
                        cmd.ExecuteNonQuery();

                    using (var cmd = new MySqlCommand(createOtobuslerTable, conn))
                        cmd.ExecuteNonQuery();

                    string checkRotalar = "SELECT COUNT(*) FROM rotalar";
                    using (var cmd = new MySqlCommand(checkRotalar, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0)
                        {
                            string insertRotalar = @"
                                INSERT INTO rotalar (Nereden, Nereye, Saat, Fiyat) VALUES
                                ('İstanbul', 'Ankara', '10:00', 250),
                                ('İstanbul', 'İzmir', '12:30', 300),
                                ('Ankara', 'İzmir', '09:15', 280),
                                ('İzmir', 'İstanbul', '08:00', 300),
                                ('Ankara', 'İstanbul', '15:45', 250)";

                            using (var insertCmd = new MySqlCommand(insertRotalar, conn))
                                insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<Rota> GetRotalar()
        {
            List<Rota> rotalar = new List<Rota>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM rotalar";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rotalar.Add(new Rota
                            {
                                RotaId = reader.GetInt32("RotaId"),
                                Nereden = reader.GetString("Nereden"),
                                Nereye = reader.GetString("Nereye"),
                                Saat = reader.GetString("Saat"),
                                Fiyat = reader.GetInt32("Fiyat")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rota yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rotalar;
        }

        public static List<Bilet> GetBiletler()
        {
            List<Bilet> biletler = new List<Bilet>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM biletler";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            biletler.Add(new Bilet
                            {
                                BiletId = reader.GetInt32("BiletId"),
                                YolcuAdi = reader.GetString("YolcuAdi"),
                                TC = reader.GetString("TC"),
                                Telefon = reader.GetString("Telefon"),
                                Cinsiyet = reader.GetString("Cinsiyet"),
                                KoltukNo = reader.GetInt32("KoltukNo"),
                                RotaId = reader.GetInt32("RotaId"),
                                SeyahatTarihi = reader.GetDateTime("SeyahatTarihi"),
                                Fiyat = reader.GetDecimal("Fiyat")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bilet yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return biletler;
        }

        public static bool AddRota(Rota rota)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO rotalar (Nereden, Nereye, Saat, Fiyat) VALUES (@nereden, @nereye, @saat, @fiyat)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nereden", rota.Nereden);
                        cmd.Parameters.AddWithValue("@nereye", rota.Nereye);
                        cmd.Parameters.AddWithValue("@saat", rota.Saat);
                        cmd.Parameters.AddWithValue("@fiyat", rota.Fiyat);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rota ekleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool AddBilet(Bilet bilet)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO biletler (YolcuAdi, TC, Telefon, Cinsiyet, KoltukNo, RotaId, SeyahatTarihi, Fiyat) 
                                   VALUES (@yolcuAdi, @tc, @telefon, @cinsiyet, @koltukNo, @rotaId, @seyahatTarihi, @fiyat)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@yolcuAdi", bilet.YolcuAdi);
                        cmd.Parameters.AddWithValue("@tc", bilet.TC);
                        cmd.Parameters.AddWithValue("@telefon", bilet.Telefon);
                        cmd.Parameters.AddWithValue("@cinsiyet", bilet.Cinsiyet);
                        cmd.Parameters.AddWithValue("@koltukNo", bilet.KoltukNo);
                        cmd.Parameters.AddWithValue("@rotaId", bilet.RotaId);
                        cmd.Parameters.AddWithValue("@seyahatTarihi", bilet.SeyahatTarihi.Date);
                        cmd.Parameters.AddWithValue("@fiyat", bilet.Fiyat);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bilet kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static List<Bilet> GetBiletlerByTC(string tcNo)
        {
            List<Bilet> biletler = new List<Bilet>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM biletler WHERE TC = @tc AND SeyahatTarihi >= CURDATE()";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tc", tcNo);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                biletler.Add(new Bilet
                                {
                                    BiletId = reader.GetInt32("BiletId"),
                                    YolcuAdi = reader.GetString("YolcuAdi"),
                                    TC = reader.GetString("TC"),
                                    Telefon = reader.GetString("Telefon"),
                                    Cinsiyet = reader.GetString("Cinsiyet"),
                                    KoltukNo = reader.GetInt32("KoltukNo"),
                                    RotaId = reader.GetInt32("RotaId"),
                                    SeyahatTarihi = reader.GetDateTime("SeyahatTarihi"),
                                    Fiyat = reader.GetDecimal("Fiyat")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bilet sorgulama hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return biletler;
        }

        public static bool DeleteBilet(int biletId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM biletler WHERE BiletId = @biletId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@biletId", biletId);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bilet silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool UpdateRota(Rota rota)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE rotalar SET Nereden=@nereden, Nereye=@nereye, Saat=@saat, Fiyat=@fiyat WHERE RotaId=@rotaId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nereden", rota.Nereden);
                        cmd.Parameters.AddWithValue("@nereye", rota.Nereye);
                        cmd.Parameters.AddWithValue("@saat", rota.Saat);
                        cmd.Parameters.AddWithValue("@fiyat", rota.Fiyat);
                        cmd.Parameters.AddWithValue("@rotaId", rota.RotaId);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rota güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DeleteRota(int rotaId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();

                    
                    string deleteBiletler = "DELETE FROM biletler WHERE RotaId = @rotaId";
                    using (var cmd = new MySqlCommand(deleteBiletler, conn))
                    {
                        cmd.Parameters.AddWithValue("@rotaId", rotaId);
                        cmd.ExecuteNonQuery();
                    }

                    
                    string deleteRota = "DELETE FROM rotalar WHERE RotaId = @rotaId";
                    using (var cmd = new MySqlCommand(deleteRota, conn))
                    {
                        cmd.Parameters.AddWithValue("@rotaId", rotaId);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rota silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }

    public partial class AdminPanelForm : Form
    {
        private TextBox txtNereden, txtNereye, txtSaat, txtFiyat;
        private Button btnRotaEkle;
        private DataGridView dgvRotalar;

        public AdminPanelForm()
        {
            InitializeAdminComponents();
            VeriYukle();
        }

        private void InitializeAdminComponents()
        {
            this.Text = "Admin Paneli - Sefer Yönetimi";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            dgvRotalar = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(540, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            Label lblBaslik = new Label
            {
                Text = "Yeni Sefer Ekle:",
                Location = new Point(20, 340),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            Label lblNereden = new Label { Text = "Nereden:", Location = new Point(20, 370), AutoSize = true };
            txtNereden = new TextBox { Location = new Point(100, 367), Size = new Size(120, 25) };

            Label lblNereye = new Label { Text = "Nereye:", Location = new Point(240, 370), AutoSize = true };
            txtNereye = new TextBox { Location = new Point(300, 367), Size = new Size(120, 25) };

            Label lblSaat = new Label { Text = "Saat:", Location = new Point(20, 410), AutoSize = true };
            txtSaat = new TextBox { Location = new Point(100, 407), Size = new Size(80, 25) };

            Label lblFiyat = new Label { Text = "Fiyat:", Location = new Point(200, 410), AutoSize = true };
            txtFiyat = new TextBox { Location = new Point(250, 407), Size = new Size(80, 25) };

            btnRotaEkle = new Button
            {
                Text = "Sefer Ekle",
                Location = new Point(350, 405),
                Size = new Size(100, 30),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRotaEkle.Click += BtnRotaEkle_Click;

            this.Controls.AddRange(new Control[] {
                dgvRotalar, lblBaslik, lblNereden, txtNereden, lblNereye, txtNereye,
                lblSaat, txtSaat, lblFiyat, txtFiyat, btnRotaEkle
            });
        }

        private void VeriYukle()
        {
            var rotalar = DatabaseHelper.GetRotalar();
            dgvRotalar.DataSource = rotalar;
        }

        private void BtnRotaEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNereden.Text) || string.IsNullOrWhiteSpace(txtNereye.Text) ||
                string.IsNullOrWhiteSpace(txtSaat.Text) || string.IsNullOrWhiteSpace(txtFiyat.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtFiyat.Text, out int fiyat))
            {
                MessageBox.Show("Geçerli bir fiyat girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Rota yeniRota = new Rota
            {
                Nereden = txtNereden.Text.Trim(),
                Nereye = txtNereye.Text.Trim(),
                Saat = txtSaat.Text.Trim(),
                Fiyat = fiyat
            };

            if (DatabaseHelper.AddRota(yeniRota))
            {
                VeriYukle();
                txtNereden.Text = txtNereye.Text = txtSaat.Text = txtFiyat.Text = "";
                MessageBox.Show("Sefer başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
