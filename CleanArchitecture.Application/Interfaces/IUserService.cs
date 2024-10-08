using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.DTOs;

namespace CleanArchitecture.Application.Interfaces;

public interface IUserService
{
    public Task<UserViewModel?> GetUserByUserIdAsync(Guid userId);
    public Task<UserViewModel?> GetCurrentUserAsync();

    public Task<PagedResult<UserViewModel>> GetAllUsersAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        int[]? searchRoles = null,
        SortQuery? sortQuery = null);

    public Task<Guid> CreateUserAsync(CreateUserViewModel user);
    public Task UpdateUserAsync(UpdateUserViewModel user);
    public Task DeleteUserAsync(Guid userId);
    public Task ChangePasswordAsync(ChangePasswordViewModel viewModel);
    public Task ChangePreferenciasAsync(ChangePreferenciasViewModel viewModel);
    public Task<TokenResponse> LoginUserAsync(LoginUserViewModel viewModel);
}