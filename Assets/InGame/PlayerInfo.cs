// プレイヤー同期関連情報クラス
using System.Linq;
using Photon.Pun;

public class PlayerInfo
{
    public const string PlayerStateKey = "PlayerState";
    public const string PlayerShotPointKey = "PlayerShotPoint";
    public const string PlayerShotPointIntervalKey = "PlayerShotPointInterval";
    public Player.PlayerState playerState{get; private set;}
    public int ShotPoint{get; private set;}
    public float ShotPointInterval {get; private set;}

    public Player player;

    private static readonly ExitGames.Client.Photon.Hashtable propsToSet = new ExitGames.Client.Photon.Hashtable();

    public void SendPlayerInfo()
    {
        var myPlayer = PhotonNetwork.LocalPlayer;
        SetPlayerInfo(myPlayer, PlayerStateKey, (int)player.State);
        SetPlayerInfo(myPlayer, PlayerShotPointKey, player.ShotPoint);
        SetPlayerInfo(myPlayer, PlayerShotPointIntervalKey, player.ShotPointInterval);
    }

    public void RecievePlayerInfo()
    {
        var otherPlayer = PhotonNetwork.PlayerListOthers.FirstOrDefault();
        playerState = GetPlayerState(otherPlayer);
        ShotPoint = GetPlayerShotPoint(otherPlayer);
        ShotPointInterval = GetPlayerShotPointInterval(otherPlayer);
    }

    private void SetPlayerInfo(Photon.Realtime.Player player, string propertyKey, int value)
    {
        propsToSet[propertyKey] = value;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    private void SetPlayerInfo(Photon.Realtime.Player player, string propertyKey, float value)
    {
        propsToSet[propertyKey] = value;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    private int GetPlayerInfo(Photon.Realtime.Player player, string propertyKey)
    {
        if(player == null) return 0;
        return (player.CustomProperties[propertyKey] is int value) ? value : 0;
    }

    private float GetPlayerShotIntervalInfo(Photon.Realtime.Player player, string propertyKey)
    {
        if(player == null) return 0;
        return (player.CustomProperties[propertyKey] is float value) ? value : 0;
    }

    private Player.PlayerState GetPlayerState(Photon.Realtime.Player player)
    {
        if(player == null) return 0;
        var state = (player.CustomProperties[PlayerStateKey] is int value) ? value : 0;
        return (Player.PlayerState)state;
    }

    private int GetPlayerShotPoint(Photon.Realtime.Player player)
    {
        return GetPlayerInfo(player, PlayerShotPointKey);
    }

    private float GetPlayerShotPointInterval(Photon.Realtime.Player player)
    {
        return GetPlayerShotIntervalInfo(player, PlayerShotPointIntervalKey);
    }
}