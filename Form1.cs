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

namespace Budget_Manager
{
    public partial class Form1 : Form
    {
        private bool windowOpen;
        private string fileName;

        private ExpenseManager em;
        private AddExpense addExpense;

        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        private DollarFormat df;
        public Form1(ExpenseManager em)
        {
            windowOpen = false;
            fileName = "";
            
            this.em = em;

            addExpense = new AddExpense(this, em);
            addExpense.Visible = false;

            openFileDialog = new OpenFileDialog();

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            df = new DollarFormat();

            InitializeComponent();

            button1.Click += new EventHandler(buttonAdd_Click);
            savingsCb.Click += new EventHandler(cb_Click);

            button2.Click += new EventHandler(buttonEdit_Click);
            button3.Click += new EventHandler(buttonRemove_Click);
            button4.Click += new EventHandler(buttonLoad_Click);
            button5.Click += new EventHandler(buttonSave_Click);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void setWindowOpen(bool windowOpen)
        {
            this.windowOpen = windowOpen;
        }
        public void setExpenseManager(ExpenseManager em)
        {
            this.em = em;
        }

        public void update()
        {
            listBox1.Items.Clear();

            if (em.getExpenses().Count > 0)
            {
                for (int i = 0; i < em.getExpenses().Count; i++)
                {
                    listBox1.Items.Add(em.listDisplay(i));
                }
            }

            // Update tax and savings totals displays
            label2.Text = ("Total Taxes: " + df.format((em.getTotalTax())));
            label3.Text = ("Total Possible Savings: " + df.format((em.getTotalSavings())));

            // Determine total to display
            if (savingsCb.Checked)
            {
                label4.Text = ("Total: " + df.format((em.getTotalPrice() - em.getTotalSavings())));
            }
            else
            {
                label4.Text = ("Total: " + df.format(em.getTotalPrice() + em.getTotalTax()));
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                addExpense.Visible = true;
                windowOpen = true;
            }
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (!windowOpen)
                {
                    addExpense.editExpense(listBox1.SelectedIndex);
                    addExpense.Visible = true;
                    windowOpen = true;
                }
            }
            else
            {
                MessageBox.Show("You must select an expense. ", "Error");
            }
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                em.removeExpense(listBox1.SelectedIndex);
                update();
            }
            else
            {
                MessageBox.Show("You must select an expense to delete. ", "Error");
            }
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                load();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                save();
            }
        }
        private void cb_Click(object sender, EventArgs e)
        {
            update();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void save()
        {
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string data = em.toString();

                File.WriteAllText(saveFileDialog.FileName, data);

                System.Diagnostics.Debug.WriteLine("Saved file successfully! ");

                MessageBox.Show("Budget saved successfully under '" + saveFileDialog.FileName + ".'", "Success");
            }
        }
        private void load()
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string[] lines;

                fileName = openFileDialog.FileName;

                System.Diagnostics.Debug.WriteLine("Selected '" + fileName + "'");

                // Load data into expense manager
                em.clearExpenses();

                lines = File.ReadAllLines(fileName);

                // Add all expenses read from file to em
                for (int i = 0; i < lines.Length; i++)
                {
                    Expense expense = new Expense(lines[i]);
                    em.addExpense(expense);
                }

                update();

                MessageBox.Show("Load successful! ", "Success");
            }
        }
    }
}
