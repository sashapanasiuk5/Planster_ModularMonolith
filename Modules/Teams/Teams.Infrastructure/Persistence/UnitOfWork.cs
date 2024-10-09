using Teams.Application.Interfaces;
using Teams.Infrastructure.Persistence.Repositories;

namespace Teams.Infrastructure.Persistence;

public class UnitOfWork: IUnitOfWork
{
    private readonly TeamsDbContext _context;
    private IProjectsRepository? _projectsRepository;
    private IMembersRepository? _membersRepository;
    
    public IProjectsRepository ProjectsRepository
    {
        get
        {
            if (_projectsRepository == null)
            {
                _projectsRepository = new ProjectsRepository(_context);
            }
            return _projectsRepository;
        }
    }

    public IMembersRepository MembersRepository {
        get
        {
            if (_membersRepository == null)
            {
                _membersRepository = new MembersRepository(_context);
            }
            return _membersRepository;
        }
    }

    public UnitOfWork(TeamsDbContext context)
    {
        _context = context;
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}