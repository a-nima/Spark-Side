namespace SparkSide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SparkSide.Data.Common.Repositories;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<UserFollow> userFollowRepository;
        private readonly UserManager<ApplicationUser> userManager;


        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<UserFollow> userFollowRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.usersRepository = usersRepository;
            this.userFollowRepository = userFollowRepository;
            this.userManager = userManager;
        }

        public UserDTO GetById(string id)
        {
            return this.usersRepository
                .All()
                .Where(a => a.Id == id)
                .Select(a => new UserDTO(a))
                .FirstOrDefault();
        }

        public UserDTO GetByLoginName(string loginName)
        {
            return this.usersRepository
               .All()
               .Where(a => a.LoginName.ToLower() == loginName.ToLower())
               .Include(e => e.FollowedByUsers)
               .Include(e => e.FollowedUsers)
               .Select(a => new UserDTO(a))
               .FirstOrDefault();
        }

        public bool IsFollowing(string currentUserId, string otherUserId)
        {
            return this.userFollowRepository
                .All()
                .Where(u => u.FollowerId == currentUserId && u.FollowedId == otherUserId)
                .Any();
        }

        public async Task FollowAsync(string currentUserId, string followedUserId)
        {
            if (!this.IsFollowing(currentUserId, followedUserId))
            {
                UserFollow entity = new UserFollow
                {
                    FollowedId = followedUserId,
                    FollowerId = currentUserId,
                };

                await this.userFollowRepository.AddAsync(entity);
                await this.userFollowRepository.SaveChangesAsync();
            }

            // TODO: throw error if following already
        }

        public async Task UnfollowAsync(string currentUserId, string otherUserId)
        {
            var followEntity = this.userFollowRepository
                .All()
                .Where(u => u.FollowerId == currentUserId && u.FollowedId == otherUserId)
                .FirstOrDefault();

            this.userFollowRepository.HardDelete(followEntity);

            await this.userFollowRepository.SaveChangesAsync();
        }

        public Task<bool> IsInRoleAsync(string id, string roleName)
        {
            ApplicationUser user = this.usersRepository
                .All()
                .Where(u => u.Id == id)
                .FirstOrDefault();
            if (user != null)
            {
                return this.userManager.IsInRoleAsync(user, roleName);
            }

            return Task.FromResult(false);
        }
    }
}
