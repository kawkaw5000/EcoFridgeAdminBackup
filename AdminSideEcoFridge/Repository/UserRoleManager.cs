using AdminSideEcoFridge.Models;

namespace AdminSideEcoFridge.Repository
{
    public class UserRoleManager
    {
        private BaseRepository<User> _userAcc;
        private BaseRepository<VwUsersRoleView> _userRole;
        private readonly EcoFridgeDbContext _context;

        public UserRoleManager()
        {
            _userAcc = new BaseRepository<User>();
            _userRole = new BaseRepository<VwUsersRoleView>();
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public VwUsersRoleView GetUserByUserId(int userId)
        {
            return _userRole._table.Where(m => m.UserId == userId).FirstOrDefault();
        }

    }
}
