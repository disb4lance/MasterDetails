using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;

namespace Client.WindowsForUpdateSpecification
{
    /// <summary>
    /// Логика взаимодействия для UpdateSpecificationWindow.xaml
    /// </summary>
    public partial class UpdateSpecificationWindow : Window
    {
        public UpdateSpecificationWindow()
        {
            InitializeComponent();
        }
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var documentId = DocumentIdTextBox.Text;
            var specificationId = SpecificationIdTextBox.Text;
            var newSpecificationName = NewSpecificationNameTextBox.Text;
            var newSpecificationCost = NewSpecificationCostTextBox.Text;

            // Проверка на валидность данных
            if (string.IsNullOrWhiteSpace(documentId) ||
                string.IsNullOrWhiteSpace(specificationId) ||
                string.IsNullOrWhiteSpace(newSpecificationName) ||
                string.IsNullOrWhiteSpace(newSpecificationCost))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Создаем объект спецификации для обновления
            var updatedSpecification = new
            {

                Name = newSpecificationName,
                Cost = decimal.Parse(newSpecificationCost) // Предполагается, что стоимость - это decimal
            };

            // Отправка HTTP PUT запроса
            var jsonContent = new StringContent(JsonSerializer.Serialize(updatedSpecification), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsync($"http://localhost:5262/api/masters/{documentId}/details/{specificationId}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Спецификация успешно обновлена.");
                    this.Close(); // Закрываем окно после успешного обновления
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении спецификации: " + response.ReasonPhrase);
                }
            }
        }

    }
}
