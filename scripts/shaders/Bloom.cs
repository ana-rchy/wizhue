using Godot;
using System;

public partial class Bloom : ColorRect {
	[Export] ShaderMaterial _shader;
	[Export] SubViewport _blendViewport;

	public override void _Ready() {
		_shader.SetShaderParameter("screen_texture", _blendViewport.GetTexture());
	}
}
