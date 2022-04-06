using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using AbstractFoodDeliveryFileImplement.Models;

namespace AbstractFoodDeliveryFileImplement.Implements
{
	public class WareHouseStorage : IWareHouseStorage
	{
		private readonly FileDataListSingleton source;

		public WareHouseStorage()
		{
			source = FileDataListSingleton.GetInstance();
		}

		public void Delete(WareHouseBindingModel model)
		{
			WareHouse element = source.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				source.WareHouses.Remove(element);
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}

		public WareHouseViewModel GetElement(WareHouseBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			var warehouse = source.WareHouses
			.FirstOrDefault(rec => rec.WareHouseName == model.WareHouseName || rec.Id
			== model.Id);
			return warehouse != null ? CreateModel(warehouse) : null;
		}

		public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return source.WareHouses
			.Where(rec => rec.WareHouseName.Contains(model.WareHouseName))
			.Select(CreateModel)
			.ToList();
		}

		public List<WareHouseViewModel> GetFullList()
		{
			return source.WareHouses
			.Select(CreateModel)
			.ToList();
		}

		public void Insert(WareHouseBindingModel model)
		{
			int maxId = source.WareHouses.Count > 0 ? source.WareHouses.Max(rec => rec.Id) : 0;
			var element = new WareHouse
			{
				Id = maxId + 1,
				WareHouseIngredients = new Dictionary<int, int>()
			};
			source.WareHouses.Add(CreateModel(model, element));
		}

		public void Update(WareHouseBindingModel model)
		{
			var element = source.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			CreateModel(model, element);
		}


		private static WareHouse CreateModel(WareHouseBindingModel model, WareHouse warehouse)
		{
			warehouse.WareHouseName = model.WareHouseName;
			warehouse.StorekeeperFIO = model.StorekeeperFIO;
			warehouse.DateCreate = model.DateCreate;
			// удаляем убранные
			foreach (var key in warehouse.WareHouseIngredients.Keys.ToList())
			{
				if (!model.WareHouseIngredients.ContainsKey(key))
				{
					warehouse.WareHouseIngredients.Remove(key);
				}
			}
			// обновляем существуюущие и добавляем новые
			foreach (var ingredient in model.WareHouseIngredients)
			{
				if (warehouse.WareHouseIngredients.ContainsKey(ingredient.Key))
				{
					warehouse.WareHouseIngredients[ingredient.Key] =
					model.WareHouseIngredients[ingredient.Key].Item2;
				}
				else
				{
					warehouse.WareHouseIngredients.Add(ingredient.Key,
					model.WareHouseIngredients[ingredient.Key].Item2);
				}
			}
			return warehouse;
		}
		private WareHouseViewModel CreateModel(WareHouse warehouse)
		{
			return new WareHouseViewModel
			{
				Id = warehouse.Id,
				WareHouseName = warehouse.WareHouseName,
				StorekeeperFIO = warehouse.StorekeeperFIO,
				DateCreate = warehouse.DateCreate,
				WareHouseIngredients = warehouse.WareHouseIngredients
				.ToDictionary(recPC => recPC.Key, recPC =>
				(source.Ingredients.FirstOrDefault(recC => recC.Id ==
				recPC.Key)?.IngredientName, recPC.Value))
			};
		}

        public bool TakeIngredientsInWork(int dishid, int count)
        {
			bool flag = true;
			var ingredients = source.Dishes.FirstOrDefault(rec => rec.Id == dishid).DishIngredients;
			
			foreach(var ingredient in ingredients)
            {
				if (flag)
				{
					var warehouses = source.WareHouses
					.Where(rec => rec.WareHouseIngredients
					.ContainsKey(ingredient.Key))
					.ToList();

					int countNeed = count * ingredient.Value;

					foreach (var warehouse in warehouses)
					{
						if (countNeed > 0)
						{
							countNeed -= warehouse.WareHouseIngredients[ingredient.Key];
						}
						else
							break;
					}

					if (countNeed > 0)
                    {
						flag = false;
						break;
                    }

					countNeed = count * ingredient.Value;
					foreach (var warehouse in warehouses)
					{
						if (countNeed > 0)
						{
							if (warehouse.WareHouseIngredients[ingredient.Key] > countNeed)
							{
								warehouse.WareHouseIngredients[ingredient.Key] = warehouse.WareHouseIngredients[ingredient.Key] - countNeed;
								countNeed = 0;
								if (warehouse.WareHouseIngredients[ingredient.Key] == 0)
									warehouse.WareHouseIngredients.Remove(ingredient.Key);
							}
							else
							{
								countNeed -= warehouse.WareHouseIngredients[ingredient.Key];
								warehouse.WareHouseIngredients.Remove(ingredient.Key);
							}
						}
						else
							break;
					}
					flag = true;
				}
			}
			return flag;
		}
    }
}
