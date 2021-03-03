using System;
using System.Collections.Generic;
using System.Text;

namespace Budget_Manager
{
    public class ExpenseManager
    {
        private List<Expense> expenses;
        private double totalPrice;
        private double totalTax;
        private double totalSavings;
        private double totalAll;

        private DollarFormat df;
        public ExpenseManager()
        {
            expenses = new List<Expense>();
            totalPrice = 0.0;
            totalTax = 0.0;
            totalSavings = 0.0;
            totalAll = 0.0;

            df = new DollarFormat();
        }
        
        // Getters
        public List<Expense> getExpenses()
        {
            return expenses;
        }
        public double getTotalPrice()
        {
            return totalPrice;
        }
        public double getTotalTax()
        {
            return totalTax;
        }
        public double getTotalSavings()
        {
            return totalSavings;
        }
        public double getTotalAll()
        {
            return totalAll;
        }

        // Other methods
        public void addExpense(Expense expense)
        {
            expenses.Add(expense);

            calcTotals();
        }
        public void removeExpense(int index)
        {
            expenses.RemoveAt(index);

            calcTotals();
        }
        public void replaceExpense(Expense expense, int index)
        {
            expenses[index].setName(expense.getName());
            expenses[index].setBasePrice(expense.getBasePrice());
            expenses[index].setTax(expense.getTax());
            expenses[index].setImportance(expense.getImportance());

            calcTotals();
        }
        public void clearExpenses()
        {
            expenses.Clear();

            calcTotals();
        }

        public string toString()
        {
            string data = "";

            for (int i = 0; i < expenses.Count; i++)
            {
                data += (expenses[i].toString() + "\n");
            }

            return data;
        }

        private void calcTotals()
        {
            totalPrice = 0.0;
            totalTax = 0.0;
            totalSavings = 0.0;
            totalAll = 0.0;

            for (int i = 0; i < expenses.Count; i++)
            {
                totalPrice += expenses[i].getBasePrice();
                totalTax += expenses[i].getTaxPrice();
                
                if (expenses[i].getImportance() == 1)
                {
                    totalSavings += expenses[i].getBasePrice() + expenses[i].getTaxPrice();
                }

                totalAll += (expenses[i].getBasePrice() + expenses[i].getTaxPrice());
            }
        }

        public string listDisplay(int index)
        {
            string data = "";

            data = (expenses[index].getName() + "   " + df.format(expenses[index].getBasePrice()) + "   T: " + df.format(expenses[index].getTaxPrice()) + "   Total: " + df.format(expenses[index].getTotalPrice()));

            return data;
        }
    }
}
