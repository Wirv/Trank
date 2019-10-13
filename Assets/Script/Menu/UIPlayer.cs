using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [Header("metti a mano")]
    public GameObject die; // mettere a mano
    public GameObject victory; // mettere a mano
    public GameObject menuGame;
    public GameObject tutorial, ringraziamenti;
    public Button BPause, BAudio;
    public Text NumeroNemici;

    [Header("Altro")]
    public GameObject player;

    public GameObject[] Enemy;
    public GameObject[] EnemyLeg;
    public GameObject[] EnemyPes;
    private GameObject Boss;


    [Header("Suoni")]
    AudioSource audiosrc;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip buttonc;
    public AudioClip closebutton;
    public RawImage AudiooffOn;
    bool sound = false;
    bool menu = false;

    string key = "livUnlock";

    private void Awake()
    {
        sound = false;
        menu = false;
        player = GameObject.FindGameObjectWithTag("Player");
        audiosrc = GetComponent<AudioSource>();
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyLeg = GameObject.FindGameObjectsWithTag("EnemyLeg");
        EnemyPes = GameObject.FindGameObjectsWithTag("EnemyPes");
        Boss = GameObject.Find("BossTesi");
        AudiooffOn = GameObject.FindGameObjectWithTag("Audio").GetComponent<RawImage>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            tutorial.SetActive(true);
            Time.timeScale = 0;
        }
        ringraziamenti.SetActive(false);
        die.SetActive(false);
        victory.SetActive(false);
        menuGame.SetActive(false);
        BAudio.interactable = true;
        BPause.interactable = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            Sound(0);
            StartCoroutine(Die());
            BAudio.interactable = false;
            BPause.interactable = false;
        }

        int num = Enemy.Length + EnemyLeg.Length + EnemyPes.Length;
        NumeroNemici.text = num.ToString();

        if(Enemy.Length == 0 && EnemyLeg.Length == 0 && EnemyPes.Length == 0 && SceneManager.GetActiveScene().buildIndex != 5)
        {
            Sound(1);
            victory.SetActive(true);
            BAudio.interactable = false;
            BPause.interactable = false;

        }

        if (SceneManager.GetActiveScene().buildIndex == 5 && Boss == null)
        {
            Sound(1);
            BAudio.interactable = false;
            BPause.interactable = false;
            ringraziamenti.SetActive(true);
        }

            if (AudioListener.pause == false)
        {
            AudiooffOn.color = Color.black;
        }
        else if (AudioListener.pause == true)
        {
            AudiooffOn.color = Color.red;
        }

    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        die.SetActive(true);
    }

    public void Restart()
    {
        BAudio.interactable = true;
        BPause.interactable = true;
        audiosrc.PlayOneShot(buttonc, 0.7f);
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quitting()
    {
        audiosrc.PlayOneShot(closebutton, 0.7f);
        Application.Quit();
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Research()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyLeg = GameObject.FindGameObjectsWithTag("EnemyLeg");
        EnemyPes = GameObject.FindGameObjectsWithTag("EnemyPes");
    }

    public void Continue()
    {
        audiosrc.PlayOneShot(buttonc, 0.7f);
        BAudio.interactable = true;
        BPause.interactable = true;
        if (MenuManager.loadData + 1 == SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt(key, MenuManager.loadData + 1);
        }

        int nextscene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextscene);

    }

    public void Sound(int i)
    {
        if (sound == false)
        {
            sound = true;
            switch (i)
            {
                case 0:
                    audiosrc.PlayOneShot(lose, 0.7f);
                    break;

                case 1:
                    audiosrc.PlayOneShot(win, 0.7f);
                    break;
            }
        }

        
    }

    public void OffOnSound()
    {
        
        if (AudioListener.pause == true)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
            
    }

    public void GameMenu()
    {
        if (menu == true)
        {
            menu = false;
            menuGame.SetActive(false);
            Time.timeScale = 1;

        }
        else if (menu == false)
        {
            menu = true;
            menuGame.SetActive(true);
            Time.timeScale = 0;

        }
    }
}
