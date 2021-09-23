# Effects and shaders for Godot Engine (3.3.3, C#)

Contains effects and shaders for Godot Engine.

## How to install

Use `git` submodules: open a command prompt in your project folder and then:

```
git submodule add https://github.com/Srynetix/godot-plugin-fx addons/fx
```

## How to use

Simply add effects in your scene and configure them using exposed properties.

## Features

### Screen effects

#### Gaussian blur (`GaussianBlur`)

**How to use**: Only add in scene tree  
**Parameters**:
- `strength`: Blur strength

#### Motion blur (`MotionBlur`)

**How to use**: Only add in scene tree  
**Parameters**:
- `strength`: Blur strength
- `angle_degrees`: Angle in degrees

#### Shockwave (`Shockwave`)

**How to use**: Use the `Start` method with a `Vector2` position  
**Parameters**:
- `force`: Reflection force
- `thickness`: Wave ring thickness

#### Vignette (`Vignette`)

**How to use**: Only add in scene tree  
**Parameters**:
- `ratio`: Vignette strength