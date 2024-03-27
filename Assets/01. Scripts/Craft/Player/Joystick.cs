using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Components")]
    [Space(2)]
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    private Vector2 originPos;
    private Vector2 originScale;

    [SerializeField]
    private Image backGroundImage;
    [SerializeField]
    private Image leverImage;

    [Space(3)]
    [Header("Specs")]
    [Space(2)]
    [Header("���̽�ƽ ������ ���� �̵����� ����")]
    [SerializeField, Range(10, 150)]
    private float stickRange;

    [TooltipAttribute("���̽�ƽ �̵� �� ������ �Ÿ��� ���� �ȱ� -> �ٱ�� ��ȯ�� �Ӱ�ġ (������ �ִ� ���ѰŸ����� �۾ƾ���.)")]
    [SerializeField]
    private float typeChangeThreshold;

    [SerializeField]
    private float leverDistance;
    public float LeverDistance { get { return leverDistance; } }

    [Space(3)]
    [Header("Balancing")]
    [Space(2)]
    [SerializeField]
    private Vector3 moveDir;
    public Vector3 MoveDir { get { return moveDir; } }

    private bool isRun = false;
    public bool IsRun { get { return isRun; } }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // ȭ�� ���ϴ��� ��Ŀ�������� ù ��ġ�� �Ҵ�
        originPos = rectTransform.anchoredPosition;
        originScale = new Vector2(rectTransform.rect.width,rectTransform.rect.height);
    }
    private void Start()
    {
        Debug.Log(lever.anchoredPosition);
        Debug.Log(rectTransform.anchoredPosition);
    }

    private void Update()
    {
        SetDirection();
    }

    public void SetDirection()
    {
        // ���̽�ƽ ������ ���� �̵� ���⼳��
        Vector3 vec = lever.anchoredPosition;
        moveDir = vec.normalized;
        leverDistance = vec.magnitude;

        // �ȱ� ���� -> �޸��� ���� ��ȯ���� ����
        isRun = leverDistance >= typeChangeThreshold;
    }

    public void EnableJoystick(Vector2 enablePos)
    {
        rectTransform.anchoredPosition = enablePos - originPos;

        // �̹��� ���İ� �����ϰ� ����
        backGroundImage.color = new Color32(255,255,255, 150);
        leverImage.color = new Color32(255,255,255, 255);
    }
    public void DisableJoystick()
    {
        // �ʱ���� (��Ȱ��ȭ ���·� ����)
        rectTransform.anchoredPosition = originPos;
        lever.anchoredPosition = Vector2.zero;
        leverDistance = 0f;
        moveDir = Vector3.zero;

        // �̹��� ���İ� �������ϰ� ����
        backGroundImage.color = new Color32(255,255,255, 80);
        leverImage.color = new Color32(255,255,255, 120);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("�̺�Ʈ ������ " + eventData.position);
        //var inputPos = eventData.position - rectTransform.anchoredPosition - originPos - originScale/2;
        // ���̽�ƽ�� �̵����� ����
        //var inputVector = inputPos.magnitude < stickRange ? inputPos : inputPos.normalized * stickRange;
        //lever.anchoredPosition = inputVector;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition - originPos - originScale/2;
        // ���̽�ƽ�� �̵����� ����
        var inputVector = inputPos.magnitude < stickRange ? inputPos : inputPos.normalized * stickRange;
        lever.anchoredPosition = inputVector;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DisableJoystick();
    }
}
