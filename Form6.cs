using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Neon_Search
{
    public partial class Form6 : Form
    {
        private readonly HttpClient httpClient = new HttpClient();
        public Form6()
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
            Form5 frm5 = new Form5();
            frm5.Show();
            this.Hide();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            frm7.Show();
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private async void btnGetInfo_Click(object sender, EventArgs e)
        {
            string token = txtToken.Text;
            txtInfo.Text = await GetDiscordInfo(token);
        }

        private async Task<string> GetDiscordInfo(string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", token);

                var userInfoResponse = await client.GetAsync("https://discord.com/api/v8/users/@me");
                if (!userInfoResponse.IsSuccessStatusCode)
                    return "Ошибка получения информации о пользователе.";

                var userInfo = JObject.Parse(await userInfoResponse.Content.ReadAsStringAsync());

                var guildsResponse = await client.GetAsync("https://discord.com/api/v9/users/@me/guilds?with_counts=true");
                var guildsCount = guildsResponse.IsSuccessStatusCode ?
                    JArray.Parse(await guildsResponse.Content.ReadAsStringAsync()).Count : 0;

                var relationshipsResponse = await client.GetAsync("https://discord.com/api/v8/users/@me/relationships");
                var friendsList = relationshipsResponse.IsSuccessStatusCode ?
                    JArray.Parse(await relationshipsResponse.Content.ReadAsStringAsync()) : new JArray();

                var friends = "";
                foreach (var friend in friendsList)
                {
                    friends += $"{friend["user"]["username"]}#{friend["user"]["discriminator"]} ({friend["user"]["id"]})\n";
                }

                string username = $"{userInfo["username"]}#{userInfo["discriminator"]}";
                string email = (string)userInfo["email"] ?? "None";
                string phone = (string)userInfo["phone"] ?? "None";
                string nitro = (userInfo["premium_type"].ToString() == "0") ? "No" : "Yes";

                return $@"
                Status: Valid
                Token: {token}
                Username: {username}
                Email: {email}
                Phone: {phone}
                Number of guilds: {guildsCount}
                Friends: {friends}
                Nitro: {nitro}";
            }
        }

        private void txtToken_TextChanged(object sender, EventArgs e)
        {

        }
    }
}