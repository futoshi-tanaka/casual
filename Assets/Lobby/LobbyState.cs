using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyState : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private FadeUI fadeUI;

    // Start is called before the first frame update
    void Awake()
    {
        fadeUI.Initialize();
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.SendRate = 60; // 1秒間にメッセージ送信を行う回数
        PhotonNetwork.SerializationRate = 60; // 1秒間にオブジェクト同期を行う回数

        cancelButton.onClick.AddListener(OnCancel);

        fadeUI.FadeOut();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // ランダムなルームに参加する
        PhotonNetwork.JoinRandomRoom();
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message) {
        // ルームの参加人数を2人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            NextState("Ingame");
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log($"join player");
        if(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            NextState("Ingame");
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log($"left player");
        PhotonNetwork.CurrentRoom.IsOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey (KeyCode.Space))
         {
            SceneManager.LoadScene("Title");
         }
    }

    private void OnCancel()
    {
        // Photonのサーバーから切断する
        PhotonNetwork.Disconnect();

        NextState("Title");
    }

    public void NextState(string sceneName)
    {
        fadeUI.FadeIn(onComplete: () => SceneManager.LoadScene(sceneName));
    }
}
