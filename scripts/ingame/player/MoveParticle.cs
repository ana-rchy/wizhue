using System;
using System.Threading.Tasks;
using Godot;

public partial class MoveParticle : AnimatedSprite2D {
	const float ANIMATION_TIME = 0.6f;

	public Vector2 TargetPos;

	public override void _Ready() {
		var tween = CreateTween();
		tween.TweenProperty(this, "global_position", TargetPos, ANIMATION_TIME)
			.SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.Out);
	}

	double _timer = ANIMATION_TIME;
	public override void _Process(double delta) {
		if (_timer <= 0) {
			QueueFree();
		}

		Frame = (int) Math.Clamp(Math.Floor(ANIMATION_TIME / _timer), 0, 9);

		_timer -= delta;
	}
}
