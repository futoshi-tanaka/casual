using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleState : MonoBehaviour
{
    [SerializeField]
    private Button onlineButton;
    [SerializeField]
    private Button offlineButton;

    // Start is called before the first frame update
    void Awake()
    {
        offlineButton.onClick.AddListener(() =>NextState("Ingame"));
        onlineButton.onClick.AddListener(() => NextState("Lobby"));
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey (KeyCode.Space))
         {
            SceneManager.LoadScene("Ingame");
         }
    }

    public void NextState(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
