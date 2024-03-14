using Godot;
using System;

public partial class CurrentColorRect : Control {
	const float ANIMATION_TIME = 0.15f;

	[Export] ColorRect _border, _green, _blue;
	
	public override void _EnterTree() {
		GetNode<LevelShift>("/root/Level/Level").ColorChanged += _OnColorChanged;
	}

	void _OnColorChanged(string color) {
		Vector2 targetPos;

		switch (color) {
			case "Red":
				targetPos = new Vector2(0, 0);
				break;
			case "Green":
				targetPos = _green.Position - new Vector2(2, 2);
				break;
			default:
				targetPos = _blue.Position - new Vector2(2, 2);
				break;
		}

		var tween = CreateTween();
		tween.TweenProperty(_border, "position", targetPos, ANIMATION_TIME)
			.SetTrans(Tween.TransitionType.Quint).SetEase(Tween.EaseType.Out);
	}
}
