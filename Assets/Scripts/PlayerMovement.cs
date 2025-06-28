using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;
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
    }

    void FixedUpdate()
    {
        if (walking)
        {
            rb.linearVelocity = new Vector2(moveSpeed * Time.deltaTime, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if (jump) Jump();
    }

    void MicrophoneDetection()
    {
        loudness = detector.MicrophoneLoudness() * loudnessSensibility;

        if (loudness < threshold)
        {
            loudness = 0;
            //Debug.Log("No Voice input");
        }

        if (loudness > yellingThreshold && canJump)
        {
            jump = true;
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
        //print("JUMP!!!");
        canJump = false;
        rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse); ;
        jump = false;
    }
    void AudioSampleDetection()
    {
        float loudness = detector.GetLoudness(source.timeSamples, source.clip);
        // lerp value between min and max
        transform.localScale = Vector2.Lerp(minScale, maxScale, loudness);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") canJump = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") canJump = false;
    }

}
