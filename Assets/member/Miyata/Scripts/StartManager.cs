using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject maingame;
    public GameObject title;
    public int sceneNumber = 0;

    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(sceneNumber == 0)
            {
                title.SetActive (false);
                maingame.SetActive (true);
                player.SetActive(true);
                sceneNumber++;
            }
        }
    }

    public void FinishGame()
    {
        Debug.Log(sceneNumber);
        SceneManager.LoadScene ("GameOverScene");
    }
}
