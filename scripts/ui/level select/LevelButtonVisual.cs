using Godot;
using System;

public partial class LevelButtonVisual : TextureButton {
	[Export] Color _color;
	[Export] float _transitionTime = 0.25f;

	public void _OnMouseEntered() {
		var tween = CreateTween();
		tween.TweenProperty(this, "modulate", _color, _transitionTime)
			.SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
	}

	public void _OnMouseExited() {
		var tween = CreateTween();
		tween.TweenProperty(this, "modulate", new Color("#fff"), _transitionTime)
			.SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
	}
}
