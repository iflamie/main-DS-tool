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
    public partial class Form7 : Form
    {
        private const string ApiUrl = "https://leakosintapi.com/";
        private const string ApiToken = "1810021980:gBJds6KN";
        public Form7()
        {
            foreach (Control control in this.Controls)
            {
                control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            }

            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string term = txtSearch.Text;
            lblStatus.Text = "Search...";
            rtbResults.Clear();

            try
            {
                using (var client = new HttpClient())
                {
                    var data = new
                    {
                        token = ApiToken,
                        request = term,
                        limit = 100,
                        lang = "ru"
                    };

                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(ApiUrl, content);
                    response.EnsureSuccessStatusCode();

                    var responseJson = await response.Content.ReadAsStringAsync();
                    var results = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);

                    if (results != null)
                    {
                        DisplayResults(results);
                    }
                    else
                    {
                        lblStatus.Text = "Ошибка: Неверный формат ответа от сервера.";
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                lblStatus.Text = $"Ошибка сети: {ex.Message}";
            }
            catch (JsonException ex)
            {
                lblStatus.Text = $"Ошибка JSON: {ex.Message}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Произошла ошибка: {ex.Message}";
            }
            finally
            {
                lblStatus.Text = "Data found";
            }
        }

        private void DisplayJsonRecursive(JToken token, int indentLevel = 0)
        {
            string indent = new string(' ', indentLevel * 4);

            if (token is JValue value)
            {
                rtbResults.AppendText($"{indent}{value.ToString()}\n");
            }
            else if (token is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    if (property.Name != "InfoLeak" && property.Name != "NumOfResults")
                    {
                        rtbResults.AppendText($"{indent}{property.Name}: {property.Value}\n");
                    }
                }
            }
            else if (token is JArray array)
            {
                foreach (var item in array)
                {
                    DisplayJsonRecursive(item, indentLevel);
                }
            }
            else
            {
                rtbResults.AppendText($"{indent}[-] Неизвестный тип данных: {token.Type}\n");
            }
        }

        private void DisplayResults(Dictionary<string, object> results)
        {
            if (results != null && results.ContainsKey("List") && results["List"] is JObject list)
            {
                foreach (var kvp in list)
                {
                    rtbResults.AppendText($"\n[@] basedata: {kvp.Key}\n");

                    if (kvp.Value is JObject dataObj)
                    {
                        if (dataObj.ContainsKey("Data\n"))
                        {
                            DisplayJsonRecursive(dataObj["Data\n"]);
                        }
                        else
                        {
                            DisplayJsonRecursive(dataObj);
                        }
                    }
                    else if (kvp.Value is JArray dataArray)
                    {
                        foreach (var item in dataArray)
                        {
                            DisplayJsonRecursive(item);
                        }
                    }
                    else
                    {
                        rtbResults.AppendText($"[!] Необработанный тип данных для {kvp.Key}: {kvp.Value.GetType()}\n");
                    }

                    rtbResults.AppendText("\n");
                }
            }
            else
            {
                rtbResults.AppendText("[-] Неверные данные:\n" + JsonConvert.SerializeObject(results, Formatting.Indented));
            }
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
            this.Hide();
        }
    }
}
