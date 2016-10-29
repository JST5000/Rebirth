using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
    Rigidbody2D rb;

    public float speed = 10.0f;
    public float strikeForce = 1000.0f;
    bool canAttack = true;
    public bool isAttacking = false;
    float attackTime = 0.0f;
    float hitTime = 0.0f;
    public float attackTimer = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        MoveHorizontal(Input.GetAxis("Horizontal"));
        MoveVertical(Input.GetAxis("Vertical"));

        if (Time.time-hitTime>= attackTimer)
        {
            isAttacking = false;
        }

        if (Input.GetButtonDown("Jump") && ((Time.time - attackTime) >= 2.0f))
        {
            isAttacking = true;
            hitTime = Time.time;
            Debug.Log(isAttacking);
            Attack(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")); // takes keyboard input and calls attack

        }
        

    }

    void Attack(float vert, float hori)
    {
        
        Vector2 direction = new Vector2(hori, vert);
        rb.AddForce(direction * strikeForce);
        attackTime = Time.time;
        ////canAttack = false;
    }

    void MoveHorizontal(float input)
    {
        Vector2 moveVe1 = rb.velocity;
        moveVe1.x = input * speed * Time.deltaTime;
        rb.velocity = moveVe1;
    }

    void MoveVertical(float input)
    {
        Vector2 moveVe1 = rb.velocity;
        moveVe1.y = input * speed * Time.deltaTime;
        rb.velocity = moveVe1;
    }
}
