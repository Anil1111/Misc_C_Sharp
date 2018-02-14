using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp.Basics
{
    public class GenericClassIssues
    {
        public GenericClassIssues()
        {
            var ipAccountStatus = new IPAccountStatus { hasPassword = false };
            var ipResult = new IPResponse<IPAccountStatus>
            {
                data = ipAccountStatus,
                errorMessage = "no password provided",
                success = false
            };

            Respone<AccountStatus> accountStatus = ipResult.MyConversion<IPAccountStatus, AccountStatus>();
        }
    }

    public static class Extensions
    {
        public static Respone<TDestination> MyConversion<T, TDestination>(this IPResponse<T> s)
            where T : class, IConvertableToGenericModel<TDestination>
            where TDestination : class
        {
            var result = new Respone<TDestination>();
            result.Result = s.data.ConvertToGenericModel();
            result.Success = s.success;
            result.Message = s.errorMessage;
            return result;
        }
    }

    public interface IConvertableToGenericModel<T>
    {
        T ConvertToGenericModel();
    }

    public class IPResponse<T>
    {
        public string errorMessage { get; set; }
        public bool success { get; set; }
        public T data { get; set; }
    }
    public class IPAccountStatus : IConvertableToGenericModel<AccountStatus>
    {
        public bool hasPassword { get; set; }

        public AccountStatus ConvertToGenericModel()
        {
            return new AccountStatus { HasPasswrod = hasPassword };
        }
    }

    public class Respone<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Result { get; set; }
    }
    public class AccountStatus
    {
        public bool HasPasswrod { get; set; }
    }

}
