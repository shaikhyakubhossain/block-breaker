using Godot;

public partial class HealthBar : ProgressBar
{

	private IHasHealth nodeWithHealthBar;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		nodeWithHealthBar = GetParent<IHasHealth>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Value = nodeWithHealthBar.health;
	}
}
