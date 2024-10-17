
using System.Windows;
using System.Net.Http;
using System.Net.Http.Json;
using Client.Models; // Для использования метода GetFromJsonAsync


namespace Client.WindowsForShowDocument
{
    /// <summary>
    /// Логика взаимодействия для ShowDocumentWindow.xaml
    /// </summary>
    public partial class ShowDocumentWindow : Window
    {
        public ShowDocumentWindow()
        {
            InitializeComponent();
        }

        private void DocumentNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DocumentNumberTextBox.Text == "Введите номер документа")
            {
                DocumentNumberTextBox.Text = string.Empty;
            }
        }

        private void DocumentNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DocumentNumberTextBox.Text))
            {
                DocumentNumberTextBox.Text = "Введите номер документа";
            }
        }

        private async void ShowDocument_Click(object sender, RoutedEventArgs e)
        {
            var documentId = DocumentNumberTextBox.Text;

            if (documentId == "Введите номер документа" || string.IsNullOrWhiteSpace(documentId))
            {
                MessageBox.Show("Введите номер документа.");
                return;
            }
            // Отправка HTTP-запроса для получения информации о документе
            using (HttpClient client = new HttpClient())
            {
                //http://localhost:5000/api/Master/MasterByNumber?number=12345

                var response = await client.GetAsync($"http://localhost:5262/api/Master/MasterByNumber?number={documentId}");

                if (response.IsSuccessStatusCode)
                {
                    // Получаем документ из ответа
                    var document = await response.Content.ReadFromJsonAsync<Document>();

                    // Открываем новое окно с информацией о документе
                    var documentInfoWindow = new DocumentInfoWindow(document);
                    documentInfoWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Документ не найден.");
                }
            }
        }

    }
}
