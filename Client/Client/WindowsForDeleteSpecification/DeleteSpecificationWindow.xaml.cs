using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.WindowsForDeleteSpecification
{
    /// <summary>
    /// Логика взаимодействия для DeleteSpecificationWindow.xaml
    /// </summary>
    public partial class DeleteSpecificationWindow : Window
    {
        public DeleteSpecificationWindow()
        {
            InitializeComponent();
        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var documentId = DocumentIdTextBox.Text;
            var specificationId = SpecificationIdTextBox.Text;

            // Проверка на валидность данных
            if (string.IsNullOrWhiteSpace(documentId) || string.IsNullOrWhiteSpace(specificationId))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Отправка HTTP DELETE запроса
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"http://localhost:5262/api/masters/{documentId}/details/{specificationId}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Спецификация успешно удалена.");
                    this.Close(); // Закрываем окно после успешного удаления
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении спецификации: " + response.ReasonPhrase);
                }
            }
        }
    }
}
