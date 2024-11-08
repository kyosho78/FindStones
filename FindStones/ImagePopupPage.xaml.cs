namespace FindStones;


public partial class ImagePopupPage : ContentPage
{
    public ImagePopupPage(string imageUrl)
    {
        InitializeComponent();
        FullScreenImage.Source = imageUrl;
    }
}