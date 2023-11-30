using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    private string[] microphones;
    private AudioClip leftMicrophoneClip;
    private AudioClip rightMicrophoneClip;
    private float lastUpdateTime;
    private readonly int sampleWindow = 32;

    public float loudnessThreshold = 0.1f;
    public float updateDelay = 2f; // Delay in seconds before checking for direction
    public MicrophoneSettings MicrophoneSettings;

    void Start()
    {
        microphones = MicrophoneSettings.Microphones;
        leftMicrophoneClip = MicrophoneSettings.LeftAudioSource.clip;
        rightMicrophoneClip = MicrophoneSettings.RightAudioSource.clip;

        lastUpdateTime = Time.time;
    }

    void Update()
    {
        // Check for update delay
        if (Time.time - lastUpdateTime < updateDelay)
            return;

        int direction = GetDirection();

        // Perform actions based on direction
        if (direction == -1)
        {
            // Move left
            Debug.Log("Move to the left");
        }
        else if (direction == 1)
        {
            // Move right
            Debug.Log("Move to the right");
        }
        else
        {
            // No movement
            Debug.Log("Stable");
        }

        // Update last update time
        lastUpdateTime = Time.time;
    }

    //private void MicrophoneToAudioClip(string microphoneName, out AudioClip microphoneClip)
    //{
    //    microphoneClip = Microphone.Start(microphoneName, true, 10, AudioSettings.outputSampleRate);
    //}

    public int GetDirection()
    {
        float leftLoudness = GetLoudness(microphones[0], leftMicrophoneClip);
        float rightLoudness = GetLoudness(microphones[1], rightMicrophoneClip);

        if (leftLoudness > loudnessThreshold && rightLoudness > loudnessThreshold)
        {
            // Both sides have loudness, choose the louder side
            return leftLoudness > rightLoudness ? -1 : 1;
        }
        else if (leftLoudness > loudnessThreshold)
        {
            // Only left side has loudness
            return -1;
        }
        else if (rightLoudness > loudnessThreshold)
        {
            // Only right side has loudness
            return 1;
        }
        else
        {
            // No movement
            return 0;
        }
    }

    private float GetLoudness(string microphoneName, AudioClip clip)
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(microphoneName), clip);
    }

    private float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        // Compute loudness
        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
