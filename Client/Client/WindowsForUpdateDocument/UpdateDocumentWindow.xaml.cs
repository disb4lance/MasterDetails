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

namespace Client.WindowsForUpdateDocument
{
    /// <summary>
    /// Логика взаимодействия для UpdateDocumentWindow.xaml
    /// </summary>
    public partial class UpdateDocumentWindow : Window
    {
        private readonly HttpClient _httpClient;
        public UpdateDocumentWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }


        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var documentId = DocumentIdTextBox.Text;
            var documentNumber = DocumentNumberTextBox.Text;
            var documentNote = DocumentNoteTextBox.Text;

            if (string.IsNullOrWhiteSpace(documentId) || string.IsNullOrWhiteSpace(documentNumber))
            {
                MessageBox.Show("ID и номер документа обязательны.");
                return;
            }

            var updateModel = new
            {
                Number = documentNumber,
                Note = documentNote
            };

            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5262/api/Master/{documentId}", updateModel);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Документ успешно обновлен.");
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении документа.");
            }

            this.Close();
        }
    }
}
