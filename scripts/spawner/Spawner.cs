using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export] PackedScene enemyScene;
	bool shouldSpawn = true;
	Methods myMethods = new Methods();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(shouldSpawn){
			// GD.Print("spawning");
			myMethods.DelayedFunction(spawnEnemy, 2000);
			shouldSpawn = false;
		}
	}

	void spawnEnemy(){
		// GD.Print("spawned");
		var enemyInstance = (Enemy)enemyScene.Instantiate();
		GetParent().AddChild(enemyInstance);
		enemyInstance.GlobalPosition = GlobalPosition;
		shouldSpawn = true;
	}
}
