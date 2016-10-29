using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    int health;

	// Use this for initialization
	void Start () {
        health = 50;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player" && coll.gameObject.GetComponent<PlayerController>().isAttacking==true)
        {
            Debug.Log("Ive been hit");
            health -= 10;
            Debug.Log(health);
            if (health<=0)
            {
                Destroy(gameObject);
            }
        }
    }
}
