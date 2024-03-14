using Godot;
using System;

public partial class Blocker : Node {
	public override void _Input(InputEvent e) {
		if (Input.IsActionPressed("SelectBlue")) {
			GetNode<Viewport>("/root").SetInputAsHandled();
		}
	}
}
