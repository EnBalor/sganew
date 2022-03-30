using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator Anim;
    
    private float Speed;

    // ** 발포 모션 이펙트.
    [SerializeField] private GameObject AttackEffect;

    // ** 쿨타임 이미지에 사용 .
    [SerializeField] private Image AttackSkill;

    // ** 스킬 쿨다운이 완료되었을때 이펙트.
    [SerializeField] private GameObject EffectObject;

    // ** SkillListCanvas
    [SerializeField] private GameObject SkillListCanvas;

    // ** 리스트 창 열기/닫기 확인.
    private bool SkillLisCheck;


    // ** 현재 공격 판단하기 위함.
    private bool Attack;

    void Start()
    {
        Speed = 5.0f;
        
        // ** 시작할때 공격중이 아닌것으로 셋팅
        Attack = false;

        AttackEffect.SetActive(false);
        
        EffectObject.SetActive(false);

        SkillListCanvas.SetActive(false);

        SkillLisCheck = false;
    }

    void Update()
    {
        float Ver = Input.GetAxisRaw("Vertical");
        float Hor = Input.GetAxisRaw("Horizontal");

        Anim.SetFloat("Horizontal", Hor);
        Anim.SetFloat("Speed", Ver);

        if(Input.GetKeyDown(KeyCode.Space))
            Fire();

        if (Input.GetKeyDown(KeyCode.Tab))
            SkillCanvasInvisible();

        Vector3 vMovement = new Vector3(0.0f, 0.0f, Ver * Speed * Time.deltaTime);
        transform.position += vMovement;
    }

    public void SkillCanvasInvisible()
    {
        SkillLisCheck = !SkillLisCheck;
        SkillListCanvas.SetActive(SkillLisCheck);
    }
    
    public void Fire()
    {
        // ** 공격중이라면 현재 함수를 종료.
        if (Attack == true)
            return;

        // ** 공격중이 아니라면 공격상태로 설정하고
        Attack = true;

        // ** 공격 애니매이션을 작동.
        Anim.SetTrigger("Attack");

        // ** 발사 이펙트를 작동.
        AttackEffect.SetActive(true);

        // ** 쿨타임 이미지를 투명하게 만듬.
        AttackSkill.fillAmount = 0.0f;

        // ** 쿨타임 동안 지속적으로 확인하기위해 코루틴함수를 실행.
        StartCoroutine( SkillCoolTime() );
    }

    IEnumerator SkillCoolTime()
    {
        // ** 시간을 누적시킬 쿨타임을 생성
        float CoolTime = 0;

        // ** fillAmount 가 1 일때 이미지 전체가 보이고, 0 일때는 안보임.
        // ** fillAmount 가 현재는 1보다 작을때 반복됨.
        while (AttackSkill.fillAmount < 1.0f)
        {
            // ** 쿨타임을 4초로 설정하기 위해 0.25를 곱해서 시간을 누적시킴.
            CoolTime += Time.deltaTime * 0.25f;

            // ** 누적된 시간 만큼의 비율로 이미지를 노출 시킴.
            AttackSkill.fillAmount = Mathf.Lerp(0.0f, 1.0f, CoolTime);

            // ** yield return null 이라면 매 프레임마다 실행됨.
            yield return null;
        }
        // ** fillAmount가 1이되면 위 반복문은 종료됨.


        // ** 그리고 스킬과 공격상태를 다시 활성화 시킴.
        Attack = false;
        AttackEffect.SetActive(false);

        // ** 쿨다운이 완료되었을때 이펙트를 작동시킴.
        EffectObject.SetActive(true);
    }
}
