using UnityEngine;

public class EndTriger : MonoBehaviour {

	public GameManager gameManager;
	void OnTriggerEnter ()
	{
		gameManager.CompleteLevel();
	}
}


