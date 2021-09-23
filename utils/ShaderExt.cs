using Godot;

namespace FxPlugin
{
    public static class ShaderExt
    {
        public static T GetShaderParam<T>(CanvasItem canvasItem, string name)
        {
            return (T)((ShaderMaterial)canvasItem.Material).GetShaderParam(name);
        }

        public static void SetShaderParam(CanvasItem canvasItem, string name, object value)
        {
            ((ShaderMaterial)canvasItem.Material).SetShaderParam(name, value);
        }
    }
}
