using Godot;
using System;
using System.Threading.Tasks;

public class Methods
{

    public async void DelayedFunction(Action RunDelayedFunction, int delayMilliseconds)
	{
		
		await Task.Delay(delayMilliseconds);
			RunDelayedFunction();
			
	}

	public void detectCollisions(Area2D detectionArea, Node classInstance, string bodyEnteredMethod){
		// GD.Print(classInstance);
		detectionArea.Connect("body_entered", new Callable(classInstance, bodyEnteredMethod));

	}
}