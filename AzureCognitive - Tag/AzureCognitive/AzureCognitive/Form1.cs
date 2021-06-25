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
            List<TagImage> tag_status = await MakeRequest(txt_Filename.Text);
            txt_Detected.Text = tag_status[0].tags[0].name + " : " + tag_status[0].tags[0].confidence.ToString();
            txt_Results.Text = JsonConvert.SerializeObject(tag_status);
        }

        static async Task<List<TagImage>> MakeRequest(string imageFilePath)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var tag_status = new List<TagImage>();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ee4bca48cc06488a9d30f0b90d414fe2");

            // Request parameters
            queryString["language"] = "en";
            queryString["model-version"] = "latest";
            var uri = "https://southeastasia.api.cognitive.microsoft.com/vision/v3.2/tag?" + queryString;

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
                    tag_status = JsonConvert.DeserializeObject<List<TagImage>>(responseContent);
                    return tag_status;
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
        
        public class Tag
        {
            public string name { get; set; }
            public double confidence { get; set; }
        }

        public class Metadata
        {
            public int height { get; set; }
            public int width { get; set; }
            public string format { get; set; }
        }

        public class TagImage
        {
            public List<Tag> tags { get; set; }
            public string requestId { get; set; }
            public Metadata metadata { get; set; }
            public string modelVersion { get; set; }
        }

    }
}
