using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Identity
{
    public interface ILoggedUser
    {
        Guid UserId { get; set; }
        string Username { get; set; }
    }
}
