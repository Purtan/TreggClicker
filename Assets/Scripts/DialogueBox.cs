using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {

    public static DialogueBox instance;
    public Image charImage;
    public TMP_Text charText;

    public Vector3 shownPos;
    public Vector3 hiddenPos;
    public float moveDuration;
    public LeanTweenType moveType;
    public bool shown;



    private void Awake() {
        instance = this;
        instance.charText.text = "";
    }

    private void Update() {
        //Any input hides the dialogue
        if(Input.anyKeyDown || Input.touchCount > 0) {
            Hide();
        }
    }

    public static void Show() {
        instance.shown = true;
        LeanTween.move(instance.gameObject, instance.shownPos, instance.moveDuration).setEase(instance.moveType);
    }

    public static void Hide() {
        LeanTween.move(instance.gameObject, instance.hiddenPos, instance.moveDuration).setEase(instance.moveType).setOnComplete(SetHidden); //TODO not sure how to create the method inline
    }

    private static void SetHidden() {
        instance.shown = false;
    }

    public static void SetDialogue(Sprite sprite, string str) {

        instance.charImage.sprite = sprite;
        instance.charText.text = str;
        Show();
    }
}
