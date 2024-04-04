using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultState : MonoBehaviour
{
    [SerializeField]
    private Button returnButton;
    void Awake()
    {
        returnButton.onClick.AddListener(NextState);
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
