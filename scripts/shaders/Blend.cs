using Godot;
using System;

public partial class Blend : ColorRect {
	[Export] ShaderMaterial _shader;
	[Export] SubViewport _red, _green, _blue;
	
	public override void _Ready() {
		var red = _red ?? GetNode<SubViewport>("/root/Level/Visuals/Red");
		var green = _green ?? GetNode<SubViewport>("/root/Level/Visuals/Green");
		var blue = _blue ?? GetNode<SubViewport>("/root/Level/Visuals/Blue");

		Show();
		
		_shader.SetShaderParameter("red", red.GetTexture());
		_shader.SetShaderParameter("green", green.GetTexture());
		_shader.SetShaderParameter("blue", blue.GetTexture());
	}
}
