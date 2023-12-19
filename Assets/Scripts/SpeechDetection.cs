using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechDetection : MonoBehaviour
{
    private MicrophoneSettings microphoneSettings;
    private string[] Microphones;
    private string selectedMicrophone1;
    private string selectedMicrophone2;
    private KeywordRecognizer keywordRecognizer;

    void Start()
    {
        microphoneSettings = GetComponent<MicrophoneSettings>();
        Microphones = microphoneSettings.Microphones;

        if (microphoneSettings != null)
        {
            selectedMicrophone1 = Microphones[0];
            selectedMicrophone2 = Microphones[1];
        }

        InitialKeyRecognition();
    }

    void InitialKeyRecognition()
    {
        string[] keywords = { "jump" };

        if (keywords == null)
        {
            Debug.LogError("No keys in the array keywords.");
            return;
        }
        else
        {
            Debug.Log($"The key is: {keywords[0]}");
        }

        keywordRecognizer = new KeywordRecognizer(keywords);

        if (keywordRecognizer == null)
        {
            Debug.LogError("KeywordRecognizer is null.");
            return;
        }

        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);

        if (speech.text == "jump")
        {
            // Call the function to make the object jump
            Jump();
        }
    }

    public void Jump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float jumpForce = 5f;

        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnDestroy()
    {
        // Stop the KeywordRecognizer when the script is destroyed or when it's no longer needed.
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
        }
        else
        {
            return;
        }
    }
}
