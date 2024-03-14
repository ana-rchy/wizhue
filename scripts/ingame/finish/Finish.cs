using Godot;
using System;

public partial class Finish : Node2D {
	const int TILE_SIZE = 32;

	[Export] byte _nextLevel;
	[Export(PropertyHint.File)] string _nextLevelOverride;
	FinishColor _currentColor;

	public override void _EnterTree() {
		GetNode<LevelShift>("/root/Level/Level").ColorChanged += _OnColorChanged;

		var levelCount = GetNodeOrNull<Label>("/root/Level/UI/Level");
		if (levelCount != null) {
			levelCount.Text = $"{_nextLevel - 1}";
		}
	}

	public override void _Process(double _) {
		if (Input.IsActionJustPressed("ShiftLeft") && _currentColor != null) {
			_currentColor.ShiftLeft();
		} else if (Input.IsActionJustPressed("ShiftRight") && _currentColor != null) {
			_currentColor.ShiftRight();
		}
	}

	public override void _PhysicsProcess(double _) {
		var count = 0;
		foreach (Area2D area in GetChildren()) {
			if (area.HasOverlappingAreas()) {
				count++;
			}
		}

		if (count == GetChildren().Count) {
			SetPhysicsProcess(false);
			GoToNextLevel();
		}
	}


	void _OnColorChanged(string color) {
		_currentColor = GetNodeOrNull<FinishColor>(color);
	}

	void GoToNextLevel() {
		using var save = FileAccess.Open("user://game.save", FileAccess.ModeFlags.Write);
		save.Store8(_nextLevel);
		var scene = _nextLevelOverride ?? $"res://scenes/levels/Level{_nextLevel}.tscn";
		GetTree().ChangeSceneToFile(scene);
	}
}
