using System.Threading.Tasks;
using Godot;

public class FXCamera : Camera2D
{
    [Export]
    public int MaxShakeStrength = 2;
    [Export]
    public float ShakeRatio = 0;

    private Tween Tween;

    public override void _Ready()
    {
        Tween = GetNode<Tween>("Tween");
    }

    public override void _Process(float delta)
    {
        UpdateShake();
    }

    private void UpdateShake()
    {
        float coef = (float)GD.RandRange(-1.0f, 1.0f) * MaxShakeStrength * ShakeRatio;
        Offset = new Vector2(coef, coef);
    }

    async public Task TweenToPosition(Vector2 position, float speed = 0.5f, float zoom = 1) {
        Tween.StopAll();
        Tween.InterpolateProperty(this, "global_position", GlobalPosition, position, speed, Tween.TransitionType.Quad, Tween.EaseType.InOut);
        Tween.InterpolateProperty(this, "zoom", Zoom, Vector2.One * zoom, speed, Tween.TransitionType.Quad, Tween.EaseType.InOut);
        Tween.Start();

        await ToSignal(Tween, "tween_all_completed");
    }
}
