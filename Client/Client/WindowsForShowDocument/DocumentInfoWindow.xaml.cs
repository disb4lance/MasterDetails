
using Client.Models;
using System.Windows;


namespace Client.WindowsForShowDocument
{
    /// <summary>
    /// Логика взаимодействия для DocumentInfoWindow.xaml
    /// </summary>
    public partial class DocumentInfoWindow : Window
    {
        public DocumentInfoWindow(Document document)
        {
            InitializeComponent();
            DataContext = document;
        }
    }
}
