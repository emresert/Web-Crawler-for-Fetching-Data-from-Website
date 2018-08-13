using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace DataBinding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {          
        }
        private void button1_Click(object sender, EventArgs e)
        {      
            string nurl = "http://bigpara.hurriyet.com.tr/"; // Header's Url
            WebRequest request = HttpWebRequest.Create(nurl);
            WebResponse response;
            response = request.GetResponse();
            StreamReader information = new StreamReader(response.GetResponseStream());
            string values = information.ReadToEnd();
            int startingheader = values.IndexOf("<title>") + 7; // Html which is called
            int endingheader = values.Substring(startingheader).IndexOf("</title>"); // End of HTML tag
            string header = values.Substring(startingheader, endingheader);
            MessageBox.Show(header);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Uri url = new Uri("http://bigpara.hurriyet.com.tr/Partial/GetPiyasaBandContent/?1533927521000"); // Example URL
            WebClient client = new WebClient();
            string html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);
            HtmlNodeCollection headers = document.DocumentNode.SelectNodes("//li/a/span"); //  Example Nodes
            int i = 0;
            foreach (HtmlNode head in headers)
            {
                string data = head.InnerText;
               data = data.Trim().Replace("  "," "); 
                if (i % 3 == 0 ) {
                    listBox1.Items.Add("\n");   
                }
                i++;
                if (data.Length > 0)
                listBox1.Items.Add(data);
            }
        }
    }
}
