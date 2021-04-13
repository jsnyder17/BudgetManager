using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Budget_Manager
{
    public class Expense
    {
        private string name;
        private double basePrice;
        private int importance;
        private double taxPrice;
        private bool taxable;

        private State state;

        public Expense()
        {
            name = "";
            basePrice = 0.0;
            importance = 0;
            taxPrice = 0.0;
            taxable = false;

            state = new State();
        }
        public Expense(string name, double basePrice, State state, int importance, bool taxable)
        {
            this.name = name;
            this.basePrice = basePrice;
            this.state = state;
            this.importance = importance;
            this.taxable = taxable;

            if (taxable)
            {
                calcTaxPrice();
            }
            else
            {
                taxPrice = 0.0;
            }
        }
        public Expense(string data)
        {
            string[] parts = data.Split(",");

            name = parts[0];
            basePrice = Double.Parse(parts[1]);
            state = new State(parts[2]);
            importance = int.Parse(parts[3]);
            taxable = Boolean.Parse(parts[4]);

            if (taxable)
            {
                calcTaxPrice();
            }
            else
            {
                taxPrice = 0.0;
            }
        }
        public Expense(Expense expense)
        {
            this.name = expense.name;
            this.basePrice = expense.basePrice;
            this.state = expense.state;
            this.importance = expense.importance;
            this.taxable = expense.taxable;

            if (taxable)
            {
                calcTaxPrice();
            }
            else
            {
                taxPrice = 0.0;
            }
        }

        // Getters
        public string getName()
        {
            return name;
        }
        public double getBasePrice()
        {
            return basePrice;
        }
        public double getTax()
        {
            return state.getTaxRate();
        }
        public State getState()
        {
            return state;
        }
        public int getImportance()
        {
            return importance;
        }
        public bool getTaxable()
        {
            return taxable;
        }
        public double getTaxPrice()
        {
            return taxPrice;
        }
        public double getTotalPrice()
        {
            return basePrice + taxPrice;
        }

        //Setters
        public void setName(string name)
        {
            this.name = name;
        }
        public void setBasePrice(double basePrice)
        {
            this.basePrice = basePrice;
            calcTaxPrice();
        }
        public void setState(State state)
        {
            this.state = state;
            calcTaxPrice();
        }
        public void setImportance(int importance)
        {
            this.importance = importance;
        }

        public string toString()
        {
            return (name + "," + basePrice + "," + state.fileOutput() + "," + importance + "," + taxable);
        }

        public String display()
        {
            return String.Format("{0:0.00}", basePrice.ToString()); // Returns the base price in currency notation 
        }

        private void calcTaxPrice()
        {
            System.Diagnostics.Debug.WriteLine("Calculating " + basePrice.ToString() + " * " + (state.getTaxRate() * 100).ToString());
            taxPrice = basePrice * state.getTaxRate();
        }

        public bool Equals(Expense e)
        {
            bool equals = false;

            if (this.name.Equals(e.name) && this.basePrice == e.basePrice && this.state.equals(e.state) && this.taxPrice == e.taxPrice && this.taxable == e.taxable)
            {
                equals = true;
            }

            return equals;
        }
    }
}
