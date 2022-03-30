using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator Anim;
    
    private float Speed;

    // ** ���� ��� ����Ʈ.
    [SerializeField] private GameObject AttackEffect;

    // ** ��Ÿ�� �̹����� ��� .
    [SerializeField] private Image AttackSkill;

    // ** ��ų ��ٿ��� �Ϸ�Ǿ����� ����Ʈ.
    [SerializeField] private GameObject EffectObject;

    // ** SkillListCanvas
    [SerializeField] private GameObject SkillListCanvas;

    // ** ����Ʈ â ����/�ݱ� Ȯ��.
    private bool SkillLisCheck;


    // ** ���� ���� �Ǵ��ϱ� ����.
    private bool Attack;

    void Start()
    {
        Speed = 5.0f;
        
        // ** �����Ҷ� �������� �ƴѰ����� ����
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
        // ** �������̶�� ���� �Լ��� ����.
        if (Attack == true)
            return;

        // ** �������� �ƴ϶�� ���ݻ��·� �����ϰ�
        Attack = true;

        // ** ���� �ִϸ��̼��� �۵�.
        Anim.SetTrigger("Attack");

        // ** �߻� ����Ʈ�� �۵�.
        AttackEffect.SetActive(true);

        // ** ��Ÿ�� �̹����� �����ϰ� ����.
        AttackSkill.fillAmount = 0.0f;

        // ** ��Ÿ�� ���� ���������� Ȯ���ϱ����� �ڷ�ƾ�Լ��� ����.
        StartCoroutine( SkillCoolTime() );
    }

    IEnumerator SkillCoolTime()
    {
        // ** �ð��� ������ų ��Ÿ���� ����
        float CoolTime = 0;

        // ** fillAmount �� 1 �϶� �̹��� ��ü�� ���̰�, 0 �϶��� �Ⱥ���.
        // ** fillAmount �� ����� 1���� ������ �ݺ���.
        while (AttackSkill.fillAmount < 1.0f)
        {
            // ** ��Ÿ���� 4�ʷ� �����ϱ� ���� 0.25�� ���ؼ� �ð��� ������Ŵ.
            CoolTime += Time.deltaTime * 0.25f;

            // ** ������ �ð� ��ŭ�� ������ �̹����� ���� ��Ŵ.
            AttackSkill.fillAmount = Mathf.Lerp(0.0f, 1.0f, CoolTime);

            // ** yield return null �̶�� �� �����Ӹ��� �����.
            yield return null;
        }
        // ** fillAmount�� 1�̵Ǹ� �� �ݺ����� �����.


        // ** �׸��� ��ų�� ���ݻ��¸� �ٽ� Ȱ��ȭ ��Ŵ.
        Attack = false;
        AttackEffect.SetActive(false);

        // ** ��ٿ��� �Ϸ�Ǿ����� ����Ʈ�� �۵���Ŵ.
        EffectObject.SetActive(true);
    }
}
