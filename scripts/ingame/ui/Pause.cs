using Godot;
using System;

public partial class Pause : Control {
	const double MOVE_TIME = 0.25f;

	[Export] Vector2 _moveTarget;
	Vector2 _originalPos;

	public override void _Ready() {
		_originalPos = Position;
	}

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("Pause") && !GetTree().Paused) {
			var tween = CreateTween();
			tween.TweenProperty(this, "position", _moveTarget, MOVE_TIME)
				.SetTrans(Tween.TransitionType.Quint).SetEase(Tween.EaseType.Out);

			GetTree().Paused = true;
		} else if (Input.IsActionJustPressed("Pause") && GetTree().Paused) {
			Unpause();
		}

		if (Input.IsActionJustPressed("Restart")) {
			GetTree().ReloadCurrentScene();
		}
	}


	public void Unpause() {
		var tween = CreateTween();
		tween.TweenProperty(this, "position", _originalPos, MOVE_TIME)
			.SetTrans(Tween.TransitionType.Quint).SetEase(Tween.EaseType.Out);

		GetTree().Paused = false;
	}
}
