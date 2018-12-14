using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.QRGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbxWidth.Text = "76";
            tbxHeight.Text = "76";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(tbxInput.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(7);

            // 원본 이미지
            Bitmap sourceImage = qrCodeImage;

            // 사이즈가 변경된 이미지(1/2로 축소)
            int width = int.Parse(tbxWidth.Text);
            int height = int.Parse(tbxHeight.Text);
            Size resize = new Size(width, height);
            Bitmap resizeImage = new Bitmap(sourceImage, resize);

            picQR.Image = resizeImage;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:

                        //this.picQR.Image.Size.Width = 200;
                        this.picQR.Image.Save(fs
                            , System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.picQR.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.picQR.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //https://www.translatorscafe.com/unit-converter/en/typography/compact/
            System.Diagnostics.Process.Start("https://www.translatorscafe.com/unit-converter/en/typography/compact/");
        }
    }
}
