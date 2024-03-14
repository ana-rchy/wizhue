using System;
using Godot;

public partial class PlayerVisual : AnimatedSprite2D {
	enum LevelColor { Red, Green, Blue };

	const double ANIMATION_FRAME_TIME = 0.075f;
	const double MOVE_TIME = 0.25f;
	const double PARTICLE_TIME = 0.5f;
	const int TILE_SIZE = 32;
	
	[Export(PropertyHint.File)] string _particleScene;
	[Export] LevelColor _color;

	Area2D _matchingArea;
	Vector2 _offset = new Vector2(16, 16);

	public override void _Ready() {
		_matchingArea = GetNodeOrNull<Area2D>($"/root/Level/Player/{_color}");

		if (_matchingArea == null) {
			QueueFree();
			return;
		}
		
		GlobalPosition = _matchingArea.GlobalPosition + _offset;
	}

	double _timer;
	public override void _Process(double delta) {
		_timer += delta;
		if (_timer >= ANIMATION_FRAME_TIME) {
			Frame = (Frame + 1) % 4;
			_timer -= ANIMATION_FRAME_TIME;
		}
	}


	void SpawnMoveParticle(Vector2 dir) {
		MoveParticle particle = (MoveParticle) GD.Load<PackedScene>(_particleScene).Instantiate();
		particle.TargetPos = _matchingArea.GlobalPosition + new Vector2(TILE_SIZE / 2, TILE_SIZE / 2)
			- dir * (TILE_SIZE * 1.35f);

		particle.GlobalPosition = _matchingArea.GlobalPosition + new Vector2(TILE_SIZE / 2, TILE_SIZE / 2)
			- dir * (TILE_SIZE * 0.6f);
		particle.Modulate = Modulate;

		GetParent().AddChild(particle);
	}

	public void _OnColorMoved(Vector2 dir) {
		Animation = dir switch {
			Vector2(0, -1) => "Up",
			Vector2(-1, 0) => "Left",
			Vector2(0, 1) => "Down",
			_ => "Right"
		};

		SpawnMoveParticle(dir);

		var tween = CreateTween();
		tween.TweenProperty(this, "global_position", _matchingArea.GlobalPosition + _offset, MOVE_TIME)
			.SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.Out);
	}
}
