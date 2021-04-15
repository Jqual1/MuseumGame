using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject canvas;
    public GameObject spriteToFade;
    public GameObject events;
    public GameObject player;

    private AudioSource sound;

    public List<Powerup> obtainedPowerups = new List<Powerup>{ };
    public List<bool> powerupStatus = new List<bool>{ };

    public string sceneToLoad = "";

    public AudioClip[] music;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);
            DontDestroyOnLoad(player);

        }
        else
        {
            Destroy(gameObject);
            Destroy(canvas);
            Destroy(events);
            Destroy(player);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (sceneToLoad != "")
        {
            LoadLevel(sceneToLoad, new Vector3(0, 0, 0));
        }
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        doGliderButtonCheck();
        if (SceneManager.GetActiveScene().name == "Spawn" || SceneManager.GetActiveScene().name == "EnemyTunnel" || SceneManager.GetActiveScene().name == "RouteToTower" || SceneManager.GetActiveScene().name == "Tower" || SceneManager.GetActiveScene().name == "PathToBoss" || SceneManager.GetActiveScene().name == "BossRoom" ){
            sound.clip = music[0];
        }
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    public void LoadLevel(string levelName, Vector3 whereTo)
    {
        StartCoroutine(LerpFunction(Color.black, 0.25f));
        StartCoroutine(LoadSceneAsync(levelName, whereTo));
    }

    IEnumerator LoadSceneAsync(string scene, Vector3 whereTo)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        player.transform.position = whereTo;
    }

    IEnumerator LerpFunction(Color endValue, float duration)
    {
        Image sprite = spriteToFade.GetComponent<Image>();
        spriteToFade.SetActive(true);

        float time = 0;
        Color startValue = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endValue;
        spriteToFade.SetActive(false);
    }

    private void doGliderButtonCheck()
    {
        if (obtainedPowerups.Contains(Powerup.Glider))
        {
            if (Input.GetKeyDown("1"))
            {
                var index = Array.IndexOf(obtainedPowerups.ToArray(), Powerup.Glider);
                var status = powerupStatus[index];
                if (!status)
                {
                    player.GetComponent<Glider>().IsGliding = true;
                    powerupStatus[index] = true;
                }
                else
                {
                    player.GetComponent<Glider>().IsGliding = false;
                    powerupStatus[index] = false;
                }
            }
        }
    }
    public GameObject GetPlayer()
    {
        return player;
    }
}
//    }
//}
//}

