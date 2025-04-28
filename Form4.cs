using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Guna.UI2.WinForms;
using System.Net;
using System.Security.Principal;
using System.Management.Instrumentation;

namespace Neon_Search
{
    public partial class Form4 : Form
    {
        private HttpClient httpClient = new HttpClient();
        public Form4()
        {
            InitializeComponent();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/Flamie_tool");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/Flamie_tool");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
            this.Hide();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            frm7.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private async void btnDeleteWebhook_Click(object sender, EventArgs e)
        {
            string webhookUrl = txtWebhookUrl.Text.Trim();

            if (string.IsNullOrEmpty(webhookUrl))
            {
                MessageBox.Show("Пожалуйста, введите URL вебхука.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Uri.IsWellFormedUriString(webhookUrl, UriKind.Absolute))
            {
                MessageBox.Show("Неверный формат URL вебхука.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                btnDeleteWebhook.Enabled = false;
                txtOutput.Text = $"{DateTime.Now:HH:mm:ss} Удаление вебхука...\r\n";

                await DeleteWebhookAsync(webhookUrl);
                txtOutput.Text += $"{DateTime.Now:HH:mm:ss} Удаление завершено.\r\n";

            }
            catch (HttpRequestException ex)
            {
                txtOutput.Text = $"{DateTime.Now:HH:mm:ss} Ошибка HTTP: {ex.Message}\r\n";
            }
            catch (Exception ex)
            {
                txtOutput.Text = $"{DateTime.Now:HH:mm:ss} Ошибка: {ex.Message}\r\n";
            }
            finally
            {
                btnDeleteWebhook.Enabled = true;
            }
        }

        private async Task DeleteWebhookAsync(string webhookUrl)
        {
            try
            {
                var response = await httpClient.DeleteAsync(webhookUrl);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"HTTP ошибка: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}