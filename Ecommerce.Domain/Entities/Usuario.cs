using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        public string nombreCompeto { get; set; } = string.Empty;
    }
}
