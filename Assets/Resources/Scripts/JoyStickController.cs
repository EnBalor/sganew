using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("������ ���")]
    public GameObject Character;

    private PlayerController PlayerControll;

    [Header("JoyStick")]
    [Tooltip("������ ����� �������� �����ϴ� ���� ��ƽ")]
    [SerializeField] protected RectTransform RightFilledStick;
    [SerializeField] protected int RightStickID;

    [Tooltip("������ ���̽�ƽ�� �� ���")]
    [SerializeField] protected GameObject RightJoyPad;
    [SerializeField] protected RectTransform RightBackBoard;


    [Header("JoyStick")]
    [Tooltip("������ ����� �������� �����ϴ� ���� ��ƽ")]
    [SerializeField] protected RectTransform LeftFilledStick;
    [SerializeField] protected int LeftStickID;

    [Tooltip("���� ���̽�ƽ�� �� ���")]
    [SerializeField] protected GameObject LeftJoyPad;
    [SerializeField] protected RectTransform LeftBackBoard;

    [SerializeField] protected bool TouchCheck;

    // ** ���̽�ƽ ������
    protected float Radius;

    // ** Ÿ���� �̵� �ӵ�
    protected float Speed;

    // ** Ÿ���� �̵��� ���� �� �ӵ� ���� �ݿ��� �ӽ� ������
    // (���̽�ƽ�� 2���� ���� 3�������� ������Ű�� ����.)
    protected Vector3 Movement;

    // ** Ÿ���� �ٶ� ����.
    protected Vector3 Rotation;


    private bool Moveing;



    private void Awake()
    {
        // ** ������ ���̽�ƽ�� �е带 �޾ƿ�. 
        RightFilledStick = GameObject.Find("RightFilledCircle").GetComponent<RectTransform>();

        // ** ������ ���̽�ƽ�� �θ�ü(�� ���� ������Ʈ)
        RightJoyPad = GameObject.Find("RightOutLineCircle");

        // ** ������ ���̽�ƽ�� �� ����� �޾ƿ�. 
        RightBackBoard = RightJoyPad.GetComponent<RectTransform>();

        // ** ���� ���̽�ƽ�� �е带 �޾ƿ�. 
        LeftFilledStick = GameObject.Find("LeftFilledCircle").GetComponent<RectTransform>();

        // ** ���� ���̽�ƽ�� �θ�ü(�� ���� ������Ʈ)
        LeftJoyPad = GameObject.Find("LeftOutLineCircle");

        // ** ���� ���̽�ƽ�� �� ����� �޾ƿ�. 
        LeftBackBoard = LeftJoyPad.GetComponent<RectTransform>();

        PlayerControll = Character.GetComponent<PlayerController>();
    }

    private void Start()
    {


        TouchCheck = false;

        // ** �������� ����.
        Radius = ((int)LeftBackBoard.rect.width >> 1);

        // ** �̵��ӵ� ����
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
                // ** �׸��� ��ġ�� ��ġ�� ���̽�ƽ�� ��ġ�� ������.
                LeftBackBoard.position = Input.mousePosition;
            }
            else
            {
                // ** �׸��� ��ġ�� ��ġ�� ���̽�ƽ�� ��ġ�� ������.
                RightBackBoard.position = Input.mousePosition;
            }
        }

        if (Input.GetMouseButton(0))
        {
            // ** �÷��� ��� ����.
            //UnityEditor.EditorApplication.isPlaying = false;

            if ((Screen.width >> 1) < Input.mousePosition.x)
            {
                // ** ��ġ�� �Էµ� ��ġ�� �̵���Ŵ.
                RightFilledStick.localPosition = new Vector2(
                    Input.mousePosition.x - RightBackBoard.position.x,
                    Input.mousePosition.y - RightBackBoard.position.y);

                // ** ���Ѽ� ����. (Radius �� �������� ���� ���ϰ� ��.)
                RightFilledStick.localPosition = Vector2.ClampMagnitude(
                    RightFilledStick.localPosition, Radius);

                // ** ���̽�ƽ�� ��ƽ ������ �޾ƿ�.
                Vector3 Direction = RightFilledStick.localPosition.normalized;

                Rotation = Direction.normalized;
            }
            else
            {
                // ** ��ġ�� �Էµ� ��ġ�� �̵���Ŵ.
                LeftFilledStick.localPosition = new Vector2(
                    Input.mousePosition.x - LeftBackBoard.position.x,
                    Input.mousePosition.y - LeftBackBoard.position.y);

                // ** ���Ѽ� ����. (Radius �� �������� ���� ���ϰ� ��.)
                LeftFilledStick.localPosition = Vector2.ClampMagnitude(
                    LeftFilledStick.localPosition, Radius);

                // ** ���̽�ƽ�� ��ƽ ������ �޾ƿ�.
                Vector3 Direction = LeftFilledStick.localPosition.normalized;

                // ** Ratio = ����
                // ** ���̽�ƽ�� �巹���� �Ÿ��� ���� ������ Ȯ���� �巹�� �� �Ÿ���ŭ�� ������ �ӵ��� ������ ��.
                float Ratio = (LeftBackBoard.position - LeftFilledStick.position).sqrMagnitude / (Radius * Radius);

                // ** ������ ��ǥ���� �������ͷ� ������Ŵ.
                // ** ������Ű�鼭 �ӵ����� �߰���.
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
                // ** ���̽�ƽ�� �ٽ� ����ġ ��Ŵ.
                LeftBackBoard.localPosition = Vector3.zero;
                LeftFilledStick.localPosition = Vector3.zero;
            }
            else
            {
                // ** ���̽�ƽ�� �ٽ� ����ġ ��Ŵ.
                RightBackBoard.localPosition = Vector3.zero;
                RightFilledStick.localPosition = Vector3.zero;


                // ** ����ü �߻� ����.
                //PlayerControll.AddBullet();
            }
        }
#else
        for (int i = 0; i < Input.touchCount; ++i)
        {
            // ** ��ġ�� �Է� �޴´�.
            Touch GetTouch = Input.GetTouch(i);

            // ** �Է¹��� ��ġ ���� ����.
            Vector2 Point = GetTouch.position;

            // ** ��ġ�� �Էµ� ����....
            if (GetTouch.phase == TouchPhase.Began)
            {
                if ((Screen.width >> 1) > GetTouch.position.x)
                {
                    // ** �Է¹��� ��ġ�� ID�� �ο�����.
                    LeftStickID = GetTouch.fingerId;

                    LeftBackBoard.position = Point;
                }
                else
                {
                    // ** �Է¹��� ��ġ�� ID�� �ο�����.
                    RightStickID = GetTouch.fingerId;

                    TouchCheck = true;

                    RightBackBoard.position = Point;
                }
            }
            else if(GetTouch.phase == TouchPhase.Moved)
            {
                if(GetTouch.fingerId == RightStickID)
                {
                    // ** ��ġ�� �Էµ� ��ġ�� �̵���Ŵ.
                    RightFilledStick.localPosition = new Vector2(
                        Point.x - RightBackBoard.position.x,
                        Point.y - RightBackBoard.position.y);

                    // ** ���Ѽ� ����. (Radius �� �������� ���� ���ϰ� ��.)
                    RightFilledStick.localPosition = Vector2.ClampMagnitude(
                        RightFilledStick.localPosition, Radius);

                    // ** ���̽�ƽ�� ��ƽ ������ �޾ƿ�.
                    Vector3 Direction = RightFilledStick.localPosition.normalized;

                    Rotation = Direction.normalized;
                }
                
                if (GetTouch.fingerId == LeftStickID)
                {
                    // ** ��ġ�� �Էµ� ��ġ�� �̵���Ŵ.
                    LeftFilledStick.localPosition = new Vector2(
                        Point.x - LeftBackBoard.position.x,
                        Point.y - LeftBackBoard.position.y);

                    // ** ���Ѽ� ����. (Radius �� �������� ���� ���ϰ� ��.)
                    LeftFilledStick.localPosition = Vector2.ClampMagnitude(
                        LeftFilledStick.localPosition, Radius);

                    // ** ���̽�ƽ�� ��ƽ ������ �޾ƿ�.
                    Vector3 Direction = LeftFilledStick.localPosition.normalized;

                    // ** Ratio = ����
                    // ** ���̽�ƽ�� �巹���� �Ÿ��� ���� ������ Ȯ���� �巹�� �� �Ÿ���ŭ�� ������ �ӵ��� ������ ��.
                    float Ratio = (LeftBackBoard.position - LeftFilledStick.position).sqrMagnitude / (Radius * Radius);

                    // ** ������ ��ǥ���� �������ͷ� ������Ŵ.
                    // ** ������Ű�鼭 �ӵ����� �߰���.
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
