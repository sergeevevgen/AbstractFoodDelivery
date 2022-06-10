using Microsoft.AspNetCore.Mvc;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic _logic;
        private readonly IMessageInfoLogic _messageLogic;
        private readonly int _messagesOnPage = 3;
        public ClientController(IClientLogic logic, IMessageInfoLogic messageInfoLogic)
        {
            _logic = logic;
            _messageLogic = messageInfoLogic;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password)
        {
            var list = _logic.Read(new ClientBindingModel
            {
                Email = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(ClientBindingModel model) =>
        _logic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(ClientBindingModel model) =>
        _logic.CreateOrUpdate(model);

        [HttpGet]
        public (List<MessageInfoViewModel>, bool) GetClientsMessages(int clientId, int page)
        {
            var list = _messageLogic.Read(new MessageInfoBindingModel
            {
                ClientId = clientId,
                ToSkip = (page - 1) * _messagesOnPage,
                ToTake = _messagesOnPage + 1
            }).ToList();
            var isNext = !(list.Count() <= _messagesOnPage);
            return (list.Take(_messagesOnPage).ToList(), isNext);
        }
    }
}
