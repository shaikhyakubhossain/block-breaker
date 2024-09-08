using Godot;

public partial class Gun1 : StaticBody2D
{
	Methods myMethods = new Methods();


	[Export]
	public PackedScene ProjectileScene;

	[Export]
	public int rateOfFire = 500;

	public bool shouldFire = true;

	public override void _Ready()
	{
		// myMethods.detectCollisions((Area2D)GetNode("Area2D"), this, "onCollision");

	}

	public override void _Process(double delta)
	{
		if (GetParent().Name == "player" && shouldFire && (Input.IsKeyPressed(Key.F) || Input.IsActionPressed("fire-left-mouse")))
		{
			myMethods.DelayedFunction(reload, rateOfFire);
			SpawnProjectile();
			shouldFire = false;
		}
	}

	private void SpawnProjectile()
	{
		var projectileInstance = (Projectile)ProjectileScene.Instantiate();
		GetParent().GetParent().AddChild(projectileInstance);
		// GD.Print(projectileInstance.GlobalPosition, "before", GlobalPosition);
		projectileInstance.GlobalPosition = GlobalPosition + Transform.X * 45;
		// GD.Print(projectileInstance.GlobalPosition, "after", GlobalPosition);
		projectileInstance.Rotation = Rotation;
		var direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation));
		projectileInstance.SetVelocity(direction);
	}

	private void reload()
	{
		shouldFire = true;
	}
}
