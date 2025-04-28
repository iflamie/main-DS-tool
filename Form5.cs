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
using System.Threading;

namespace Neon_Search
{
    public partial class Form5 : Form
    {
        public Form5()
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

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

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private CancellationTokenSource cancellationTokenSource;
        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            string webhookUrl = txtWebhookUrl.Text.Trim();
            string usernameWebhook = txtUsername.Text.Trim();
            string avatarWebhook = txtAvatarUrl.Text.Trim();
            int threadsNumber;

            if (!int.TryParse(txtThreads.Text.Trim(), out threadsNumber) || threadsNumber <= 0)
            {
                MessageBox.Show("Введите корректное количество потоков!");
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Request(webhookUrl, usernameWebhook, avatarWebhook, threadsNumber, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                AppendToOutput("Процесс остановлен.");
            }
        }

        private async Task Request(string webhookUrl, string usernameWebhook, string avatarWebhook, int threadsNumber, CancellationToken token)
        {
            var tasks = new List<Task>();

            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < threadsNumber; i++)
                {
                    tasks.Add(WebhookCheck(webhookUrl, usernameWebhook, avatarWebhook, token));
                }

                await Task.WhenAll(tasks);
                tasks.Clear();
            }
        }

        private async Task WebhookCheck(string webhookUrl, string usernameWebhook, string avatarWebhook, CancellationToken token)
        {
            if (token.IsCancellationRequested) return;

            long first = new Random().Next(0, int.MaxValue) + (long)new Random().Next(0, int.MaxValue) * (long)int.MaxValue;
            string second = GenerateRandomString(68);
            string webhookTestCode = $"{first}/{second}";
            string webhookTestUrl = $"https://discord.com/api/webhooks/{webhookTestCode}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Head, webhookTestUrl));

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var embedContent = new
                        {
                            title = "Webhook Valid!",
                            description = $"**Webhook:**\n{webhookTestCode}",
                            color = 0x00FF00,
                            footer = new
                            {
                                text = "Webhook Generator",
                                icon_url = "https://example.com/avatar.png",
                            }
                        };

                        await SendWebhook(webhookUrl, embedContent, usernameWebhook, avatarWebhook);
                        AppendToOutput($"Status: Valid Webhook: {webhookTestCode}");
                    }
                    else
                    {
                        AppendToOutput($"Status: Invalid Webhook: {webhookTestCode}");
                    }
                }
                catch (Exception ex)
                {
                    AppendToOutput($"Error: {ex.Message} Webhook: {webhookTestCode}");
                }
            }
        }

        private async Task SendWebhook(string webhookUrl, object embedContent, string usernameWebhook, string avatarWebhook)
        {
            var payload = new
            {
                embeds = new[] { embedContent },
                username = usernameWebhook,
                avatar_url = avatarWebhook
            };

            using (HttpClient client = new HttpClient())
            {
                var json = System.Text.Json.JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(webhookUrl, content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    AppendToOutput($"Error sending webhook: {ex.Message}");
                }
            }
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randomString = new char[length];
            var rng = new Random();

            for (int i = 0; i < length; i++)
            {
                randomString[i] = chars[rng.Next(chars.Length)];
            }

            return new string(randomString);
        }

        private void AppendToOutput(string message)
        {
            txtOutput.AppendText(message + Environment.NewLine);
            txtOutput.ScrollToCaret();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
