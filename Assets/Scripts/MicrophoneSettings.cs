using UnityEngine;

public class MicrophoneSettings : MonoBehaviour
{
    public string[] Microphones;
    public AudioSource audioSourceLeft;
    public AudioSource audioSourceRight;

    void Start()
    {
        //audioSource = gameObject.AddComponent<AudioSource>();
        InitialMicrophone();
    }

    // Get two separate microphones
    public void InitialMicrophone()
    {
        Microphones = Microphone.devices;

        if (Microphones.Length > 0)
        {
            audioSourceLeft = gameObject.AddComponent<AudioSource>();
            audioSourceLeft.clip = Microphone.Start(Microphones[0], true, 10, AudioSettings.outputSampleRate);
            audioSourceLeft.loop = true;
            audioSourceLeft.mute = true;

            audioSourceRight = gameObject.AddComponent<AudioSource>();
            audioSourceRight.clip = Microphone.Start(Microphones[1], true, 10, AudioSettings.outputSampleRate);
            audioSourceRight.loop = true;
            audioSourceRight.mute = true;

            while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the microphone has started

            audioSourceLeft.Play();
            audioSourceRight.Play();

            for (int i = 0; i < Microphones.Length; i++)
                Debug.Log($"Mic {i}: {Microphones[i]} is available!");

        }
        else
        {
            Debug.LogError("No microphone is available!");
        }
    }
}
