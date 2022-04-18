using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.ViewModels;

namespace AbstractFoodDeliveryContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        /// <summary>
        /// Получение списка блюд с указанием, какие ингредиенты в них используются
        /// </summary>
        /// <returns></returns>
        List<ReportDishIngredientViewModel> GetDishIngredient();
        
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        
        /// <summary>
        /// Сохранение блюд в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveDishesToWordFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение блюд с указанием ингредиентов, которые в них используются, в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SaveDishIngredientsToExcelFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        void SaveOrdersToPdfFile(ReportBindingModel model);
    }
}
