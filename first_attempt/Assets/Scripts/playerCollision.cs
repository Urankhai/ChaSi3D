using UnityEngine;

public class playerCollision : MonoBehaviour {
public playerMovement movement;

	void OnCollisionEnter (Collision collisionInfo) 
	{
		
		if (collisionInfo.collider.tag == "Obstacle")
		{
			//Debug.Log("We hit the obstacle!");
			movement.enabled = false;
			//Debug.Log(collisionInfo.collider.name);

			FindObjectOfType<GameManager>().EndGame();
		} 
	}
}
