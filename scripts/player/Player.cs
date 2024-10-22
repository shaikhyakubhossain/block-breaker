using System;
using Godot;

public partial class Player : CharacterBody2D, IHasHealth
{
	[Export] public float health { get; private set; } = 100.0f;
	[Export] public float maxHealth { get; set; } = 100.0f;
	[Export] public float healthRegenerationRate { get; private set; } = 0.01f;
	[Export] public float currentExp = 0.0f;
	[Export] public float expReqForNextLevel = 100.0f;
	[Export] public float level = 1.0f;
	[Export] public float movementSpeed = 300.0f;
	[Export] public float JumpVelocity = -400.0f;
	[Export] public Node2D currentWeapon;
	private Methods myMethods = new Methods();
	[Export]
	private PackedScene powerUpUI;
	private Area2D area;
	private Vector2 velocity;
	private bool isWeaponEquipped = false;

	public override void _Ready()
	{
		area = GetNode<Area2D>("Area2D");
		myMethods.addSignalForBodyEntered(area, this, "pickUpWeapon");
	}

	public override void _PhysicsProcess(double delta)
	{

		healthRegeneration();

		velocity = Velocity;
		// GD.Print("hi ");
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			if (velocity.Y >= 3000.0f)
			{
				Falling();
			}
		}

		if (Input.IsKeyPressed(Key.F))
		{
		}

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		Vector2 direction = Input.GetVector("move-left", "move-right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * movementSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, movementSpeed);
		}

		Velocity = velocity;
		MoveAndSlide();
		if (isWeaponEquipped)
		{
			rotateWeaponTowardsMousePosition(currentWeapon);
		}
	}

	private void rotateWeaponTowardsMousePosition(Node2D node)
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 direction = mousePosition - node.GlobalPosition;
		float angle = Mathf.Atan2(direction.Y, direction.X);
		node.Rotation = angle;
	}

	private void pickUpWeapon(Node weapon)
	{
		// GD.Print("player: ", weapon.Name);
		if (weapon.IsInGroup("weaponGroup"))
		{
			currentWeapon = weapon as StaticBody2D;
			myMethods.removeSignalForBodyEntered(area, this, "pickUpWeapon");
			currentWeapon.GetParent().RemoveChild(currentWeapon);
			AddChild(currentWeapon);
			currentWeapon.Owner = this;
			currentWeapon.RemoveChild(currentWeapon.GetChild<CollisionShape2D>(2));
			currentWeapon.SetGlobalPosition(GlobalPosition);
			isWeaponEquipped = true;
		}
	}

	private void Falling()
	{
		myMethods.changeScene(this, "scenes/gameOver.tscn");
	}

	public void takeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			myMethods.changeScene(this, "scenes/gameOver.tscn");
		}
	}

	public void healthRegeneration()
	{
		if (health < maxHealth)
		{
			health += healthRegenerationRate;
		}
	}

	public void gainExp(float amountOfExpToGain)
	{
		currentExp += amountOfExpToGain;
		if (currentExp >= expReqForNextLevel)
		{
			currentExp = 0;
			level += 1;
			expReqForNextLevel = level * 100;
			// myMethods.changeScene(this, "scenes/powerUpScreen.tscn");
			var powerUp = (PowerUpUI)powerUpUI.Instantiate();
			GD.Print(powerUp);
			GetParent().AddChild(powerUp);
			powerUp.GlobalPosition = GlobalPosition;
			GetTree().Paused = true;
		}
	}

}
