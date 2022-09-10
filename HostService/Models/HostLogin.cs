﻿using System.ComponentModel.DataAnnotations;

namespace HostService.Models
{
    public class HostLogin
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Roles { get; set; }

    }
}
