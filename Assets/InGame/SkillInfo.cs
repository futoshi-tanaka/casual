// プレイヤー情報クラス
using System.Linq;

public class SkillInfo
{
    private int id;
    public int Id => id;
    private string name;
    public string Name => name;

    public SkillInfo(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public void Initialize(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}