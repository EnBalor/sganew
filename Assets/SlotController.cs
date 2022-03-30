using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IDropHandler
{
    //[SerializeField] private Canvas _Canvas;

    private Image _OldImage = null;
    private RectTransform _RectTransform;

    private void Awake()
    {
        _RectTransform = GetComponent<RectTransform>();
        _OldImage = GetComponent<Image>();
    }

    Canvas GetCanvas(Transform _Transform)
    {
        Canvas __Canvas = _Transform.parent.GetComponent<Canvas>();

        if (__Canvas == null)
            __Canvas = GetCanvas(_Transform.parent);

        return __Canvas;
    }

    public void OnDrop(PointerEventData eventData)
    {
        SkillIconDrag.MouseOver = true;

        _OldImage.sprite = GetComponent<Image>().sprite;
        GetComponent<Image>().sprite = 
            eventData.pointerDrag.GetComponent<Image>().sprite;
    }
}
