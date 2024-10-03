using AdminSideEcoFridge.Models;
using Microsoft.Data.SqlClient; // Make sure to include this namespace for SqlParameter
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class UserSearcRepository
{
    private readonly EcoFridgeDbContext _context;

    public UserSearcRepository()
    {
        _context = new EcoFridgeDbContext();
    }

    public List<VwUsersRoleView> SearchUsers(string keyword)
    {
        var parameter = new SqlParameter("@Keyword", keyword ?? (object)DBNull.Value);

        var users = _context.VwUsersRoleViews
            .FromSqlRaw("EXEC SearchUsers @Keyword", parameter)
            .ToList();

        return users ?? new List<VwUsersRoleView>();
    }

}
