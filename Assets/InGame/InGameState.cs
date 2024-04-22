using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Unity.Mathematics;
using System.Linq;

public class InGameState : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private Player _player;
    [SerializeField]
    private Enemy _enemy;

    [SerializeField]
    private GaugeUI _shotPointGaugeUI;
    [SerializeField]
    private PointUI _shotPointUI;

    [SerializeField]
    private Button _shotButton1;

    [SerializeField]
    private Button _shotButton2;

    [SerializeField]
    private Button _shotButton3;

    [SerializeField]
    private GaugeUI _enemyShotPointGaugeUI;
    [SerializeField]
    private PointUI _enemyShotPointUI;

    [SerializeField]
    private Button _enemyShotButton1;

    [SerializeField]
    private Button _enemyShotButton2;

    [SerializeField]
    private Button _enemyShotButton3;

    [SerializeField]
    private Button _cancelButton;

    [SerializeField]
    private Transform _initPlayerPosition;

    [SerializeField]
    private Transform _initEnemyPosition;

    private bool isOffline = PhotonNetwork.OfflineMode;

    private PlayerInfo myPlayerInfo;
    private PlayerInfo otherPlayerInfo;

    private void Awake()
    {
        // プレイヤー生成
        var playerPosition = PhotonNetwork.IsMasterClient ? _initPlayerPosition.transform.localPosition : _initEnemyPosition.transform.localPosition;
        var playerLotation = PhotonNetwork.IsMasterClient ? Quaternion.identity : new Quaternion(0.5f, 0.0f, 0.0f, 0.0f);
        if(isOffline)
        {
            _player = Instantiate(_player, playerPosition, playerLotation).GetComponent<Player>();
            _player.CreateBullet(true);

            _enemy = Instantiate(_enemy, _initEnemyPosition.transform.localPosition, quaternion.identity);
        }
        else
        {
            _player = PhotonNetwork.Instantiate("Player", playerPosition, playerLotation).GetComponent<Player>();
            _player.CreateBullet();
        }
        // UI設定
        _shotButton1.onClick.AddListener(_player.Shot1);
        _shotButton2.onClick.AddListener(_player.Shot2);
        _shotButton3.onClick.AddListener(_player.Shot3);
        _cancelButton.onClick.AddListener(OnCancel);

        // メインカメラの対戦方向の回転
        var cameraRotZ = PhotonNetwork.IsMasterClient ? 0.0f : 180.0f;
        _mainCamera.transform.rotation = new Quaternion(0.0f, 0.0f, cameraRotZ, 0.0f);

        // プレイヤー同期情報初期化
        myPlayerInfo = new PlayerInfo();
        myPlayerInfo.player = _player;
        otherPlayerInfo = new PlayerInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsConnectedAndReady)
        {
            myPlayerInfo.SendPlayerInfo();
            otherPlayerInfo.RecievePlayerInfo();

            if(otherPlayerInfo.playerState == Player.PlayerState.DEFEAT)
        {
            _player.SetWin();
        }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // 自分のUI
        _shotPointGaugeUI.UpdateGauge(_player.InitShotPointInterval - _player.ShotPointInterval, _player.InitShotPointInterval);
        _shotPointUI.SetText($"{_player.ShotPoint}");

        // 対戦相手のUI
        if(isOffline)
        {
            _enemyShotPointGaugeUI.UpdateGauge(_enemy.InitShotPointInterval - _enemy.ShotPointInterval, _enemy.InitShotPointInterval);
            _enemyShotPointUI.SetText($"{_enemy.ShotPoint}");
        }
        else
        {
            _enemyShotPointGaugeUI.UpdateGauge(_player.InitShotPointInterval - otherPlayerInfo.ShotPointInterval, _player.InitShotPointInterval);
            _enemyShotPointUI.SetText($"{otherPlayerInfo.ShotPoint}");
        }
    }

    private void Win()
    {

    }

    private void Lose()
    {

    }

    private void OnCancel()
    {
        // Photonのサーバーから切断する
        PhotonNetwork.Disconnect();

        NextState("Title");
    }

    public void NextState(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    int debugWidth = 300;
    int debugHeight = 20;
    int debugX = 1;
    int debugY = 1;

    private void ShowDebugText(string text)
    {
        GUI.Box(new Rect(debugX, debugY, debugWidth, debugHeight), text);
        debugY += debugHeight;
    }
    public void OnGUI()
    {
        debugX = 1;
        debugY = 1;
        ShowDebugText($"connect players : {PhotonNetwork.PlayerList.Length}");
        ShowDebugText($"player state : {_player.State}");
        ShowDebugText($"otherPlayer state : {otherPlayerInfo.playerState}");
        ShowDebugText($"master client : {PhotonNetwork.IsMasterClient}");
        /*
        foreach(var bullet in _player.Bullets)
        {
            ShowDebugText($"bulletState : {bullet.State} bulletWait : {bullet.ShotWait}");
        }
        */
    }
}