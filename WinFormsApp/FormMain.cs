using ClassLibrary;

namespace WinFormsApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            DataContext = new Logic();
            Logic logic = (Logic)DataContext;

            ListViewMain.View = View.Details;
            ListViewMain.Columns.Add("HP", -2);
            ListViewMain.Columns.Add("Name", -2);
            ListViewMain.Columns.Add("Color", -2);

            UpdateViewListMain();

            foreach (var item in logic.GetColorFlagNames())
            {
                ComboBoxColor.Items.Add(item);
            }

            ComboBoxColor.SelectedIndex = 0;
        }



        private FormGame formGame;



        /// <summary>
        /// ��������� ����� ������� � ListView.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">���. ���������� � ������� ��� �����������.</param>
        private void ButtonCreateShip_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            if (!string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text != "_No_Color_")
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = logic.CreateShip(TextBoxName.Text, ComboBoxColor.SelectedItem);

                TextBoxName.Text = "";
            }

            UpdateViewListMain();
        }



        /// <summary>
        /// ������� ��������� ������� �� ListView.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">���. ���������� � ������� ��� �����������.</param>
        private void ButtonDeleteShip_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                logic.DeleteShip(selectedItem.Tag);

                UpdateViewListMain();
            }
        }



        /// <summary>
        /// ������ �������� � ���� ���������� ������� � ListView.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">���. ���������� � ������� ��� �����������.</param>
        private void ButtonChangeShipStats_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                logic.ChangeShipAttributes(selectedItem.Tag, TextBoxName.Text, ComboBoxColor.SelectedItem.ToString());
            }

            UpdateViewListMain();
            TextBoxName.Text = "";
        }



        /// <summary>
        /// ��������� ����� ������� ����.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">���. ���������� � ������� ��� �����������.</param>
        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            if (logic.GetShipsList().Count > 1)
            {
                logic.RecoverHP();
                UpdateViewListMain();

                formGame = new FormGame((Logic)DataContext, ListViewMain);
                formGame.ShowDialog();
                UpdateViewListMain();
            }

            else
            {
                MessageBox.Show("�������� ������ �������� �������!!!!!!!!!!!!!!!!!!!!!");
            }
        }



        /// <summary>
        /// ������������� ����������� �������� � ListView.
        /// </summary>
        private void UpdateViewListMain()
        {
            Logic logic = (Logic)DataContext;

            ListViewMain.Items.Clear();

            foreach (var ship in logic.GetShipsList())
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = ship;

                listViewItem.SubItems[0].Text = logic.GetShip(ship).GetType().GetProperty("Hp").GetValue(ship).ToString();
                listViewItem.SubItems.Add(logic.GetShip(ship).GetType().GetProperty("Name").GetValue(ship).ToString());
                listViewItem.SubItems.Add(logic.GetShip(ship).GetType().GetProperty("FlagColor").GetValue(ship).ToString());

                listViewItem.ForeColor = logic.GetColorByFlagColor(ship);

                ListViewMain.Items.Add(listViewItem);
            }
        }



        /// <summary>
        /// ������� ���� � �����������.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">���. ���������� � ������� ��� �����������.</param>
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }
    }
}
