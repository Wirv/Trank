using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu, Levels;

    public GameObject[] livelli;

    AudioSource audiosrc;

    public AudioClip buttonc;
    public AudioClip closebutton;

    string key = "livUnlock";
    public static int loadData;

    private void Awake()
    {
        livelli = GameObject.FindGameObjectsWithTag("livelli");

        audiosrc = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Menu = GameObject.Find("Menu");
        Levels = GameObject.Find("Livelli");
        Levels.SetActive(false);
        Menu.SetActive(true);
        if(!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
            Debug.Log("creato");
        }

        loadData = PlayerPrefs.GetInt(key);
        for(int i = loadData + 1; i < livelli.Length; i++)
        {
            livelli[i].GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        audiosrc.PlayOneShot(buttonc, 0.7f);
        SceneManager.LoadScene(1);
    }

    public void Livelli()
    {
        audiosrc.PlayOneShot(buttonc, 0.7f);
        Menu.SetActive(false);

        Levels.SetActive(true);
    }

    public void UndoLivelli()
    {
        audiosrc.PlayOneShot(closebutton, 0.7f);
        Menu.SetActive(true);

        Levels.SetActive(false);
    }

    public void Quitting()
    {
        audiosrc.PlayOneShot(closebutton, 0.7f);
        Application.Quit();
    }

    public void ChoiseLevel(int i)
    {
        audiosrc.PlayOneShot(buttonc, 0.7f);
        switch (i)
        {
            case 1:
                SceneManager.LoadScene(1);
                break;
            case 2:
                SceneManager.LoadScene(2);
                break;
            case 3:
                SceneManager.LoadScene(3);
                break;
            case 4:
                SceneManager.LoadScene(4);
                break;
            case 5:
                SceneManager.LoadScene(5);
                break;
            case 6:
                SceneManager.LoadScene(6);
                break;
        }
    }
}
