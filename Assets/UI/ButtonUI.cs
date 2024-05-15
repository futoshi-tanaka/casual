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
    private Text text;
    [SerializeField]
    private Image image;

    public class Entity
    {
        public int id;
        public string name;
        public bool visibleName;
        public Sprite sprite;
        public UnityAction onClick;
    }

    public Entity entity;

    public void Initialize(Entity entity)
    {
        this.entity = new Entity();
        this.entity.id = entity.id;
        this.entity.name = entity.name;
        this.entity.onClick = entity.onClick;
        this.entity.sprite = entity.sprite;
        this.entity.visibleName = entity.visibleName;
        Set();
    }

    public void Set()
    {
        if(entity.visibleName) text.text = entity.name;
        image.sprite = entity.sprite;
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