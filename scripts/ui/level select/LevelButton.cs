using Godot;
using System;

public partial class LevelButton : Button {
	[Export] Control _matchingVisualControl;

	public override void _Ready() {
		var matchingVisual = _matchingVisualControl.GetNode<LevelButtonVisual>($"{Name}");

		MouseEntered += matchingVisual._OnMouseEntered;
		MouseExited += matchingVisual._OnMouseExited;
		Pressed += _OnPressed;
	}

	
	void _OnPressed() {
		GetTree().ChangeSceneToFile($"res://scenes/levels/Level{Name}.tscn");
	}
}
