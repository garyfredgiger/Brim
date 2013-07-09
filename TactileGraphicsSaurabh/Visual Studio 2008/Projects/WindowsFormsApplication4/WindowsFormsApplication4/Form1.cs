using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            



    Button OkButton=new Button();
    OkButton.Text = "Ok";
    OkButton.DialogResult = DialogResult.OK;
    OkButton.Location = new Point(8,20);
    OkButton.Size = new Size(50,24);
    this.Controls.Add(OkButton);

    Button CancelButton=new Button();
    CancelButton.Text = "Cancel";
    CancelButton.DialogResult = DialogResult.Cancel;
    CancelButton.Location = new Point(64,20);
    CancelButton.Size = new Size(50,24);
    this.Controls.Add(CancelButton);

    this.Text="Dialog";
    this.Size = new Size(130,90);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.StartPosition = FormStartPosition.CenterParent;
    this.ControlBox = false;
  }
}


public class SimpleDialogTest
{
  
    Form1 dlg = new Form1();

  

  

}

        }
    

