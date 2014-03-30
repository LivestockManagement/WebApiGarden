using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiGarden.Business.Purchases.Entities;

namespace WebApiGarden.Web.Api.Services
{
    public class IdentityService : IIdentityService
    {
        public string CurrentUser
        {
            get
            {
#if DEBUG
                return "Username1";
#else
                return Thread.CurrentPrincipal.Identity.Name;
#endif
            }
        }
    }
}
