using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    private float lastUpdateTime;
    private int lastDirection = 2;
    private readonly int sampleWindow = 64;
    private MicrophoneSettings microphoneSettings;
    private string[] Microphones;

    private AudioSource audioSourceLeft;
    private AudioSource audioSourceRight;

    public float loudnessThreshold = 0.1f;
    public float updateDelay = 2f; // Delay in seconds before checking for direction

    void Start()
    {
        microphoneSettings = GetComponent<MicrophoneSettings>();

        if (microphoneSettings != null)
        {
            Microphones = microphoneSettings.Microphones;
            audioSourceLeft = microphoneSettings.audioSourceLeft;
            audioSourceRight = microphoneSettings.audioSourceRight;
        }

        lastUpdateTime = Time.time;
    }

    void Update()
    {
        // Check for update delay
        if (Time.time - lastUpdateTime < updateDelay)
            return;

        int currentDirection = GetDirection();

        if (currentDirection != lastDirection)
        {
            // Direction has changed, perform actions
            lastDirection = currentDirection;

            // Perform actions based on direction
            if (currentDirection == -1)
            {
                // Move left
                Debug.Log("Move to the left");
            }
            else if (currentDirection == 1)
            {
                // Move right
                Debug.Log("Move to the right");
            }
            else
            {
                // No movement
                Debug.Log("Stable");
            }
        }

        // Update last update time
        lastUpdateTime = Time.time;
    }

    public int GetDirection()
    {
        float leftLoudness = GetLoudness(Microphones[0], audioSourceLeft.clip);
        float rightLoudness = GetLoudness(Microphones[1], audioSourceRight.clip);


        float dynamicThreshold = Mathf.Max(leftLoudness, rightLoudness);

        if (dynamicThreshold > loudnessThreshold)
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
        float totalLoudness = 0;
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
