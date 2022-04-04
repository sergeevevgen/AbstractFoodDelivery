using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.BindingModels;

namespace AbstractFoodDeliveryView
{
    public partial class FormResupply : Form
    {
        private readonly IWareHouseLogic _logic;
        private readonly IIngredientLogic _ingredientLogic;

        public int Id
        {
            get { return Convert.ToInt32(comboBoxIngredient.SelectedValue); }
            set { comboBoxIngredient.SelectedValue = value; }
        }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        public FormResupply(IWareHouseLogic logic, IIngredientLogic ingredientLogic)
        {
            InitializeComponent();
            _logic = logic;
            _ingredientLogic = ingredientLogic;
        }

        private void FormResupply_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _logic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[4].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
                var list2 = _ingredientLogic.Read(null);
                if (list2 != null)
                {
                    comboBoxIngredient.DisplayMember = "IngredientName";
                    comboBoxIngredient.ValueMember = "Id";
                    comboBoxIngredient.DataSource = list2;
                    comboBoxIngredient.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        { 
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _logic.AddIngredient(new WareHouseBindingModel
                    {
                        Id = id
                    }, Id, Count);
                    LoadData();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
