﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web;

namespace AzureCognitive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            txt_Results.Text = "";
            txt_Detected.Text = "";


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "image files (*.jpg)|*.jpg";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    txt_Filename.Text = openFileDialog.FileName;
                    pic_Image.Image = new Bitmap(txt_Filename.Text);
                }
            }
        }

        private async void btn_Calculate_Click(object sender, EventArgs e)
        {
            List<Describe> des_status = await MakeRequest(txt_Filename.Text);
            txt_Detected.Text = des_status[0].description.captions[0].text;
            txt_Results.Text = JsonConvert.SerializeObject(des_status);
        }

        static async Task<List<Describe>> MakeRequest(string imageFilePath)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var des_status = new List<Describe>();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ee4bca48cc06488a9d30f0b90d414fe2");

            // Request parameters
            queryString["maxCandidates"] = "1";
            queryString["language"] = "en";
            queryString["model-version"] = "latest";
            var uri = "https://southeastasia.api.cognitive.microsoft.com/vision/v3.2/describe?" + queryString;

            HttpResponseMessage response;
            string responseContent;

            // Request body
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                responseContent = response.Content.ReadAsStringAsync().Result;
                responseContent = "["+responseContent+"]";
                if ((int)response.StatusCode == 200)
                {
                    des_status = JsonConvert.DeserializeObject<List<Describe>>(responseContent);
                    return des_status;
                }
                else
                    return null;
            }
        }
                
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Caption
        {
            public string text { get; set; }
            public double confidence { get; set; }
        }

        public class Description
        {
            public List<string> tags { get; set; }
            public List<Caption> captions { get; set; }
        }

        public class Metadata
        {
            public int width { get; set; }
            public int height { get; set; }
            public string format { get; set; }
        }

        public class Describe
        {
            public Description description { get; set; }
            public string requestId { get; set; }
            public Metadata metadata { get; set; }
            public string modelVersion { get; set; }
        }

    }
}
