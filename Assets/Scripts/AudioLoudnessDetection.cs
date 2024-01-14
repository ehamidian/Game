using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public float loudnessThreshold = 0.2f;
    public int GetDirection(string mic1, string mic2, AudioClip audioClipLeft, AudioClip audioClipRight)
    {

        if (audioClipLeft && audioClipRight)
        {
            float leftLoudness = GetLoudness(mic1, audioClipLeft);
            float rightLoudness = GetLoudness(mic2, audioClipRight);

            if(leftLoudness >  loudnessThreshold && rightLoudness> loudnessThreshold)
            {
                return 0;
            }
            else if (leftLoudness > rightLoudness + loudnessThreshold)
            {
                // Only left side has loudness
                return -1;
            }
            else if (rightLoudness > leftLoudness + loudnessThreshold)
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
        else
        {
            Debug.LogError("Microphones not assigned!");
            return 0;
        }
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
