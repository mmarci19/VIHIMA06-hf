using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffStore.Bll.Interfaces
{
    public interface IUserService
    {
        string GetCurrentUserId();
        string GetCurrentUserName();
        string GetCurrentUserRole();
    }
}
