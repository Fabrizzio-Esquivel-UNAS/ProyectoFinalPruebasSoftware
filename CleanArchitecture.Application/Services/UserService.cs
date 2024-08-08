using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Users.GetAll;
using CleanArchitecture.Application.Queries.Users.GetUserById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Commands.Users.ChangePassword;
using CleanArchitecture.Domain.Commands.Users.ChangePreferencias;
using CleanArchitecture.Domain.Commands.Users.CreateUser;
using CleanArchitecture.Domain.Commands.Users.DeleteUser;
using CleanArchitecture.Domain.Commands.Users.LoginUser;
using CleanArchitecture.Domain.Commands.Users.UpdateUser;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.DTOs;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IMediatorHandler _bus;
    private readonly IUser _user;

    public UserService(IMediatorHandler bus, IUser user)
    {
        _bus = bus;
        _user = user;
    }

    public async Task<UserViewModel?> GetUserByUserIdAsync(Guid userId)
    {
        return await _bus.QueryAsync(new GetUserByIdQuery(userId));
    }

    public async Task<UserViewModel?> GetCurrentUserAsync()
    {
        return await _bus.QueryAsync(new GetUserByIdQuery(_user.GetUserId()));
    }

    public async Task<PagedResult<UserViewModel>> GetAllUsersAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        int[]? searchRoles = null,
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllUsersQuery(query, includeDeleted, searchTerm, searchRoles, sortQuery));
    }

    public async Task<Guid> CreateUserAsync(CreateUserViewModel user)
    {
        var userId = Guid.NewGuid();
        var tenantId = Ids.Seed.TenantId;

        await _bus.SendCommandAsync(new CreateUserCommand(
            userId,
            user.LineaInvestigacionId,
            user.EscuelaId,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Password,
            user.Telefono,
            user.Codigo,
            user.Role));

        return userId;
    }

    public async Task UpdateUserAsync(UpdateUserViewModel user)
    {
        await _bus.SendCommandAsync(new UpdateUserCommand(
            user.Id,
            user.EscuelaId,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Telefono,
            user.Notificaciones,
            user.Role,
            user.AsesoriaEstado));
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        await _bus.SendCommandAsync(new DeleteUserCommand(userId));
    }

    public async Task ChangePasswordAsync(ChangePasswordViewModel viewModel)
    {
        await _bus.SendCommandAsync(new ChangePasswordCommand(viewModel.Password, viewModel.NewPassword));
    }

    public async Task ChangePreferenciasAsync(ChangePreferenciasViewModel viewModel)
    {
        await _bus.SendCommandAsync(new ChangePreferenciasCommand(viewModel.NewPreferencias));
    }

    public async Task<TokenResponse> LoginUserAsync(LoginUserViewModel viewModel)
    {
        return await _bus.QueryAsync(new LoginUserCommand(viewModel.Email, viewModel.Password));
    }
}