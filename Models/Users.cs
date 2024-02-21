using System;
using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }

}
