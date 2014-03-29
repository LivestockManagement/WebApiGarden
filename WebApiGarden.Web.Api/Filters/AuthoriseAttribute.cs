using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System;
using System.Text;
using WebMatrix.WebData;
using System.Security.Principal;
using System.Threading;
using WebApiGarden.Business.Purchases;
using System.Linq;

namespace WebApiGarden.Web.Api.Filters
{
    public class AuthoriseAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }

            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", System.StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    // Valid!
                    var rawCredentials = authHeader.Parameter;
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                    var split = credentials.Split(':');
                    var username = split[0];
                    var password = split[1];


                    // validate account - WebSecurity.Initialized.
                    OrderRepository repository = new OrderRepository();
                    var user = repository.Users.Find(x => x.Username == username && x.Password == password);

                    if (user != null)
                    {
                        var principal = new GenericPrincipal(new GenericIdentity(user.Username), null);
                        Thread.CurrentPrincipal = principal;
                        return;
                    }
                }
            }

            HandleUnAuthorised(actionContext);
        }

        void HandleUnAuthorised(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='NLIS' location='http://nlisgetauthenticated.com/info'");
        }
    }
}