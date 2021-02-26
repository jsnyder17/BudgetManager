using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Budget_Manager
{
    public partial class AddExpense : Form
    {
        private TextBox[] textBoxes;
        private RadioButton[] radioButtons;
        private ExpenseManager em;
        private Form1 form1;
        private int index;
        public AddExpense(Form1 form, ExpenseManager em)
        {
            form1 = form;
            this.em = em;
            this.index = -1;

            //Initialize arrays
            textBoxes = new TextBox[3];
            radioButtons = new RadioButton[3];

            InitializeComponent();

            // Add text boxes to array 
            textBoxes[0] = nameTextBox;
            textBoxes[1] = costTextBox;
            textBoxes[2] = taxTextBox;

            // Add radio buttons to array
            radioButtons[0] = highRb;
            radioButtons[1] = medRb;
            radioButtons[2] = lowRb;

            buttonAdd.Click += new EventHandler(buttonAdd_Click);
            buttonCancel.Click += new EventHandler(buttonCancel_Click);
        }

        public void editExpense(int index)
        {
            this.index = index;

            updateFields();
        }

        private void AddExpense_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!checkBadData())
            {
                int importanceInt = 0;

                if (highRb.Checked)
                {
                    importanceInt = 3;
                }
                else if (medRb.Checked)
                {
                    importanceInt = 2;
                }
                else
                {
                    importanceInt = 1;
                }

                Expense expense = new Expense(nameTextBox.Text, Double.Parse(costTextBox.Text), Double.Parse(taxTextBox.Text), importanceInt);

                // If editing expense
                if (index != -1)
                {
                    em.replaceExpense(expense, index);
                    index = -1;
                }

                // If adding new expense 
                else
                {
                    em.addExpense(expense);
                }

                form1.setExpenseManager(em);
                form1.update();
                form1.setWindowOpen(false);
                this.Visible = false;
                reset();
            }
        }
        public void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            form1.setWindowOpen(false);
            reset();
        }
        private bool checkBadData()
        {
            bool badData = false;
            bool end = false;
            int uncheckedAmt = 0;

            while (!badData && !end)
            {
                // Check text boxes
                for (int i = 0; i < 3; i++)
                {
                    if (textBoxes[i].Text.Equals(""))
                    {
                        System.Diagnostics.Debug.WriteLine("Found null text field. ");
                        badData = true;
                    }
                }

                System.Diagnostics.Debug.WriteLine("Continuing checkBadData() ... ");

                // Check radio buttons
                for (int i = 0; i < 3; i++)
                {
                    if (!radioButtons[i].Checked)
                    {
                        uncheckedAmt += 1;
                    }
                }
                if (uncheckedAmt > 2)
                {
                    badData = true;
                }
                end = true;
            }

            return badData;
        }
        private void reset()    // Reset fields to blank values 
        {
            for (int i = 0; i < 3; i++)
            {
                textBoxes[i].Text = "";
            }
            for (int i = 0; i < 3; i++)
            {
                if (radioButtons[i].Checked)
                {
                    radioButtons[i].Checked = false;
                }
            }
        }
        private void updateFields()     // Update fields with selectd expense data (editing expense)
        {
            nameTextBox.Text = em.getExpenses()[index].getName();
            costTextBox.Text = em.getExpenses()[index].getBasePrice().ToString();
            taxTextBox.Text = em.getExpenses()[index].getTax().ToString();

            // Radio buttons
            switch (em.getExpenses()[index].getImportance())
            {
                case 1:
                    lowRb.Checked = true;
                    break;
                case 2:
                    medRb.Checked = true;
                    break;
                case 3:
                    highRb.Checked = true;
                    break;
            }
        }
    }
}
