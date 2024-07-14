using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoCreatus.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; init; } // Guid creates a unique identifier and init makes it immutable

        [Required]
        [StringLength(100)]
        public string Name { get; private set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; private set; }

        [Required]
        [StringLength(100)]
        public string Password { get; private set; }

        [Required]
        [Range(1, 5)]
        public int Level { get; private set; }
        
        public bool IsActive { get; private set; }
        public string Role { get; set; }

        public User(string name, string email, string password, int level)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Level = level;
            IsActive = true;
            if (level >= 4)
            {
                Role = "Admin";
            }
            else Role = "User";
        }
        public void UpdateUser(string name = null, string email = null, string password = null, int? level = null)
        {
            Name = name ?? Name;
            Email = email ?? Email;
            Password = password ?? Password;
            Level = level ?? Level;
        }

        public void DeleteUser()
        {
            IsActive = false;
        }
        
    }
}
