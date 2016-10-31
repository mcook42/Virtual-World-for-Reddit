/**PlayerController.cs
 * Author: Caleb Whitman
 * October 29, 2016
 * 
 * Moves the player character in a two dimensional plane by adding a force to the rigid body.
 */


using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	//Where the player is located right before they go inside.
	//This allows us to appear outside the same house we entered.
	private Vector3 outsidePosition;

	//The rigidbody of an object defines how physics interacts with the object. 
	//Applying a force to the rigidbody will cause the object to move.
	private Rigidbody rigidbody; 

	//speed of the player.
	//Setting this variable to public allows us to set its value out side of the script. 
	//To set the speed, go back to Unity, click on the player object, go to the inspector, and then go to the script section.
	public float speed;

	//Called upon instantiation.
	void Start () {
		//gameObject is the object we are working with. 
		//GetComponent<Rigidbody> grabs this components rigidbody.
		rigidbody = gameObject.GetComponent<Rigidbody> ();

		//Prevents the player from rotating. When this is false, the player will spin upon hitting anything.
		rigidbody.freezeRotation = true;
	}

	//FixedUpdate is called everytime the physics is updated.
	void FixedUpdate () {
		//If the user is pressing the up arrow key (or W), Input.GetAxis will return 1.
		//If the user is pressing the down arrow key (or D), Input.GetAxis will return -1;
		//If the user is not pressing anything, then 0 is returned.
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//Vector representing direction player will be moving.
		Vector3 directionVector=new Vector3 (moveHorizontal, 0, moveVertical);

		//applying our force to our rigidbody with a speed.
		rigidbody.velocity=directionVector * speed;

	}

	//sets the players outside position after they go inside.
	public void SetOutsidePosition(Vector3 position){
		outsidePosition = position;

	}

	//Resets the players position to the outside position.
	public void resetOutsidePosition()
	{
		rigidbody.position = outsidePosition;

	}
}
