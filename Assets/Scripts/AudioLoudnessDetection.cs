using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public static AudioLoudnessDetection Instance;
    public AudioSource[] audioSources;
    public string[] devices;
    public int audioLoudness = 0;

    private float leftLoudness = 0f;
    private float rightLoudness = 0f;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (audioSources == null)
            Debug.LogError("AudioSources are not set!");
    }

    private void Update()
    {
        leftLoudness = GetLoudness(devices[0], audioSources[0].clip);
        rightLoudness = GetLoudness(devices[1], audioSources[1].clip);
        audioLoudness = GetDirection(leftLoudness, rightLoudness);
    }

    public int GetDirection(float leftLoudness, float rightLoudness)
    {
        float loudnessThreshold1 = 0.1f;
        float loudnessThreshold2 = 0.2f;
        float loudnessThreshold3 = 0.3f;

        if (leftLoudness > loudnessThreshold1 && rightLoudness > loudnessThreshold1)
            return 0;

        // Left side is louder
        if (leftLoudness > rightLoudness + loudnessThreshold3)
            return 3;
        else if (leftLoudness > rightLoudness + loudnessThreshold2)
            return 2;
        else if (leftLoudness > rightLoudness + loudnessThreshold1)
            return 1;

        // Right side is louder
        if (rightLoudness > leftLoudness + loudnessThreshold3)
            return 4;
        else if (rightLoudness > leftLoudness + loudnessThreshold2)
            return 5;
        else if (rightLoudness > leftLoudness + loudnessThreshold1)
            return 6;

        return 0;
    }

    private float GetLoudness(string microphoneName, AudioClip clip)
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(microphoneName), clip);
    }

    private float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int sampleWindow = 64;
        float totalLoudness = 0f;
        int startPosition = clipPosition - sampleWindow;
        float[] waveData = new float[sampleWindow];

        if (startPosition < 0)
            return 0;

        clip.GetData(waveData, startPosition);


        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
