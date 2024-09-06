using Godot;

public partial class Gun1 : StaticBody2D
{
	Methods myMethods = new Methods();


	[Export]
	public PackedScene ProjectileScene;

	[Export]
	public int rateOfFire = 500;
	
	bool shouldFire = true;
	
	public override void _Ready()
	{
	}

	
	public override void _Process(double delta)
	{
		if (shouldFire && (Input.IsKeyPressed(Key.F) || Input.IsActionPressed("fire-left-mouse")))  
		{
			myMethods.DelayedFunction(reload, rateOfFire);
			SpawnProjectile();
			shouldFire = false;
		}
	}

	private void SpawnProjectile()
	{
		
		var projectileInstance = (Projectile)ProjectileScene.Instantiate();
		
		
		projectileInstance.GlobalPosition = GlobalPosition + Transform.X * 45;
		projectileInstance.Rotation = Rotation;  
		
		
		GetParent().GetParent().AddChild(projectileInstance);
		
		
		var direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation));
		projectileInstance.SetVelocity(direction);
	}

	private void reload(){
		shouldFire = true;
	}
}