namespace Teams.Application.Interfaces;

public interface IUnitOfWork
{
    IProjectsRepository ProjectsRepository { get; }
    IMembersRepository MembersRepository { get; }

    Task SaveChangesAsync();
}