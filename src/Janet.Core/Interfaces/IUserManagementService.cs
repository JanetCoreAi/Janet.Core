using Janet.Core.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janet.Core.Interfaces
{
    public class IUserManagementService
    {
        public UserProfile CurrentUser { get; set; }
    }
}
