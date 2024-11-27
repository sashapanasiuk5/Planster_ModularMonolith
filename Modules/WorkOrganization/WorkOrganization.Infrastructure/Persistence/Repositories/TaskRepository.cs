using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Dto.Work.Tasks;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Domain.Models;
using TaskStatus = WorkOrganization.Domain.Models.TaskStatus;

namespace WorkOrganization.Infrastructure.Persistence.Repositories;

public class TaskRepository: ITaskRepository
{
    private readonly WorkDbContext _context;

    public TaskRepository(WorkDbContext context)
    {
        _context = context;
    }
    public async Task<ProjectTask?> GetWithHierarchyByIdAsync(int id)
    {
        var tasks = await _context.Tasks.FromSqlRaw(RawQueries.RecursiveTaskById, id)
            .Include(x => x.Status)
            .Include(x => x.Assignee)
            .ToListAsync();
        return tasks.FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<ProjectTask>> GetAllWithHierarchyAsync(int projectId, TaskFilterDto? filter = null)
    {
        IQueryable<ProjectTask> query = _context.Tasks
            .Include(x => x.Assignee)
            .Include(x => x.Sprint)
            .Include(x => x.Status);
        
        if (filter != null)
        {
            if (filter.SprintId != null)
            {
                query = query.Where(x => x.Sprint!.Id == filter.SprintId);
            }
            if (filter.StatusId != null)
            {
                query = query.Where(x => x.Status!.Id == filter.StatusId);
            }
        }
        var tasks = await query.ToListAsync();
        return tasks.Where(x => x.ParentTaskId == null).ToList();
    }
    public async Task<ProjectTask?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<ProjectTask?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Tasks
            .Include(x => x.Assignee)
            .Include(x => x.Sprint)
            .Include(x => x.Status)
            .Include(x => x.SubTasks)
            .Include(x => x.ParentTask)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TaskStatus?> GetStatusByIdAsync(int id)
    {
        return await _context.TaskStatuses.FindAsync(id);
    }

    public Task<List<TaskStatus>> GetAllStatusesAsync()
    {
        return _context.TaskStatuses.ToListAsync();
    }

    public Task<List<ProjectTask>> FindTasksByTileAsync(int projectId, string title)
    {
        return _context.Tasks
            .Where(x => x.ProjectId == projectId)
            .Where(x => x.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();
    }

    public void RemoveTask(ProjectTask projectTask)
    {
        _context.Tasks.Remove(projectTask);
    }

    public void AddTask(ProjectTask task)
    {
        _context.Tasks.Add(task);
    }
}