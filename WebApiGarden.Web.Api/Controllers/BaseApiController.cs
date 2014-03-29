using System.Web.Http;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Web.Api.Models;
namespace WebApiGarden.Web.Api.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected OrderRepository _OrderRepository;
        private ModelFactory _modelFactory;
        protected ModelFactory _ModelFactory
        {
            get
            {
                if (_modelFactory == null)
                { 
                    _modelFactory = new ModelFactory(this.Request);
                }
                return _modelFactory;
            }

        }

        public BaseApiController(OrderRepository orderRepository)
        {
            _OrderRepository = orderRepository;
        }


    }
}