using Godot;
using System;

public partial class Back : TextureButton {
	[Export] Color _color;
	[Export] float _transitionTime = 0.25f;

	public override void _Ready() {
		MouseEntered += _OnMouseEntered;
		MouseExited += _OnMouseExited;
		Pressed += _OnButtonPressed;
	}


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

	void _OnButtonPressed() {
		Pressed -= _OnButtonPressed;
		GetTree().ChangeSceneToFile("res://scenes/Menu.tscn");
	}
}
