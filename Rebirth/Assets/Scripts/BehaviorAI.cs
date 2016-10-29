using UnityEngine;
using System.Collections;

public class BehaviorAI : MonoBehaviour {

    [Range(0, 1000)]
    public float speed;

    [Range(1, 10000)]
    public float awarenessRadius;

    private string typeOfCreature;

    private GameObject[] nearbyEntities;

    private GameObject firstHostile;

    private GameObject firstFleeFrom;
    
    private GameObject current;

    private string behavior;

    private int power;

    private int hp;

    private string[] typesOfFood;

    private float timeSinceLastPathing = 0;

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
        bool nowFleeing = false;
        bool nowHostile = false;
        int[] tally = new int[2];
        nearbyEntities = GetGameObjectsWithin(awarenessRadius);
        for(int i = 0; i<nearbyEntities.GetLength(0); i++)
        {
            current = nearbyEntities[i];
            if(current.tag == "Alive")
            {
                string expected = CheckExpectedBehavior(current);
                if(Equals(expected, "Hostile"))
                {
                    firstHostile = current;
                }else if(Equals(expected, "Flee"))
                {
                    firstFleeFrom = current;
                    nowFleeing = true;
                }
            }
        }
        if(nowFleeing)
        {
            behavior = "Flee";
        } else if(nowHostile)
        {
            behavior = "Hostile";
        } else
        {
            behavior = "Idle";
        }
        Act();

	}

    string CheckExpectedBehavior(GameObject livingEntity)
    {
        if (power >= livingEntity.GetComponent<BehaviorAI>().power)
        {
            if (CanEat(gameObject, livingEntity))
            {
                return "Hostile";
            }
        }
        else if(CanEat(livingEntity, gameObject))
        {
            return "Flee";
        } else
        {
            return "Idle";
        }


        return null;
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
        timeSinceLastPathing += Time.deltaTime;
        if (timeSinceLastPathing > .1)
        {
            if (Equals(behavior, "Idle"))
            {
                float xRandom = Random.Range(.00001f, 1);
                float yRandom = Random.Range(.00001f, 1);
                float angle = Mathf.Atan(xRandom / yRandom);
                float x = Mathf.Cos(angle);
                float y = Mathf.Sin(angle);
                if (Random.value < .5)
                {
                    x *= -1;
                }
                if (Random.value < .5)
                {
                    y *= -1;
                }


                gameObject.transform.Translate(new Vector3(x, y) * speed * Time.deltaTime);

            }
            else if (Equals(behavior, "Hostile"))
            {
                float angle = Mathf.Atan((firstHostile.transform.position.y - gameObject.transform.position.y) /
                    (firstHostile.transform.position.x - gameObject.transform.position.y));
                float x = Mathf.Cos(angle);
                float y = Mathf.Sin(angle);
                gameObject.transform.Translate(new Vector3(x, y) * speed * Time.deltaTime);

            }
            else if (Equals(behavior, "Flee"))
            {
                float angle = -Mathf.Atan((firstHostile.transform.position.y - gameObject.transform.position.y) /
                    (firstHostile.transform.position.x - gameObject.transform.position.y));
                float x = Mathf.Cos(angle);
                float y = Mathf.Sin(angle);
                gameObject.transform.Translate(new Vector3(x, y) * speed * Time.deltaTime);
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
