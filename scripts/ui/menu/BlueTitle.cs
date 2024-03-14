using Godot;
using System;

public partial class BlueTitle : TextureRect {
	[Export] float _cycleTime = 10f;
	[Export] float _distance = 100f;

	public override void _Ready() {
		var tween = CreateTween();
		var target = new Vector2(_distance, 0);

		tween.SetLoops();
		tween.TweenProperty(this, "position", Position + target, _cycleTime / 4)
			.SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
		tween.TweenProperty(this, "position", Position, _cycleTime / 4)
			.SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
		tween.TweenProperty(this, "position", Position - target, _cycleTime / 4)
			.SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
		tween.TweenProperty(this, "position", Position, _cycleTime / 4)
			.SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
	}
}
