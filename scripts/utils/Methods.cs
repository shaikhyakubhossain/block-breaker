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

	public void addSignalForBodyEntered(Area2D detectionArea, Node classInstance, string bodyEnteredMethod){
		// GD.Print(classInstance.Name);
		detectionArea.Connect("body_entered", new Callable(classInstance, bodyEnteredMethod));

	}

	public void removeSignalForBodyEntered(Area2D detectionArea, Node classInstance, string bodyEnteredMethod){
		// GD.Print(classInstance.Name);
		detectionArea.Disconnect("body_entered", new Callable(classInstance, bodyEnteredMethod));

	}

	public void changeScene(Node node, string sceneName){
		node.GetTree().ChangeSceneToFile(sceneName);
	}

	public void handleFallDown(CharacterBody2D body, Action whenFallDown){
		
	}

}