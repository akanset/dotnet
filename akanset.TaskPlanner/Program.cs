using akanset.TaskPlanner.Domain.Logic;
using akanset.TaskPlanner.Domain.Models;
using akanset.TaskPlanner.DataAccess;
using akanset.TaskPlanner.DataAccess.Abstractions;
internal static class Program
{
    public static void Main(string[] args)
    {
        List<WorkItem> list = new List<WorkItem>();

        IWorkItemsRepository repo = new FileWorkItemsRepository();
        SimpleTaskPlanner planner = new SimpleTaskPlanner(repo);

        while (true)
        {
            Console.WriteLine("[A]dd" + '\n' + "[B]uild plan" + '\n' + "[M]ark completed" + '\n' + "[R]emove" + '\n' + "[Q]uit");
            Console.Write("Choose: ");

            var cmd = Console.ReadLine()?.Trim();
            switch (cmd?.ToUpperInvariant())
            {
                case "A":
                    AddWorkItem(repo);
                    break;

                case "B":
                    BuildPlan(planner);
                    break;

                case "M":
                    MarkCompleted(repo);
                    break;

                case "R":
                    RemoveItem(repo);
                    break;

                case "Q":
                    repo.SaveChanges();
                    Console.WriteLine("Changes saved. Quitting..");
                    return;

                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
    private static void AddWorkItem(IWorkItemsRepository repo)
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Enter description: ");
        string desc = Console.ReadLine() ?? "";

        Console.Write("Enter priority (1-5): ");
        int priority = int.TryParse(Console.ReadLine(), out var p) ? p : 1;

        Console.Write("Enter complexity (1-5): ");
        int complexity = int.TryParse(Console.ReadLine(), out var c) ? c : 1;

        var item = new WorkItem
        {
            Title = title,
            Description = desc,
            Priority = (Priority)priority,
            Complexity = (Complexity)complexity,
            CreationDate = DateTime.Now,
            IsCompleted = false
        };

        var id = repo.Add(item);
        Console.WriteLine($"WorkItem added with ID: {id}");
    }
    private static void BuildPlan(SimpleTaskPlanner planner)
    {
        var plan = planner.CreatePlan();

        Console.WriteLine("=== Plan ===");
        foreach (var item in plan)
        {
            Console.WriteLine($"[{item.Id}] {item.Title} - Completed: {item.IsCompleted}");
        }
    }
    private static void MarkCompleted(IWorkItemsRepository repo)
    {
        Console.Write("Enter ID of work item to mark completed: ");
        if (Guid.TryParse(Console.ReadLine(), out var id))
        {
            var item = repo.Get(id);
            if (item != null)
            {
                item.IsCompleted = true;
                repo.Update(item);
                Console.WriteLine("Marked as completed.");
            }
            else
            {
                Console.WriteLine("Work item not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid GUID.");
        }
    }
    private static void RemoveItem(IWorkItemsRepository repo)
    {
        Console.Write("Enter ID of work item to remove: ");
        if (Guid.TryParse(Console.ReadLine(), out var id))
        {
            if (repo.Remove(id))
            {
                Console.WriteLine("Work item removed.");
            }
            else
            {
                Console.WriteLine("Work item not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid GUID.");
        }
    }
}