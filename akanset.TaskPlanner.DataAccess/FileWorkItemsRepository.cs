using akanset.TaskPlanner.Domain.Models;
using akanset.TaskPlanner.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace akanset.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "work-items.json";
        private readonly Dictionary<Guid, WorkItem> _storage;

        public FileWorkItemsRepository()
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    var items = JsonConvert.DeserializeObject<WorkItem[]>(json) ?? Array.Empty<WorkItem>();

                    _storage = items.ToDictionary(w => w.Id, w => w);
                    return;
                }
            }
            _storage = new Dictionary<Guid, WorkItem>();
        }
        public Guid Add(WorkItem workItem)
        {
            var copied = workItem.Clone();
            var newId = Guid.NewGuid();
            copied.Id = newId;

            _storage[newId] = copied;
            return newId;
        }
        public WorkItem Get(Guid id)
        {
            return _storage.TryGetValue(id, out var item) ? item.Clone() : null;
        }
        public WorkItem[] GetAll()
        {
            return _storage.Values.Select(w => w.Clone()).ToArray();
        }
        public bool Update(WorkItem workItem)
        {
            if (!_storage.ContainsKey(workItem.Id))
                return false;

            _storage[workItem.Id] = workItem;
            return true;
        }
        public bool Remove(Guid id)
        {
            return _storage.Remove(id);
        }
        public void SaveChanges()
        {
            var items = _storage.Values.ToArray();
            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
    }
}