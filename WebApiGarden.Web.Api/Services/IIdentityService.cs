using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiGarden.Business.Purchases.Entities;

namespace WebApiGarden.Web.Api.Services
{
    public interface IIdentityService
    {
        string CurrentUser { get; }
    }
}
