using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    AudioManager audioManager;  

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager.PlaySFX(audioManager.UIHover);
    }
}
