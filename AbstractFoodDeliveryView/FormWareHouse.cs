using AbstractFoodDeliveryContracts.BindingModels;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.ViewModels;
using Unity;

namespace AbstractFoodDeliveryView
{
    public partial class FormWareHouse : Form
    {
        public int Id { set { id = value; } }

        public bool IsShow { set { _isShow = value; } }

        private bool _isShow = false;

        private readonly IWareHouseLogic _logic;

        private int? id;

        private Dictionary<int, (string, int)>? wareHouseIngredients;

        private DateTime warehouseDate;

        public FormWareHouse(IWareHouseLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormWareHouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WareHouseViewModel view = _logic.Read(new WareHouseBindingModel
                    { 
                        Id = id.Value 
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.WareHouseName;
                        textBoxStoreKeeper.Text = view.StorekeeperFIO;
                        wareHouseIngredients = view.WareHouseIngredients;
                        textBoxDateCreate.Text = view.DateCreate.ToShortDateString();
                        if(_isShow == true)
                        {
                            textBoxName.Enabled = false;
                            textBoxStoreKeeper.Enabled = false;
                            textBoxDateCreate.Enabled = false;
                        }
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                wareHouseIngredients = new Dictionary<int, (string, int)>();
                warehouseDate = DateTime.Now;
                textBoxDateCreate.Visible = false;
                labelDateCreate.Visible = false;
            }
        }

        private void LoadData()
        {
            try
            {
                if (wareHouseIngredients != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in wareHouseIngredients)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1,
                        pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxStoreKeeper.Text))
            {
                MessageBox.Show("Введите кладовщика", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new WareHouseBindingModel
                {
                    Id = id,
                    WareHouseName = textBoxName.Text,
                    StorekeeperFIO = textBoxStoreKeeper.Text,
                    WareHouseIngredients = wareHouseIngredients,
                    DateCreate = warehouseDate
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
