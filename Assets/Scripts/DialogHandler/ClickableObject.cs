using UnityEngine;
using TMPro;
using System.Collections;

public class ClickableObject : MonoBehaviour
{
    public enum ObjectType
    {
        Bed, WashingMachine, DresserFlower, Toilet, BathroomSink, CoatRack, Blender, TrashCan,
        Chair, TV, Skeleton
    }

    [Header("Settings")]
    public ObjectType objectType;

    [Header("References")]
    public DialogueHandler dialogueHandler;
    public GameObject mainGameObject; // parent container for UI
    public TMP_Text textBox;

    private Coroutine currentCoroutine;

    void Start()
    {
        if (mainGameObject != null) mainGameObject.SetActive(false);
        if (textBox != null) textBox.gameObject.SetActive(false);

        if (dialogueHandler == null)
            dialogueHandler = FindObjectOfType<DialogueHandler>();
    }

    public void OnObjectClicked()
    {
        if (dialogueHandler == null || textBox == null || mainGameObject == null)
        {
            Debug.LogWarning($"[ClickableObject] Missing references on {gameObject.name}");
            return;
        }

        // Stop any running dialogue
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        StopAllCoroutines();
        dialogueHandler.StopAllCoroutines();

        mainGameObject.SetActive(true);
        textBox.gameObject.SetActive(true);

        string description = GetObjectDescription(objectType);

        dialogueHandler.textToSpeak = description;
        dialogueHandler.textBox = textBox.gameObject;

        currentCoroutine = StartCoroutine(ShowTextAndClose());
    }


    IEnumerator ShowTextAndClose()
    {
        // Start the dialogue typing
        yield return StartCoroutine(dialogueHandler.NormalDisplayText());

        // Wait for text to finish typing before hiding
        yield return new WaitForSeconds(2f);

        if (mainGameObject != null)
            mainGameObject.SetActive(false);
        if (textBox != null)
            textBox.gameObject.SetActive(false);
    }

    private string GetObjectDescription(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.Bed:
                return "The bed that gave you severe back pain.";
            case ObjectType.WashingMachine:
                return "It's a broken washing machine and a busted up dryer.";
            case ObjectType.DresserFlower:
                return "What unemployment does to humanity";
            case ObjectType.Toilet:
                return "Skibidi";
            case ObjectType.BathroomSink:
                return "There's a chance that water is dirty. Better not test it out.";
            case ObjectType.CoatRack:
                return "Size: XXL Men's Jacket from Florida's Best Leather. Made in Murica.";
            case ObjectType.Blender:
                return "A very resourceful 3-D modeling tool";
            case ObjectType.TrashCan:
                return "Every now an then I always look at this and remind myself of who I am.";
            case ObjectType.Chair:
                return "It smells worst than the toilet...";
            case ObjectType.TV:
                return "The news? Boooring!";
            case ObjectType.Skeleton:
                return "Pardon me, m'lady!";
            default:
                return "It’s something, but you can’t quite tell what.";
        }
    }
}
