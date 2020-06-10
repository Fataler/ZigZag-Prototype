using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 8;

    private bool isRunnging = false;

    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isRunnging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector3.right * speed;
                isRunnging = true;
                GameController.instance.GameStarted();
            }
        }

        if (Input.GetMouseButtonDown(0) && !GameController.instance.GameOver)
        {
            SwitchDirection();
            isRunnging = true;
        }

        if (!Physics.Raycast(transform.position, Vector3.down, 5f))
        {
            rb.velocity = new Vector3(rb.velocity.x, -25f, rb.velocity.z);
            GameController.instance.GameOver = true;
        }
        //check for restart
        if (transform.position.y < -15f)
        {
            GameController.instance.Reset();
        }
    }

    private void SwitchDirection()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
    }
}