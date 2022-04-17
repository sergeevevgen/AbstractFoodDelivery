using AbstractFoodDeliveryBusinessLogic.OfficePackage;
using AbstractFoodDeliveryBusinessLogic.OfficePackage.HelperModels;
using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryContracts.ViewModels;


namespace AbstractFoodDeliveryBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IIngredientStorage _ingredientStorage;
        private readonly IDishStorage _dishStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(IDishStorage dishStorage, IIngredientStorage
        ingredientStorage, IOrderStorage orderStorage,
        AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord,
        AbstractSaveToPdf saveToPdf)
        {
            _dishStorage = dishStorage;
            _ingredientStorage = ingredientStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        /// <summary>
        /// Получение списка блюд с указанием, какие ингредиенты в них используются
        /// </summary>
        /// <returns></returns>
        public List<ReportDishIngredientViewModel> GetDishIngredient()
        {
            var dishes = _dishStorage.GetFullList();
            var list = new List<ReportDishIngredientViewModel>();

            foreach (var dish in dishes)
            {
                var record = new ReportDishIngredientViewModel
                {
                    DishName = dish.DishName,
                    Ingredients = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var ingredient in dish.DishIngredients)
                {
                    if (dish.DishIngredients.ContainsKey(ingredient.Key))
                    {
                        record.Ingredients.Add(new Tuple<string, int>(ingredient.Value.Item1,
                        ingredient.Value.Item2));
                        record.TotalCount += ingredient.Value.Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                DishName = x.DishName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }

        /// <summary>
        /// Сохранение блюд в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveDishesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список блюд",
                Dishes = _dishStorage.GetFullList()
            });
        }

        /// <summary>
        /// Сохранение блюд с указанием ингредиентов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveDishIngredientsToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список блюд",
                DishIngredients = GetDishIngredient()
            });
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
