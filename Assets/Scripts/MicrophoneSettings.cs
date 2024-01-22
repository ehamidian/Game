using UnityEngine;

public class MicrophoneSettings : MonoBehaviour
{
    private string[] devices;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public string mic1 = "";
    public string mic2 = "";

    void Start()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();

        InitializeMicrophone();
    }

    void InitializeMicrophone()
    {
        devices = Microphone.devices;

        if (devices.Length > 0)
        {

            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i].Contains("PnP"))
                {
                    mic1 = devices[i];
                    Debug.Log("Left Microphone:" + mic1);
                }
                else if (devices[i].Contains("USB Audio Device"))
                {
                    mic2 = devices[i];
                    Debug.Log("Right Microphone:" + mic2);
                }
            }

            audioSource1.clip = Microphone.Start(mic1, true, 10, AudioSettings.outputSampleRate);
            audioSource1.loop = true;
            audioSource1.mute = true;

            audioSource2.clip = Microphone.Start(mic2, true, 10, AudioSettings.outputSampleRate);
            audioSource2.loop = true;
            audioSource2.mute = true;

            while (!(Microphone.GetPosition(null) > 0)) { }

            audioSource1.Play();
            audioSource2.Play();

            AudioLoudnessDetection.Instance.audioSources = new AudioSource[] { audioSource1, audioSource2 };
            AudioLoudnessDetection.Instance.devices = new string[] { mic1, mic2 };
        }
        else
        {
            Debug.Log("No microphone detected");
        }
    }

    void OnDisable()
    {
        foreach(string mic in devices)
        {
            Microphone.End(mic);
        }
    }
}
