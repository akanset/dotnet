namespace akanset.TaskPlanner.Domain.Logic;

using akanset.TaskPlanner.Domain.Models;

public class SimpleTaskPlanner
{
    public WorkItem[] CreatePlan(WorkItem[] items)
    {
        List<WorkItem> list = items.ToList();
        list.Sort((x, y) =>
            {
                int result = y.Priority.CompareTo(x.Priority);
                if (result == 0)
                {
                    result = x.DueDate.CompareTo(y.DueDate);
                }
                if (result == 0)
                {
                    result = string.Compare(x.Title, y.Title, StringComparison.OrdinalIgnoreCase);
                }
                return result;
            });

            return list.ToArray();
    }
}
