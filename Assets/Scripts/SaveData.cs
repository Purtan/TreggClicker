using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

    public clicker cl;

    private void Start() {
        Load();
    }

    private void OnApplicationQuit() {
        Save();
    }

    public void Save() {
        PlayerPrefs.SetInt("Treggs", cl.GetTregg());
    }

    public void Load() {
        int treggs = PlayerPrefs.GetInt("Treggs");
        cl.SetTregg(treggs);
    }
}
