using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomState : MonoBehaviour
{
    [SerializeField]
    private Text characterName;

    [SerializeField]
    private Transform characterScrollViewContent;

    [SerializeField]
    private Transform skill1ScrollViewContent;

    [SerializeField]
    private Transform skill2ScrollViewContent;

    [SerializeField]
    private Transform skill3ScrollViewContent;

    [SerializeField]
    private Button characterButton;

    [SerializeField]
    private Button skill1Button;

    [SerializeField]
    private Button skill2Button;

    [SerializeField]
    private Button skill3Button;

    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button returnButton;

    [SerializeField]
    private FadeUI fadeUI;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        fadeUI.Initialize();
        returnButton.onClick.AddListener(() => NextState("Title"));
        nextButton.onClick.AddListener(() => NextState("Title"));
        // プレイヤーキャラ選択情報をロード
        var characterId = PlayerPrefsManager.LoadPlayerId();
        var slot1Id = PlayerPrefsManager.LoadPlayerSlot1Id();
        var slot2Id = PlayerPrefsManager.LoadPlayerSlot2Id();
        var slot3Id = PlayerPrefsManager.LoadPlayerSlot3Id();

        var sprite = Resources.Load<Sprite>("bag2");

        var characterList = new List<CharacterInfo>();
        characterList.Add(new CharacterInfo(1, "chara1", "Sprites/Player"));
        characterList.Add(new CharacterInfo(2, "chara2", "Sprites/Player"));

        var skill1List = new List<SkillInfo>();
        skill1List.Add(new SkillInfo(1, "1"));
        skill1List.Add(new SkillInfo(2, "2"));
        skill1List.Add(new SkillInfo(3, "3"));

        var skill2List = new List<SkillInfo>();
        skill2List.Add(new SkillInfo(1, "a"));
        skill2List.Add(new SkillInfo(2, "b"));
        skill2List.Add(new SkillInfo(3, "c"));

        var skill3List = new List<SkillInfo>();
        skill3List.Add(new SkillInfo(1, "A"));
        skill3List.Add(new SkillInfo(2, "B"));
        skill3List.Add(new SkillInfo(3, "C"));

        // キャラクタ選択ボタンUI
        var characterButtonList = new List<Button>();
        foreach(var character in characterList)
        {
            var button = Instantiate(characterButton, characterScrollViewContent);
            var entity = new ButtonUI.Entity();
            entity.id = character.Id;
            entity.name = character.Name;
            entity.sprite = character.sprite;
            entity.onClick = () =>
            {
                characterName.text = entity.name;
                UpdateCharacterButton(entity.id);
                PlayerPrefsManager.SavePlayerId(entity.id);
            };
            button.GetComponent<ButtonUI>().Initialize(entity);
            characterButtonList.Add(button);
        }

        foreach(var characterButton in characterButtonList)
        {
            characterButton.GetComponent<ButtonUI>().In();
        }

        UpdateCharacterButton(characterId);

        // スキル1ボタンUI
        var skill1ButtonList = new List<Button>();
        foreach(var skill in skill1List)
        {
            var button = Instantiate(skill1Button, skill1ScrollViewContent);
            var entity = new ButtonUI.Entity();
            entity.id = skill.Id;
            entity.name = skill.Name;
            entity.onClick = () =>
            {
                UpdateSkillButton(entity.id, skill1ScrollViewContent);
                PlayerPrefsManager.SavePlayerSlot1(entity.id);
            };
            button.GetComponent<ButtonUI>().Initialize(entity);
            skill1ButtonList.Add(button);
        }

        foreach(var skill1Button in skill1ButtonList)
        {
            skill1Button.GetComponent<ButtonUI>().In();
        }

        UpdateSkillButton(slot1Id, skill1ScrollViewContent);

        // スキル2ボタンUI
        var skill2ButtonList = new List<Button>();
        foreach(var skill in skill2List)
        {
            var button = Instantiate(skill2Button, skill2ScrollViewContent);
            var entity = new ButtonUI.Entity();
            entity.id = skill.Id;
            entity.name = skill.Name;
            entity.onClick = () =>
            {
                UpdateSkillButton(entity.id, skill2ScrollViewContent);
                PlayerPrefsManager.SavePlayerSlot2(entity.id);
            };
            button.GetComponent<ButtonUI>().Initialize(entity);
            skill2ButtonList.Add(button);
        }

        foreach(var skillButton in skill2ButtonList)
        {
            skillButton.GetComponent<ButtonUI>().In();
        }

        UpdateSkillButton(slot2Id, skill2ScrollViewContent);

        // スキル3ボタンUI
        var skill3ButtonList = new List<Button>();
        foreach(var skill in skill3List)
        {
            var button = Instantiate(skill3Button, skill3ScrollViewContent);
            var entity = new ButtonUI.Entity();
            entity.id = skill.Id;
            entity.name = skill.Name;
            entity.onClick = () =>
            {
                UpdateSkillButton(entity.id, skill3ScrollViewContent);
                PlayerPrefsManager.SavePlayerSlot3(entity.id);
            };
            button.GetComponent<ButtonUI>().Initialize(entity);
            skill3ButtonList.Add(button);
        }

        foreach(var skillButton in skill3ButtonList)
        {
            skillButton.GetComponent<ButtonUI>().In();
        }

        UpdateSkillButton(slot3Id, skill3ScrollViewContent);
        fadeUI.FadeOut();
    }

    private void UpdateCharacterButton(int selectId)
    {
        var buttons = characterScrollViewContent.GetComponentsInChildren<ButtonUI>();
        foreach(var button in buttons)
        {
            button.SetColor( button.entity.id == selectId ? Color.yellow : Color.white);
        }
    }

    private void UpdateSkillButton(int selectId, Transform scrollViewContent)
    {
        var buttons = scrollViewContent.GetComponentsInChildren<ButtonUI>();
        foreach(var button in buttons)
        {
            button.SetColor( button.entity.id == selectId ? Color.yellow : Color.white);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextState(string sceneName)
    {
        fadeUI.FadeIn(onComplete: () => SceneManager.LoadScene(sceneName));
    }
}
