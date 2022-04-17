namespace AbstractFoodDeliveryView
{
    partial class FormWareHouse
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxIngredients = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelStoreKeeper = new System.Windows.Forms.Label();
            this.textBoxStoreKeeper = new System.Windows.Forms.TextBox();
            this.labelDateCreate = new System.Windows.Forms.Label();
            this.textBoxDateCreate = new System.Windows.Forms.TextBox();
            this.groupBoxIngredients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxIngredients
            // 
            this.groupBoxIngredients.Controls.Add(this.dataGridView);
            this.groupBoxIngredients.Location = new System.Drawing.Point(19, 169);
            this.groupBoxIngredients.Name = "groupBoxIngredients";
            this.groupBoxIngredients.Size = new System.Drawing.Size(425, 289);
            this.groupBoxIngredients.TabIndex = 11;
            this.groupBoxIngredients.TabStop = false;
            this.groupBoxIngredients.Text = "Ингредиенты";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView.Location = new System.Drawing.Point(6, 22);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(412, 261);
            this.dataGridView.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 5;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Ингредиент";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 269;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Количество";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(288, 464);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(369, 464);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(28, 20);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(62, 15);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "Название:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(120, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(271, 23);
            this.textBoxName.TabIndex = 15;
            // 
            // labelStoreKeeper
            // 
            this.labelStoreKeeper.AutoSize = true;
            this.labelStoreKeeper.Location = new System.Drawing.Point(28, 61);
            this.labelStoreKeeper.Name = "labelStoreKeeper";
            this.labelStoreKeeper.Size = new System.Drawing.Size(73, 15);
            this.labelStoreKeeper.TabIndex = 16;
            this.labelStoreKeeper.Text = "Кладовщик:";
            // 
            // textBoxStoreKeeper
            // 
            this.textBoxStoreKeeper.Location = new System.Drawing.Point(120, 58);
            this.textBoxStoreKeeper.Name = "textBoxStoreKeeper";
            this.textBoxStoreKeeper.Size = new System.Drawing.Size(271, 23);
            this.textBoxStoreKeeper.TabIndex = 17;
            // 
            // labelDateCreate
            // 
            this.labelDateCreate.AutoSize = true;
            this.labelDateCreate.Location = new System.Drawing.Point(28, 110);
            this.labelDateCreate.Name = "labelDateCreate";
            this.labelDateCreate.Size = new System.Drawing.Size(88, 15);
            this.labelDateCreate.TabIndex = 18;
            this.labelDateCreate.Text = "Дата создания:";
            // 
            // textBoxDateCreate
            // 
            this.textBoxDateCreate.Enabled = false;
            this.textBoxDateCreate.Location = new System.Drawing.Point(120, 110);
            this.textBoxDateCreate.Name = "textBoxDateCreate";
            this.textBoxDateCreate.Size = new System.Drawing.Size(271, 23);
            this.textBoxDateCreate.TabIndex = 19;
            // 
            // FormWareHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 499);
            this.Controls.Add(this.textBoxDateCreate);
            this.Controls.Add(this.labelDateCreate);
            this.Controls.Add(this.textBoxStoreKeeper);
            this.Controls.Add(this.labelStoreKeeper);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxIngredients);
            this.Name = "FormWareHouse";
            this.Text = "Склад";
            this.Load += new System.EventHandler(this.FormWareHouse_Load);
            this.groupBoxIngredients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBoxIngredients;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private Button buttonSave;
        private Button buttonCancel;
        private Label labelName;
        private TextBox textBoxName;
        private Label labelStoreKeeper;
        private TextBox textBoxStoreKeeper;
        private Label labelDateCreate;
        private TextBox textBoxDateCreate;
    }
}