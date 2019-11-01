using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class User
    {
        public User()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public byte[] Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
