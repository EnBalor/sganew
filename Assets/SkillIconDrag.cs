using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SkillIconDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _Canvas;

    private RectTransform _RectTransform;
    private CanvasGroup _CanvasGroup;
    [SerializeField] private Vector2 _OldPosition;

    [HideInInspector] static public bool MouseOver;

    private void Awake()
    {
        _RectTransform = GetComponent<RectTransform>();
        _CanvasGroup = GetComponent<CanvasGroup>();

        //_Canvas = GetCanvas(transform);
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        MouseOver = false;
        _Canvas = GetCanvas(transform);
    }

    Canvas GetCanvas(Transform _Transform)
    {
        _Canvas = _Transform.parent.GetComponent<Canvas>();

        if (_Canvas == null)
        {
            //i++;
            _Canvas = GetCanvas(_Transform.parent);
        }

        return _Canvas;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _OldPosition = _RectTransform.anchoredPosition;
        _CanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        _RectTransform.anchoredPosition += eventData.delta / _Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _RectTransform.anchoredPosition = _OldPosition;

        _CanvasGroup.blocksRaycasts = true;
        MouseOver = false;
    }
}
