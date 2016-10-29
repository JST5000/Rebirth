using UnityEngine;
using System.Collections;

public class BehaviorAI : MonoBehaviour {

    [Range(0, 1000)]
    public float speed;

    [Range(1, 10000)]
    public float awarenessRadius;

    [Range(0, 1000)]
    public float power;

    public string typeOfCreature;

    public string[] typesOfFood;

    private GameObject[] nearbyEntities;

    private GameObject firstHostile;

    private GameObject firstFleeFrom;
    
    private GameObject current;

    private string behavior;

    private int hp;


    private Vector3 direction;

    private float timeSinceLastPathing = 0;

    // Use this for initialization
    void Start () {
        behavior = "Idle";
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
        float resetTime = 1;
        if (Equals(behavior, "Idle"))
        {
            if (timeSinceLastPathing > resetTime)
                direction = GetIdlePath();
            firstFleeFrom = null;
            firstHostile = null;
        }
        else if (Equals(behavior, "Hostile"))
        {
            if (timeSinceLastPathing > resetTime)
                direction = GetHostilePath();
            firstFleeFrom = null;
        }
        else if (Equals(behavior, "Flee"))
        {
            if (timeSinceLastPathing > resetTime)
                direction = GetFleePath();
            firstHostile = null;
        }
        gameObject.transform.Translate(direction * speed * Time.deltaTime);
        if (timeSinceLastPathing > 1)
        {
            timeSinceLastPathing = 0;
        }
    }

    Vector3 GetIdlePath()
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
        return new Vector3(x, y);
    }

    Vector3 GetHostilePath()
    {
        float angle = Mathf.Atan((firstHostile.transform.position.y - gameObject.transform.position.y) /
                (firstHostile.transform.position.x - gameObject.transform.position.y));
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        return new Vector3(x, y);
    }

    Vector3 GetFleePath()
    {
        float angle = -Mathf.Atan((firstFleeFrom.transform.position.y - gameObject.transform.position.y) /
                (firstFleeFrom.transform.position.x - gameObject.transform.position.y));
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        return new Vector3(x, y);
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
