using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRot : MonoBehaviour {

    void Update() {

        GetComponent<RectTransform>().rotation = new Quaternion();
    }
}
