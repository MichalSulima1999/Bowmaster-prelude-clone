using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreateProfile : MonoBehaviour
{
    [SerializeField] InputField userNameInput;
    [SerializeField] GameObject createProfileObject;
    [SerializeField] GameObject profileButton;
    [SerializeField] Transform profileContent;

    [SerializeField] StatsMenu statsMenu;

    private GameSaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<GameSaveManager>();

        ShowProfiles();

        if (NoProfiles()) {
            createProfileObject.SetActive(true);
        } else {
            createProfileObject.SetActive(false);
        }
    }

    public void CreateUser() {
        if (userNameInput.text != "" && !PlayerExists(userNameInput.text)) {
            saveManager.inventory.CreateNewUser(userNameInput.text);
            saveManager.SaveGame();

            ShowProfiles();

            createProfileObject.SetActive(false);
        }
        
    }

    private bool PlayerExists(string playerName) {
        DirectoryInfo dir = new DirectoryInfo(saveManager.saveFolder);
        FileInfo[] info = dir.GetFiles("*.save");
        foreach (FileInfo f in info) {
            Debug.Log(f.Name);
            if (f.Name.Equals(playerName + ".save")) {
                return true;
            }
        }

        return false;
    }

    private bool NoProfiles() {
        DirectoryInfo dir = new DirectoryInfo(saveManager.saveFolder);
        FileInfo[] info = dir.GetFiles("*.save");
        foreach (FileInfo f in info) {
            return false;
        }

        return true;
    }

    private void ShowProfiles() {
        DirectoryInfo dir = new DirectoryInfo(saveManager.saveFolder);
        FileInfo[] info = dir.GetFiles("*.save");

        foreach (Transform child in profileContent.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (FileInfo f in info) {
            GameObject button = Instantiate(profileButton, profileContent.position, Quaternion.identity, profileContent);
            button.GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(f.Name);
            button.GetComponent<Button>().onClick.AddListener(delegate { saveManager.LoadGame(f.Name); });
            button.GetComponent<Button>().onClick.AddListener(statsMenu.UpdateText);
        }
    }
}
