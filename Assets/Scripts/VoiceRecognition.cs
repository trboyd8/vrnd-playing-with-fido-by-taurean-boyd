using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour {

    private KeywordRecognizer recognizer;
    private string[] keywords;
    public DogController dogController;

    private void Start()
    {
        keywords = new string[]
        {
            "Speak",
            "Dead",
            "Lay",
            "Sit",
            "Come",
            "Follow",
            "Stop"
        };
        recognizer = new KeywordRecognizer(keywords);
        recognizer.OnPhraseRecognized += OnPhraseRecognized;
        recognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        switch (args.text)
        {
            case "Speak":
                dogController.Speak();
                break;
            case "Dead":
                dogController.Dead();
                break;
            case "Lay":
                dogController.Lay();
                break;
            case "Sit":
                dogController.Sit();
                break;
            case "Come":
                dogController.Come();
                break;
            case "Follow":
                dogController.Follow();
                break;
            case "Stop":
                dogController.Stop();
                break;
        }
    }
}
