
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    //configuration parameters
    [SerializeField] PaddleScript paddle1;
    [SerializeField] float xPush = 45f;
    [SerializeField] float yPush = 60f;
    [SerializeField] float randomFactor = 0.6f;
    // Start is called before the first frame update
    //state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    //cached component referances
    AudioSource myAudioSourse;
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSourse = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }
    private void LaunchOnMouseClick()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        } 
        
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f,randomFactor),Random.Range(0f,randomFactor));
        if(hasStarted)
        {
            myAudioSourse.Play();
            myRigidBody2D.velocity += velocityTweak;
        }
    }
} 
