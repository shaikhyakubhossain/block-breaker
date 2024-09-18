using Godot;

public partial class Enemy : CharacterBody2D, IHasHealth
{
	[Export] public float health { get; private set; } = 30.0f;
	[Export] public float Speed = 100.0f;
	[Export] public float JumpVelocity = -400.0f;
	[Export] public float giveExp = 50.0f;
	[Export] public float damage = 10;
	private Node2D target;

	Vector2 velocity;
	Area2D area;

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
			if (velocity.Y >= 3000.0f)
			{
				Falling();
			}
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
		if(body == target){
			((Player)target).takeDamage(10);
		}
	}

	private void Falling()
	{
		QueueFree();
	}

	public void takeDamage(int damage){
		health -= damage;
		if(health <= 0){
			((Player)target).gainExp(giveExp);
			QueueFree();
		}
	}
}
