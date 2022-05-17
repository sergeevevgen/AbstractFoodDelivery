using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;

namespace AbstractFoodDeliveryContracts.BusinessLogicsContracts
{
    public interface IBackUpLogic
    {
        void CreateBackUp(BackUpSaveBindingModel model);
    }
}
