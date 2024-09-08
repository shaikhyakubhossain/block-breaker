using Godot;

public partial class Enemy : CharacterBody2D
{
	[Export] public float Speed = 100.0f;
	[Export] public float JumpVelocity = -400.0f;
	[Export] public Node2D target;

	Vector2 velocity;
	Area2D area;
	Methods myMethods = new Methods();


	public override void _Ready()
	{
		area = GetNode<Area2D>("Area2D");
		target = GetParent().GetNode<CharacterBody2D>("player");
		// myMethods.addSignalForBodyEntered(area, this, "onCollision");
		area.BodyEntered += onCollision;

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
	private void onCollision(Node body){
		// GD.Print("enemy: ", body.Name);
		if(body == target){
			myMethods.changeScene(body, "scenes/gameOver.tscn");
			// body.QueueFree();
		}
	}
}
