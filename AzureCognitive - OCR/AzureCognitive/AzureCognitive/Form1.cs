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
            string ocr_result = "";
            List<Ocr> ocr_status = await MakeRequest(txt_Filename.Text);
            foreach (Ocr a in ocr_status)
                foreach (Region b in a.regions)
                    foreach (Line c in b.lines)
                        foreach (Word d in c.words)
                            ocr_result += d.text + " ";

            txt_Detected.Text = ocr_result;
            txt_Results.Text = JsonConvert.SerializeObject(ocr_status);
        }

        static async Task<List<Ocr>> MakeRequest(string imageFilePath)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var ocr_status = new List<Ocr>();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ee4bca48cc06488a9d30f0b90d414fe2");

            // Request parameters
            queryString["language"] = "unk";
            queryString["detectOrientation"] = "true";
            queryString["model-version"] = "latest";
            var uri = "https://southeastasia.api.cognitive.microsoft.com/vision/v3.2/ocr?" + queryString;

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
                    ocr_status = JsonConvert.DeserializeObject<List<Ocr>>(responseContent);
                    return ocr_status;
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
        public class Word
        {
            public string boundingBox { get; set; }
            public string text { get; set; }
        }

        public class Line
        {
            public string boundingBox { get; set; }
            public List<Word> words { get; set; }
        }

        public class Region
        {
            public string boundingBox { get; set; }
            public List<Line> lines { get; set; }
        }

        public class Ocr
        {
            public string language { get; set; }
            public double textAngle { get; set; }
            public string orientation { get; set; }
            public List<Region> regions { get; set; }
            public string modelVersion { get; set; }
        }

    }
}
