using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera CurrentCamera;


    // ** 바라볼 타겟 
    [SerializeField] private GameObject Target;

    // ** 실제 바라볼 위치. (타겟이 카메라 중심으로 정중앙보다 살잘 아래에 위치하게함.)
    private Vector3 TargetPoint;




    // ** 시작점           SmoothTime         도착지점
    // **   ↓                 ↓                 ↓
    // **   [//////////////////                 ]
    // ** SmoothTime = 0 ~ 1 까지 누적 값. 0은 시작점이고 1은 도착지점.
    private float SmoothTime;

    // ** 카메라 속도
    private float CameraSpeed;


    // ** 카메라가 월드상에 중심점과 실제 카메라 위치의 벡터값.
    private Vector3 Offset;

    private void Awake()
    {
        CurrentCamera = GetComponent<Camera>();
    }

    void Start()
    {
        // ** Vector3.zero + Vector3(0.0f, 13.5f, -13.0f)이 실제 카메라 위치.
        Offset = new Vector3(0.0f, 13.5f, -13.0f);

        // ** 카메라를 움직일 비율
        SmoothTime = 0.25f;

        // ** 카메라가 움직일 속도
        CameraSpeed = 5.0f;
    }

    void Update()
    {
        // ** 시작되자마자 바로 카메라가 실제 바라볼 위치를 셋팅.
        TargetPoint = Target.transform.position + Target.transform.forward * 5.0f;


        // ** 마우스 우측키 입력으로 타겟을 회전시킴.
        if (Input.GetMouseButton(1))
            Target.transform.Rotate(Target.transform.up * Input.GetAxis("Mouse X") * 5.0f);
        


        // ** 카메라의 부드러운 회전.
        transform.rotation = Quaternion.Lerp(
                transform.rotation,

                // ** (바라볼 지점 - 현재위치).normalized = 내위치에서 타겟을 바라볼 벡서의 방향
                Quaternion.LookRotation((TargetPoint - transform.position).normalized),
                SmoothTime * CameraSpeed * Time.deltaTime);



        // ** Target.transform.position 
        // ** Target.transform.forward = [x(0.0f), y(0.0f), z(1.0f)]
        // ** Target.transform.up      = [x(0.0f), y(1.0f), z(0.0f)]

        // ** 벡터와 스칼라의 곱 : 5.0f * V1(x, y, z) = (x * 5.0f, y * 5.0f, z * 5.0f)
        Vector3 Movement = Target.transform.position +
            (Target.transform.forward * Offset.z) + (Target.transform.up * Offset.y);



        // ** 카메라의 부드러운 이동.
        transform.position = Vector3.Lerp(
            transform.position,
            Movement,
            SmoothTime * CameraSpeed * Time.deltaTime);
    }
}
