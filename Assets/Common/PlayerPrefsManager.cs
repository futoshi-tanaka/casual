using UnityEngine;

public static class PlayerPrefsManager
{
    private const string PlayerIdKey = "PlayerId";
    private const string PlayerSkillSlot1IdKey = "PlayerSkillSlot1Id";
    private const string PlayerSkillSlot2IdKey = "PlayerSkillSlot2Id";
    private const string PlayerSkillSlot3IdKey = "PlayerSkillSlot3Id";

    public static void SavePlayerId(int id)
    {
        PlayerPrefs.SetInt(PlayerIdKey, id);
    }

    public static int LoadPlayerId()
    {
        return PlayerPrefs.GetInt(PlayerIdKey);
    }

    public static void SavePlayerSlot1(int id)
    {
        PlayerPrefs.SetInt(PlayerSkillSlot1IdKey, id);
    }

    public static int LoadPlayerSlot1Id()
    {
        return PlayerPrefs.GetInt(PlayerSkillSlot1IdKey);
    }

    public static void SavePlayerSlot2(int id)
    {
        PlayerPrefs.SetInt(PlayerSkillSlot2IdKey, id);
    }

    public static int LoadPlayerSlot2Id()
    {
        return PlayerPrefs.GetInt(PlayerSkillSlot2IdKey);
    }

    public static void SavePlayerSlot3(int id)
    {
        PlayerPrefs.SetInt(PlayerSkillSlot3IdKey, id);
    }

    public static int LoadPlayerSlot3Id()
    {
        return PlayerPrefs.GetInt(PlayerSkillSlot3IdKey);
    }
}
