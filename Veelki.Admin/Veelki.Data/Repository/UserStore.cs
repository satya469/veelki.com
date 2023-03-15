using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Veelki.Data.Entities;

namespace Veelki.Data.Repository
{
    public class UserStore : IUserStore<Users>, IUserEmailStore<Users>, IUserPhoneNumberStore<Users>,
        IUserTwoFactorStore<Users>, IUserPasswordStore<Users>, IUserRoleStore<Users>
    {
        private readonly string _connectionString;

        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IdentityResult> CreateAsync(Users user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                user.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [Users] ([UserName], [NormalizedUserName], [FullName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [RoleId], [CreatedDate], [RollingCommission], [AssignCoin], [Commision], [ExposureLimit], [ParentId], [Status], [City], [State])
                    VALUES (@{nameof(Users.UserName)}, @{nameof(Users.NormalizedUserName)}, @{nameof(Users.FullName)}, @{nameof(Users.Email)}, @{nameof(Users.NormalizedEmail)}, @{nameof(Users.EmailConfirmed)}, @{nameof(Users.PasswordHash)}, @{nameof(Users.PhoneNumber)}, @{nameof(Users.PhoneNumberConfirmed)}, @{nameof(Users.TwoFactorEnabled)}, @{nameof(Users.RoleId)}, @{nameof(Users.CreatedDate)}, @{nameof(Users.RollingCommission)}, @{nameof(Users.AssignCoin)}, @{nameof(Users.Commision)}, @{nameof(Users.ExposureLimit)}, @{nameof(Users.ParentId)}, @{nameof(Users.Status)}, @{nameof(Users.City)}, @{nameof(Users.State)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Users user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [Users] WHERE [Id] = @{nameof(Users.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public async Task<Users> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<Users>($@"SELECT * FROM [Users]
                    WHERE [Id] = @{nameof(userId)}", new { userId });
            }
        }

        //public async Task<Users> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync(cancellationToken);
        //        return await connection.QuerySingleOrDefaultAsync<Users>($@"SELECT * FROM [Users]
        //            WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });
        //    }
        //}

        // 26/07/2022 updated because of username finding need.
        public async Task<Users> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<Users>($@"SELECT * FROM [Users]
                    WHERE [UserName] = @{nameof(userName)}", new { userName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(Users user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(Users user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(Users user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [Users] SET
                    [UserName] = @{nameof(Users.UserName)},
                    [NormalizedUserName] = @{nameof(Users.NormalizedUserName)},
                    [Email] = @{nameof(Users.Email)},
                    [NormalizedEmail] = @{nameof(Users.NormalizedEmail)},
                    [EmailConfirmed] = @{nameof(Users.EmailConfirmed)},
                    [PasswordHash] = @{nameof(Users.PasswordHash)},
                    [PhoneNumber] = @{nameof(Users.PhoneNumber)},
                    [PhoneNumberConfirmed] = @{nameof(Users.PhoneNumberConfirmed)},
                    [TwoFactorEnabled] = @{nameof(Users.TwoFactorEnabled)}
                    WHERE [Id] = @{nameof(Users.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public Task SetEmailAsync(Users user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<Users> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<Users>($@"SELECT * FROM [Users]
                    WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }

        public Task<string> GetNormalizedEmailAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(Users user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(Users user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(Users user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(Users user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task AddToRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @{nameof(normalizedName)}", new { normalizedName });
                if (!roleId.HasValue)
                    roleId = await connection.ExecuteAsync($"INSERT INTO [ApplicationRole]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                        new { roleName, normalizedName });

                await connection.ExecuteAsync($"IF NOT EXISTS(SELECT 1 FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}) " +
                    $"INSERT INTO [ApplicationUserRole]([UserId], [RoleId]) VALUES(@userId, @{nameof(roleId)})",
                    new { userId = user.Id, roleId });
            }
        }

        public async Task RemoveFromRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (!roleId.HasValue)
                    await connection.ExecuteAsync($"DELETE FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.Id, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(Users user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResults = await connection.QueryAsync<string>("SELECT r.[Name] FROM [ApplicationRole] r INNER JOIN [ApplicationUserRole] ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.Id });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (roleId == default(int)) return false;
                var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                    new { userId = user.Id, roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<Users>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var queryResults = await connection.QueryAsync<Users>("SELECT u.* FROM [Users] u " +
                    "INNER JOIN [ApplicationUserRole] ur ON ur.[UserId] = u.[Id] INNER JOIN [ApplicationRole] r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }

        //public async Task<IList<Claim>> GetClaimsAsync(Users user, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync(cancellationToken);
        //        var queryResults = await connection.QueryAsync<UserClaims>("SELECT top 1 * FROM [UserClaims] WHERE RoleId = @RoleId", new { RoleId = user.RoleId });

        //        return queryResults?.Select(y => new Claim(y.ClaimType, y.ClaimValue)).ToList();
        //    }
        //}

        //public async Task AddClaimsAsync(Users user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync(cancellationToken);
        //        foreach (var item in claims)
        //        {
        //            await connection.ExecuteAsync($"INSERT INTO [UserClaims]([RoleId], [ClaimType], [ClaimValue]) VALUES(@{nameof(user.RoleId)}, @{nameof(item.ValueType)}, @{nameof(item.Value)})",
        //              new { user.RoleId, item.ValueType, item.Value });
        //        }                
        //    }
        //}

        //public Task ReplaceClaimAsync(Users user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task RemoveClaimsAsync(Users user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync(cancellationToken);
        //        //foreach (var item in claims)
        //        //{
        //        //    await connection.ExecuteAsync($"DELETE FROM [UserClaims] WHERE RoleId = @{nameof(user.RoleId)}",
        //        //      new { user.RoleId });
        //        //}
        //        await connection.ExecuteAsync($"DELETE FROM [UserClaims] WHERE RoleId = @{nameof(user.RoleId)}",
        //             new { user.RoleId });
        //    }
        //}

        //public Task<IList<Users>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
