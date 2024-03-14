using Godot;
using System;

public partial class Player : Node2D {
	const float SHIFT_REPEAT_TIME = 0.15f;

	PlayerColor _currentColor;

	public override void _EnterTree() {
		GetNode<LevelShift>("/root/Level/Level").ColorChanged += _OnColorChanged;
	}

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ShiftLeft") && _currentColor != null) {
			_currentColor.ShiftLeft();
		} else if (Input.IsActionJustPressed("ShiftRight") && _currentColor != null) {
			_currentColor.ShiftRight();
		}
	}


	void _OnColorChanged(string color) {
		_currentColor = GetNodeOrNull<PlayerColor>(color);
	}
}
