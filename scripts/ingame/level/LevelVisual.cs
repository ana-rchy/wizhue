using Godot;
using System;

public partial class LevelVisual : TileMap {
	enum LevelColor { Red, Green, Blue };
	[Export] LevelColor _color;

	public override void _EnterTree() {
		GetNode<LevelShift>("/root/Level/Level").LayerMoved += _OnLayerMoved;
	}

	public void _OnLayerMoved(Godot.Collections.Array cells, int color) {
		if (color != (int) _color) return;
		
		Clear();
		foreach (Vector2I cellPos in cells) {
			SetCell(0, cellPos, 0, new Vector2I(0, 0));
		}
	}
}
