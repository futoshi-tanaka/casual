using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameState : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GaugeUI _shotPointGaugeUI;
    [SerializeField]
    private PointUI _shotPointUI;

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

         if (Input.GetKey (KeyCode.Space))
         {
            SceneManager.LoadScene("Result");
         }
    }

    private void UpdateUI()
    {
        _shotPointGaugeUI.UpdateGauge(_player.InitShotPointInterval - _player.ShotPointInterval, _player.InitShotPointInterval);
        _shotPointUI.SetText($"{_player.ShotPoint}");
    }
}
