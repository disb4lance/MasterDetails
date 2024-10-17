
using Client.WindowsForShowDocument;
using System.Windows;


namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddDocument_Click(object sender, RoutedEventArgs e)
        {
            // Логика для добавления документа
        }

        private void ShowDocument_Click(object sender, RoutedEventArgs e)
        {
            var documentInputWindow = new ShowDocumentWindow();
            documentInputWindow.Show();
            // Логика для показа документа по номеру
        }

        private void UpdateDocument_Click(object sender, RoutedEventArgs e)
        {
            // Логика для обновления документа по номеру
        }

        private void DeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            // Логика для удаления документа по номеру
        }

        private void ShowSpecifications_Click(object sender, RoutedEventArgs e)
        {
            // Логика для показа спецификаций по номеру документа
        }

        private void AddSpecification_Click(object sender, RoutedEventArgs e)
        {
            // Логика для добавления спецификации к документу
        }

        private void UpdateSpecification_Click(object sender, RoutedEventArgs e)
        {
            // Логика для обновления спецификации по номеру документа и ID спецификации
        }

        private void DeleteSpecification_Click(object sender, RoutedEventArgs e)
        {
            // Логика для удаления спецификации по номеру документа и ID спецификации
        }
    }
}