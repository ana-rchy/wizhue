using Godot;
using System;

public partial class LevelShift : TileMap {
	[Signal] public delegate void ColorChangedEventHandler(string color);
	[Signal] public delegate void LayerMovedEventHandler(Godot.Collections.Array cells, int color);
	enum LevelColor { Red, Green, Blue };
	const int TILE_SIZE = 32;

	[Export] int _rightEdgeColumn = 9;
	LevelColor _currentColor;
	AudioStreamPlayer _shiftSFX, _colorChangeSFX;

	public override void _Ready() {
		_currentColor = LevelColor.Red;
		EmitSignal(SignalName.ColorChanged, $"{_currentColor}");
		_shiftSFX = GetNode<AudioStreamPlayer>("ShiftSFX");
		_colorChangeSFX = GetNode<AudioStreamPlayer>("ColorChangeSFX");
		
		for (int i = 0; i <= 2; i++) {
			EmitSignal(SignalName.LayerMoved, GetUsedCells(i), i);
		}
	}

	public override void _UnhandledInput(InputEvent e) {
		if (Input.IsActionJustPressed("SelectRed")) {
			_currentColor = LevelColor.Red;
			EmitSignal(SignalName.ColorChanged, $"{_currentColor}");
			_colorChangeSFX.Play();
		} else if (Input.IsActionJustPressed("SelectGreen")) {
			_currentColor = LevelColor.Green;
			EmitSignal(SignalName.ColorChanged, $"{_currentColor}");
			_colorChangeSFX.Play();
		} else if (InputMap.HasAction("SelectBlue") && Input.IsActionJustPressed("SelectBlue")) {
			_currentColor = LevelColor.Blue;
			EmitSignal(SignalName.ColorChanged, $"{_currentColor}");
			_colorChangeSFX.Play();
		}
	}

	public override void _Process(double _) {
		if (Input.IsActionJustPressed("ShiftLeft") && !AreTilesAtColumn(_currentColor, 0)) {
			ShiftCells(_currentColor, new Vector2I(-1, 0));
		} else if (Input.IsActionJustPressed("ShiftRight") && !AreTilesAtColumn(_currentColor, _rightEdgeColumn)) {
			ShiftCells(_currentColor, new Vector2I(1, 0));
		}

		var color = (int) _currentColor;
		EmitSignal(SignalName.LayerMoved, GetUsedCells(color), color);
	}

	

	bool AreTilesAtColumn(LevelColor color, int column) {
		var layer = (int) color;

		for (int i = 0; i <= 16; i++) {
			if (GetCellTileData(layer, new Vector2I(column, i)) != null ||
				GetCellTileData(layer, new Vector2I(column, i)) != null) {
				
				return true;
			}
		}
		
		return false;
	}

	void ShiftCells(LevelColor color, Vector2I direction) {
		var layer = (int) color;
		var usedCells = GetUsedCells(layer);
		
		ClearLayer(layer);
		foreach (var cellPos in usedCells) {
			SetCell(layer, cellPos + direction, layer, new Vector2I(0, 0));
		}

		_shiftSFX.Play();
	}
}
