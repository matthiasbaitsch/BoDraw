using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace BoDraw;

public partial class MainWindow : Window
{
    private AnimationController? animation;
    private bool updatingSlider;

    public MainWindow()
    {
        this.InitializeComponent();
    }

    public void Animate(double duration, Action<double> frame)
    {
        this.animation = new AnimationController(this, duration, t =>
        {
            frame(t);
            this.Canvas.InvalidateVisual();
        });
        this.animation.TimeChanged += t =>
        {
            this.TimeLabel.Text = $"{t:F1} / {duration:F1}";
            this.updatingSlider = true;
            this.TimeSlider.Value = t;
            this.updatingSlider = false;
        };
        this.animation.PlayingChanged += playing =>
        {
            this.PlayPauseButton.Content = playing ? "⏸" : "⏵";
        };
        this.TimeSlider.Maximum = duration;
        this.AnimationControls.IsVisible = true;
    }

    private void OnPlayPause(object? sender, RoutedEventArgs e)
    {
        if (this.animation!.IsPlaying) { this.animation.Pause(); } else { this.animation.Play(); }
    }

    private void OnStop(object? sender, RoutedEventArgs e)
    {
        this.animation?.SeekTo(0);
    }

    private void OnSliderChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if (this.updatingSlider) { return; }
        this.animation?.SeekTo(e.NewValue);
    }
}
