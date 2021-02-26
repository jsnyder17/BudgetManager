using System;
using System.Collections.Generic;
using System.Text;

namespace Budget_Manager
{
    public class DollarFormat
    {

        public DollarFormat() {}

        public string format(double amount)
        {
            return String.Format("${0:0.00}", amount);
        }
    }
}
