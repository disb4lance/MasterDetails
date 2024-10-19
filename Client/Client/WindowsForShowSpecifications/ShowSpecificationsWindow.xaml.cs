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
using System.Text.Json;
using Client.Models.TrancferObjects;

namespace Client.WindowsForShowSpecifications
{
    /// <summary>
    /// Логика взаимодействия для ShowSpecificationsWindow.xaml
    /// </summary>
    public partial class ShowSpecificationsWindow : Window
    {
        private readonly HttpClient _httpClient;

        public ShowSpecificationsWindow(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            var documentId = DocumentIdTextBox.Text;

            if (string.IsNullOrWhiteSpace(documentId))
            {
                MessageBox.Show("ID документа обязателен для получения спецификаций.");
                return;
            }

            var response = await _httpClient.GetAsync($"http://localhost:5262/api/Masters/{documentId}/details");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var specifications = JsonSerializer.Deserialize<List<SpecificationDto>>(content);
                SpecificationsListBox.ItemsSource = specifications;
            }
            else
            {
                MessageBox.Show("Ошибка при получении спецификаций.");
            }
        }
    }
}
