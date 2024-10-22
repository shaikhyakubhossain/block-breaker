using Godot;
using System;

public partial class PowerUpUI : Control
{

	[Export] public float increasedMovementSpeed = 100;
	[Export] public int increasedRateOfFire = 15;
	[Export] public float increasedMaxHealth = 10;
	private Player player;
	private Gun1 gun1;
	private ProgressBar healthBar;
	HBoxContainer btnContainer;
	Button rateOfFireBtn;
	Button maxHealthBtn;
	Button movementSpeedBtn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent().GetChild<Player>(0);
		gun1 = player.GetChild<Gun1>(6);
		healthBar = player.GetChild<ProgressBar>(3);
		// GD.Print(player.GetChild<Gun1>(6), gun1.rateOfFire);
		GD.Print(player.GetChildren());
		btnContainer = GetNode<HBoxContainer>("btnContainer");
		
		movementSpeedBtn = (Button)btnContainer.GetChild(0);;
		rateOfFireBtn = (Button)btnContainer.GetChild(1);
		maxHealthBtn = (Button)btnContainer.GetChild(2);
	
		rateOfFireBtn.Pressed += onRateOfFireBtnPressed;
		maxHealthBtn.Pressed += onMaxHealthBtnPressed;
		movementSpeedBtn.Pressed += onMovementSpeedBtnPressed;

	}
	private void onRateOfFireBtnPressed(){
		GD.Print("rate of fire btn pressed");
		gun1.rateOfFire -= increasedRateOfFire;
		GD.Print(gun1.rateOfFire);
		GetTree().Paused = false;
		QueueFree();
		
	}
	private void onMaxHealthBtnPressed(){
		GD.Print("max health btn pressed");
		player.maxHealth += increasedMaxHealth;
		healthBar.MaxValue += increasedMaxHealth;
		GetTree().Paused = false;
		QueueFree();
	}
	private void onMovementSpeedBtnPressed(){
		GD.Print("movement speed btn pressed");
		player.movementSpeed += 100;
		GetTree().Paused = false;
		QueueFree();
	}
}
