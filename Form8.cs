using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neon_Search
{
    public partial class Form8 : Form
    {
        private const string CurrentVersion = "1.0.0"; 
        private const string VersionCheckUrl = "https://pastebin.com/raw/g2T73kFn";
        public Form8()
        {
            InitializeComponent();
            CheckForUpdates();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string hwid = WindowsIdentity.GetCurrent().User.Value;
            string login = txtLogin.Text.Trim();

            if (string.IsNullOrEmpty(login))
            {
                txtLogin.Clear();
                MessageBox.Show("Введите ключ");
                return;
            }

            using (WebClient webClient = new WebClient())
            {
                string text;
                try
                {
                    text = webClient.DownloadString("https://pastebin.com/raw/F80mDzEb");
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Ошибка при подключении к серверу: " + ex.Message);
                    return;
                }

                string[] users = text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                bool hasAccess = false;

                foreach (string user in users)
                {
                    string[] parts = user.Split(' ');

                    if (parts.Length == 3)
                    {
                        string storedHwid = parts[0];
                        string storedKey = parts[1];
                        string expirationDateStr = parts[2];

                        DateTime expirationDate;
                        if (DateTime.TryParseExact(expirationDateStr, "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expirationDate))
                        {
                            if (storedHwid == hwid && storedKey.Equals(login, StringComparison.OrdinalIgnoreCase))
                            {
                                if (expirationDate > DateTime.Now)
                                {
                                    hasAccess = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (hasAccess)
                {
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Hide();
                }
                else
                {
                    txtLogin.Clear();
                    MessageBox.Show("У вас нет доступа");
                }
            }
        }

        private async void CheckForUpdates()
        {
            string latestVersion = await GetLatestVersionAsync();

            if (latestVersion == CurrentVersion)
            {
                
            }
            else
            {
                MessageBox.Show("Доступ запрещен!", "Тех-работы", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit(); 
            }
        }

        private async Task<string> GetLatestVersionAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string version = await client.GetStringAsync(VersionCheckUrl);
                    return version.Trim();
                }
                catch (Exception)
                {
                    return CurrentVersion; 
                }
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
                
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}