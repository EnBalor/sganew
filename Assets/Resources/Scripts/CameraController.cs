using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera CurrentCamera;


    // ** �ٶ� Ÿ�� 
    [SerializeField] private GameObject Target;

    // ** ���� �ٶ� ��ġ. (Ÿ���� ī�޶� �߽����� ���߾Ӻ��� ���� �Ʒ��� ��ġ�ϰ���.)
    private Vector3 TargetPoint;




    // ** ������           SmoothTime         ��������
    // **   ��                 ��                 ��
    // **   [//////////////////                 ]
    // ** SmoothTime = 0 ~ 1 ���� ���� ��. 0�� �������̰� 1�� ��������.
    private float SmoothTime;

    // ** ī�޶� �ӵ�
    private float CameraSpeed;


    // ** ī�޶� ����� �߽����� ���� ī�޶� ��ġ�� ���Ͱ�.
    private Vector3 Offset;

    private void Awake()
    {
        CurrentCamera = GetComponent<Camera>();
    }

    void Start()
    {
        // ** Vector3.zero + Vector3(0.0f, 13.5f, -13.0f)�� ���� ī�޶� ��ġ.
        Offset = new Vector3(0.0f, 13.5f, -13.0f);

        // ** ī�޶� ������ ����
        SmoothTime = 0.25f;

        // ** ī�޶� ������ �ӵ�
        CameraSpeed = 5.0f;
    }

    void Update()
    {
        // ** ���۵��ڸ��� �ٷ� ī�޶� ���� �ٶ� ��ġ�� ����.
        TargetPoint = Target.transform.position + Target.transform.forward * 5.0f;


        // ** ���콺 ����Ű �Է����� Ÿ���� ȸ����Ŵ.
        if (Input.GetMouseButton(1))
            Target.transform.Rotate(Target.transform.up * Input.GetAxis("Mouse X") * 5.0f);
        


        // ** ī�޶��� �ε巯�� ȸ��.
        transform.rotation = Quaternion.Lerp(
                transform.rotation,

                // ** (�ٶ� ���� - ������ġ).normalized = ����ġ���� Ÿ���� �ٶ� ������ ����
                Quaternion.LookRotation((TargetPoint - transform.position).normalized),
                SmoothTime * CameraSpeed * Time.deltaTime);



        // ** Target.transform.position 
        // ** Target.transform.forward = [x(0.0f), y(0.0f), z(1.0f)]
        // ** Target.transform.up      = [x(0.0f), y(1.0f), z(0.0f)]

        // ** ���Ϳ� ��Į���� �� : 5.0f * V1(x, y, z) = (x * 5.0f, y * 5.0f, z * 5.0f)
        Vector3 Movement = Target.transform.position +
            (Target.transform.forward * Offset.z) + (Target.transform.up * Offset.y);



        // ** ī�޶��� �ε巯�� �̵�.
        transform.position = Vector3.Lerp(
            transform.position,
            Movement,
            SmoothTime * CameraSpeed * Time.deltaTime);
    }
}
