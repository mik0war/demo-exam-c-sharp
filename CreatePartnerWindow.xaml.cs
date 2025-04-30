using System.Windows;

namespace demo_exam
{
    public partial class CreatePartnerWindow : Window
    {
        public Partner NewPartner { get; private set; }
        private readonly int _editingId = 0;

        public CreatePartnerWindow(List<string> partnerTypes)
        {
            InitializeComponent();
            TypeComboBox.ItemsSource = partnerTypes;
        }

        public CreatePartnerWindow(List<string> partnerTypes, Partner partner)
        {
            InitializeComponent();

            Title = "Редактирование партнёра";
            NameTextBox.Text = partner.Name;
            AddressTextBox.Text = partner.Address;
            InnTextBox.Text = partner.Inn.ToString();
            LastNameTextBox.Text = partner.DirectorLastName;
            FirstNameTextBox.Text = partner.DirectorFirstName;
            SurnameTextBox.Text = partner.DirectorSurname;
            AddressTextBox.Text = partner.Address;
            PhoneTextBox.Text = partner.Phone;
            RatingTextBox.Text = partner.Rating.ToString();


            TypeComboBox.ItemsSource = partnerTypes;
            TypeComboBox.SelectedItem = partner.Type;

            _editingId = partner.Id;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Валидация обязательных полей
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Наименование партнёра обязательно для заполнения");
                return;
            }

            if (TypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать тип партнёра");
                return;
            }

            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                MessageBox.Show("ФИО директора обязательно для заполнения");
                return;
            }

            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Телефон обязателен для заполнения");
                return;
            }

            // Создание нового партнёра
            NewPartner = new Partner
            {
                Id = _editingId,
                Name = NameTextBox.Text,
                Type = TypeComboBox.SelectedItem.ToString(),
                DirectorLastName = LastNameTextBox.Text,
                DirectorFirstName = FirstNameTextBox.Text,
                DirectorSurname = SurnameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                Address = AddressTextBox.Text,
                Inn = long.TryParse(InnTextBox.Text, out var inn) ? inn : 0,
                Rating = int.TryParse(RatingTextBox.Text, out var rating) ? rating : 0
            };

            DialogResult = true;
            Close();
        }
    }
}
