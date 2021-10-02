using Godot;

namespace SxGD
{
    public static class ShaderExt
    {
        public static T GetShaderParam<T>(CanvasItem canvasItem, string name) where T : new()
        {
            if (canvasItem != null && canvasItem.Material != null)
            {
                return (T)((ShaderMaterial)canvasItem.Material).GetShaderParam(name);
            }

            return new T();
        }

        public static void SetShaderParam(CanvasItem canvasItem, string name, object value)
        {
            if (canvasItem != null && canvasItem.Material != null)
            {
                ((ShaderMaterial)canvasItem.Material).SetShaderParam(name, value);
            }
        }
    }
}
