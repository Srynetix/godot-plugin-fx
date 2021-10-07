using Godot;

namespace SxGD
{
    [Tool]
    public class BetterBlur : Control
    {
        [Export]
        public float Strength
        {
            get => _Strength;
            set
            {
                _Strength = value;
                SetStrength(value);
            }
        }

        private ColorRect _Step1;
        private ColorRect _Step2;
        private BackBufferCopy _Copy;
        private float _Strength;

        public override void _Ready()
        {
            _Step1 = GetNode<ColorRect>("Step1");
            _Step2 = GetNode<ColorRect>("BackBufferCopy/Step2");
            _Copy = GetNode<BackBufferCopy>("BackBufferCopy");
            _Copy.Rect = new Rect2(RectPosition, RectSize);

            SetStrength(_Strength);
        }

        public override void _Process(float delta)
        {
            if (Engine.EditorHint)
            {
                SetStrength(_Strength);
            }
        }

        public void SetStrength(float value)
        {
            if (Engine.EditorHint)
            {
                if (_Step1 == null && _Step2 == null)
                {
                    _Ready();
                }
            }

            if (_Step1 != null && _Step2 != null)
            {
                ShaderExt.SetShaderParam(_Step1, "strength", value);
                ShaderExt.SetShaderParam(_Step2, "strength", value);
            }
        }
    }
}
