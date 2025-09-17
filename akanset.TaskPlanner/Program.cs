using akanset.TaskPlanner.Domain.Logic;
using akanset.TaskPlanner.Domain.Models;
internal static class Program
{
    public static void Main(string[] args)
    {
        List<WorkItem> list = new List<WorkItem>();

        SimpleTaskPlanner planner = new SimpleTaskPlanner();
        
        
        while (true)
        {
            Console.WriteLine("Choose the option: (type the number)" + '\n' + "1. Create new Task." + '\n' + "2. See list of Tasks." + '\n' + "3. Exit.");
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter Title:");
                    string title = Console.ReadLine();

                    Console.WriteLine("Enter Due Date (dd.mm.yyyy):");
                    string dueDate = Console.ReadLine();

                    Console.WriteLine("Enter Priority: (0-4)");
                    int prior = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Complexity: (0-4)");
                    int comp = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Description:");
                    string desc = Console.ReadLine();

                    WorkItem toAdd = new WorkItem(title, DateTime.Parse(dueDate), (Priority)prior, (Complexity)comp, desc);
                    list.Add(toAdd);

                    break;
                case 2:
                    WorkItem[] newItem = planner.CreatePlan(list.ToArray());

                    Console.WriteLine("List:");

                    foreach (var item in newItem)
                    {
                        Console.WriteLine(item.ToString());
                    }

                    break;
                case 3:
                    Environment.Exit(0);

                    break;
            }
        }
    }
}