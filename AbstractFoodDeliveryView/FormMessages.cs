using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.BindingModels;
using Unity;

namespace AbstractFoodDeliveryView
{
    public partial class FormMessages : Form
    {
        private readonly IMessageInfoLogic _logic;
        public FormMessages(IMessageInfoLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormMessages_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(_logic.Read(null), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
    }
}
