using Godot;

namespace FxPlugin
{
    [Tool]
    public class MotionBlur : ColorRect
    {
        [Export]
        public float Strength
        {
            get => ShaderExt.GetShaderParam<float>(this, "strength");
            set => ShaderExt.SetShaderParam(this, "strength", value);
        }

        [Export]
        public float AngleDegrees
        {
            get => ShaderExt.GetShaderParam<float>(this, "angle_degrees");
            set => ShaderExt.SetShaderParam(this, "angle_degrees", value);
        }
    }
}
