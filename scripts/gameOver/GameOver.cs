using Godot;
using System;

public partial class GameOver : CanvasLayer
{
	Methods myMethods = new Methods();
	
	Button retryBtn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		retryBtn = GetNode<Button>("retryBtn");
		// myMethods.addSignalOnUIBtnClick(retryBtn, "retry");
		retryBtn.Pressed += retry;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public void retry()
	{
		GD.Print("retried");
		myMethods.changeScene(this, "scenes/game.tscn");
	}
}
