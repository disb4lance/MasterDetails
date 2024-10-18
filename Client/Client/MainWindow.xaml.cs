
using Client.WindowsForAddDocuments;
using Client.WindowsForDeleteDocument;
using Client.WindowsForDeleteSpecification;
using Client.WindowsForShowDocument;
using Client.WindowsForShowSpecifications;
using Client.WindowsForUpdateDocument;
using Client.WindowsForUpdateSpecification;
using System.Net.Http;
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
            var addDocumentWindow = new AddDocumentWindow();
            addDocumentWindow.ShowDialog();
        }

        private void ShowDocument_Click(object sender, RoutedEventArgs e)
        {
            var documentInputWindow = new ShowDocumentWindow();
            documentInputWindow.Show();
            // Логика для показа документа по номеру
        }

        private void UpdateDocument_Click(object sender, RoutedEventArgs e)
        {
            var updateWindow = new UpdateDocumentWindow();
            updateWindow.ShowDialog();
        }

        private void DeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            var deleteDocumentWindow = new DeleteDocumentWindow(new HttpClient());
            deleteDocumentWindow.ShowDialog();
        }

        private void ShowSpecifications_Click(object sender, RoutedEventArgs e)
        {
            var showSpecificationsWindow = new ShowSpecificationsWindow(new HttpClient());
            showSpecificationsWindow.ShowDialog();
        }

        private void AddSpecification_Click(object sender, RoutedEventArgs e)
        {
            var showSpecificationsWindow = new ShowSpecificationsWindow(new HttpClient());
            showSpecificationsWindow.ShowDialog();
        }

        private void UpdateSpecification_Click(object sender, RoutedEventArgs e)
        {
            var updateWindow = new UpdateSpecificationWindow();
            updateWindow.ShowDialog();
        }

        private void DeleteSpecification_Click(object sender, RoutedEventArgs e)
        {
            var deleteWindow = new DeleteSpecificationWindow();
            deleteWindow.ShowDialog();
        }
    }
}