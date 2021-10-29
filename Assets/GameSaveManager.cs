using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance;

    public PlayerInventorySO inventory;

    public string saveFolder { get; set; }

    private string savePath;

    // Start is called before the first frame update
    void Awake() { 
        if (instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(this);
        }
        DontDestroyOnLoad(this);

        saveFolder = Application.persistentDataPath + "/game_save/character_data";
        savePath = saveFolder + "/" + inventory.userName + ".save";
    }

    public bool IsSavedFile() {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void SaveGame() {
        if (!IsSavedFile()) {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        savePath = saveFolder + "/" + inventory.userName + ".save";
        FileStream file = File.Create(savePath);
        var json = JsonUtility.ToJson(inventory);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame(string profileName) {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/character_data")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        }

        BinaryFormatter bf = new BinaryFormatter();

        string profile = saveFolder + "/" + profileName;
        Debug.Log(profile);

        if (File.Exists(profile)) {
            FileStream file = File.Open(profile, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), inventory);
            file.Close();
        }
    }
}
