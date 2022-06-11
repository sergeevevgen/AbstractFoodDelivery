using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryListImplement.Models;

namespace AbstractFoodDeliveryListImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        private readonly DataListSingleton source;
        public ImplementerStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Delete(ImplementerBindingModel model)
        {
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == model.Id.Value)
                {
                    source.Implementers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var implementer in source.Implementers)
            {
                if (implementer.Id == model.Id || implementer.FIO ==
                    model.FIO)
                {
                    return CreateModel(implementer);
                }
            }
            return null;
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new List<ImplementerViewModel>();
            foreach (var implementer in source.Implementers)
            {
                if (implementer.FIO.Contains(model.FIO))
                {
                    result.Add(CreateModel(implementer));
                }
            }
            return result;
        }

        public List<ImplementerViewModel> GetFullList()
        {
            var result = new List<ImplementerViewModel>();
            foreach (var implementer in source.Implementers)
            {
                result.Add(CreateModel(implementer));
            }
            return result;
        }

        public void Insert(ImplementerBindingModel model)
        {
            var tempImplementer = new Implementer { Id = 1 };
            foreach (var implementer in source.Implementers)
            {
                if (implementer.Id >= tempImplementer.Id)
                {
                    tempImplementer.Id = implementer.Id + 1;
                }
            }
            source.Implementers.Add(CreateModel(model, tempImplementer));
        }

        public void Update(ImplementerBindingModel model)
        {
            Implementer tempImplementer = null;
            foreach (var implementer in source.Implementers)
            {
                if (implementer.Id == model.Id)
                {
                    tempImplementer = implementer;
                }
            }
            if (tempImplementer == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempImplementer);
        }

        private static Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            implementer.FIO = model.FIO;
            implementer.PauseTime = model.PauseTime;
            implementer.WorkingTime = model.WorkingTime;
            return implementer;
        }

        private static ImplementerViewModel CreateModel(Implementer implementer)
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
