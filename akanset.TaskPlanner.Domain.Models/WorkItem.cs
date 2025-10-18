namespace akanset.TaskPlanner.Domain.Models;

public class WorkItem
{
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Complexity Complexity { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public WorkItem(string title, DateTime dueDate, Priority priority, Complexity complexity, string? description)
    {
        Title = title;
        DueDate = dueDate;
        Priority = priority;
        Complexity = complexity;
        Description = description;
    }
    public override string ToString()   
    {
        string DueDate_converted = DueDate.ToString("dd.MM.yyyy");
        string Priority_converted = Priority.ToString().ToLower();
        string s = $"{Title}: due {DueDate_converted}, {Priority_converted} priority";
        return s;
    }
}

