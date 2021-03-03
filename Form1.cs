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
        private bool darkTheme;
        private bool madeChanges;

        private Color backgroundColor;
        private Color buttonColor;
        private Color textColor;
        private Color listColor;

        private ExpenseManager origEm;
        private ExpenseManager em;

        private AddExpense addExpense;
        
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        private DollarFormat df;

        private List<Label> labels;
        private List<Button> buttons;
        private List<CheckBox> checkBoxes;
        public Form1(ExpenseManager em)
        {
            windowOpen = false;
            darkTheme = false;
            madeChanges = false;

            this.origEm = new ExpenseManager(em);
            this.em = em;

            addExpense = new AddExpense(this, em);
            addExpense.Visible = false;

            openFileDialog = new OpenFileDialog();

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            df = new DollarFormat();

            InitializeComponent();

            backgroundColor = this.BackColor;
            buttonColor = button1.BackColor;
            textColor = label1.ForeColor;
            listColor = listBox1.BackColor;

            labels = new List<Label>();
            buttons = new List<Button>();
            checkBoxes = new List<CheckBox>();

            labels.Add(label1);
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);

            button1.Click += new EventHandler(buttonAdd_Click);
            buttons.Add(button1);

            savingsCb.Click += new EventHandler(cb_Click);
            darkThemeCb.Click += new EventHandler(darkThemeCb_Click);

            button2.Click += new EventHandler(buttonEdit_Click);
            buttons.Add(button2);

            button3.Click += new EventHandler(buttonRemove_Click);
            buttons.Add(button3);

            button4.Click += new EventHandler(buttonLoad_Click);
            buttons.Add(button4);

            button5.Click += new EventHandler(buttonSave_Click);
            buttons.Add(button5);

            button6.Click += new EventHandler(buttonMerge_Click);
            buttons.Add(button6);

            button7.Click += new EventHandler(buttonClear_Click);
            buttons.Add(button7);

            buttonQuit.Click += new EventHandler(buttonQuit_Click);
            buttons.Add(buttonQuit);

            checkBoxes.Add(savingsCb);
            checkBoxes.Add(darkThemeCb);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public bool getDarkTheme()
        {
            return darkTheme;
        }
        public Color getBackgroundColor()
        {
            return backgroundColor;
        }
        public Color getButtonColor()
        {
            return buttonColor;
        }
        public Color getTextColor()
        {
            return textColor;
        }
        public void setWindowOpen(bool windowOpen)
        {
            this.windowOpen = windowOpen;
        }
        public void setExpenseManager(ExpenseManager em)
        {
            this.em = em;

            checkChanges();
        }

        public void update()
        {
            updateTheme(); // Update theme if changed
            updateList(); // Update list with latest expenses
            updateTotals(); // Update totals based off newly updated list 
        }
        private void updateTheme()
        {
            if (darkTheme)
            {
                backgroundColor = Color.FromArgb(37, 37, 37);
                buttonColor = Color.FromArgb(57, 57, 57);
                textColor = Color.White;
                listColor = Color.Black;
            }
            else
            {
                backgroundColor = SystemColors.Control;
                buttonColor = SystemColors.Control;
                textColor = SystemColors.ControlText;
                listColor = SystemColors.Window;
            }

            this.BackColor = backgroundColor;

            // Labels
            foreach (Label label in labels)
            {
                label.ForeColor = textColor;
            }
            // Buttons
            foreach (Button button in buttons)
            {
                button.ForeColor = textColor;
                button.BackColor = buttonColor;
            }
            // Check boxes
            foreach (CheckBox checkBox in checkBoxes)
            {
                checkBox.ForeColor = textColor;
            }
            // List
            listBox1.BackColor = listColor;
            listBox1.ForeColor = textColor;
        }
        private void updateList()
        {
            // Update list contents 
            listBox1.Items.Clear();

            if (em.getExpenses().Count > 0)
            {
                for (int i = 0; i < em.getExpenses().Count; i++)
                {
                    listBox1.Items.Add(em.listDisplay(i));
                }
            }
        }
        private void updateTotals()
        {
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
                addExpense.updateTheme();
                addExpense.Visible = true;
                
                switchWindowOpen();
            }
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (!windowOpen)
                {
                    addExpense.editExpense(listBox1.SelectedIndex);
                    addExpense.updateTheme();
                    addExpense.Visible = true;

                    switchWindowOpen();
                }
            }
            else
            {
                MessageBox.Show("You must select an expense. ", "Error");
            }
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                switchWindowOpen();

                if (listBox1.SelectedIndex != -1)
                {
                    DialogResult confirmResult = promptConfirm("Are you sure you want to remove '" + em.getExpenses()[listBox1.SelectedIndex].getName() + "' from the list? ");

                    if (confirmResult == DialogResult.Yes)
                    {
                        em.removeExpense(listBox1.SelectedIndex);
                        update();
                    }
                }
                else
                {
                    MessageBox.Show("You must select an expense to delete. ", "Error");
                }

                switchWindowOpen();
            }
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                switchWindowOpen();

                if (madeChanges)
                {
                    DialogResult savePromptResult = savePrompt("You have made changes. All changes will be deleted if not saved. ");

                    if (savePromptResult != DialogResult.Cancel)
                    {
                        if (savePromptResult == DialogResult.Yes)
                        {
                            save();
                        }

                        // User selects file to load 
                        DialogResult dialogResult = openFileDialog.ShowDialog();

                        if (dialogResult == DialogResult.OK)
                        {
                            loadFile(openFileDialog.FileName, false);
                        }
                    }
                }
                else
                {
                    // User selects file to load 
                    DialogResult dialogResult = openFileDialog.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        loadFile(openFileDialog.FileName, false);
                    }
                }

                switchWindowOpen();
            }
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                switchWindowOpen();

                DialogResult savePromptResult = savePrompt("This will remove all currently listed expenses. ");

                if (savePromptResult != DialogResult.Cancel)
                {
                    if (savePromptResult == DialogResult.Yes)
                    {
                        save();
                    }

                    em.clearExpenses();

                    update();
                }

                switchWindowOpen();
            }
        }
        private void buttonMerge_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                switchWindowOpen();

                DialogResult dialogResult = openFileDialog.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    loadFile(openFileDialog.FileName, true);
                }

                switchWindowOpen();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                switchWindowOpen();

                save();

                switchWindowOpen();
            }
        }
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            if (!windowOpen)
            {
                switchWindowOpen();

                if (madeChanges)
                {
                    DialogResult savePromptResult = savePrompt("You have made changes. ");

                    if (savePromptResult != DialogResult.Cancel)
                    {
                        if (savePromptResult == DialogResult.Yes)
                        {
                            save();
                        }

                        closeApplication();
                    }
                }
                else
                {
                    DialogResult confirmResult = promptConfirm("Are you sure you want to quit? ");

                    if (confirmResult == DialogResult.Yes)
                    {
                        closeApplication();
                    }
                }

                switchWindowOpen();
            }
        }
        private void closeApplication()
        {
            // Close application 
            int formAmt = Application.OpenForms.Count;

            for (int i = formAmt - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i] != this)
                {
                    Application.OpenForms[i].Close();
                }
            }

            this.Close();

            Application.Exit();
        }
        private void cb_Click(object sender, EventArgs e)
        {
            update();
        }
        private void darkThemeCb_Click(object sender, EventArgs e)
        {
            if (!darkTheme)
            {
                darkTheme = true;
            }
            else
            {
                darkTheme = false;
            }

            System.Diagnostics.Debug.WriteLine("Dark theme: " + darkTheme);

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

                madeChanges = false;
            }
        }

        public void loadFile(string fileName, bool combine)
        {
            string[] lines;
            int errorCount = 0;

            System.Diagnostics.Debug.WriteLine("Selected '" + fileName + "'");

            // Load data into expense manager
            if (!combine)
            {
                em.clearExpenses();
            }

            lines = File.ReadAllLines(fileName);

            // Add all expenses read from file to em
            for (int i = 0; i < lines.Length; i++)
            {
                // Check if line contains valid data
                if (!checkLoadInvalid(lines[i]))
                {
                    Expense expense = new Expense(lines[i]);
                    em.addExpense(expense);
                }
                else
                {
                    errorCount += 1;
                }
            }

            origEm = new ExpenseManager(em);

            madeChanges = false;

            update();

            if (errorCount == 0)
            {
                MessageBox.Show("Load successful! ", "Success");
            }
            else
            {
                MessageBox.Show("Loaded budget with the exception of (" + errorCount + ") expenses, which contained formatting errors. ");
            }
        }
        private bool checkLoadInvalid(string data)
        {
            bool badData = false;
            bool end = false;
            double testDouble = 0.0;
            int testInt = 0;

            string[] dataSplit = data.Split(",");

            System.Diagnostics.Debug.WriteLine(dataSplit.Length);

            while (!badData && !end)
            {
                System.Diagnostics.Debug.WriteLine(badData);
                // Check array length
                if (dataSplit.Length != 4)
                {
                    badData = true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("=== LENGTH CHECK PASSED ===");
                    // Check data formatting
                    for (int i = 1; i < 4; i++)
                    {
                        try
                        {
                            if (i == 1 || i == 2)
                            {
                                testDouble = Double.Parse(dataSplit[i]);
                            }
                            else
                            {
                                testInt = int.Parse(dataSplit[i]);
                                if (testInt <= 0 || testInt > 4)
                                {
                                    badData = true;
                                }
                            }
                        }
                        catch (FormatException e)
                        {
                            badData = true;
                            System.Diagnostics.Debug.WriteLine("=== DATA CHECK FAILED FOR '" + dataSplit[i] + "' ===");
                            //System.Diagnostics.Debug.WriteLine(badData);
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("=== DATA CHECK PASSED ===");
                }

                end = true;
            }

            return badData;
        }
        private DialogResult savePrompt(string reasonText)
        {
            DialogResult dialogResult = promptConfirm(reasonText + "Do you wish to save before performing this action? ");

            return dialogResult;
        }
        private void checkChanges()
        {
            if (!origEm.Equals(em))
            {
                madeChanges = true;
            }
        }
        public void switchWindowOpen()
        {
            if (!windowOpen)
            {
                windowOpen = true;
            }
            else
            {
                windowOpen = false;
            }
        }
        private DialogResult promptConfirm(string promptText)
        {
            bool confirm = false;

            DialogResult dialogResult = MessageBox.Show(promptText, "Confirm", MessageBoxButtons.YesNo);

            return dialogResult;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            MessageBox.Show("Please use the 'Quit' button to close the application. ", "Notice");

            e.Cancel = true;
        }
    }
}
