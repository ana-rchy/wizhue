using Godot;
using System;

public partial class PauseButton : TextureButton {
	[Export] Color _color;
	[Export] float _transitionTime = 0.25f;

	public override void _Ready() {
		MouseEntered += _OnMouseEntered;
		MouseExited += _OnMouseExited;
		ButtonDown += _OnButtonDown;
		ButtonUp += _OnButtonUp;
	}


	void _OnResumePressed() {
		GetParent<Pause>().Unpause();	
	}

	void _OnMenuPressed() {
		ButtonUp -= _OnButtonUp;
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://scenes/Menu.tscn");
	}

	void _OnMouseEntered() {
		var tween = CreateTween();
		tween.TweenProperty(this, "modulate", _color, _transitionTime)
			.SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
	}

	void _OnMouseExited() {
		var tween = CreateTween();
		tween.TweenProperty(this, "modulate", new Color("#fff"), _transitionTime)
			.SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
	}

	void _OnButtonDown() {
		var tween = CreateTween();
		tween.TweenProperty(this, "modulate", _color.Darkened(0.4f), _transitionTime)
			.SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
	}

	void _OnButtonUp() {
		var tween = CreateTween();
		tween.TweenProperty(this, "modulate", _color, _transitionTime)
			.SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
	}
}
