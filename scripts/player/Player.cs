using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;
	[Export]
	public Node2D currentWeapon;
	Methods myMethods = new Methods();
	Area2D area;
	Vector2 velocity;
	bool isWeaponEquipped = false;

	public override void _Ready()
	{
		area = GetNode<Area2D>("Area2D");
		myMethods.addSignalForBodyEntered(area, this, "pickUpWeapon");
	}

	public override void _PhysicsProcess(double delta)
	{
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
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
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

}
