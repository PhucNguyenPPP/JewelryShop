using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DTO;
namespace BLL.Interfaces
{
    public interface IAuthService
    {
        LoginResponse CheckLogin(string username, string password);
        byte[] HashPassword(string password);
        bool CompareByteArrays(byte[] array1, byte[] array2);
	}
}
