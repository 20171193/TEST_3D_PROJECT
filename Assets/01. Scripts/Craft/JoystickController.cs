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

    [Header("���̽�ƽ ������ ���� �̵����� ����")]
    [SerializeField, Range(10, 150)]
    private float stickRange;

    [TooltipAttribute("���̽�ƽ �̵� �� ������ �Ÿ��� ���� �ȱ� -> �ٱ�� ��ȯ�� �Ӱ�ġ (������ �ִ� ���ѰŸ����� �۾ƾ���.)")]
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
        // ȭ���� ������ �Ѿ ��� ���̽�ƽ ���� x 
        if (eventData.position.x >= Screen.width / 2)
        {
            OnEndDrag(eventData);
            return;
        }

        var inputPos = eventData.position - rectTransform.anchoredPosition;
        // ���̽�ƽ�� �̵����� ����
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
        // ���̽�ƽ�� �̵����� ����
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
