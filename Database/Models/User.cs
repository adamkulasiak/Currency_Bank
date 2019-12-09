using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CurrencyBank.Database.Models
{
    public class User
    {
        public User()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
