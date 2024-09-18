using Godot;
using System;

public partial class ExpBar : ProgressBar
{

	private RichTextLabel level;

	private CharacterBody2D target;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		target = GetParent().GetParent().GetNode<CharacterBody2D>("player");
		level = GetNode<RichTextLabel>("level");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		level.Text = "LVL-" + ((Player)target).level.ToString();
		MaxValue = ((Player)target).expReqForNextLevel;
		Value = ((Player)target).currentExp;
	}
}
