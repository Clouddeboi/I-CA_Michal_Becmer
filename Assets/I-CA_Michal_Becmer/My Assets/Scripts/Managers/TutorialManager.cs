using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TMP_Text subtitleText;//Reference to the UI Text for subtitles
    [SerializeField] private AudioManager audioManager;//Reference to the AudioManager

    //[SerializeField] private InputActionAsset inputActions;//Reference to the Input Actions Asset

    /*
        Note:
        This doesn't feel like an efficient way to do this but I have never codded a tutorial before,
        I would prefer to maybe activate a script whenever we get to a certain voiceline 
        but that wasn't working for me.
        If it works don't fix it i suppose lmao.
    */
    [System.Serializable]
    public class TutorialStep
    {
        public AudioClip voiceLine;//Voice line to play
        public string subtitleText;//Subtitle to display
        public bool requiresInput;//Bool to check if this step requires input to proceed
        public InputAction inputAction;//The input action to trigger for this step
    }

    [Header("Tutorial Steps")]
    //Array of tutorial steps, each step has a voiceline and subtitles
    [SerializeField] private TutorialStep[] tutorialSteps;

    private int currentStepIndex = 0;

    private void Awake()
    {
        //Initialize all the input actions from the asset
        foreach (var step in tutorialSteps)
        {
            //Enable input actions
            step.inputAction?.Enable();
        }
    }

    private void Start()
    {
        if (tutorialSteps.Length > 0)
        {
            //Start the tutorial
            StartCoroutine(PlayTutorial());
        }
        else
        {
            //Warning if no tutorial steps are set
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

            //If this step requires input, wait for it
            if (step.requiresInput && step.inputAction != null)
            {
                //Wait until the input is triggered
                yield return new WaitUntil(() => step.inputAction.triggered);
            }

            //Wait 1 second before the next step
            yield return new WaitForSeconds(1f);

            //Move to the next step
            currentStepIndex++;
        }

        //Log when the tutorial is completed
        Debug.Log("Tutorial finished!");
    }

    private void OnDisable()
    {
        //Disable the input actions when the tutorial ends or the game is closed
        foreach (var step in tutorialSteps)
        {
            //Disable input actions
            step.inputAction?.Disable();
        }
    }
}
