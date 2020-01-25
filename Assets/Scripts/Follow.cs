using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    public GameObject target;
    public Vector3 offset;

    private void Update() {
        transform.position = target.transform.position + offset;
    }
}
