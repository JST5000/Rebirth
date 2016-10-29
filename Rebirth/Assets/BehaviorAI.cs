using UnityEngine;
using System.Collections;

public class BehaviorAI : MonoBehaviour {



    public float awarenessRadius;

    private string typeOfCreature;

    private GameObject[] nearbyEntities;
    
    private GameObject current;

    private string behavior;

    private int power;

    private int hp;

    private string[] typesOfFood;

    // Use this for initialization
    void Start () {
        behavior = "Idle";
        typeOfCreature = "Animal";
        hp = 100;
        power = 100;
	}

    void initTypesOfFood()
    {
        typesOfFood = new string[2];
        typesOfFood[0] = "Plant";
        typesOfFood[1] = "Animal";
    }
	
	// Update is called once per frame
	void Update () {
        nearbyEntities = GetGameObjectsWithin(awarenessRadius);
        for(int i = 0; i<nearbyEntities.GetLength(0); i++)
        {
            current = nearbyEntities[i];
            if(current.tag == "Alive")
            {
                AdaptBehavior(current);



            } else if(Equals(current.tag, "Dead"))
            {

            }
        }

	}

    void AdaptBehavior(GameObject livingEntity)
    {
        if(power >= livingEntity.GetComponent<BehaviorAI>().power
            && CanEat(livingEntity))
        {
            behavior = "Hostile";
        }

    }

    bool CanEat(GameObject livingEntity)
    {
        for(int i = 0; i<typesOfFood.GetLength(0); i++)
        {
            if(Equals(typesOfFood[i], livingEntity.GetComponent<BehaviorAI>().typeOfCreature))
            {
                return true;
            }
        }
        return false;
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
