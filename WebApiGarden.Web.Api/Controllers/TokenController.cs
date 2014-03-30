using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Business.Purchases.Entities;
using WebApiGarden.Web.Api.Models;

namespace WebApiGarden.Web.Api.Controllers
{
    public class TokenController : BaseApiController
    {
        public TokenController(OrderRepository orderRepository)
            : base(orderRepository)
        {
        }

        public HttpResponseMessage Post([FromBody]TokenRequestModel model)
        {
            try
            {

                Developer developer = _OrderRepository.Developers.Where(u => u.AppId == model.ApiKey).FirstOrDefault();
                if (developer != null)
                {
                    var secret = developer.Secret;

                    // Simplistic implementation DO NOT USE
                    var key = Convert.FromBase64String(secret);
                    var provider = new System.Security.Cryptography.HMACSHA256(key);
                    // Compute Hash from API Key (NOT SECURE)
                    var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(developer.AppId));
                    var signature = Convert.ToBase64String(hash);

                    if (signature == model.Signature)
                    {
                        var rawTokenInfo = string.Concat(developer.AppId + DateTime.UtcNow.ToString("d"));
                        var rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
                        var token = provider.ComputeHash(rawTokenByte);
                        var authToken = new AuthToken()
                        {
                            Token = Convert.ToBase64String(token),
                            Expiration = DateTime.UtcNow.AddDays(7),
                            Developer = developer
                        };

                        // Save Token (VALIDATE?!?!?!)
                        _OrderRepository.AuthTokens.Add(authToken);

                        return Request.CreateResponse(HttpStatusCode.Created, _ModelFactory.Create(authToken));
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }


}
