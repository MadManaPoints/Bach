using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource source;
    public Vector2 minScale, maxScale;
    public AudioLoudnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;
    public Image voiceInputFill;
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
            Debug.Log("No Voice input");
        }
        // lerp value between min and max
        transform.localScale = Vector2.Lerp(minScale, maxScale, loudness);

        // update the UI fill amount
        voiceInputFill.fillAmount = loudness / 100f;
    }
    void AudioSampleDetection()
    {
        float loudness = detector.GetLoudness(source.timeSamples, source.clip);
        // lerp value between min and max
        transform.localScale = Vector2.Lerp(minScale, maxScale, loudness);
    }
 
}
