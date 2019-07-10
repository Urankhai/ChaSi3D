using UnityEngine;

// all the information is taken from http://devassets.com/

public class playerMovement : MonoBehaviour {

	public Rigidbody rb;
	
	public float forwardForce = 800f;
	public float sidewaysForce = 80f;
	
	// Update is called once per frame
	// for physics always use fixupdate
	void FixedUpdate () 
	{
		rb.AddForce(0, 0, forwardForce * Time.deltaTime);

		if ( Input.GetKey("d") )
		{
			rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
		}

		if ( Input.GetKey("a") )
		{
			rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
		}

		if (rb.position.y < -1f)
		{
			FindObjectOfType<GameManager>().EndGame(); 
		}
	}
}
