using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryFileImplement.Models;

namespace AbstractFoodDeliveryFileImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        private readonly FileDataListSingleton source;

        public ImplementerStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void Delete(ImplementerBindingModel model)
        {
            Implementer implementer = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (implementer != null)
            {
                source.Implementers.Remove(implementer);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var implementer = source.Implementers
            .FirstOrDefault(rec => rec.FIO == model.FIO ||
            rec.Id == model.Id);
            return implementer != null ? CreateModel(implementer) : null;
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Implementers
            .Where(rec => rec.FIO.Contains(model.FIO))
            .Select(CreateModel)
            .ToList();
        }

        public List<ImplementerViewModel> GetFullList()
        {
            return source.Implementers
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(ImplementerBindingModel model)
        {
            var element = new Implementer { Id = source.Implementers.Count > 0 ? source.Implementers.Max(rec => rec.Id) + 1 : 0 };
            source.Implementers.Add(CreateModel(model, element));
        }

        public void Update(ImplementerBindingModel model)
        {
            var element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        private static Implementer CreateModel(ImplementerBindingModel model, Implementer
        implementer)
        {
            implementer.FIO = model.FIO;
            implementer.PauseTime = model.PauseTime;
            implementer.WorkingTime = model.WorkingTime;
            return implementer;
        }
        private ImplementerViewModel CreateModel(Implementer implementer)
        {
            return new ImplementerViewModel
            {
                Id = implementer.Id,
                FIO = implementer.FIO,
                PauseTime = implementer.PauseTime,
                WorkingTime = implementer.WorkingTime
            };
        }
    }
}
