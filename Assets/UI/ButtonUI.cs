using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Image image;

    public class Entity
    {
        public int id;
        public string name;
        public UnityAction onClick;
    }

    public Entity entity;

    public void Initialize(Entity entity)
    {
        this.entity = new Entity();
        this.entity.id = entity.id;
        this.entity.name = entity.name;
        this.entity.onClick = entity.onClick;
        Set();
    }

    public void Set()
    {
        text.SetText(entity.name);
        button.onClick.AddListener(entity.onClick);
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void In()
    {
        button.gameObject.SetActive(true);
    }

    public void Out()
    {
        button.gameObject.SetActive(false);
    }
}