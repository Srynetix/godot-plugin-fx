using Godot;

namespace SxGD
{
    [Tool]
    public class Vignette : ColorRect
    {
        [Export]
        public float Ratio
        {
            get => ShaderExt.GetShaderParam<float>(this, "ratio");
            set => ShaderExt.SetShaderParam(this, "ratio", value);
        }

        public async void Fade(float duration)
        {
            var tween = new Tween();
            AddChild(tween);

            tween.InterpolateProperty(this, nameof(Ratio), Ratio, 1, duration);
            tween.Start();

            await ToSignal(tween, "tween_all_completed");

            tween.QueueFree();
        }

        public override void _ExitTree()
        {
            Ratio = 0;
        }
    }
}
