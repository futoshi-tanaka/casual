// プレイヤー情報クラス
using System.Linq;
using UnityEngine;

public class CharacterInfo
{
    public int Id;
    public string Name;

    public CharacterInfo(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public void Initialize(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}