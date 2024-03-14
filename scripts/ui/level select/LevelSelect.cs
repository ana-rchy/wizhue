using Godot;
using System;

public partial class LevelSelect : Control {
	[Export] Label _levelCount;
	[Export] TextureRect _hint;

	public override void _Ready() {
		using var save = FileAccess.Open("user://game.save", FileAccess.ModeFlags.Read);
		var saveValue = save.Get8();
		_levelCount.Text = $"{saveValue - 1}";

		if (saveValue == 11) {
			_hint.Show();
		}
	}
}
