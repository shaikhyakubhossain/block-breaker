using Godot;
using System;

public partial class PowerUp : CanvasLayer
{
	private CharacterBody2D player;
	private Button rateOfFireBtn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rateOfFireBtn = GetNode<Button>("rateOfFireBtn");
		rateOfFireBtn.Pressed += onRateOfFireBtnPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void onRateOfFireBtnPressed(){
		((Player)player).currentExp += 0.1f;
	}
}
