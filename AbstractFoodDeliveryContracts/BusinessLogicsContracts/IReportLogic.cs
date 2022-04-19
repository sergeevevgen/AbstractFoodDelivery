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
        /// Получение списка складов с указанием, какие ингредиенты в них находятся
        /// </summary>
        /// <returns></returns>
        public List<ReportWareHouseIngredientViewModel> GetWareHouseIngredient();

        /// <summary>
        /// Получение списка складов, отсортированных по датам
        /// </summary>
        /// <returns></returns>
        public List<ReportOrdersByDateViewModel> GetOrdersByDate(ReportBindingModel model);

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

        /// <summary>
        /// Сохранение складов с указанием ингредиентов, которые в них находятся
        /// </summary>
        /// <param name="model"></param>
        void SaveWareHouseIngredientsToExcelFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение складов в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveWareHousesToWordFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение заказов в файл-Pdf, отсортированных по датам
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersByDateToPdfFile(ReportBindingModel model);
    }
}
