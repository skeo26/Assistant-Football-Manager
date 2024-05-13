using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public interface IUserProvider
    {
        Task<List<User>> LoadUsersAsync(string path);
    }
}
