using Godot;
using System;

public partial class LevelSelectShift : Control {
	const int TILE_SIZE = 64;

	[Export] Control _red, _green;
	[Export] Control _redVisual, _greenVisual;
	Control _currentColor, _currentColorVisual;

	public override void _Ready() {
		_currentColor = _red;
		_currentColorVisual = _redVisual;
	}

	public override void _Process(double _) {
		if (Input.IsActionJustPressed("SelectRed")) {
			_currentColor = _red;
			_currentColorVisual = _redVisual;
		} else if (Input.IsActionJustPressed("SelectGreen")) {
			_currentColor = _green;
			_currentColorVisual = _greenVisual;
		} 

		if (Input.IsActionJustPressed("ShiftLeft")) {
			Shift(_currentColor, _currentColorVisual, new Vector2(-1, 0));
		} else if (Input.IsActionJustPressed("ShiftRight")) {
			Shift(_currentColor, _currentColorVisual, new Vector2(1, 0));
		}
	}

	
	void Shift(Control color, Control colorVisual, Vector2 dir) {
		colorVisual.Position = color.Position += dir * TILE_SIZE;
	}
}
