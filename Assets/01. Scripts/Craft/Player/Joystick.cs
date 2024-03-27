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
    [Header("조이스틱 내부의 레버 이동범위 제한")]
    [SerializeField, Range(10, 150)]
    private float stickRange;

    [TooltipAttribute("조이스틱 이동 시 레버의 거리에 따라 걷기 -> 뛰기로 전환될 임계치 (레버의 최대 제한거리보다 작아야함.)")]
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
        // 화면 좌하단의 앵커에서부터 첫 위치를 할당
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
        // 조이스틱 레버에 따른 이동 방향설정
        Vector3 vec = lever.anchoredPosition;
        moveDir = vec.normalized;
        leverDistance = vec.magnitude;

        // 걷기 상태 -> 달리기 상태 전환조건 설정
        isRun = leverDistance >= typeChangeThreshold;
    }

    public void EnableJoystick(Vector2 enablePos)
    {
        rectTransform.anchoredPosition = enablePos - originPos;

        // 이미지 알파값 선명하게 복구
        backGroundImage.color = new Color32(255,255,255, 150);
        leverImage.color = new Color32(255,255,255, 255);
    }
    public void DisableJoystick()
    {
        // 초기상태 (비활성화 상태로 복구)
        rectTransform.anchoredPosition = originPos;
        lever.anchoredPosition = Vector2.zero;
        leverDistance = 0f;
        moveDir = Vector3.zero;

        // 이미지 알파값 반투명하게 조절
        backGroundImage.color = new Color32(255,255,255, 80);
        leverImage.color = new Color32(255,255,255, 120);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("이벤트 데이터 " + eventData.position);
        //var inputPos = eventData.position - rectTransform.anchoredPosition - originPos - originScale/2;
        // 조이스틱의 이동범위 제한
        //var inputVector = inputPos.magnitude < stickRange ? inputPos : inputPos.normalized * stickRange;
        //lever.anchoredPosition = inputVector;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition - originPos - originScale/2;
        // 조이스틱의 이동범위 제한
        var inputVector = inputPos.magnitude < stickRange ? inputPos : inputPos.normalized * stickRange;
        lever.anchoredPosition = inputVector;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DisableJoystick();
    }
}
