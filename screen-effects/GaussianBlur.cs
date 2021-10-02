using Godot;

namespace SxGD
{
    [Tool]
    public class GaussianBlur : ColorRect
    {
        [Export]
        public float Strength
        {
            get => ShaderExt.GetShaderParam<float>(this, "strength");
            set => ShaderExt.SetShaderParam(this, "strength", value);
        }
    }
}
