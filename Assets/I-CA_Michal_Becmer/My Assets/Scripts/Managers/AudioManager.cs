using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //We define our audio sources and clips
    [Header("---Audio Sources---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("---Audio Clips---")]
    public AudioClip backgroundMusic;   
    public AudioClip SwitchToPhotoMode;
    public AudioClip TakePhoto;
    public AudioClip UIHover;
    public AudioClip Notification;

    [Header("---Tutorial Voice Over---")]
    //These are reusable voicelines
    public AudioClip Hello;
    public AudioClip What;
    public AudioClip Excellent;

    //These are in order
    public AudioClip IsThisOn;
    public AudioClip HereWeGo;
    public AudioClip Welcome;
    public AudioClip YouHaveBeen;
    public AudioClip WhyDoWeNot;
    public AudioClip JimNASA;
    public AudioClip WhySwitch;
    public AudioClip NextMeeting;
    public AudioClip Anyways;
    public AudioClip PressWAndS;
    public AudioClip Traversal;
    public AudioClip Photo;
    public AudioClip Tab;
    public AudioClip Track;
    public AudioClip InterstellarReference;//Hehehehe
    public AudioClip SafeTravels;
    public AudioClip OneMorething;
    public AudioClip Problems;
    public AudioClip FaultySpaceCraft;
    public AudioClip HoustonOut;

    [Header("---End Game Voice Over---")]
    public AudioClip ExcellentJob;
    public AudioClip WeKnew;
    public AudioClip ImAbout;
    public AudioClip AhYes;
    public AudioClip Sandwich;
    public AudioClip PeanutButter;
    public AudioClip ILike;
    public AudioClip ReturnHome;
    public AudioClip ThanksForPlaying;

    //When the game starts we play the background music
    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    //We call this method when we want to play an audio clip
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}