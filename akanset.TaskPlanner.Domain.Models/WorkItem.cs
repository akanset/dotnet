namespace akanset.TaskPlanner.Domain.Models;

using akanset.TaskPlanner.Domain.Models;
public class WorkItem
{
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Complexity Complexity { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public Guid Id { get; set; }
    public WorkItem()
    {
        Title = "default";
        DueDate = new DateTime(11, 11, 1111);
        Priority = Priority.None;
        Complexity = Complexity.None;
        Description = "default";
    } 
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
        string s = $"{Title}: due {DueDate_converted}, {Priority} priority";
        return s;
    }
    public WorkItem Clone()
    {
        return (WorkItem)this.MemberwiseClone();
    }
}
