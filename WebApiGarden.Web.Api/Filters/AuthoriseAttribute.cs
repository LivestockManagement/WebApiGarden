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
using System.Web;
using Microsoft.Practices.Unity;

namespace WebApiGarden.Web.Api.Filters
{
    // This attribute has two modes.
    // 1. Restrict access by both Basic Auth User Access and also Token Auth Client App access. 
    // 2. Only Token Auth Client App access.
    public class AuthoriseAttribute : AuthorizationFilterAttribute
    {
        [Dependency]
        public OrderRepository _OrderRepository { get; set; }
        private bool _PerUser;

        public AuthoriseAttribute(bool perUser = true)
        {
            _PerUser = perUser;
        }


        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            const string APIKEYNAME = "apikey";
            const string TOKENNAME = "token";

            var query = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query, Encoding.UTF8);

            if (!string.IsNullOrWhiteSpace(query[APIKEYNAME]) &&
              !string.IsNullOrWhiteSpace(query[TOKENNAME]))
            {

                var apikey = query[APIKEYNAME];
                var token = query[TOKENNAME];

                var authToken = _OrderRepository.GetAuthToken(token);

                if (authToken != null && authToken.Developer.AppId == apikey && authToken.Expiration > DateTime.UtcNow)
                {

                    // don't bother checking if Token Auth is all we need.
                    if (_PerUser)
                    {
                        // User based authorisation
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
                                var user = _OrderRepository.Users.Find(x => x.Username == username && x.Password == password);

                                if (user != null)
                                {
                                    var principal = new GenericPrincipal(new GenericIdentity(user.Username), null);
                                    Thread.CurrentPrincipal = principal;
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            HandleUnAuthorised(actionContext);
        }

        void HandleUnAuthorised(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            if (_PerUser) 
            { 
                actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='NLIS' location='http://nlisgetauthenticated.com/info'");
            }
        }
    }
}