using UnityEngine;

public class SoundEFManager : MonoBehaviour
{
    public static SoundEFManager instance;
    public AudioClip hitClip;
    public AudioClip flameClip;
    public AudioClip jumpClip;
    public AudioClip loseClip;
    public AudioClip winClip;
    public AudioClip moveClip;
    public AudioClip mountainClip;
    public AudioClip healthClip;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    public void PlaySoundEffect(string filter)
    {
        AudioClip audioClip = null;

        if(filter=="hit")
        {
            audioClip = hitClip;
        }
        else if(filter=="flame")
        {
            audioClip = flameClip;
        }
        else if(filter=="jump")
        {
            audioClip = jumpClip;
        }
        else if(filter=="lose")
        {
            audioClip = loseClip;
        }
        else if(filter=="win")
        {
            audioClip = winClip;
        }
        else if(filter=="move")
        {
            audioClip = moveClip;
        }
        else if(filter=="mountain")
        {
            audioClip = mountainClip;
        }
        else if(filter=="health")
        {
            audioClip = healthClip;
        }

        if(audioClip!=null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogError("AudioClip is null!");
        }
    }
}
