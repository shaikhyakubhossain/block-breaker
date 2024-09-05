using Godot;

public partial class Projectile : StaticBody2D
{

	[Export]
	public float Speed = 300.0f;

	[Export]
	public int timeOut = 1000;

	Vector2 velocity;
	Area2D area;
	Methods myMethods = new Methods();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		velocity = Transform.X * Speed;
		myMethods.DelayedFunction(timeOutMethod, timeOut);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += velocity * (float)delta;

		// myMethod.detectCollisions(area, this, "onCollision");

	}

	private void onCollision(){
		GD.Print("hit");
	}
	public void SetVelocity(Vector2 direction)
	{
		velocity = direction.Normalized() * Speed;
	}

	private void timeOutMethod(){
		QueueFree();
	}
	
}
