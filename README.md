# Effects and shaders for Godot Engine (3.3.3, C#)

Contains effects and shaders for Godot Engine.

## How to install

Use `git` submodules: open a command prompt in your project folder and then:

```
git submodule add https://github.com/Srynetix/godot-plugin-fx addons/fx
```

## How to use

Simply add effects in your scene and configure them using exposed properties.

There is an included sample scene in `samples/Sample.tscn` to play with the available effects and its parameters in a real-time demo.

## Features

### Screen effects

#### Gaussian blur (`GaussianBlur`)

**How to use**: Only add in scene tree  
**Parameters**:
- `strength`: Blur strength

![gaussianblur](./misc/gaussianblur.gif)

#### Better blur (`BetterBlur`)

**How to use**: Only add in scene tree  
**Parameters**:
- `strength`: Blur strength

*Note*: This is the `GaussianBlur` effect, but Web compatible (GLES2).

![betterblur](./misc/betterblur.gif)

#### Motion blur (`MotionBlur`)

**How to use**: Only add in scene tree  
**Parameters**:
- `strength`: Blur strength
- `angle_degrees`: Angle in degrees

![motionblur](./misc/motionblur.gif)

#### Shockwave (`Shockwave`)

**How to use**: Use the `Start` method with a `Vector2` position  
**Parameters**:
- `force`: Reflection force
- `thickness`: Wave ring thickness

![shockwave](./misc/shockwave.gif)

#### Vignette (`Vignette`)

**How to use**: Only add in scene tree  
**Parameters**:
- `ratio`: Vignette strength

![vignette](./misc/vignette.gif)