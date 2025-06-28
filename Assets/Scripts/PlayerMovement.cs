using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;
    public float playerHeight;
    public LayerMask daGround;
    bool onGround;
    Rigidbody2D rb;
    public AudioSource source;
    public Vector2 minScale, maxScale;
    public AudioLoudnessDetection detector;
    public float loudness;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;
    [SerializeField] float yellingThreshold = 4.0f;
    [SerializeField] float moveSpeed = 100.0f;
    [SerializeField] float jumpForce = 500.0f;

    bool walking, canJump = true, jump;
    public Image voiceInputFill;
    float buffer = 1.0f;

    void Awake()
    {
        player = this;
    }
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MicrophoneDetection();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, playerHeight + 0.2f, daGround);
        if (hit)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (!onGround) jump = true;
        if (jump && onGround)
        {
            canJump = true;
            jump = false;
        }
    }

    void FixedUpdate()
    {
        if (walking)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void MicrophoneDetection()
    {
        loudness = detector.MicrophoneLoudness() * loudnessSensibility;

        if (loudness < threshold)
        {
            loudness = 0;
            //Debug.Log("No Voice input");
        }

        if (loudness > yellingThreshold && canJump && onGround)
        {
            Jump();
        }
        else if (loudness >= threshold)
        {
            //print("WALKING");
            walking = true;
        }
        else
        {
            //print("NADA");
            walking = false;
        }

        //print(canJump);
        // lerp value between min and max
        //transform.localScale = Vector2.Lerp(minScale, maxScale, loudness);
    }

    void Jump()
    {
        canJump = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    void AudioSampleDetection()
    {
        float loudness = detector.GetLoudness(source.timeSamples, source.clip);
        // lerp value between min and max
        transform.localScale = Vector2.Lerp(minScale, maxScale, loudness);

    }
}
