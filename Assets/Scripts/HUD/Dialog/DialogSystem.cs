using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour {

    public static DialogSystem dialogSystem;

    private DialogTrigger D, dtg1, dtg2;

    private Queue<Dialog> sentences;

    float closePos;
    [Range(.1f, 1f)]
    public float transitionTime = .5f;

    [Range(1, 100)]
    public float typingSpeed = 50f;

    [Space]
    public Image speakerAvatar;

    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI speakerText;

    public Button NextButton;
    public Button buttonOne;
    public Button buttonTwo;

    TextMeshProUGUI speakerAnswer1;
    TextMeshProUGUI speakerAnswer2;

    private void Awake() {
        if (dialogSystem != null) {
            Destroy(gameObject);
            return;
        }
        else dialogSystem = this;

        sentences = new Queue<Dialog>();
        speakerAnswer1 = buttonOne.GetComponentInChildren<TextMeshProUGUI>();
        speakerAnswer2 = buttonTwo.GetComponentInChildren<TextMeshProUGUI>();

        buttonOne.onClick.AddListener(MakeChoiceOne);
        buttonTwo.onClick.AddListener(MakeChoiceTwo);
    }
    private void Start() {
        closePos = transform.localPosition.y;
    }
    public void StartConservation(DialogTrigger dtg) {
        //animator.SetBool("IsOpen", true);
        LeanTween.moveLocalY(gameObject, closePos - 200, transitionTime).setEaseInOutCubic();
        D = dtg;

        dtg1 = D.option1;
        dtg2 = D.option2;

        PlayerController.playerController.freeze = D.freeze;
        PlayerController.playerController.shotable = D.shotable;

        NextButton.interactable = true;
        buttonOne.interactable = false;
        buttonTwo.interactable = false;

        speakerAnswer1.text = "";
        speakerAnswer2.text = "";

        speakerName.text = D._name;

        sentences.Clear();

        foreach (Dialog dialog in D.sentences) {
            sentences.Enqueue(dialog);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence() {

        if (sentences.Count == 0) {
            if (D.isContinuated == true) {
                NextButton.interactable = false;
                if (D.answer1 != "" && D.option1 != null) buttonOne.interactable = true;
                if (D.answer2 != "" && D.option2 != null) buttonTwo.interactable = true;

                speakerAnswer1.text = D.answer1;
                speakerAnswer2.text = D.answer2;
            }
            else EndDialogue();
            return;
        }

        switch (sentences.Peek().face) {
            case Dialog.Face.happy:
                speakerAvatar.sprite = D.Happy;
                break;
            case Dialog.Face.scared:
                speakerAvatar.sprite = D.Scared;
                break;
            case Dialog.Face.sad:
                speakerAvatar.sprite = D.Sad;
                break;
        }
        if (sentences.Peek().cutScene != null) sentences.Peek().cutScene.StartCutScene();
        string _sentence = sentences.Dequeue().sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(_sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        bool color = false;
        string segment = "";

        speakerText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            if (letter == '>') {
                color = false;
                segment += letter;
                speakerText.text += segment;
                segment = "";
            }
            else if (letter == '<' || color) {
                color = true;
                segment += letter;
            }
            else if (letter == ',') {
                speakerText.text += letter;
                yield return new WaitForSeconds((1 / typingSpeed) * 5f);
            }
            else if (letter == '.') {
                speakerText.text += letter;
                yield return new WaitForSeconds((1 / typingSpeed) * 10f);
            }
            else if (letter == '-') {
                speakerText.text += letter;
                yield return new WaitForSeconds((1 / typingSpeed) * 10f);
            }
            else if (letter == '?') {
                speakerText.text += letter;
                yield return new WaitForSeconds((1 / typingSpeed) * 25f);
            }
            else {
                speakerText.text += letter;
                yield return new WaitForSeconds(1 / typingSpeed);
            }
        }
    }

    void EndDialogue() {
        //animator.SetBool("IsOpen", false);
        LeanTween.moveLocalY(gameObject, closePos, transitionTime).setEaseInOutCubic();

        PlayerController.playerController.freeze = false;
        PlayerController.playerController.shotable = true;
    }
    public void MakeChoiceOne() {
        dtg1.TriggerEvent();
    }
    public void MakeChoiceTwo() {
        dtg2.TriggerEvent();
    }
}
