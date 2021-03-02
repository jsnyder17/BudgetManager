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
        private List<TextBox> textBoxes;
        private List<RadioButton> radioButtons;
        private List<Label> labels;
        private List<Button> buttons;

        private ExpenseManager em;
        private Form1 form1;
        private int index;
        public AddExpense(Form1 form, ExpenseManager em)
        {
            form1 = form;
            this.em = em;
            this.index = -1;

            //Initialize arrays
            textBoxes = new List<TextBox>();
            radioButtons = new List<RadioButton>();
            labels = new List<Label>();
            buttons = new List<Button>();

            InitializeComponent();

            // Add text boxes to array 
            textBoxes.Add(nameTextBox);
            textBoxes.Add(costTextBox);
            textBoxes.Add(taxTextBox);

            // Add radio buttons to array
            radioButtons.Add(highRb);
            radioButtons.Add(medRb);
            radioButtons.Add(lowRb);

            labels.Add(label1);
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);

            buttons.Add(buttonCancel);
            buttons.Add(buttonAdd);

            buttonAdd.Click += new EventHandler(buttonAdd_Click);
            buttonCancel.Click += new EventHandler(buttonCancel_Click);
        }

        public void editExpense(int index)
        {
            this.index = index;

            updateFields();
        }
        public void updateTheme()
        {
            System.Diagnostics.Debug.WriteLine("Dark theme: " + form1.getDarkTheme());
            this.BackColor = form1.getBackgroundColor();

            foreach (Label label in labels)
            {
                label.ForeColor = form1.getTextColor();
            }
            foreach (TextBox textBox in textBoxes)
            {
                textBox.BackColor = form1.getBackgroundColor();
                textBox.ForeColor = form1.getTextColor();
            }
            foreach (Button button in buttons)
            {
                button.ForeColor = form1.getTextColor();
                button.BackColor = form1.getButtonColor();
            }
            foreach (RadioButton radioButton in radioButtons)
            {
                radioButton.ForeColor = form1.getTextColor();
            }
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
            else
            {
                MessageBox.Show("Invalid entries. Please make sure you fill out each field and select the appropriate importance level. ", "Error");
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
            double testDouble = 0.0;

            while (!badData && !end)
            {
                // Check text boxes
                foreach (TextBox tb in textBoxes)
                {
                    if (tb.Text.Equals(""))
                    {
                        System.Diagnostics.Debug.WriteLine("Found null text field. ");
                        badData = true;
                    }
                    else
                    {
                        if (textBoxes.IndexOf(tb) != 0)
                        {
                            try
                            {
                                testDouble = Double.Parse(tb.Text);
                            }
                            catch (FormatException)
                            {
                                badData = true;
                            }
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine("Continuing checkBadData() ... ");

                // Check radio buttons
                foreach (RadioButton rb in radioButtons)
                {
                    if (!rb.Checked)
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
            foreach (TextBox tb in textBoxes)
            {
                tb.Text = "";
            }
            foreach(RadioButton rb in radioButtons)
            {
                if (rb.Checked)
                {
                    rb.Checked = false;
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
