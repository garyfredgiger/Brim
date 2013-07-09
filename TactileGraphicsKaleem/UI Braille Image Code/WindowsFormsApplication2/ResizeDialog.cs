using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections;


namespace WindowsFormsApplication2
{
    public class ResizeDialog : Form
    {
        private Button button2;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Button button1;
    
    public ResizeDialog()
    {
        
  }

    private void InitializeComponent()
    {
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(52, 227);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 0;
        this.button1.Text = "Ok";
        this.button1.UseVisualStyleBackColor = true;
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(171, 227);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 1;
        this.button2.Text = "Cancel";
        this.button2.UseVisualStyleBackColor = true;
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(78, 108);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(140, 20);
        this.textBox1.TabIndex = 2;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(50, 74);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(0, 13);
        this.label1.TabIndex = 3;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(56, 74);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(0, 13);
        this.label2.TabIndex = 4;
        this.label2.Click += new System.EventHandler(this.label2_Click);
        // 
        // ResizeDialog
        // 
        this.ClientSize = new System.Drawing.Size(292, 266);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.Name = "ResizeDialog";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }
    
}



}

