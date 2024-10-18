using Client.Models.TrancferObjects;
using System.Net.Http.Json;

using System.Net.Http;

using System.Windows;

using System.Windows.Media;


namespace Client.WindowsForAddDocuments
{
    /// <summary>
    /// Логика взаимодействия для AddDocumentWindow.xaml
    /// </summary>
    public partial class AddDocumentWindow : Window
    {
        private readonly HttpClient _httpClient;
        public AddDocumentWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }
        // Событие для удаления заполнителя (placeholder) при фокусе
        private void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            if (DocumentNumberTextBox.Text == "Введите номер документа")
            {
                DocumentNumberTextBox.Text = "";
                DocumentNumberTextBox.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholder(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DocumentNumberTextBox.Text))
            {
                DocumentNumberTextBox.Text = "Введите номер документа";
                DocumentNumberTextBox.Foreground = Brushes.Gray;
            }
        }

        private void RemovePlaceholderNote(object sender, RoutedEventArgs e)
        {
            if (NoteTextBox.Text == "Введите примечание")
            {
                NoteTextBox.Text = "";
                NoteTextBox.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholderNote(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NoteTextBox.Text))
            {
                NoteTextBox.Text = "Введите примечание";
                NoteTextBox.Foreground = Brushes.Gray;
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var documentNumber = DocumentNumberTextBox.Text;
            var note = NoteTextBox.Text;

            if (documentNumber == "Введите номер документа" || string.IsNullOrWhiteSpace(documentNumber))
            {
                MessageBox.Show("Введите номер документа.");
                return;
            }

            // Отправляем POST запрос на создание документа
            try
            {
                var document = new MasterForCreatingDto
                {
                    number = documentNumber,
                    note = note
                };

                var response = await _httpClient.PostAsJsonAsync("http://localhost:5262/api/Master", document);

                if (response.IsSuccessStatusCode)
                {
                    var createdDocument = await response.Content.ReadFromJsonAsync<MasterDto>();
                    ResultTextBlock.Text = $"Документ создан: id: {createdDocument.Id} Номер: {createdDocument.Number}, Дата: {createdDocument.Date}, Цена: {createdDocument.SumPrices}, Примечание: {createdDocument.note}";
                }
                else
                {
                    ResultTextBlock.Text = "Ошибка при создании документа.";
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
