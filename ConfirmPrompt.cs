using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Budget_Manager
{
    public class ConfirmPrompt
    {

        public ConfirmPrompt() {}

        public bool displayPrompt(string promptText)
        {
            bool confirm = false;

            DialogResult dialogResult = MessageBox.Show(promptText, "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                confirm = true;
            }

            return confirm;
        }
    }
}
