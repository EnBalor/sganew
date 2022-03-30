using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillListController : MonoBehaviour
{
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject SkillPrefab;

    [SerializeField] private GameObject _Scrollbar;

    [SerializeField] private List<Sprite> SkillList = new List<Sprite>();

    private void OnEnable()
    {
        Scrollbar Bar = _Scrollbar.GetComponent<Scrollbar>();
        Bar.value = 1.0f;
    }

    void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        object[] Objects = LoadDataManager.GetInstance.LoadData("Sprites");

        if (Objects == null)
            return;

        for (int i = 0; i < 55 /*Objects.Length*/; ++i)
        {
            GameObject SkillObj = Instantiate(SkillPrefab);
            SkillObj.transform.SetParent(Content.transform);
            SkillObj.transform.name = "Skill_" + i.ToString();

            Image Texture = SkillObj.GetComponent<Image>();
            Texture.sprite = Objects[i] as Sprite;

            SkillList.Add(Texture.sprite);
        }

        int Count = (Objects.Length / 11);

        if ((Objects.Length % 11 != 0))
            ++Count;

        RectTransform ContentRect = Content.GetComponent<RectTransform>();

        ContentRect.sizeDelta = new Vector2(1383.0f, Count * 122 + 40);
    }
}
