using Godot;
using System;

public partial class FinishVisual : Sprite2D {
	enum LevelColor { Red, Green, Blue };

	const double MOVE_TIME = 0.25f;

	[Export] LevelColor _color;
	Area2D _matchingArea;
	Vector2 offset = new Vector2(16, 16);

	public override void _Ready() {
		_matchingArea = GetNodeOrNull<Area2D>($"/root/Level/Finish/{_color}");

		if (_matchingArea == null) {
			QueueFree();
			return;
		}

		GlobalPosition = _matchingArea.GlobalPosition + offset;
	}

	public void _OnColorMoved(Vector2 dir) {
		var tween = CreateTween();
		tween.TweenProperty(this, "global_position", _matchingArea.GlobalPosition + offset, MOVE_TIME)
			.SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.Out);
	}
}
