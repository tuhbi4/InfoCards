using InfoCards.PL.APIClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InfoCards.PL.WPF
{
    public partial class MainWindow : Window
    {
        private readonly ApiClient _apiClient;
        private readonly Base64ImageConverter _base64ImageConverter;

        public MainWindow()
        {
            _apiClient = new ApiClient();
            _base64ImageConverter = new Base64ImageConverter();
            Initialized += RenderInfoCardsFrame;
            InitializeComponent();
        }

        private static int GetInfoCardIdFromSender(object sender)
        {
            MenuItem menuItem = sender as MenuItem;
            int position = menuItem.Name.IndexOf("_");
            int.TryParse(menuItem.Name.Substring(position + 1), out int targetId);

            return targetId;
        }

        private ContextMenu CreateContextMenu(int id)
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem miChange = new MenuItem
            {
                Header = "Change",
                Name = "miChangeInfoCard_" + id.ToString(),
                Icon = new Image
                {
                    Source = new BitmapImage(new Uri("icons/pencil-tool.png", UriKind.Relative))
                }
            };
            miChange.Click += new RoutedEventHandler(miChange_Click);
            MenuItem miDelete = new MenuItem
            {
                Header = "Delete",
                Name = "miDeleteInfoCard_" + id.ToString(),
                Icon = new Image
                {
                    Source = new BitmapImage(new Uri("icons/trash-bin.png", UriKind.Relative))
                }
            };
            miDelete.Click += new RoutedEventHandler(miDelete_Click);
            contextMenu.Items.Add(miChange);
            contextMenu.Items.Add(miDelete);

            return contextMenu;
        }

        private async void RenderInfoCardsFrame(object sender, EventArgs e)
        {
            var result = await _apiClient.GetAllInfoCardsAsync(); //need catch offline server
            wrapFrame.Children.Clear();

            foreach (var card in result)
            {
                wrapFrame.Children.Add(new StackPanel()
                {
                    Name = "Panel_" + card.Id.ToString(),
                    Children =
                    {
                        new Label() { Content = card.Name, FontSize = 20, HorizontalAlignment = HorizontalAlignment.Center, Padding = new Thickness(10) },
                        new Image() { Source = _base64ImageConverter.Base64ToImage(card.ImageBase64), MaxWidth = 300 }
                    },
                    Margin = new Thickness(10),
                    ContextMenu = CreateContextMenu(card.Id)
                });
            }
        }

        private async void Uploading_Button_Click(object sender, RoutedEventArgs e)
        {
            ModalWindow modalWindow = new ModalWindow();
            modalWindow.ShowDialog();

            if (!ModalWindow.IsClosed)
            {
                if (string.IsNullOrEmpty(ModalWindow.InfoCardName))
                {
                    MessageBox.Show("You must enter the InfoCard name.");
                }
                else if (string.IsNullOrEmpty(ModalWindow.ImageFilePath))
                {
                    MessageBox.Show("You must specify the path to the image.");
                }
                else
                {
                    string cardName = ModalWindow.InfoCardName;
                    string base64 = _base64ImageConverter.ImageToBase64(ModalWindow.ImageFilePath);
                    int newId = (await _apiClient.GetAllInfoCardsAsync()).Count + 1;
                    await _apiClient.CreateInfoCardAsync(new InfoCardModel() { Id = newId, Name = cardName, ImageBase64 = base64 });
                    MessageBox.Show("InfoCard uploaded successfully!");
                    RenderInfoCardsFrame(sender, e);
                }
            }

            ModalWindow.IsClosed = false;
        }

        private void Refreshing_Button_Click(object sender, RoutedEventArgs e)
        {
            RenderInfoCardsFrame(sender, e);
            MessageBox.Show("Gallery updated successfully!");
        }

        private void Pencil_Tool_Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Trash_Bin_Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void miChange_Click(object sender, RoutedEventArgs e)
        {
            int targetId = GetInfoCardIdFromSender(sender);
            ModalWindow modalWindow = new ModalWindow();
            InfoCardModel targetInfoCard = await _apiClient.GetInfoCardByIdAsync(targetId);
            modalWindow.tbInfoCardName.Text = targetInfoCard.Name;
            modalWindow.ShowDialog();

            if (!ModalWindow.IsClosed)
            {
                if (string.IsNullOrEmpty(ModalWindow.InfoCardName))
                {
                    MessageBox.Show("You must enter the InfoCard name.");
                }
                else if (ModalWindow.ImageFilePath is null)
                {
                    MessageBox.Show("You must specify the path to the image.");
                }
                else
                {
                    string cardName = ModalWindow.InfoCardName;
                    string base64 = string.Empty;

                    if (ModalWindow.ImageFilePath == string.Empty)
                    {
                        base64 = targetInfoCard.ImageBase64;
                    }
                    else
                    {
                        base64 = _base64ImageConverter.ImageToBase64(ModalWindow.ImageFilePath);
                    }

                    await _apiClient.UpdateInfoCardByIdAsync(targetId, new InfoCardModel() { Id = targetId, Name = cardName, ImageBase64 = base64 });
                    MessageBox.Show("InfoCard uploaded successfully!");
                    RenderInfoCardsFrame(sender, e);
                }
            }

            ModalWindow.IsClosed = false;
        }

        private async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            int targetId = GetInfoCardIdFromSender(sender);

            if (MessageBox.Show("Delete InfoCard \"" + (await _apiClient.GetInfoCardByIdAsync(targetId)).Name + "\"?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _apiClient.DeleteInfoCardByIdAsync(targetId);
                MessageBox.Show("InfoCard deleted successfully!");
                RenderInfoCardsFrame(sender, e);
            }
        }
    }
}