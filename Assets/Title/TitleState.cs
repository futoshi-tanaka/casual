using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleState : MonoBehaviour
{
    [SerializeField]
    private Button onlineButton;
    [SerializeField]
    private Button offlineButton;
    [SerializeField]
    private Button characterButton;
    [SerializeField]
    private ModalUI modalUI;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private FadeUI fadeUI;

    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        offlineButton.onClick.AddListener(() =>
        {
            PhotonNetwork.OfflineMode = true;
            NextState("Ingame");
        });
        onlineButton.onClick.AddListener(() => NextState("Lobby"));
        characterButton.onClick.AddListener(() => NextState("custom"));

        modalUI = Instantiate(modalUI).GetComponent<ModalUI>();
        modalUI.transform.SetParent(canvas.transform, false);
        modalUI.Open("Update",
                     "update infomation!",
                    () => {
                        modalUI.Close();
                    });
        fadeUI.Initialize();
        fadeUI.FadeOut();
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
        fadeUI.FadeIn(onComplete: () => SceneManager.LoadScene(sceneName));
    }
}
