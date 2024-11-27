using WorkOrganization.Application.Interfaces;
using WorkOrganization.Infrastructure.Persistence;
using WorkOrganization.Infrastructure.Persistence.Repositories;

namespace WorkOrganization.Infrastructure;

public class UnitOfWork: IUnitOfWork
{
    private readonly WorkDbContext _context;
    private ITaskRepository? _taskRepository;
    private ISprintRepository? _sprintRepository;
    private IAssigneeRepository? _assigneeRepository;
    private IProjectRepository? _projectRepository;

    public UnitOfWork(WorkDbContext context)
    {
        _context = context;
    }
    public ITaskRepository TaskRepository {
        get
        {
            if (_taskRepository == null)
                _taskRepository = new TaskRepository(_context);
            return _taskRepository;
        }
    }
    public IProjectRepository ProjectRepository {
        get
        {
            if(_projectRepository == null)
                _projectRepository = new ProjectRepository(_context);
            return _projectRepository;
        }
    }
    public IAssigneeRepository AssigneeRepository {
        get
        {
            if(_assigneeRepository == null)
                _assigneeRepository = new AssigneeRepository(_context);
            return _assigneeRepository;
        }
    }

    public ISprintRepository SprintRepository
    {
        get
        {
            if(_sprintRepository == null)
                _sprintRepository = new SprintRepository(_context);
            return _sprintRepository;
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}