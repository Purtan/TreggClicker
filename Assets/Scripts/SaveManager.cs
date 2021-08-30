using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager {
    // Path for file storage
    static string filePath = Application.dataPath;
    static string fileName = "save.json";
    static string dateFormat = "MM:dd:yyyy:HH:mm:ss";
    public static SaveFile saveFile;

    // Write save to file
    public static void Save() {
        saveFile.lastSave = System.DateTime.Now.ToString(dateFormat);

        if(!Directory.Exists(filePath)) {
            Directory.CreateDirectory(filePath);
        }

        if(saveFile != null) {
            File.WriteAllText(GetPath(), saveFile.ToString());
        } else {
            //No save
            CreateNewSave();
        }

        Debug.Log("Saved to " + GetPath());
    }

    public static void CreateNewSave() {
        saveFile = new SaveFile();
        Save();
    }

    // Load file to game
    public static void Load() {
        if(File.Exists(GetPath())) {
            //file exists, load normally
            saveFile = JsonUtility.FromJson<SaveFile>(File.ReadAllText(GetPath()));
            Debug.Log(saveFile.lastSave);
        } else {
            //file absent, create new
            CreateNewSave();
            Debug.Log("File doesn't exist at " + GetPath() + ", creating new file");
        }
    }

    public static string GetPath() {
        return filePath + "/" + fileName;
    }
}