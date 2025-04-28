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
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Contexts;

namespace Neon_Search
{
    public partial class Form2 : Form
    {
        private HttpClient httpClient = new HttpClient();
        public Form2()
        {
            InitializeComponent();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/Flamie_tool");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/Flamie_tool");
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

        private void Form2_Load(object sender, EventArgs e)
        {
            txtOutput.ReadOnly = true;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            string token = txtToken.Text.Trim();
            string channel = txtChannel.Text.Trim();
            string message = txtMessage.Text.Trim();
            int threadsNumber;
            int messagesCount;

            if (!int.TryParse(txtThreads.Text.Trim(), out threadsNumber) || threadsNumber < 1 || threadsNumber > 10)
            {
                MessageBox.Show("Please enter a valid number of threads (1 to 10).");
                return;
            }

            if (!int.TryParse(txtMessages.Text.Trim(), out messagesCount) || messagesCount < 1)
            {
                MessageBox.Show("Please enter a valid number of messages greater than 0.");
                return;
            }

            await StartSpamming(token, channel, message, threadsNumber, messagesCount);
        }

        private async Task StartSpamming(string token, string channel, string message, int threadsNumber, int messagesCount)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", token);

                for (int j = 0; j < messagesCount; j++)
                {
                    for (int i = 0; i < threadsNumber; i++)
                    {
                        SendMessage(client, channel, message);
                    }

                    await Task.Delay(500); 
                }
            }
        }

        private async void SendMessage(HttpClient client, string channel, string message)
        {
            var content = new StringContent("{\"content\": \"" + message + "\"}", System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://discord.com/api/v10/channels/{channel}/messages", content);

            if (response.IsSuccessStatusCode)
            {
                txtOutput.AppendText($"{DateTime.Now.ToString("HH:mm:ss")} Message: {message}... Channel: {channel} Status: Sent\n");
            }
            else
            {
                txtOutput.AppendText($"{DateTime.Now.ToString("HH:mm:ss")} Error sending message: {response.ReasonPhrase}\n");
            }
        }

        private void txtThreads_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
