using UnityEngine;
using System.Collections;

public class BehaviorAI : MonoBehaviour {

    public string behavior;

    public float awarenessRadius;

    private GameObject[] nearbyEntities;
    
    private GameObject current;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        nearbyEntities = GetGameObjectsWithin(awarenessRadius);
        for(int i = 0; i<nearbyEntities.GetLength(0); i++)
        {
            current = nearbyEntities[i];
            if(current.GetComponent<"BehaviorAI"> == 1)
            {

            }
        }

	}

    GameObject[] GetGameObjectsWithin(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
        GameObject[] entitiesWithin = new GameObject[colliders.GetLength(0)];
        for (int i = 0; i < colliders.GetLength(0); i++)
        {
            entitiesWithin[i] = colliders[i].transform.parent.gameObject;
        }
        return entitiesWithin;
    }
}
