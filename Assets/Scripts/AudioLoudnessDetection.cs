using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int GetDirection(string mic1, string mic2, AudioClip audioClipLeft, AudioClip audioClipRight)
    {
        float loudnessThreshold = 0.1f;

        if (audioClipLeft == null || audioClipRight == null)
        {
            Debug.LogError("Something is wrong with the microphones!");
            return 0;
        }

        float leftLoudness = GetLoudness(mic1, audioClipLeft);
        float rightLoudness = GetLoudness(mic2, audioClipRight);


        float dynamicLoudness = Mathf.Max(leftLoudness, rightLoudness);


        if (dynamicLoudness > loudnessThreshold)
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
