using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	bool GameHasEnded = false;
	public float restartDelay = 1f;

	public GameObject completeLevelUI;

	public void CompleteLevel ()
	{
		//Debug.Log ("LEVEL WON!");
		completeLevelUI.SetActive(true);
	}

	public void EndGame ()
	{
		if (GameHasEnded == false)
		{
			GameHasEnded = true;
			Debug.Log("GAME OVER");
			//Restart(); //Instead we wait for the next step
			Invoke("Restart", restartDelay);
		}
	}

	void Restart ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	}

	
}
