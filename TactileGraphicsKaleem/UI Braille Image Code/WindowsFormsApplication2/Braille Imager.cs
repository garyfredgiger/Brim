/* Code Written by Mohammed Kaleemur Rahman and Saurabh Sanghvi modified 31/07/08 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class BrailleImager : Form
    {
        public BrailleImager()
        {
            InitializeComponent();
            picture = new Bitmap(2, 2);
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
           // g.DrawImage(picture, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(picture.Width), (int)(picture.Height)));
            g.DrawImage(picture, new Rectangle(0, 24, (int)(picture.Width), (int)(picture.Height)));
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void File_Load(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                picture = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
                this.AutoScroll = true;
                this.AutoScrollMinSize = new Size((picture.Width),(picture.Height));
                this.Invalidate();
            }
        }
    /*    private void File_Open(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = "c:\\";
            openFile.Filter = "Text files (*.txt)|*.txt";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;

            if (DialogResult.OK == openFile.ShowDialog())
            {
                TextBox txtFileNm = new TextBox();
                txtFileNm.Text = openFile.FileName;
                this.AutoScroll = true;
                // this.AutoScrollMinSize = new Size((picture.Width), (picture.Height));
                this.Invalidate();
            }
            
        } */
        private void File_Save(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg";
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                picture.Save(saveFileDialog.FileName);
                this.AutoScroll = true;
                this.Invalidate();
            }
        }
     
        private void File_Quit(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void Image_Resize_25(object sender, System.EventArgs e)
        {
            
            Bitmap temp = scaleDown(picture, 0.25);
            picture = temp;
            this.AutoScrollMinSize = new Size((int)(picture.Width), (int)(picture.Height));
            this.Invalidate();
        }
        private void Image_Resize_50(object sender, System.EventArgs e)
        {

            Bitmap temp = scaleDown(picture, 0.5);
            picture = temp;
            this.AutoScrollMinSize = new Size((int)(picture.Width), (int)(picture.Height));
            this.Invalidate();
        }
        private void Image_Resize_75(object sender, System.EventArgs e)
        {

            Bitmap temp = scaleDown(picture, 0.75);
            picture = temp;
            this.AutoScrollMinSize = new Size((int)(picture.Width), (int)(picture.Height));
            this.Invalidate();
        }
        private void Image_Resize_200(object sender, System.EventArgs e)
        {

            Bitmap temp = scaleDown(picture, 2);
            picture = temp;
            this.AutoScrollMinSize = new Size((int)(picture.Width), (int)(picture.Height));
            this.Invalidate();
        }
        private void Image_Resize_Default(object sender, System.EventArgs e)
        {

            double scale = getScaleFactor(picture);
            Bitmap temp = scaleDown(picture, scale);
            picture = temp;
            this.AutoScrollMinSize = new Size((int)(picture.Width), (int)(picture.Height));
            this.Invalidate();
        }
        public static double getScaleFactor(Bitmap image)
        {

            return (84 / (double)Math.Max(image.Width, image.Height));

        }

        public static Bitmap scaleDown(Bitmap image, double scaleFactor)
        {

            Bitmap newImage = new Bitmap((int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor));

            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);   //test

            Graphics g = Graphics.FromImage(newImage);

            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            g.DrawImage(image,

                new Rectangle(0, 0, (int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor)),

                0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            newImage.Save("scaled.bmp", ImageFormat.Bmp);

            return newImage;

        }
        private void Image_Program(object sender, EventArgs e)
        {
            BrailleImaging.Program.Braille(); 

        }
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

       
    }
}
