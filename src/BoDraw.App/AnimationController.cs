using Avalonia.Controls;

namespace BoDraw;

internal class AnimationController
{
    private readonly double duration;
    private readonly TopLevel topLevel;
    private readonly Action<double> onRender;

    private bool isPlaying;
    private double currentTime;
    private TimeSpan? lastFrameTime;

    internal event Action<double>? TimeChanged;
    internal event Action<bool>? PlayingChanged;

    internal bool IsPlaying { get { return this.isPlaying; } }

    internal AnimationController(TopLevel topLevel, double duration, Action<double> onRender)
    {
        this.topLevel = topLevel;
        this.duration = duration;
        this.onRender = onRender;
        this.Render(0);
    }

    internal void Play()
    {
        if (this.currentTime >= this.duration) { this.currentTime = 0; }
        this.isPlaying = true;
        this.PlayingChanged?.Invoke(true);
        this.lastFrameTime = null;
        this.topLevel.RequestAnimationFrame(this.OnAnimationFrame);
    }

    internal void Pause()
    {
        if (!this.isPlaying) { return; }
        this.isPlaying = false;
        this.PlayingChanged?.Invoke(false);
    }

    internal void SeekTo(double t)
    {
        this.Pause();
        this.Render(t);
    }

    private void Render(double t)
    {
        this.currentTime = t;
        this.onRender(t);
        this.TimeChanged?.Invoke(t);
    }

    private void OnAnimationFrame(TimeSpan time)
    {
        if (!this.isPlaying) { return; }

        if (this.lastFrameTime.HasValue)
        {
            double dt = (time - this.lastFrameTime.Value).TotalSeconds;
            double next = this.currentTime + dt;
            if (next >= this.duration)
            {
                this.Render(this.duration);
                this.Pause();
                return;
            }
            this.Render(next);
        }

        this.lastFrameTime = time;
        this.topLevel.RequestAnimationFrame(this.OnAnimationFrame);
    }
}
