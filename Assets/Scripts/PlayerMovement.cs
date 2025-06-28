using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public AudioSource source;
    public Vector2 minScale, maxScale;
    public AudioLoudnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MicrophoneDetection();
    }

    void MicrophoneDetection()
    { 
        float loudness = detector.MicrophoneLoudness() * loudnessSensibility;

        if (loudness < threshold)
        {
            loudness = 0;
        }
        // lerp value between min and max
        transform.localScale = Vector2.Lerp(minScale, maxScale, loudness); 
    }
    void AudioSampleDetection()
    { 
       float loudness = detector.GetLoudness(source.timeSamples, source.clip);
        // lerp value between min and max
        transform.localScale = Vector2.Lerp(minScale, maxScale, loudness); 
    }
}
