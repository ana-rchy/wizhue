using Godot;
using System;

public partial class PlayerColor : Area2D {
	[Signal] public delegate void ColorMovedEventHandler(Vector2 direction);

	const int TILE_SIZE = 32;
	const float MOVE_REPEAT_TIME = 0.15f;
	
	RayCast2D _rayUp, _rayLeft, _rayDown, _rayRight;
	AudioStreamPlayer _moveSFX;

	public override void _Ready() {
		_rayUp = GetNode<RayCast2D>("Raycasts/Up");
		_rayLeft = GetNode<RayCast2D>("Raycasts/Left");
		_rayDown = GetNode<RayCast2D>("Raycasts/Down");
		_rayRight = GetNode<RayCast2D>("Raycasts/Right");
		_rayUp.CollisionMask = _rayLeft.CollisionMask = _rayDown.CollisionMask = _rayRight.CollisionMask = CollisionMask;

		_moveSFX = GetNodeOrNull<AudioStreamPlayer>("MoveSFX");

		ColorMoved += GetNodeOrNull<PlayerVisual>($"/root/Level/Visuals/{Name}/PlayerVisual")._OnColorMoved;
	}
	
	double _timer;
	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("Up") && !_rayUp.IsColliding()) {
			Position += new Vector2(0, -TILE_SIZE);
			EmitSignal(SignalName.ColorMoved, new Vector2(0, -1));
			if (_moveSFX != null) {
				_moveSFX.Play();
			}
		} else if (Input.IsActionJustPressed("Left") && !_rayLeft.IsColliding()) {
			Position += new Vector2(-TILE_SIZE, 0);
			EmitSignal(SignalName.ColorMoved, new Vector2(-1, 0));
			if (_moveSFX != null) {
				_moveSFX.Play();
			}
		} else if (Input.IsActionJustPressed("Down") && !_rayDown.IsColliding()) {
			Position += new Vector2(0, TILE_SIZE);
			EmitSignal(SignalName.ColorMoved, new Vector2(0, 1));
			if (_moveSFX != null) {
				_moveSFX.Play();
			}
		} else if (Input.IsActionJustPressed("Right") && !_rayRight.IsColliding()) {
			Position += new Vector2(TILE_SIZE, 0);
			EmitSignal(SignalName.ColorMoved, new Vector2(1, 0));
			if (_moveSFX != null) {
				_moveSFX.Play();
			}
		}

		//if (Input.IsActionPressed("Up") || Input.IsActionPressed("Left")
		//	|| Input.IsActionPressed("Down") || Input.IsActionPressed("Right")) {

		//	_timer += delta;
		//} else {
		//	_timer = 0;
		//	return;
		//}
		//
		//if (_timer >= MOVE_REPEAT_TIME) {
		//	_timer -= MOVE_REPEAT_TIME;

		//	if (Input.IsActionPressed("Up") && !_rayUp.IsColliding()) {
		//		Position += new Vector2(0, -TILE_SIZE);
		//	} else if (Input.IsActionPressed("Left") && !_rayLeft.IsColliding()) {
		//		Position += new Vector2(-TILE_SIZE, 0);
		//	} else if (Input.IsActionPressed("Down") && !_rayDown.IsColliding()) {
		//		Position += new Vector2(0, TILE_SIZE);
		//	} else if (Input.IsActionPressed("Right") && !_rayRight.IsColliding()) {
		//		Position += new Vector2(TILE_SIZE, 0);
		//	}
		//}
	}


	public void ShiftLeft() {
		if (!_rayLeft.IsColliding()) {
			Position += new Vector2(-TILE_SIZE, 0);
			EmitSignal(SignalName.ColorMoved, new Vector2(-1, 0));
		}
	}

	public void ShiftRight() {
		if (!_rayRight.IsColliding()) {
			Position += new Vector2(TILE_SIZE, 0);
			EmitSignal(SignalName.ColorMoved, new Vector2(1, 0));
		}
	}
}
