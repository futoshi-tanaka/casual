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
    private Player _player;
    [SerializeField]
    private Enemy _enemy;

    [SerializeField]
    private GaugeUI _shotPointGaugeUI;
    [SerializeField]
    private PointUI _shotPointUI;

    [SerializeField]
    private Button _shotButton;

    private const string PlayerStateKey = "PlayerState";

    private static readonly ExitGames.Client.Photon.Hashtable propsToSet = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {
        // プレイヤー生成
        var playerPosition = PhotonNetwork.IsMasterClient ? new Vector3(0.0f, -2.0f, 0.0f) : new Vector3(0.0f, 3.0f, 0.0f);
        var playerLotation = PhotonNetwork.IsMasterClient ? Quaternion.identity : new Quaternion(0.5f, 0.0f, 0.0f, 0.0f);
        _player = PhotonNetwork.Instantiate("Player", playerPosition, playerLotation).GetComponent<Player>();
        _player.CreateBullet();
        // UI設定
        _shotButton.onClick.AddListener(_player.Shot);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log($"join player");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log($"left player");
    }

    public static Player.PlayerState GetPlayerState(Photon.Realtime.Player player)
    {
        if(player == null) return 0;
        var state = (player.CustomProperties[PlayerStateKey] is int value) ? value : 0;
        return (Player.PlayerState)state;
    }

    public static void SetPlayerState(Photon.Realtime.Player player, Player.PlayerState playerState) {
        propsToSet[PlayerStateKey] = (int)playerState;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

        SetPlayerState(PhotonNetwork.LocalPlayer, _player.State);
        var otherState = GetPlayerState(PhotonNetwork.PlayerListOthers.FirstOrDefault());
        if(otherState == Player.PlayerState.DEFEAT)
        {
            _player.SetWin();
        }
    }

    private void UpdateUI()
    {
        _shotPointGaugeUI.UpdateGauge(_player.InitShotPointInterval - _player.ShotPointInterval, _player.InitShotPointInterval);
        _shotPointUI.SetText($"{_player.ShotPoint}");
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
                var otherState = GetPlayerState(PhotonNetwork.PlayerListOthers.FirstOrDefault());
        ShowDebugText($"otherPlayer state : {otherState}");
        ShowDebugText($"master client : {PhotonNetwork.IsMasterClient}");
    }
}
