using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField]
    private Vector3 moveDir;
    public Vector3 MoveDir { get { return moveDir; } }

    [Header("조이스틱 내부의 레버 이동범위 제한")]
    [SerializeField, Range(10, 150)]
    private float stickRange;

    [TooltipAttribute("조이스틱 이동 시 레버의 거리에 따라 걷기 -> 뛰기로 전환될 임계치 (레버의 최대 제한거리보다 작아야함.)")]
    [SerializeField]
    private float typeChangeThreshold;

    [SerializeField]
    private float leverDistance;
    public float LeverDistance { get { return leverDistance; } }

    private bool isRun = false;
    public bool IsRun { get { return isRun; } }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        SetDirection();
    }

    public void SetDirection()
    {
        Vector3 vec = lever.transform.position - rectTransform.transform.position;

        moveDir = vec.normalized;
        leverDistance = vec.magnitude;

        isRun = leverDistance >= typeChangeThreshold;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 화면의 절반을 넘어간 경우 조이스틱 동작 x 
        if (eventData.position.x >= Screen.width / 2)
        {
            OnEndDrag(eventData);
            return;
        }

        var inputPos = eventData.position - rectTransform.anchoredPosition;
        // 조이스틱의 이동범위 제한
        var inputVector = inputPos.magnitude < stickRange ? inputPos : inputPos.normalized * stickRange;
        lever.anchoredPosition = inputVector;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x >= Screen.width / 2)
        {
            OnEndDrag(eventData);
            return;
        }

        var inputPos = eventData.position - rectTransform.anchoredPosition;
        // 조이스틱의 이동범위 제한
        var inputVector = inputPos.magnitude < stickRange ? inputPos : inputPos.normalized * stickRange;
        lever.anchoredPosition = inputVector;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Init Mode
        lever.anchoredPosition = Vector2.zero;
        leverDistance = 0f;
        moveDir = Vector3.zero;
    }
}
