namespace TaskManagerApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Role Role { get; set; }

    }
    public class Role
    {
        public string Name { get; set; }
        public Role(string name) => Name = name;
    }

}
