using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TMP_Text subtitleText;//Reference to the UI Text for subtitles
    [SerializeField] private AudioManager audioManager;//Reference to the AudioManager

    [System.Serializable]
    public class TutorialStep
    {
        public AudioClip voiceLine;//Voice line to play
        public string subtitleText;//Subtitle to display
    }

    [Header("Tutorial Steps")]
    //Array of tutorial steps, each step has a voiceline and subtitles
    [SerializeField] private TutorialStep[] tutorialSteps;

    private int currentStepIndex = 0;

    private void Start()
    {
        if (tutorialSteps.Length > 0)
        {
            StartCoroutine(PlayTutorial());
        }
        else
        {
            Debug.LogWarning("No tutorial steps found!");
        }
    }

    private IEnumerator PlayTutorial()
    {
        while (currentStepIndex < tutorialSteps.Length)
        {
            TutorialStep step = tutorialSteps[currentStepIndex];

            //Display the subtitle
            subtitleText.text = step.subtitleText;

            //Play the voice line
            if (step.voiceLine != null && audioManager != null)
            {
                audioManager.PlaySFX(step.voiceLine);

                //Wait until the audio finishes
                yield return new WaitForSeconds(step.voiceLine.length);
            }
            else
            {
                //In case we filled in the step incorrectly
                Debug.LogWarning($"Voice line or AudioManager missing for step {currentStepIndex}");
                //Default duration
                yield return new WaitForSeconds(2f);
            }

            //Clear the subtitle after this step
            subtitleText.text = "";
            
            //Wait 1 seconds before the next step
            yield return new WaitForSeconds(1f);

            //Move to the next step
            currentStepIndex++;
        }

        Debug.Log("Tutorial finished!");
    }
}
