using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;
	[Export]
	public Node2D currentWeapon;

	KinematicCollision2D collision;
	Vector2 velocity;

	bool isWeaponEquipped = false;
	

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;
		// GD.Print("hi ");

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			// GD.Print("flying");
		}

		if(Input.IsKeyPressed(Key.F)){
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
		if(isWeaponEquipped){
			rotateWeaponTowardsMousePosition(currentWeapon);
		}

		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			collision = GetSlideCollision(i);
			if(((Node2D)collision.GetCollider()).IsInGroup("weaponGroup")){
				currentWeapon = (Node2D)collision.GetCollider();
				currentWeapon.GetParent().RemoveChild(currentWeapon);
				AddChild(currentWeapon);
				currentWeapon.Owner = this;
				currentWeapon.RemoveChild(currentWeapon.GetChild<CollisionShape2D>(1));
				currentWeapon.SetGlobalPosition(GlobalPosition);
				// GD.Print(currentWeapon.Position);
				isWeaponEquipped = true;
			}
		}


	}

	private void rotateWeaponTowardsMousePosition(Node2D node){
		Vector2 mousePosition = GetGlobalMousePosition();
		
		Vector2 direction = mousePosition - node.GlobalPosition;
		
		float angle = Mathf.Atan2(direction.Y, direction.X);
		
		node.Rotation = angle;
	}

}
