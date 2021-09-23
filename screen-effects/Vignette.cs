using Godot;

namespace FxPlugin
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
    }
}
