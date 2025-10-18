using System;
using System.Collections.Generic;
using System.Linq;
using akanset.TaskPlanner.Domain.Logic;
using akanset.TaskPlanner.Domain.Models;
using akanset.TaskPlanner.DataAccess.Abstractions;
using Moq;
using Xunit;

namespace akanset.TaskPlanner.Domain.Logic.Tests;

public class SimpleTaskPlannerTests
{
    [Fact]
    public void Test1() //no completed tasks
    {
        var mockRepo = new Mock<IWorkItemsRepository>();

        var items = new[]
        {
            new WorkItem { Id = Guid.NewGuid(), Title = "Done", IsCompleted = true },
            new WorkItem { Id = Guid.NewGuid(), Title = "Pending", IsCompleted = false }
        };

        mockRepo.Setup(r => r.GetAll()).Returns(items);

        var planner = new SimpleTaskPlanner(mockRepo.Object);

        var plan = planner.CreatePlan();

        Assert.Single(plan);
        Assert.Equal("Pending", plan[0].Title);
    }
    [Fact]
    public void Test2() //empty if all tasks are completed
    {
        var mockRepo = new Mock<IWorkItemsRepository>();

        var items = new[]
        {
            new WorkItem { Id = Guid.NewGuid(), Title = "Done1", IsCompleted = true },
            new WorkItem { Id = Guid.NewGuid(), Title = "Done2", IsCompleted = true }
        };

        mockRepo.Setup(r => r.GetAll()).Returns(items);

        var planner = new SimpleTaskPlanner(mockRepo.Object);

        var plan = planner.CreatePlan();

        Assert.Empty(plan);
    }
    [Fact]
    public void Test3() //sort by priority -> due date
    {
        var mockRepo = new Mock<IWorkItemsRepository>();

        var items = new[]
        {
            new WorkItem { Id = Guid.NewGuid(), Title = "Low priority", Priority = Priority.Low, DueDate = DateTime.Now.AddDays(2), IsCompleted = false },
            new WorkItem { Id = Guid.NewGuid(), Title = "High priority", Priority = Priority.High, DueDate = DateTime.Now.AddDays(5), IsCompleted = false },
            new WorkItem { Id = Guid.NewGuid(), Title = "Medium priority", Priority = Priority.Medium, DueDate = DateTime.Now.AddDays(1), IsCompleted = false }
        };

        mockRepo.Setup(r => r.GetAll()).Returns(items);

        var planner = new SimpleTaskPlanner(mockRepo.Object);

        var plan = planner.CreatePlan();

        Assert.Equal(3, plan.Length);
        Assert.Equal("High priority", plan[0].Title);
        Assert.Equal("Medium priority", plan[1].Title);
        Assert.Equal("Low priority", plan[2].Title);
    }
}
