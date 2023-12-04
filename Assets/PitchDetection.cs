<<<<<<< HEAD
using UnityEngine;

public class PitchDetection : MonoBehaviour
{
    private float lastUpdateTime;
    private AudioSource leftAudioSource;

    public float pitchThreshold = 1000f;
    public float updateDelay = 0.1f; // Delay in seconds before checking for direction
    public MicrophoneSettings MicrophoneSettings;

    void Start()
    {
        // Access microphone settings from MicrophoneSettings
        leftAudioSource = MicrophoneSettings.LeftAudioSource;
    }

    void Update()
    {
        // Check for update delay
        if (Time.time - lastUpdateTime < updateDelay)
            return;

        float pitch = GetPitch();

        // Debug information
        //Debug.Log($"Pitch: {pitch}");

        if (pitch > pitchThreshold)
        {
            Debug.Log("Jump!");
            Jump();
        }
        else
        {
            Debug.Log("No movement!");
        }

        // Update last update time
        lastUpdateTime = Time.time;
    }

    public int Jump()
    {
        float pitch = GetPitch();

        if (pitch > pitchThreshold)
        {
            return 1;
        }

        return 0;
    }

    float GetPitch()
    {
        float[] spectrumData = new float[1024];
        leftAudioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Hamming);

        float maxAmplitude = 0f;
        int maxIndex = 0;

        for (int i = 0; i < spectrumData.Length; i++)
        {
            if (spectrumData[i] > maxAmplitude)
            {
                maxAmplitude = spectrumData[i];
                maxIndex = i;
            }
        }

        float pitch = maxIndex * AudioSettings.outputSampleRate / 2 / spectrumData.Length;

        //Debug.Log($"Max Amplitude: {maxAmplitude}, Max Index: {maxIndex}, Pitch: {pitch}");
        return pitch;
    }
}
=======
using UnityEngine;

public class PitchDetection : MonoBehaviour
{
    private float lastUpdateTime;
    private AudioSource leftAudioSource;

    public float pitchThreshold = 1000f;
    public float updateDelay = 0.1f; // Delay in seconds before checking for direction
    public MicrophoneSettings MicrophoneSettings;

    void Start()
    {
        // Access microphone settings from MicrophoneSettings
        leftAudioSource = MicrophoneSettings.LeftAudioSource;
    }

    void Update()
    {
        // Check for update delay
        if (Time.time - lastUpdateTime < updateDelay)
            return;

        float pitch = GetPitch();

        // Debug information
        Debug.Log($"Pitch: {pitch}");

        if (pitch > pitchThreshold)
        {
            Debug.Log("Jump!");
            Jump();
        }
        else
        {
            Debug.Log("No movement!");
        }

        // Update last update time
        lastUpdateTime = Time.time;
    }

    public int Jump()
    {
        float pitch = GetPitch();

        if (pitch > pitchThreshold)
        {
            return 1;
        }

        return 0;
    }

    float GetPitch()
    {
        float[] spectrumData = new float[1024];
        leftAudioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Hamming);

        float maxAmplitude = 0f;
        int maxIndex = 0;

        for (int i = 0; i < spectrumData.Length; i++)
        {
            if (spectrumData[i] > maxAmplitude)
            {
                maxAmplitude = spectrumData[i];
                maxIndex = i;
            }
        }

        float pitch = maxIndex * AudioSettings.outputSampleRate / 2 / spectrumData.Length;

        //Debug.Log($"Max Amplitude: {maxAmplitude}, Max Index: {maxIndex}, Pitch: {pitch}");
        return pitch;
    }
}
>>>>>>> e0ffee059ea6b266d29539e6e92285809074c5b5
