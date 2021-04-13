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

        private List<State> states;

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

            states = new List<State>();

            InitializeComponent();

            label6.Visible = false;
            comboBox1.Visible = false;

            // Add text boxes to array 
            textBoxes.Add(nameTextBox);
            textBoxes.Add(costTextBox);

            // Add radio buttons to array
            radioButtons.Add(highRb);
            radioButtons.Add(medRb);
            radioButtons.Add(lowRb);

            labels.Add(label1);
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label5);
            labels.Add(label6);

            // Add buttons to button array 
            buttons.Add(buttonCancel);
            buttons.Add(buttonAdd);

            // Add states to drop-down selection
            comboBox1.Items.Add("AL");
            comboBox1.Items.Add("AK");
            comboBox1.Items.Add("AZ");
            comboBox1.Items.Add("AR");
            comboBox1.Items.Add("CA");
            comboBox1.Items.Add("CO");
            comboBox1.Items.Add("CT");
            comboBox1.Items.Add("DE");
            comboBox1.Items.Add("FL");
            comboBox1.Items.Add("GA");
            comboBox1.Items.Add("HI");
            comboBox1.Items.Add("ID");
            comboBox1.Items.Add("IL");
            comboBox1.Items.Add("IN");
            comboBox1.Items.Add("IA");
            comboBox1.Items.Add("KS");
            comboBox1.Items.Add("KY");
            comboBox1.Items.Add("LA");
            comboBox1.Items.Add("ME");
            comboBox1.Items.Add("MD");
            comboBox1.Items.Add("MA");
            comboBox1.Items.Add("MI");
            comboBox1.Items.Add("MN");
            comboBox1.Items.Add("MS");
            comboBox1.Items.Add("MO");
            comboBox1.Items.Add("MT");
            comboBox1.Items.Add("NE");
            comboBox1.Items.Add("NV");
            comboBox1.Items.Add("NH");
            comboBox1.Items.Add("NJ");
            comboBox1.Items.Add("NM");
            comboBox1.Items.Add("NY");
            comboBox1.Items.Add("NC");
            comboBox1.Items.Add("ND");
            comboBox1.Items.Add("OH");
            comboBox1.Items.Add("OK");
            comboBox1.Items.Add("OR");
            comboBox1.Items.Add("PA");
            comboBox1.Items.Add("RI");
            comboBox1.Items.Add("SC");
            comboBox1.Items.Add("SD");
            comboBox1.Items.Add("TN");
            comboBox1.Items.Add("TX");
            comboBox1.Items.Add("UT");
            comboBox1.Items.Add("VT");
            comboBox1.Items.Add("VA");
            comboBox1.Items.Add("WA");
            comboBox1.Items.Add("WV");
            comboBox1.Items.Add("WI");
            comboBox1.Items.Add("WY");

            // Add states to state list
            State al = new State("AL", 0.0922);
            states.Add(al);
            State ak = new State("AK", 0.0176);
            states.Add(ak);
            State ar = new State("AR", 0.0947);
            states.Add(ar);
            State ca = new State("CA", 0.0866);
            states.Add(ca);
            State co = new State("CO", 0.0765);
            states.Add(co);
            State ct = new State("CT", 0.0635);
            states.Add(ct);
            State de = new State("DE", 0.00);
            states.Add(de);
            State fl = new State("FL", 0.0705);
            states.Add(fl);
            State ga = new State("GA", 0.0731);
            states.Add(ga);
            State hi = new State("HI", 0.0444);
            states.Add(hi);
            State id = new State("ID", 0.0603);
            states.Add(id);
            State il = new State("IL", 0.0908);
            states.Add(il);
            State ind = new State("IN", 0.0700);
            states.Add(ind);
            State ia = new State("IA", 0.0694);
            states.Add(ia);
            State ks = new State("KS", 0.0868);
            states.Add(ks);
            State ky = new State("KY", 0.0600);
            states.Add(ky);
            State la = new State("LA", 0.0952);
            states.Add(la);
            State me = new State("ME", 0.0550);
            states.Add(me);
            State md = new State("MD", 0.0600);
            states.Add(md);
            State ma = new State("MA", 0.0625);
            states.Add(ma);
            State mi = new State("MI", 0.0600);
            states.Add(mi);
            State mn = new State("MN", 0.0746);
            states.Add(mn);
            State ms = new State("MS", 0.0707);
            states.Add(ms);
            State mo = new State("MO", 0.0818);
            states.Add(mo);
            State mt = new State("MT", 0.00);
            states.Add(mt);
            State ne = new State("NE", 0.0693);
            states.Add(ne);
            State nv = new State("NV", 0.0832);
            states.Add(nv);
            State nh = new State("NH", 0.00);
            states.Add(nh);
            State nj = new State("NJ", 0.0660);
            states.Add(nj);
            State nm = new State("NM", 0.0782);
            states.Add(nm);
            State ny = new State("NY", 0.0852);
            states.Add(ny);
            State nc = new State("NC", 0.0697);
            states.Add(nc);
            State nd = new State("ND", 0.0686);
            states.Add(nd);
            State oh = new State("OH", 0.0717);
            states.Add(oh);
            State ok = new State("OK", 0.0864);
            states.Add(ok);
            State or = new State("OR", 0.00);
            states.Add(or);
            State pa = new State("PA", 0.0634);
            states.Add(pa);
            State ri = new State("RI", 0.0700);
            states.Add(ri);
            State sc = new State("SC", 0.0746);
            states.Add(sc);
            State sd = new State("SD", 0.0640);
            states.Add(sd);
            State tn = new State("TN", 0.0953);
            states.Add(tn);
            State tx = new State("TX", 0.0819);
            states.Add(tx);
            State ut = new State("UT", 0.0718);
            states.Add(ut);
            State vt = new State("VT", 0.0622);
            states.Add(vt);
            State va = new State("VA", 0.0565);
            states.Add(va);
            State wa = new State("WA", 0.0921);
            states.Add(wa);
            State wv = new State("WV", 0.0641);
            states.Add(wv);
            State wi = new State("WI", 0.0546);
            states.Add(wi);
            State wy = new State("WY", 0.0534);
            states.Add(wy);


            buttonAdd.Click += new EventHandler(buttonAdd_Click);
            buttonCancel.Click += new EventHandler(buttonCancel_Click);

            taxCheckBox.Click += new EventHandler(taxCheckBox_Click);
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
                State state;

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
                
                if (taxCheckBox.Checked)
                {
                    state = getState();
                }
                else
                {
                    state = new State();
                }

                Expense expense = new Expense(nameTextBox.Text, Double.Parse(costTextBox.Text), state, importanceInt, taxCheckBox.Checked);

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

        public void taxCheckBox_Click(object sender, EventArgs e)
        {
            if (taxCheckBox.Checked)
            {
                comboBox1.Visible = true;
                label6.Visible = true;
            }
            else
            {
                comboBox1.SelectedIndex = -1;
                comboBox1.Visible = false;
                label6.Visible = false;
            }
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

                // Check combo box
                if (comboBox1.SelectedIndex == -1)
                {
                    if (taxCheckBox.Checked)
                    {
                        badData = true;
                    }
                }

                end = true;
            }

            return badData;
        }
        private State getState()
        {
            bool found = false;
            int stateIndex = 0;

            while (!found && stateIndex < states.Count)
            {
                if (states[stateIndex].getAbb().Equals(comboBox1.SelectedItem.ToString()))
                {
                    found = true;
                }
                else
                {
                    stateIndex += 1;
                }
            }

            return states[stateIndex];
        }
        private void reset()    // Reset fields to blank values 
        {
            index = -1;

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

            comboBox1.SelectedIndex = -1;

            taxCheckBox.Checked = false;

            label6.Visible = false;
            comboBox1.Visible = false;
        }
        private void updateFields()     // Update fields with selected expense data (editing expense)
        {
            int stateIndex = 0;

            nameTextBox.Text = em.getExpenses()[index].getName();
            costTextBox.Text = em.getExpenses()[index].getBasePrice().ToString();

            taxCheckBox.Checked = em.getExpenses()[index].getTaxable();

            if (taxCheckBox.Checked)
            {
                label6.Visible = true;
                comboBox1.Visible = true;

                foreach (State s in states)
                {
                    System.Diagnostics.Debug.WriteLine("Is " + s + " == " + em.getExpenses()[index].getState().getAbb() + "? ");

                    if (s.getAbb().Equals(em.getExpenses()[index].getState().getAbb()))
                    {
                        comboBox1.SelectedIndex = stateIndex + 1;
                    }
                    else
                    {
                        stateIndex += 1;
                    }
                }
            }

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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AddExpense_Load_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
