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

namespace Client.WindowsForDeleteDocument
{
    /// <summary>
    /// Логика взаимодействия для DeleteDocumentWindow.xaml
    /// </summary>
    public partial class DeleteDocumentWindow : Window
    {
        private readonly HttpClient _httpClient;

        public DeleteDocumentWindow(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var documentId = DocumentIdTextBox.Text;

            if (string.IsNullOrWhiteSpace(documentId))
            {
                MessageBox.Show("ID документа обязателен для удаления.");
                return;
            }

            var response = await _httpClient.DeleteAsync($"http://localhost:5262/api/Master/{documentId}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Документ успешно удален.");
            }
            else
            {
                MessageBox.Show("Ошибка при удалении документа.");
            }

            this.Close();
        }
    }
}
