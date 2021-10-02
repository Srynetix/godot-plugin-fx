using Godot;

public class FXCamera : Camera2D
{
    [Export]
    public int MaxShakeStrength = 2;
    [Export]
    public float ShakeRatio = 0;

    public override void _Process(float delta)
    {
        UpdateShake();
    }

    private void UpdateShake()
    {
        float coef = (float)GD.RandRange(-1.0f, 1.0f) * MaxShakeStrength * ShakeRatio;
        Offset = new Vector2(coef, coef);
    }
}
