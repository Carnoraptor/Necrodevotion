using Godot;
using System;

public partial class TheButton : TextureButton
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("pls");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _Pressed()
	{
		Stats.devotion += 1;
	}
}
