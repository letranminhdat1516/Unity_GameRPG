using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public Animator myAnim;
    public static PlayerController instance;// instance for player 
    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 TopRightLimit;
    
    public bool canMove;
    
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);// Destroy xN player
            }

        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
      
        if (canMove)
        {
            // Setup moving with input setting "Horizontal" & "Vertical"
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
        // Set position moving wwith animator when u created 2 float in animator
        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        // Set position idle
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }

        }
        // Control player in map 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, TopRightLimit.x),
        Mathf.Clamp(transform.position.y, bottomLeftLimit.y, TopRightLimit.y),
        transform.position.z);
    }
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
        TopRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }
}
