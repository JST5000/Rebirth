using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject player; //stores a rverence to the player game object

    private Vector3 offset;  //stores the offset distance between the player and the camera 


	// Use this for initialization
	void Start () {

        // Calculate and store the offset value by getting the distance 
        // between the player's positions and the camera's position
        offset = transform.position - player.transform.position;

	}
	
	// LateUpdate is called after Update each frame
	void LateUpdate () {

        // Set the position of the camera's position to be the same as the player's
        // but offset by the calculated offset distance
        transform.position = player.transform.position + offset; 

	}
}
