using BoDraw;
using Avalonia.Controls;

namespace BoDraw.Demo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();

        Polyline l1 = new Polyline();
        l1.Thickness = 0.75;
        l1.Color = Colors.Tomato;
        l1.AddPoint(0, 0);
        l1.AddPoint(10, 10);
        l1.AddPoint(20, 0);
        l1.AddPoint(30, 10);
        l1.AddPoint(40, 0);

        Polyline l2 = new Polyline();
        l2.Color = Colors.HotPink;
        l2.AddPoint(0, -2.5);
        l2.AddPoint(40, -2.5);

        Polyline l3 = new Polyline();
        l3.Color = Colors.BlueViolet;
        l3.AddPoint(0, 12.5);
        l3.AddPoint(40, 12.5);

        this.BoDrawCanvas.Add(l1, l2, l3);
    }
}