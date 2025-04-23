using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Transaction
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } // "Income" or "Expense"
    public string Category { get; set; }
    public DateTime Date { get; set; }

    public Transaction(string description, decimal amount, string type, string category, DateTime date)
    {
        if (type != "Income" && type != "Expense")
            throw new ArgumentException("Type must be 'Income' or 'Expense'");
        Description = description;
        Amount = amount;
        Type = type;
        Category = category;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} | {Type} | {Category} | {Description} | ${Amount}";
    }
}

public class BudgetTracker
{
    private List<Transaction> transactions = new List<Transaction>();

    public void AddTransaction(Transaction t)
    {
        transactions.Add(t);
    }

    public decimal GetTotalIncome()
    {
        return transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
    }

    public decimal GetTotalExpenses()
    {
        return transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
    }

    public decimal GetNetSavings()
    {
        return GetTotalIncome() - GetTotalExpenses();
    }

    public Dictionary<string, decimal> GetSpendingByCategory()
    {
        return transactions
            .Where(t => t.Type == "Expense")
            .GroupBy(t => t.Category)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
    }

    public void SortTransactions(string criteria)
    {
        switch (criteria.ToLower())
        {
            case "date":
                transactions = transactions.OrderBy(t => t.Date).ToList();
                break;
            case "category":
                transactions = transactions.OrderBy(t => t.Category).ToList();
                break;
            case "amount":
                transactions = transactions.OrderByDescending(t => t.Amount).ToList();
                break;
        }
    }

    public void PrintSummary()
    {
        Console.WriteLine("\n===== Summary =====");
        Console.WriteLine($"Total Income: ${GetTotalIncome()}");
        Console.WriteLine($"Total Expenses: ${GetTotalExpenses()}");
        Console.WriteLine($"Net Savings: ${GetNetSavings()}");

        Console.WriteLine("\n--- Expenses by Category ---");
        foreach (var pair in GetSpendingByCategory())
        {
            Console.WriteLine($"{pair.Key}: ${pair.Value}");
        }

        var maxCategory = GetSpendingByCategory().OrderByDescending(kvp => kvp.Value).FirstOrDefault();
        Console.WriteLine($"\nMost spent category: {maxCategory.Key} (${maxCategory.Value})");
    }

    public void PrintAllTransactions()
    {
        Console.WriteLine("\n--- All Transactions ---");
        foreach (var t in transactions)
        {
            Console.WriteLine(t);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            foreach (var t in transactions)
            {
                sw.WriteLine($"{t.Description}|{t.Amount}|{t.Type}|{t.Category}|{t.Date}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename)) return;

        foreach (var line in File.ReadAllLines(filename))
        {
            var parts = line.Split('|');
            AddTransaction(new Transaction(parts[0], decimal.Parse(parts[1]), parts[2], parts[3], DateTime.Parse(parts[4])));
        }
    }
}

class Program
{
    static void Main()
    {
        BudgetTracker tracker = new BudgetTracker();
        string fileName = "transactions.txt";
        tracker.LoadFromFile(fileName);

        while (true)
        {
            Console.WriteLine("\n1. Add Transaction\n2. View Summary\n3. View Transactions\n4. Sort Transactions\n5. Save & Exit");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Description: ");
                        string desc = Console.ReadLine();
                        Console.Write("Amount: ");
                        decimal amount = decimal.Parse(Console.ReadLine());
                        Console.Write("Type (Income/Expense): ");
                        string type = Console.ReadLine();
                        Console.Write("Category: ");
                        string cat = Console.ReadLine();
                        Console.Write("Date (yyyy-mm-dd): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        tracker.AddTransaction(new Transaction(desc, amount, type, cat, date));
                        break;

                    case "2":
                        tracker.PrintSummary();
                        break;

                    case "3":
                        tracker.PrintAllTransactions();
                        break;

                    case "4":
                        Console.Write("Sort by (date/category/amount): ");
                        string sortBy = Console.ReadLine();
                        tracker.SortTransactions(sortBy);
                        break;

                    case "5":
                        tracker.SaveToFile(fileName);
                        Console.WriteLine("Data saved. Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}