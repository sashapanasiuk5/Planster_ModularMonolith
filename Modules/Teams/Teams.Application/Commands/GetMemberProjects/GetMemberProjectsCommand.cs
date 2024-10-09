using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams;

namespace Teams.Application.Commands.GetMemberProjects;

public record GetMemberProjectsCommand(int MemberId): IRequest<Result<List<ProjectDto>>>;