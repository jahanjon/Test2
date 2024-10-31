using Application.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure.UserAccessors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var x = _httpContextAccessor.HttpContext.User.FindFirstValue("user_id");
            if (Guid.TryParse(x, out Guid guid))
            {
                return guid; // تبدیل موفقیت‌آمیز
            }
            else
            {
                throw new InvalidOperationException("خطا در تبدیل رشته به GUID."); // تبدیل ناموفق
            }
        }

        public string GetUserNationalNumber()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue("nationalNumber");
        }
    }
}
