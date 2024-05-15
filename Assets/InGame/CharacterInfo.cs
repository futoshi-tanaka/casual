// プレイヤー情報クラス
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInfo
{
    public int Id;
    public string Name;

    public Sprite sprite;

    public CharacterInfo(int id, string name, string spritePath)
    {
        this.Id = id;
        this.Name = name;
        this.sprite = LoadTexture(spritePath)[0];
    }

    public Sprite[] LoadTexture(string spritePath)
    {
       var sprites = Resources.LoadAll<Sprite>(spritePath);
       return sprites;
    }

    public void Initialize(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}