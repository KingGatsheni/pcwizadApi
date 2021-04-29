using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserInfo
    {
        [Key]
        public Guid StuffId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}