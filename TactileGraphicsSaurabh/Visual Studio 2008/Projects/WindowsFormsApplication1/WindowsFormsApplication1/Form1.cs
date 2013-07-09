using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
            private System.Drawing.Bitmap m_Bitmap;
            private System.Drawing.Bitmap m_Undo;
            private System.Windows.Forms.MainMenu mainMenu1;
            private System.Windows.Forms.MenuItem menuItem1;
            private System.Windows.Forms.MenuItem menuItem2;
            private System.Windows.Forms.MenuItem menuItem3;
            private System.Windows.Forms.MenuItem FileOpen;
            private System.Windows.Forms.MenuItem FileSaveAs;
            private System.Windows.Forms.MenuItem FileSave;
            private System.Windows.Forms.MenuItem FileExit;
            private System.Windows.Forms.MenuItem Undo;
            private MenuItem menuItem4;
            private MenuItem menuItem5;
            private Button button1;
            private MenuItem menuItem6;
            private MenuItem menuItem7;
            private IContainer components;
            public String NameofFile;
            
            public Form1()
            {
                InitializeComponent();

                m_Bitmap = new Bitmap(8, 8);
            }

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (components != null)
                    {
                        components.Dispose();
                    }
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code
            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.components = new System.ComponentModel.Container();
                this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
                this.menuItem1 = new System.Windows.Forms.MenuItem();
                this.FileOpen = new System.Windows.Forms.MenuItem();
                this.FileSave = new System.Windows.Forms.MenuItem();
                this.FileSaveAs = new System.Windows.Forms.MenuItem();
                this.FileExit = new System.Windows.Forms.MenuItem();
                this.menuItem2 = new System.Windows.Forms.MenuItem();
                this.Undo = new System.Windows.Forms.MenuItem();
                this.menuItem5 = new System.Windows.Forms.MenuItem();
                this.menuItem6 = new System.Windows.Forms.MenuItem();
                this.menuItem7 = new System.Windows.Forms.MenuItem();
                this.menuItem3 = new System.Windows.Forms.MenuItem();
                this.menuItem4 = new System.Windows.Forms.MenuItem();
                this.button1 = new System.Windows.Forms.Button();
                this.SuspendLayout();
                // 
                // mainMenu1
                // 
                this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem5,
            this.menuItem6});
                // 
                // menuItem1
                // 
                this.menuItem1.Index = 0;
                this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileOpen,
            this.FileSave,
            this.FileSaveAs,
            this.FileExit});
                this.menuItem1.Text = "File";
                // 
                // FileOpen
                // 
                this.FileOpen.Index = 0;
                this.FileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
                this.FileOpen.Text = "Open";
                this.FileOpen.Click += new System.EventHandler(this.File_Open);
                // 
                // FileSave
                // 
                this.FileSave.Index = 1;
                this.FileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
                this.FileSave.Text = "Save";
                this.FileSave.Click += new System.EventHandler(this.File_Save);
                // 
                // FileSaveAs
                // 
                this.FileSaveAs.Index = 2;
                this.FileSaveAs.Text = "Save As";
                this.FileSaveAs.Click += new System.EventHandler(this.File_SaveAs);
                // 
                // FileExit
                // 
                this.FileExit.Index = 3;
                this.FileExit.Text = "Exit";
                this.FileExit.Click += new System.EventHandler(this.File_Exit);
                // 
                // menuItem2
                // 
                this.menuItem2.Index = 1;
                this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Undo});
                this.menuItem2.Text = "Edit";
                // 
                // Undo
                // 
                this.Undo.Index = 0;
                this.Undo.Text = "Undo";
                this.Undo.Click += new System.EventHandler(this.OnUndo);
                // 
                // menuItem5
                // 
                this.menuItem5.Index = 2;
                this.menuItem5.Text = "Draw";
                // 
                // menuItem6
                // 
                this.menuItem6.Index = 3;
                this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7});
                this.menuItem6.Text = "Convert";
                // 
                // menuItem7
                // 
                this.menuItem7.Index = 0;
                this.menuItem7.Text = "Convert to a text file";
                this.menuItem7.Click += new System.EventHandler(this.File_Convert);
                // 
                // menuItem3
                // 
                this.menuItem3.Index = -1;
                this.menuItem3.Text = "";
                // 
                // menuItem4
                // 
                this.menuItem4.Index = -1;
                this.menuItem4.Text = "";
                // 
                // button1
                // 
                this.button1.Location = new System.Drawing.Point(401, 386);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(75, 23);
                this.button1.TabIndex = 1;
                this.button1.Text = "Convert";
                this.button1.UseVisualStyleBackColor = true;
                this.button1.Click += new System.EventHandler(this.button1_Click);
                // 
                // Form1
                // 
                this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
                this.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.ClientSize = new System.Drawing.Size(488, 421);
                this.Controls.Add(this.button1);
                this.Menu = this.mainMenu1;
                this.Name = "Form1";
                this.Text = "Braille Embosser";
                this.Load += new System.EventHandler(this.Form1_Load);
                this.ResumeLayout(false);

            }
            #endregion

            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
           // static void Main()
            //{
            //    Application.Run(new Form1());
            //}

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;

                g.DrawImage(m_Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(m_Bitmap.Width), (int)(m_Bitmap.Height)));
            }

            private void Form1_Load(object sender, System.EventArgs e)
            {
            }

            private void File_Open(object sender, System.EventArgs e)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (DialogResult.OK == openFileDialog.ShowDialog())
                {
                    m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
                    NameofFile = openFileDialog.FileName;
                    this.AutoScroll = true;
                    this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width), (int)(m_Bitmap.Height));
                    this.Invalidate();
                }
            }

            private void File_Save(object sender, System.EventArgs e)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                if (DialogResult.OK == saveFileDialog.ShowDialog())
                {
                
                m_Bitmap.Save(NameofFile);
               
                }
            }
            private void File_SaveAs(object sender, System.EventArgs e)
            {
                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (DialogResult.OK == saveFileDialog.ShowDialog())
                {
                    m_Bitmap.Save(saveFileDialog.FileName);
                }


            }

            private void File_Exit(object sender, System.EventArgs e)
            {
                this.Close();
            }



            private void OnUndo(object sender, System.EventArgs e)
            {
                Bitmap temp = (Bitmap)m_Bitmap.Clone();
                m_Bitmap = (Bitmap)m_Undo.Clone();
                m_Undo = (Bitmap)temp.Clone();
                this.Invalidate();
            }
            private void File_Convert(object sender, System.EventArgs e)
            {
                //insert convert code
                

            }
            private void button1_Click(object sender, EventArgs e)
            {

            }

        }
        
}
