using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo_exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PartnerRepository _repository;
        public MainWindow()
        {
            InitializeComponent();
            _repository = new PartnerRepository();

            DataContext = this;
            LoadPartners();
        }

        private void LoadPartners()
        {
            try
            {
                var partners = _repository.GetPartners();

                foreach (var partner in partners)
                {
                    partner.CalculateDiscount(_repository);
                }

                PartnersList.ItemsSource = partners;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void PartnerItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Partner selectedPartner)
            {
                var types = _repository.GetPartnerTypes();
                var editWindow = new CreatePartnerWindow(types, selectedPartner);

                if (editWindow.ShowDialog() == true)
                {
                    _repository.UpdatePartner(editWindow.NewPartner, selectedPartner);
                    LoadPartners();
                }
            }
        }

        private void CreatePartner_Click(object sender, RoutedEventArgs e)
        {
            // Получаем типы партнеров из БД
            var types = _repository.GetPartnerTypes();

            var createWindow = new CreatePartnerWindow(types);
            if (createWindow.ShowDialog() == true && createWindow.NewPartner != null)
            {
                // Сохраняем нового партнера
                _repository.CreatePartner(createWindow.NewPartner);

                // Обновляем список
                LoadPartners();
            }
        }
    }
}