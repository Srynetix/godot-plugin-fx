using Godot;

namespace FxPlugin
{
    [Tool]
    public class Shockwave : ColorRect
    {
        [Export]
        public float Force
        {
            get => ShaderExt.GetShaderParam<float>(this, "force");
            set => ShaderExt.SetShaderParam(this, "force", value);
        }

        [Export]
        public float Thickness
        {
            get => ShaderExt.GetShaderParam<float>(this, "thickness");
            set => ShaderExt.SetShaderParam(this, "thickness", value);
        }

        [Export]
        public Vector2 Center
        {
            get => ShaderExt.GetShaderParam<Vector2>(this, "center");
            set => ShaderExt.SetShaderParam(this, "center", value);
        }

        private Tween _Tween;

        public override void _Ready()
        {
            if (Engine.EditorHint)
            {
                return;
            }

            _Tween = GetNode<Tween>("Tween");
        }

        public void Start(Vector2 position)
        {
            Center = position;
            _Tween.StopAll();

            _Tween.InterpolateProperty(Material, "shader_param/size", 0.1f, 1.25f, 2);
            _Tween.InterpolateProperty(Material, "shader_param/force", 0.0f, 0.25f, 2);
            _Tween.InterpolateProperty(Material, "shader_param/thickness", 0.0f, 0.1f, 2);
            _Tween.Start();
        }

        public bool IsRunning()
        {
            return _Tween.IsActive();
        }

        public override void _ExitTree()
        {
            _Tween.StopAll();
            Thickness = 0;
            Force = 0;
        }
    }
}
