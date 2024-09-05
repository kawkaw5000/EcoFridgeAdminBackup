namespace AdminSideEcoFridge.Models
{
    public class DashboardViewModel
    {
        public List<VwUsersRoleView> UserList { get; set; }
        public List<VwUsersFoodItem> FoodList { get; set; }
        public List<User> User { get; set; }
    }
}
