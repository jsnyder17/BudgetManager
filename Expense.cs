using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Budget_Manager
{
    public class Expense
    {
        private string name;
        private double basePrice;
        private double tax;
        private int importance;
        private double taxPrice;

        public Expense()
        {
            name = "";
            basePrice = 0.0;
            tax = 0.0;
            importance = 0;
            taxPrice = 0.0;
        }
        public Expense(string name, double basePrice, double tax, int importance)
        {
            this.name = name;
            this.basePrice = basePrice;
            this.tax = tax;
            this.importance = importance;
            calcTaxPrice();
        }
        public Expense(string data)
        {
            string[] parts = data.Split(",");

            name = parts[0];
            basePrice = Double.Parse(parts[1]);
            tax = Double.Parse(parts[2]);
            importance = int.Parse(parts[3]);

            calcTaxPrice();
        }
        public Expense(Expense expense)
        {
            this.name = expense.name;
            this.basePrice = expense.basePrice;
            this.tax = expense.tax;
            this.importance = expense.importance;
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
            return tax;
        }
        public int getImportance()
        {
            return importance;
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
        public void setTax(double tax)
        {
            this.tax = tax;
            calcTaxPrice();
        }
        public void setImportance(int importance)
        {
            this.importance = importance;
        }

        public string toString()
        {
            return (name + "," + basePrice + "," + tax + "," + importance);
        }

        public String display()
        {
            return String.Format("{0:0.00}", basePrice.ToString()); // Returns the base price in currency notation 
        }

        private void calcTaxPrice()
        {
            System.Diagnostics.Debug.WriteLine("Calculating " + basePrice.ToString() + " * " + (tax / 100).ToString());
            taxPrice = basePrice * (tax / 100);
        }
    }
}
