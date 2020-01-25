using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class background : MonoBehaviour {
    public Image img;
    public float speed;

    void Update() {
        //var amt = GetComponent<Image>().fillAmount;
        //GetComponent<Image>().fillAmount += Time.deltaTime * speed;

        transform.Rotate(0, 0, speed * Time.deltaTime);

    }
}
