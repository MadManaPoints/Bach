using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    public AudioClip micClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MicrophoneToAudioClip()
    {
        //grab first mic from device list
        string micName = Microphone.devices[0];
        micClip = Microphone.Start(micName,true,20,AudioSettings.outputSampleRate);
    }

    public float MicrophoneLoudness()
    {
        return GetLoudness(Microphone.GetPosition(Microphone.devices[0]), micClip);
    }

    public float GetLoudness(int clipsPos, AudioClip clip)
    {
        int start = clipsPos - sampleWindow;

        if (start < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, start);

        //compute loudness
        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }
}
