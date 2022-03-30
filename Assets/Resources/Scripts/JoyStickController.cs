using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("움직일 대상")]
    public GameObject Character;

    private PlayerController PlayerControll;

    [Header("JoyStick")]
    [Tooltip("움직일 대상의 움직임을 제어하는 왼쪽 스틱")]
    [SerializeField] protected RectTransform RightFilledStick;
    [SerializeField] protected int RightStickID;

    [Tooltip("오른쪽 조이스틱의 뒷 배경")]
    [SerializeField] protected GameObject RightJoyPad;
    [SerializeField] protected RectTransform RightBackBoard;


    [Header("JoyStick")]
    [Tooltip("움직일 대상의 움직임을 제어하는 왼쪽 스틱")]
    [SerializeField] protected RectTransform LeftFilledStick;
    [SerializeField] protected int LeftStickID;

    [Tooltip("왼쪽 조이스틱의 뒷 배경")]
    [SerializeField] protected GameObject LeftJoyPad;
    [SerializeField] protected RectTransform LeftBackBoard;

    [SerializeField] protected bool TouchCheck;

    // ** 조이스틱 반지름
    protected float Radius;

    // ** 타겟의 이동 속도
    protected float Speed;

    // ** 타겟이 이동할 방향 및 속도 등을 반영할 임시 포지션
    // (조이스틱의 2차원 값을 3차원으로 변형시키기 위함.)
    protected Vector3 Movement;

    // ** 타겟이 바라볼 방향.
    protected Vector3 Rotation;


    private bool Moveing;



    private void Awake()
    {
        // ** 오른쪽 조이스틱의 패드를 받아옴. 
        RightFilledStick = GameObject.Find("RightFilledCircle").GetComponent<RectTransform>();

        // ** 오른쪽 조이스틱의 부모객체(빈 게임 오브젝트)
        RightJoyPad = GameObject.Find("RightOutLineCircle");

        // ** 오른쪽 조이스틱의 뒷 배경을 받아옴. 
        RightBackBoard = RightJoyPad.GetComponent<RectTransform>();

        // ** 왼쪽 조이스틱의 패드를 받아옴. 
        LeftFilledStick = GameObject.Find("LeftFilledCircle").GetComponent<RectTransform>();

        // ** 왼쪽 조이스틱의 부모객체(빈 게임 오브젝트)
        LeftJoyPad = GameObject.Find("LeftOutLineCircle");

        // ** 왼쪽 조이스틱의 뒷 배경을 받아옴. 
        LeftBackBoard = LeftJoyPad.GetComponent<RectTransform>();

        PlayerControll = Character.GetComponent<PlayerController>();
    }

    private void Start()
    {


        TouchCheck = false;

        // ** 반지름을 구함.
        Radius = ((int)LeftBackBoard.rect.width >> 1);

        // ** 이동속도 설정
        Speed = 5.0f;

        Moveing = false;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Moveing = true;
            if ((Screen.width >> 1) > Input.mousePosition.x)
            {
                // ** 그리고 터치된 위치로 조이스틱의 위치를 변경함.
                LeftBackBoard.position = Input.mousePosition;
            }
            else
            {
                // ** 그리고 터치된 위치로 조이스틱의 위치를 변경함.
                RightBackBoard.position = Input.mousePosition;
            }
        }

        if (Input.GetMouseButton(0))
        {
            // ** 플레이 모드 종료.
            //UnityEditor.EditorApplication.isPlaying = false;

            if ((Screen.width >> 1) < Input.mousePosition.x)
            {
                // ** 터치가 입력된 위치로 이동시킴.
                RightFilledStick.localPosition = new Vector2(
                    Input.mousePosition.x - RightBackBoard.position.x,
                    Input.mousePosition.y - RightBackBoard.position.y);

                // ** 상한선 제한. (Radius 의 반지름을 넘지 못하게 함.)
                RightFilledStick.localPosition = Vector2.ClampMagnitude(
                    RightFilledStick.localPosition, Radius);

                // ** 조이스틱의 스틱 방향을 받아옴.
                Vector3 Direction = RightFilledStick.localPosition.normalized;

                Rotation = Direction.normalized;
            }
            else
            {
                // ** 터치가 입력된 위치로 이동시킴.
                LeftFilledStick.localPosition = new Vector2(
                    Input.mousePosition.x - LeftBackBoard.position.x,
                    Input.mousePosition.y - LeftBackBoard.position.y);

                // ** 상한선 제한. (Radius 의 반지름을 넘지 못하게 함.)
                LeftFilledStick.localPosition = Vector2.ClampMagnitude(
                    LeftFilledStick.localPosition, Radius);

                // ** 조이스틱의 스틱 방향을 받아옴.
                Vector3 Direction = LeftFilledStick.localPosition.normalized;

                // ** Ratio = 비율
                // ** 조이스틱을 드레그한 거리에 대한 비율을 확인후 드레그 한 거리만큼의 값으로 속도에 강약을 줌.
                float Ratio = (LeftBackBoard.position - LeftFilledStick.position).sqrMagnitude / (Radius * Radius);

                // ** 평면상의 좌표값을 공간벡터로 변형시킴.
                // ** 변형시키면서 속도값을 추가함.
                Movement = new Vector3(
                    Direction.x * Speed * Ratio * Time.deltaTime,
                    0.0f,
                    Direction.y * Speed * Ratio * Time.deltaTime);

                Rotation = Direction.normalized;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Moveing = false;

            if ((Screen.width >> 1) > Input.mousePosition.x)
            {
                // ** 조이스틱을 다시 원위치 시킴.
                LeftBackBoard.localPosition = Vector3.zero;
                LeftFilledStick.localPosition = Vector3.zero;
            }
            else
            {
                // ** 조이스틱을 다시 원위치 시킴.
                RightBackBoard.localPosition = Vector3.zero;
                RightFilledStick.localPosition = Vector3.zero;


                // ** 투사체 발사 예정.
                //PlayerControll.AddBullet();
            }
        }
#else
        for (int i = 0; i < Input.touchCount; ++i)
        {
            // ** 터치를 입력 받는다.
            Touch GetTouch = Input.GetTouch(i);

            // ** 입력받은 위치 값을 참조.
            Vector2 Point = GetTouch.position;

            // ** 터치가 입력된 순간....
            if (GetTouch.phase == TouchPhase.Began)
            {
                if ((Screen.width >> 1) > GetTouch.position.x)
                {
                    // ** 입력받은 터치의 ID를 부여받음.
                    LeftStickID = GetTouch.fingerId;

                    LeftBackBoard.position = Point;
                }
                else
                {
                    // ** 입력받은 터치의 ID를 부여받음.
                    RightStickID = GetTouch.fingerId;

                    TouchCheck = true;

                    RightBackBoard.position = Point;
                }
            }
            else if(GetTouch.phase == TouchPhase.Moved)
            {
                if(GetTouch.fingerId == RightStickID)
                {
                    // ** 터치가 입력된 위치로 이동시킴.
                    RightFilledStick.localPosition = new Vector2(
                        Point.x - RightBackBoard.position.x,
                        Point.y - RightBackBoard.position.y);

                    // ** 상한선 제한. (Radius 의 반지름을 넘지 못하게 함.)
                    RightFilledStick.localPosition = Vector2.ClampMagnitude(
                        RightFilledStick.localPosition, Radius);

                    // ** 조이스틱의 스틱 방향을 받아옴.
                    Vector3 Direction = RightFilledStick.localPosition.normalized;

                    Rotation = Direction.normalized;
                }
                
                if (GetTouch.fingerId == LeftStickID)
                {
                    // ** 터치가 입력된 위치로 이동시킴.
                    LeftFilledStick.localPosition = new Vector2(
                        Point.x - LeftBackBoard.position.x,
                        Point.y - LeftBackBoard.position.y);

                    // ** 상한선 제한. (Radius 의 반지름을 넘지 못하게 함.)
                    LeftFilledStick.localPosition = Vector2.ClampMagnitude(
                        LeftFilledStick.localPosition, Radius);

                    // ** 조이스틱의 스틱 방향을 받아옴.
                    Vector3 Direction = LeftFilledStick.localPosition.normalized;

                    // ** Ratio = 비율
                    // ** 조이스틱을 드레그한 거리에 대한 비율을 확인후 드레그 한 거리만큼의 값으로 속도에 강약을 줌.
                    float Ratio = (LeftBackBoard.position - LeftFilledStick.position).sqrMagnitude / (Radius * Radius);

                    // ** 평면상의 좌표값을 공간벡터로 변형시킴.
                    // ** 변형시키면서 속도값을 추가함.
                    Movement = new Vector3(
                        Direction.x * Speed * Ratio * Time.deltaTime,
                        0.0f,
                        Direction.y * Speed * Ratio * Time.deltaTime);

                    if(!TouchCheck)
                        Rotation = Direction.normalized;
                }
            }
            else if(GetTouch.phase == TouchPhase.Ended || GetTouch.phase == TouchPhase.Canceled)
            {
                if (GetTouch.fingerId == RightStickID)
                {
                    RightFilledStick.localPosition = Vector3.zero;
                    RightBackBoard.localPosition = Vector3.zero;
                    TouchCheck = false;
                    RightStickID = -1;
                }

                if (GetTouch.fingerId == LeftStickID)
                {
                    LeftFilledStick.localPosition = Vector3.zero;
                    LeftBackBoard.localPosition = Vector3.zero;
                    Movement = Vector3.zero;
                    LeftStickID = -1;
                }
            }
        }
#endif
    }

    private void LateUpdate()
    {
        if(!Moveing)
        {
            if (Input.touchCount == 0)
            {
                LeftStickID = -1;
                RightStickID = -1;
                TouchCheck = false;
            }
            
            Movement = Vector3.zero;
        }
        

        Character.transform.position += Movement;

        Character.transform.localRotation = Quaternion.Euler(
            0.0f,
            Mathf.Atan2(Rotation.x, Rotation.y) * Mathf.Rad2Deg,
            0.0f);
    }
}
