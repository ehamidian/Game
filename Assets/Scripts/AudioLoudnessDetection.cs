using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    private string leftMicrophoneName;
    private string rightMicrophoneName;
    private float lastUpdateTime;
    private AudioClip leftMicrophoneClip;
    private AudioClip rightMicrophoneClip;

    public const int sampleWindow = 32;
    public const float loudnessThreshold = 0.1f;
    public const float updateDelay = 2f; // Delay in seconds before checking for direction

    void Start()
    {
        InitializeMicrophones();
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

    private void MicrophoneToAudioClip(string microphoneName, out AudioClip microphoneClip)
    {
        microphoneClip = Microphone.Start(microphoneName, true, 10, AudioSettings.outputSampleRate);
    }

    public int GetDirection()
    {
        float leftLoudness = GetLoudness(leftMicrophoneName, leftMicrophoneClip);
        float rightLoudness = GetLoudness(rightMicrophoneName, rightMicrophoneClip);

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

    public void InitializeMicrophones()
    {
        string[] microphones = Microphone.devices;

        if (microphones.Length >= 2)
        {
            leftMicrophoneName = microphones[0];
            rightMicrophoneName = microphones[1];

            foreach (var microphone in microphones)
            {
                Debug.Log($"Mic {microphone} is available");
            }

            MicrophoneToAudioClip(leftMicrophoneName, out leftMicrophoneClip);
            MicrophoneToAudioClip(rightMicrophoneName, out rightMicrophoneClip);
        }
        else
        {
            Debug.LogError("Not enough microphones available.");
        }
    }
    public AudioClip GetLeftMicrophoneClip()
    {
        return leftMicrophoneClip;
    }

    public AudioClip GetRightMicrophoneClip()
    {
        return rightMicrophoneClip;
    }
}
