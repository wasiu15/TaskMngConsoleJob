﻿using Microsoft.EntityFrameworkCore;
using TaskManagerJob.Data;
using TaskManagerJob.Entities;
using TaskManagerJob.Repositories.Interfaces;
using TaskManagerJob.Utilities;

namespace TaskManagerJob.Repositories.Implementations
{
    public class TaskRepository : RepositoryBase<UserTask>, ITaskRepository
    {
        public TaskRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserTask>> GetAnyUnCompletedTaskToDueInTwoDays(bool trackChanges)
        {
            var fetchTasksFromDb = await FindAll(false).ToListAsync();

            var dueTasks = fetchTasksFromDb.Where(x => x.Status != Status.completed && Util.IsDateDue(x.DueDate)).ToList();
            return dueTasks;

        }
    }
}
