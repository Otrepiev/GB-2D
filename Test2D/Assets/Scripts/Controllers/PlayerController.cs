using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody2D playerRb;
    public float forceJump = 5.0f;

    private Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector2.up * forceJump,ForceMode2D.Impulse);
        }
        
        if (horizontalInput < 0 || horizontalInput > 0 )
        {
            playerAnim.SetBool("Walk", true);
        }
        else if (horizontalInput == 0)
        {
            playerAnim.SetBool("Walk", false);
        }

        if (horizontalInput < -0.25)
        {
            transform.rotation = new Quaternion(0,-180,0,0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
