using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class EndingsVoicelinesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text subtitleText;//Reference to the UI Text for subtitles
    [SerializeField] private AudioManager audioManager;//Reference to the AudioManager

    [System.Serializable]
    public class EndGameStep
    {
        public AudioClip voiceLine;//Voice line to play
        public string subtitleText;//Subtitle to display
    }

    [Header("Tutorial Steps")]
    //Array of tutorial steps, each step has a voiceline and subtitles
    [SerializeField] private EndGameStep[] endGameSteps;

    private int currentStepIndex = 0;

    public IEnumerator PlayEndGame()
    {
        while (currentStepIndex < endGameSteps.Length)
        {
            EndGameStep step = endGameSteps[currentStepIndex];

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

            //Wait 1 second before the next step
            yield return new WaitForSeconds(1f);

            //Move to the next step
            currentStepIndex++;
        }

        //Log when the tutorial is completed
        Debug.Log("Ending finished!");
    }

}
