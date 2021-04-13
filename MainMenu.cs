using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Budget_Manager
{
    public partial class MainMenu : Form
    {
        private OpenFileDialog openFileDialog;
        private Form1 form;

        public MainMenu()
        {
            openFileDialog = new OpenFileDialog();

            InitializeComponent();

            buttonNew.Click += new EventHandler(buttonNew_Click);
            buttonLoad.Click += new EventHandler(buttonLoad_Click);
            buttonQuit.Click += new EventHandler(buttonQuit_Click);
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            //this.Visible = false;

            form = new Form1(new ExpenseManager());
            form.Visible = true;

        }
        public void buttonLoad_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                this.Visible = false;

                form = new Form1(new ExpenseManager());
                form.Visible = true;

                System.Diagnostics.Debug.WriteLine(openFileDialog.FileName);

                form.loadFile(openFileDialog.FileName, false);
            }
        }
        public void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
