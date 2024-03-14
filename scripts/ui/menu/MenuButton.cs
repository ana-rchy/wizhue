using Godot;
using System;

public partial class MenuButton : TextureButton {
	[Export] Color _color;
	[Export] float _transitionTime = 0.25f;

	public override void _Ready() {
		if (Name == "Continue" && !FileAccess.FileExists("user://game.save")) {
			Disabled = true;
			Modulate = Modulate.Darkened(0.5f);
			MouseDefaultCursorShape = Control.CursorShape.Arrow;
			return;
		}

		MouseEntered += _OnMouseEntered;
		MouseExited += _OnMouseExited;
		ButtonDown += _OnButtonDown;
		ButtonUp += _OnButtonUp;
	}

	
	void _OnPlayPressed() {
		ButtonUp -= _OnButtonUp;
		GetTree().ChangeSceneToFile("res://scenes/LevelSelect.tscn");
	}

	void _OnContinuePressed() {
		using var save = FileAccess.Open("user://game.save", FileAccess.ModeFlags.Read);
		var levelNum = save.Get8();
		ButtonUp -= _OnButtonUp;
		GetTree().ChangeSceneToFile($"res://scenes/levels/Level{levelNum}.tscn");
	}

	void _OnQuitPressed() {
		GetTree().Quit();
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
