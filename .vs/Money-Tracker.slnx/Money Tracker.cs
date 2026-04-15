using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTracker
{
    class Program
    {
        static List<string> categories = new List<string> { "Rent", "Groceries", "Internet" };
        static List<double> expenses = new List<double> { 950, 250.00, 60.00 };
        static double monthlyLimit = 2000.00;

        static void Main(string[] args)
        {
            bool active = true;

            while (active)
            {
                Console.Clear();
                Console.WriteLine("PERSONAL CASH FLOW TRACKER");
                // Display current expenses
                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"[{i}] {categories[i].PadRight(12)} : ${expenses[i]:N2}");
                }
                // Display total and budget status
                double currentTotal = expenses.Sum();
                string report = GenerateTotalReport(currentTotal);

                Console.WriteLine("----------------------------------");
                Console.WriteLine(report);
                Console.WriteLine("----------------------------------");
                // Navigation options
                Console.WriteLine("\n[A] Add | [D] Delete | [S] Set Limit | [Q] Quit");
                Console.Write("Action: ");
                string nav = Console.ReadLine().ToUpper();
                // Handle user input
                switch (nav)
                {
                    case "A":
                        Console.Write("Label: ");
                        string label = Console.ReadLine();
                        Console.Write("Cost: ");
                        if (double.TryParse(Console.ReadLine(), out double cost))
                        {
                            categories.Add(label);
                            expenses.Add(cost);
                        }
                        break;

                    case "D":
                        Console.Write("Enter ID to drop: ");
                        if (int.TryParse(Console.ReadLine(), out int id) && id < categories.Count)
                        {
                            categories.RemoveAt(id);
                            expenses.RemoveAt(id);
                        }
                        break;

                    case "S":
                        Console.Write("New Monthly Limit: ");
                        double.TryParse(Console.ReadLine(), out monthlyLimit);
                        break;

                    case "Q":
                        active = false;
                        break;
                }
            }
        }

        // Generate a report showing total expenses, budget limit, and status
        static string GenerateTotalReport(double total)
        {
            double remaining = monthlyLimit - total;
            string status = (remaining >= 0) ? "UNDER BUDGET" : "OVER BUDGET";

            return $"TOTAL: ${total:N2} | LIMIT: ${monthlyLimit:N2} | {status} (${Math.Abs(remaining):N2})";
        }
    }
}