using Avalonia.Controls;

namespace bodraw;

public partial class MainWindow : Window
{
    public MainWindow() { }

    public MainWindow(Drawing drawing)
    {
        this.InitializeComponent();
        this.Canvas.Drawing = drawing;
    }
}