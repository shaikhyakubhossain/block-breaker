using Godot;

public partial class HealthBar : ProgressBar
{

	[Export] public bool showWhenFull = false;
	private IHasHealth nodeWithHealthBar;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		nodeWithHealthBar = GetParent<IHasHealth>();
		MaxValue = nodeWithHealthBar.health;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!showWhenFull && Value == MaxValue){
			Visible = false;
		}
		else{
			Visible = true;
		}
		Value = nodeWithHealthBar.health;
	}
}
