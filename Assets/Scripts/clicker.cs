using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clicker : MonoBehaviour {

    public ParticleSystem ps;
    public RectTransform t;
    public Text text;
    public string treggString;
    public Vector3 rotateTregg;

    public int treggCount;

    void Update() {
        t.Rotate(rotateTregg * Time.deltaTime);
        Vector3 target = new Vector3(1, 1, 1);
        Vector3 current = t.localScale;
        float lerp = 0.2f;

        t.localScale = Vector3.Lerp(current, target, lerp);
    }

    public void Click() {
        IncrementTregg();
        Emit(40);
        t.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void IncrementTregg() {
        SetTregg(treggCount + 1);
    }

    public void SetTregg(int count) {
        treggCount = count;
        text.text = treggString + " " + treggCount;
    }



    public int GetTregg() {
        return treggCount;
    }

    public void ResetTregg() {
        SetTregg(0);
    }

    public void Emit(int parts) {
        ps.Emit(parts);
    }
}