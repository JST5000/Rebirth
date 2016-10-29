using UnityEngine;
using System.Collections;

public class BehaviorAI : MonoBehaviour {

    [Range(10, 100)]
    public float speed;

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

            }
            Act();
        }

	}

    void AdaptBehavior(GameObject livingEntity)
    {
        if (power >= livingEntity.GetComponent<BehaviorAI>().power)
        {
            if (CanEat(gameObject, livingEntity))
            {
                behavior = "Hostile";
            }
        }
        else if(CanEat(livingEntity, gameObject))
        {
            behavior = "Flee";
        } else
        {
            behavior = "Idle";
        }
              

    }

    bool CanEat(GameObject attackingCreature, GameObject other)
    {
        string[] types = attackingCreature.GetComponent<BehaviorAI>().typesOfFood;
        for (int i = 0; i<types.GetLength(0); i++)
        {
            if(Equals(types[i], other.GetComponent<BehaviorAI>().typeOfCreature))
            {
                return true;
            }
        }
        return false;
    }

    void Act()
    {

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
