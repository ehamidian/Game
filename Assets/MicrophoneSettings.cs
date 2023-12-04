<<<<<<< HEAD
using UnityEngine;

public class MicrophoneSettings : MonoBehaviour
{

    public string[] Microphones { get; private set; }
    public AudioSource LeftAudioSource { get; private set; }
    public AudioSource RightAudioSource { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Microphones = Microphone.devices;

        if (Microphones.Length >= 2)
        {
            InitializeMicrophones();

            // Left audio source
            LeftAudioSource = gameObject.AddComponent<AudioSource>();
            LeftAudioSource.clip = Microphone.Start(Microphones[0], true, 10, AudioSettings.outputSampleRate);
            LeftAudioSource.loop = true;

            // Right audio source
            RightAudioSource = gameObject.AddComponent<AudioSource>();
            RightAudioSource.clip = Microphone.Start(Microphones[1], true, 10, AudioSettings.outputSampleRate);
            RightAudioSource.loop = true;

            while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the microphone has started

            if (Microphone.GetPosition(null) > 0)
            {
                LeftAudioSource.Play();
                RightAudioSource.Play();
            }
            else
            {
                Debug.LogError("Microphone not started!");
            }
        }
    }

    void InitializeMicrophones()
    {
        foreach (var microphone in Microphones)
        {
            Debug.Log($"Mic {microphone} is available");
        }
    }
}
=======
using UnityEngine;

public class MicrophoneSettings : MonoBehaviour
{

    public string[] Microphones { get; private set; }
    public AudioSource LeftAudioSource { get; private set; }
    public AudioSource RightAudioSource { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Microphones = Microphone.devices;

        if (Microphones.Length >= 2)
        {
            InitializeMicrophones();

            // Left audio source
            LeftAudioSource = gameObject.AddComponent<AudioSource>();
            LeftAudioSource.clip = Microphone.Start(Microphones[0], true, 10, AudioSettings.outputSampleRate);
            LeftAudioSource.loop = true;

            // Right audio source
            RightAudioSource = gameObject.AddComponent<AudioSource>();
            RightAudioSource.clip = Microphone.Start(Microphones[1], true, 10, AudioSettings.outputSampleRate);
            RightAudioSource.loop = true;

            while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the microphone has started

            if (Microphone.GetPosition(null) > 0)
            {
                LeftAudioSource.Play();
                RightAudioSource.Play();
            }
            else
            {
                Debug.LogError("Microphone not started!");
            }
        }
    }

    void InitializeMicrophones()
    {
        foreach (var microphone in Microphones)
        {
            Debug.Log($"Mic {microphone} is available");
        }
    }
}
>>>>>>> e0ffee059ea6b266d29539e6e92285809074c5b5
