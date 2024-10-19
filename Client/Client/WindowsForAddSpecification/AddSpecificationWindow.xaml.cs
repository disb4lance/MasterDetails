using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
namespace Client.WindowsForAddSpecification
{
    /// <summary>
    /// Логика взаимодействия для AddSpecificationWindow.xaml
    /// </summary>
    public partial class AddSpecificationWindow : Window
    {
        private readonly HttpClient _httpClient;

        public AddSpecificationWindow(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var documentId = DocumentIdTextBox.Text;
            var specificationName = SpecificationNameTextBox.Text;
            var specificationCost = SpecificationCostTextBox.Text;

            if (string.IsNullOrWhiteSpace(documentId) || string.IsNullOrWhiteSpace(specificationName) || string.IsNullOrWhiteSpace(specificationCost))
            {
                MessageBox.Show("Все поля обязательны для заполнения.");
                return;
            }

            var specificationDto = new
            {
                Name = specificationName,
                price = specificationCost
            };

            var jsonContent = JsonSerializer.Serialize(specificationDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"http://localhost:5262/api/masters/{documentId}/details", httpContent);


            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Спецификация успешно добавлена.");
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении спецификации.");
            }
        }
    }
}
