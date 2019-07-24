using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu, Levels;

    public GameObject[] livelli;

    private void Awake()
    {
        livelli = GameObject.FindGameObjectsWithTag("livelli");
    }

    // Start is called before the first frame update
    void Start()
    {
        Menu = GameObject.Find("Menu");
        Levels = GameObject.Find("Livelli");
        Levels.SetActive(false);
        Menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Livelli()
    {
        Menu.SetActive(false);

        Levels.SetActive(true);
    }

    public void UndoLivelli()
    {
        Menu.SetActive(true);

        Levels.SetActive(false);
    }

    public void Quitting()
    {
        Application.Quit();
    }

    public void ChoiseLevel(int i)
    {
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
