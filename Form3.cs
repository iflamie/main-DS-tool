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
using System.Globalization;

namespace Neon_Search
{
    public partial class Form3 : Form
    {
        public Form3()
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

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
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

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private async void btnGetInfo_Click(object sender, EventArgs e)
        {
            string webhookUrl = txtWebhookUrl.Text.Trim();
            if (!webhookUrl.StartsWith("https://discord.com/api/webhooks/"))
            {
                MessageBox.Show("Invalid webhook URL!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string info = await GetWebhookInfo(webhookUrl);
            txtWebhookInfo.Text = info;
        }

        private async Task<string> GetWebhookInfo(string webhookUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(webhookUrl);
                    response.EnsureSuccessStatusCode(); 

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var webhookInfo = JsonConvert.DeserializeObject<WebhookInfo>(jsonString);

                    if (webhookInfo == null)
                    {
                        return "Error: Unable to parse webhook info from JSON.";
                    }

                    return FormatWebhookInfo(webhookInfo);
                }
                catch (HttpRequestException ex)
                {
                    return $"Network error: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    return $"JSON parsing error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    return $"An unexpected error occurred: {ex.Message}";
                }
            }
        }

        private string FormatWebhookInfo(WebhookInfo webhookInfo)
        {
            return $@"
ID: {webhookInfo.Id}
Token: {webhookInfo.Token}
Name: {webhookInfo.Name}
Avatar: {webhookInfo.Avatar ?? "None"}
Type: {(webhookInfo.Type == 1 ? "Bot" : "User webhook")}
Channel ID: {webhookInfo.ChannelId ?? "None"}
Server ID: {webhookInfo.GuildId ?? "None"} 
";
        }
    }

    public class WebhookInfo
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int Type { get; set; }
        public string ChannelId { get; set; }
        public string GuildId { get; set; }
    }
}