namespace akanset.TaskPlanner.Domain.Logic;

using akanset.TaskPlanner.Domain.Models;
using akanset.TaskPlanner.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

public class SimpleTaskPlanner
{
    private readonly IWorkItemsRepository _repository;
    public SimpleTaskPlanner(IWorkItemsRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public WorkItem[] CreatePlan()
    {
        var items = _repository.GetAll();
        var activeItems = items.Where(w => !w.IsCompleted).ToList();
        List<WorkItem> list = activeItems.ToList();
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
