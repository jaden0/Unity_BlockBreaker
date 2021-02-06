using UnityEngine;
public class Ball : MonoBehaviour
{

    [SerializeField] Paddle paddle1;
    bool hasStarted = false;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFaktor = 0.2f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float minSpeed = 1f;
    Vector2 paddleToBallVector;
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == false)
        {
            LockBall();
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidbody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBall()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
            {
            float currentSpeed = myRigidbody2D.velocity.magnitude; 
           
            Vector2 velocityTweak = new Vector2(Random.Range(-randomFaktor,randomFaktor)*currentSpeed/maxSpeed,Random.Range(-randomFaktor,randomFaktor)*currentSpeed/maxSpeed);
            myRigidbody2D.velocity += velocityTweak;
            //myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x * currentSpeed / myRigidbody2D.velocity.magnitude, myRigidbody2D.velocity.y * currentSpeed / myRigidbody2D.velocity.magnitude);
           
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);

        }
    }

    private void FixedUpdate()
    {
        float speedFactor = 1;
        float currentSpeed = myRigidbody2D.velocity.magnitude;
        Debug.Log(currentSpeed);
        if (currentSpeed <= minSpeed)
        {
            speedFactor = minSpeed / currentSpeed;
        }
        if (currentSpeed >= maxSpeed)
        {
            speedFactor = maxSpeed / currentSpeed;
        }

        Vector2 oldVelocity = myRigidbody2D.velocity;
        Vector2 newVelocity = new Vector2(oldVelocity.x * speedFactor, oldVelocity.y * speedFactor);

        myRigidbody2D.velocity = newVelocity;
    }

}
