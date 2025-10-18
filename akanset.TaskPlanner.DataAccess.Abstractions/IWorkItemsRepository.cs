namespace akanset.TaskPlanner.DataAccess.Abstractions;

using akanset.TaskPlanner.Domain.Models;
public interface IWorkItemsRepository
{
    public Guid Add(WorkItem workItem);
    WorkItem Get(Guid id);
    WorkItem[] GetAll();
    bool Update(WorkItem workItem);
    bool Remove(Guid id);
    void SaveChanges();
}
