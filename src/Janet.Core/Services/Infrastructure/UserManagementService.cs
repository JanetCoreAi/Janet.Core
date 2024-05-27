using Janet.Core.Interfaces;
using Janet.Core.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janet.Core.Services.Infrastructure
{
    public class UserManagementService : IUserManagementService
    {
        public UserManagementService()
        {
            CurrentUser = new UserProfile();
        }

        public UserProfile CurrentUser { get; set; }
    }
}
