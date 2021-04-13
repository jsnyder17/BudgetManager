using System;
using System.Collections.Generic;
using System.Text;

namespace Budget_Manager
{
    public class State
    {
        private string abb;
        private double taxRate;

        public State()
        {
            abb = "";
            taxRate = 0.0;
        }
        public State(string abb, double taxRate)
        {
            this.abb = abb;
            this.taxRate = taxRate;

            System.Diagnostics.Debug.WriteLine("State constructor called. ");
        }
        public State(String data)
        {
            string[] parts = data.Split("$");

            abb = parts[0];
            taxRate = Double.Parse(parts[1]);
        }

        public string getAbb()
        {
            return abb;
        }
        public double getTaxRate()
        {
            return taxRate;
        }

        public string fileOutput()
        {
            return (abb + "$" + taxRate);
        }
        public bool equals(State state)
        {
            return (this.abb.Equals(state.abb) && this.taxRate == state.taxRate);
        }
        public override String ToString()
        {
            return abb;
        }
    }
}
