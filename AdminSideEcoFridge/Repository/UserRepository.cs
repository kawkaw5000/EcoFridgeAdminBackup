using AdminSideEcoFridge.Models;
using Microsoft.Data.SqlClient; // Make sure to include this namespace for SqlParameter
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class UserRepository
{
    private readonly EcoFridgeDbContext _context;

    public UserRepository()
    {
        _context = new EcoFridgeDbContext();
    }

    // Other existing methods...

    public List<User> SearchUsers(string keyword)
    {
        var parameter = new SqlParameter("@Keyword", keyword ?? (object)DBNull.Value);

        var users = _context.Users
            .FromSqlRaw("EXEC SearchUsers @Keyword", parameter)
            .ToList();

        return users ?? new List<User>(); // Return an empty list if users is null
    }

}
