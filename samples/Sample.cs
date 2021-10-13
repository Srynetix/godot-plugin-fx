using Godot;
using Godot.Collections;
using SxGD;

public class Sample : Control
{
    private Control _Background;
    private OptionButton _EffectSelection;
    private VBoxContainer _Params;
    private Vignette _Vignette;
    private Shockwave _Shockwave;
    private MotionBlur _MotionBlur;
    private GaussianBlur _GaussianBlur;
    private BetterBlur _BetterBlur;

    private Array<Sprite> _Sprites = new Array<Sprite>();
    private bool _Touched;
    private Vector2 _LastTouchedPosition;

    public override void _Ready()
    {
        var texture = GD.Load<Texture>("res://addons/fx/assets/icon.png");
        var vpSize = GetViewportRect().Size;

        _Background = GetNode<Control>("Background");
        _EffectSelection = GetNode<OptionButton>("UI/Margin/Margin/HBox/EffectType/Value");
        _Params = GetNode<VBoxContainer>("UI/Margin/Margin/HBox/Params");
        _Vignette = GetNode<Vignette>("Effects/Vignette");
        _Shockwave = GetNode<Shockwave>("Effects/Shockwave");
        _MotionBlur = GetNode<MotionBlur>("Effects/MotionBlur");
        _GaussianBlur = GetNode<GaussianBlur>("Effects/GaussianBlur");
        _BetterBlur = GetNode<BetterBlur>("Effects/BetterBlur");

        // Spawn some sprites
        var spriteCount = 50;
        for (int i = 0; i < spriteCount; ++i)
        {
            var sprite = new Sprite()
            {
                Texture = texture,
                Scale = new Vector2((float)GD.RandRange(0.5, 2), (float)GD.RandRange(0.5, 2)),
                Position = new Vector2((float)GD.RandRange(0, vpSize.x), (float)GD.RandRange(0, vpSize.y)),
                Modulate = Color.Color8((byte)GD.RandRange(0, 255), (byte)GD.RandRange(0, 255), (byte)GD.RandRange(0, 255)),
                RotationDegrees = (float)GD.RandRange(0, 360)
            };

            _Sprites.Add(sprite);
            _Background.AddChild(sprite);
        }

        _EffectSelection.AddItem("Vignette");
        _EffectSelection.AddItem("Shockwave");
        _EffectSelection.AddItem("MotionBlur");
        _EffectSelection.AddItem("GaussianBlur");
        _EffectSelection.AddItem("BetterBlur");

        _EffectSelection.Connect("item_selected", this, nameof(EffectSelected));

        // Initial selection
        EffectSelected(0);
    }

    private void EffectSelected(int index)
    {
        var effect = _EffectSelection.GetItemText(index);
        BuildParams(effect);
    }

    private void PRVisible(Control control)
    {
        var visibleHbox = new HBoxContainer()
        {
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill
        };
        var visibleLabel = new Label()
        {
            Text = "Visible",
            RectMinSize = new Vector2(40, 0)
        };
        var visibleCheckbox = new CheckBox()
        {
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
            Pressed = control.Visible
        };
        visibleCheckbox.Connect("toggled", this, nameof(EffectVisibility), new Array() { control });
        visibleHbox.AddChild(visibleLabel);
        visibleHbox.AddChild(visibleCheckbox);
        _Params.AddChild(visibleHbox);
    }

    private void PRFloat(Control control, string name, string paramName = "", float step = 0.01f, float minSizeX = 50)
    {
        paramName = name;
        var current = (float)control.Get(name);
        var hbox = new HBoxContainer()
        {
            Name = name,
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill
        };
        var labelObj = new Label()
        {
            Text = paramName.Capitalize(),
            RectMinSize = new Vector2(minSizeX, 0),
        };
        var input = new SpinBox()
        {
            Name = "Value",
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
            Step = step,
            Value = current
        };
        input.Connect("value_changed", this, nameof(ValueChanged), new Array() { control, paramName });
        hbox.AddChild(labelObj);
        hbox.AddChild(input);
        _Params.AddChild(hbox);
    }

    private void PRVector2(Control control, string name, float step = 0.01f, float minSizeX = 50)
    {
        var current = (Vector2)control.Get(name);
        var hbox = new HBoxContainer()
        {
            Name = name,
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill
        };
        var labelObj = new Label()
        {
            Name = "Label",
            Text = name,
            RectMinSize = new Vector2(minSizeX, 0),
        };
        var vbox = new VBoxContainer()
        {
            Name = "VBox",
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill
        };
        var inputX = new SpinBox()
        {
            Name = "X",
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
            MaxValue = 9999,
            Step = step,
            Value = current.x
        };
        var inputY = new SpinBox()
        {
            Name = "Y",
            MaxValue = 9999,
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
            Step = step,
            Value = current.x
        };
        inputX.Connect("value_changed", this, nameof(ValueChangedVector2), new Array() { "x", control, name });
        inputY.Connect("value_changed", this, nameof(ValueChangedVector2), new Array() { "y", control, name });
        vbox.AddChild(inputX);
        vbox.AddChild(inputY);

        hbox.AddChild(labelObj);
        hbox.AddChild(vbox);
        _Params.AddChild(hbox);
    }

    private void OnTouchPositionUpdate()
    {
        var selectedIdx = _EffectSelection.Selected;
        var effectName = _EffectSelection.GetItemText(selectedIdx);

        if (effectName == "Shockwave")
        {
            var vpSize = GetViewportRect().Size;

            // Get vector2
            var x = _Params.GetNode<SpinBox>("Center/VBox/X");
            var y = _Params.GetNode<SpinBox>("Center/VBox/Y");
            x.Value = _LastTouchedPosition.x / vpSize.x;
            y.Value = 1 - _LastTouchedPosition.y / vpSize.y;
        }
    }

    private void BuildParams(string type)
    {
        foreach (Node node in _Params.GetChildren())
        {
            node.QueueFree();
            _Params.RemoveChild(node);
        }

        if (type == "Vignette")
        {
            PRVisible(_Vignette);
            PRFloat(_Vignette, nameof(Vignette.Size));
            PRFloat(_Vignette, nameof(Vignette.Ratio));
        }

        else if (type == "Shockwave")
        {
            PRVisible(_Shockwave);
            PRFloat(_Shockwave, nameof(Shockwave.Size));
            PRFloat(_Shockwave, nameof(Shockwave.Force));
            PRFloat(_Shockwave, nameof(Shockwave.Thickness));
            PRVector2(_Shockwave, nameof(Shockwave.Center));

            // Btn
            var h = new HBoxContainer()
            {
                SizeFlagsHorizontal = (int)SizeFlags.ExpandFill
            };
            var btn = new Button()
            {
                Text = "Animate",
                SizeFlagsHorizontal = (int)SizeFlags.ExpandFill
            };
            btn.Connect("pressed", this, nameof(AnimateShockwave));
            h.AddChild(btn);
            _Params.AddChild(h);
        }

        else if (type == "MotionBlur")
        {
            PRVisible(_MotionBlur);
            PRFloat(_MotionBlur, nameof(MotionBlur.Strength));
            PRFloat(_MotionBlur, nameof(MotionBlur.AngleDegrees), paramName: "angle_degrees");
        }

        else if (type == "GaussianBlur")
        {
            PRVisible(_GaussianBlur);
            PRFloat(_GaussianBlur, nameof(GaussianBlur.Strength));
        }

        else if (type == "BetterBlur")
        {
            PRVisible(_BetterBlur);
            PRFloat(_BetterBlur, nameof(BetterBlur.Strength));
        }
    }

    private void UpdateParams(string type)
    {
        if (type == "Vignette")
        {
            _Params.GetNode<SpinBox>("Ratio/Value").Value = _Vignette.Ratio;
            _Params.GetNode<SpinBox>("Size/Value").Value = _Vignette.Size;
        }
        else if (type == "Shockwave")
        {
            var center = _Shockwave.Center;
            _Params.GetNode<SpinBox>("Center/VBox/X").Value = center.x;
            _Params.GetNode<SpinBox>("Center/VBox/Y").Value = center.y;
            _Params.GetNode<SpinBox>("Force/Value").Value = _Shockwave.Force;
            _Params.GetNode<SpinBox>("Thickness/Value").Value = _Shockwave.Thickness;
            _Params.GetNode<SpinBox>("Size/Value").Value = _Shockwave.Size;
        }
        else if (type == "MotionBlur")
        {
            _Params.GetNode<SpinBox>("Strength/Value").Value = _MotionBlur.Strength;
            _Params.GetNode<SpinBox>("AngleDegrees/Value").Value = _MotionBlur.AngleDegrees;
        }
        else if (type == "GaussianBlur")
        {
            _Params.GetNode<SpinBox>("Strength/Value").Value = _GaussianBlur.Strength;
        }
        else if (type == "BetterBlur")
        {
            _Params.GetNode<SpinBox>("Strength/Value").Value = _BetterBlur.Strength;
        }
    }

    private void AnimateShockwave()
    {
        var x = _Params.GetNode<SpinBox>("Center/VBox/X");
        var y = _Params.GetNode<SpinBox>("Center/VBox/Y");
        _Shockwave.Start(new Vector2((float)x.Value, (float)y.Value));
    }

    private void EffectVisibility(bool value, Control obj)
    {
        obj.Visible = value;
    }

    private void ValueChanged(float value, Control obj, string name)
    {
        UpdateShader(obj, name, value);
    }

    private void ValueChangedVector2(float value, string coord, Control obj, string name)
    {
        var current = (Vector2)obj.Get(name);
        // var paramName = name.ToLower();
        var paramName = name;

        if (coord == "x")
        {
            UpdateShader(obj, paramName, new Vector2(value, current.y));
        }
        else
        {
            UpdateShader(obj, paramName, new Vector2(current.x, value));
        }
    }

    private void UpdateShader(Control obj, string name, object value)
    {
        obj.Set(name, value);
        // ShaderExt.SetShaderParam(obj, name, value);
    }

    public override void _Process(float delta)
    {
        var vpSize = GetViewportRect().Size;

        foreach (var sprite in _Sprites)
        {
            var pos = sprite.Position;
            var rot = sprite.RotationDegrees;
            var texSize = sprite.Texture.GetSize();
            var totalSize = sprite.Scale * texSize;

            pos.x -= sprite.Scale.x * 100 * delta;
            rot += sprite.Scale.y * 10 * delta;
            while (rot > 360)
            {
                rot -= 360;
            }

            if (pos.x < -totalSize.x)
            {
                pos.x = vpSize.x + totalSize.x;
            }
            else if (pos.x > vpSize.x + totalSize.x)
            {
                pos.x = -totalSize.x;
            }

            sprite.RotationDegrees = rot;
            sprite.Position = pos;
        }

        var selectedIdx = _EffectSelection.Selected;
        var selectedEffect = _EffectSelection.GetItemText(selectedIdx);
        UpdateParams(selectedEffect);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            _Touched = mouseButton.Pressed;

            if (mouseButton.Pressed)
            {
                _LastTouchedPosition = mouseButton.Position;
                OnTouchPositionUpdate();
            }
        }

        else if (@event is InputEventMouseMotion mouseMotion)
        {
            if (_Touched)
            {
                _LastTouchedPosition = mouseMotion.Position;
                OnTouchPositionUpdate();
            }
        }

        else if (@event is InputEventScreenTouch touch)
        {
            _Touched = touch.Pressed;

            if (touch.Pressed)
            {
                _LastTouchedPosition = touch.Position;
                OnTouchPositionUpdate();
            }
        }
    }
}
