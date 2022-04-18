using AbstractFoodDeliveryBusinessLogic.OfficePackage.HelperEnums;
using AbstractFoodDeliveryBusinessLogic.OfficePackage.HelperModels;

namespace AbstractFoodDeliveryBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> 
                { 
                    (info.Title, new WordTextProperties 
                    { 
                        Bold = true, Size = "24", 
                    })
                },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var dish in info.Dishes)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> 
                    {
                        (dish.DishName + ": ", new WordTextProperties 
                        { 
                            Size = "24",
                            Bold = true
                        }),
                        (dish.Price.ToString(), new WordTextProperties
                        {
                            Size = "24",
                        })
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }
        /// <summary>
        /// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateWord(WordInfo info);

        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        protected abstract void CreateParagraph(WordParagraph paragraph);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveWord(WordInfo info);
    }
}
