using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleState : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    // Start is called before the first frame update
    void Awake()
    {
        startButton.onClick.AddListener(NextState);
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey (KeyCode.Space))
         {
            SceneManager.LoadScene("Ingame");
         }
    }

    public void NextState()
    {
        SceneManager.LoadScene("Ingame");
    }
}
