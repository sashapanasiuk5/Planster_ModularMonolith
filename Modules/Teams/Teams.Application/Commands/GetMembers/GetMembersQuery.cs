using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Member;

namespace Teams.Application.Commands.GetMembers;

public record GetMembersQuery(int ProjectId) : IRequest<Result<List<MemberDto>>>;
