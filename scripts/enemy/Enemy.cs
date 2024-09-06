using Godot;

public partial class Enemy : CharacterBody2D
{
	[Export] public float Speed = 100.0f;
	[Export] public float JumpVelocity = -400.0f;
	[Export] public Node2D target;

		Vector2 velocity;


	public override void _Ready()
	{

		target = GetParent().GetNode<CharacterBody2D>("player");

	}

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}


		Vector2 direction = (target.GlobalPosition - GlobalPosition).Normalized();
		velocity.X = direction.X * Speed;

		
		jumpTowardsTarget();

		Velocity = velocity;
		MoveAndSlide();
	}

	private void jumpTowardsTarget(){
		if(target.GlobalPosition.Y <= GlobalPosition.Y - 60 && IsOnFloor()){
			velocity.Y = JumpVelocity;
		}
	}
}
