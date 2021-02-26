using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Budget_Manager
{
    public partial class SaveBudget : Form
    {
        private Form1 form;
        public SaveBudget(Form1 form)
        {
            this.form = form;

            InitializeComponent();

            button1.Click += new EventHandler(saveButton_Click);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                //form.setFileName(textBox1.Text + ".txt");
                //form.save();

                this.Visible = false;
                reset();
            }
        }

        private void reset()
        {
            textBox1.Text = "";
        }
    }
}
