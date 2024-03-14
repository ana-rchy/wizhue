using Godot;
using System;

public partial class FinishColor : Area2D {
	[Signal] public delegate void ColorMovedEventHandler(Vector2 direction);

	const int TILE_SIZE = 32;
	const float MOVE_REPEAT_TIME = 0.15f;
	
	RayCast2D _rayLeft, _rayRight;

	public override void _Ready() {
		_rayLeft = GetNode<RayCast2D>("Raycasts/Left");
		_rayRight = GetNode<RayCast2D>("Raycasts/Right");
		_rayLeft.CollisionMask = _rayRight.CollisionMask = CollisionMask + 1;

		ColorMoved += GetNodeOrNull<FinishVisual>($"/root/Level/Visuals/{Name}/FinishVisual")._OnColorMoved;
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
