using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class MyGUI : MonoBehaviour
{
    public void Output()
    {
        Debug.Log("Output");
    }
}





[CustomEditor(typeof(MyGUI))]
public class MyGUIInspector : Editor
{
    MyGUI Target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Target = (MyGUI)target;

        if (Target == null)
            return;

        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("Button"))
        {
            Target.Output();
        }

        EditorGUILayout.EndVertical();
    }
}


[CustomEditor(typeof(MyGUI))]
public class MyGUIEditor : EditorWindow
{
    private GameObject[] Enemys = new GameObject[4];

    private List<Object> EmptyObjects = new List<Object>();


    [MenuItem("Editor Tools/ControllBox")]
    static void ControllBox()
    {
        GetWindow(typeof(MyGUIEditor)).Show();
    }

    private void Awake()
    {
        GameObject Manager = new GameObject("EnemyManager");

        for (int i = 0; i < Enemys.Length; ++i)
        {
            Enemys[i] = new GameObject("Enemy_" + i.ToString());
            Enemys[i].transform.SetParent(Manager.transform);
        }

        for (int i = 0; i < 10; ++i)
            EmptyObjects.Add(new Object());
    }

    private void OnGUI()
    {

        /*****************************************************************************************************************
         * [EditorStyles]  �۲�
         * 
         * ����
         * https://docs.unity3d.com/kr/530/ScriptReference/EditorStyles.html
         * 
         * boldFont	���� �۲�(Bold)
         * boldLabel	���� �۲� ��Ÿ��
         * centeredGreyMiniLabel	���� �۲��� �� ��Ÿ��
         * colorField	���� �ʵ忡�� ���Ǵ� ��Ÿ��
         * foldout	EditorGUI.Foldout���� ���Ǵ� ��Ÿ��
         * foldoutPreDrop	EditorGUI.Foldout���� ���Ǵ� ��Ÿ��
         * helpBox	EditorGUI.HelpBox�� ���Ǵ� ��Ÿ��
         * inspectorDefaultMargins	�����ڿ��� ���Ǵ� �⺻ ������ ��� ���� �������� �� ��Ÿ�Ϸ� ���� �׷쿡�� ���մϴ�.
         * inspectorFullWidthMargins	�����ڿ��� ���Ǵ� ���� ������ ��� ���� �������� �� ��Ÿ�Ϸ� ���� �׷쿡�� ���մϴ�.
         * label	���� �ٴ� ��� EditorGUI���� ���Ǵ� ��Ÿ��
         * largeLabel	ū �۲��� �� ��Ÿ��
         * layerMaskField	���̾� ����ũ���� ���Ǵ� ��Ÿ��
         * miniBoldFont	�۰� ���� �۲�
         * miniBoldLabel	���� �۾��� ���� �󺧿� ���Ǵ� ��Ÿ��
         * miniButton	���� ��ư�� ��Ÿ��
         * miniButtonLeft	�۰� �������� ��ư ��Ÿ��
         * miniButtonMid	�۰� �߾ӿ� ����� ��ư ��Ÿ��
         * miniButtonRight	�۰� �������� ��ư ��Ÿ��
         * miniFont	    ���� �۲�
         * miniLabel	���� �۲��� �� ��Ÿ��
         * miniTextField	���� �ؽ�Ʈ �ʵ忡�� ���Ǵ� ��Ÿ��
         * numberField	���ڸ� �Է��ϴ� �ʵ忡�� ���Ǵ� ��Ÿ��
         * objectField	������Ʈ �ʵ忡�� ���Ǵ� ��Ÿ��
         * objectFieldMiniThumb	Style used for object fields that have a thumbnail (e.g Textures).
         * objectFieldThumb	������Ʈ �ʵ忡�� ���Ǵ� ������ �̹����� ��Ÿ��
         * popup	EditorGUI.Popup�� EditorGUI.EnumPopup���� ���Ǵ� ��Ÿ��
         * radioButton	���� ��ư�� ���Ǵ� ��Ÿ��
         * standardFont	�Ϲ����� ǥ�� �۲�
         * textArea	EditorGUI.TextArea�� ���Ǵ� ��Ÿ��
         * textField	EditorGUI.TextField���� ���Ǵ� ��Ÿ��
         * toggle	EditorGUI.Toggle���� ���Ǵ� ��Ÿ��
         * toggleGroup	EditorGUILayout.BeginToggleGroup���� ���Ǵ� ��Ÿ��
         * toolbar	â ��ܿ� ǥ���� ���� ������ ��� ��Ÿ��
         * toolbarButton	���� ���� ���߿��� ���Ǵ� ��Ÿ��
         * toolbarDropDown	���� ������ ��� �ٿ�� ���Ǵ� ��Ÿ��
         * toolbarPopup	���� ������ �˾����� ���Ǵ� ��Ÿ��
         * toolbarTextField	���� ������ �ؽ�Ʈ �ʵ忡 ���Ǵ� ��Ÿ��
         * whiteBoldLabel	��� �����̰� ���� �� ��Ÿ��
         * whiteLabel	��� ���� �� ��Ÿ��
         * whiteLargeLabel	��� ������ ū �۲� �� ��Ÿ��
         * whiteMiniLabel	��� ���� ���� �۲��� �� ��Ÿ��
         * wordWrappedLabel	���� �̻��� ���ڰ� �ԷµǾ��� ��, ��ȯ�ϴ� ��Ÿ��
         * wordWrappedMiniLabel	���� �۲÷� ���� �̻��� ���ڰ� �ԷµǾ��� ��, ��ȯ�ϴ� ��Ÿ��
         ******************************************************************************************************************/


        Dropdown.OptionData dl = new Dropdown.OptionData();

        List<GameObject> EnemyList = new List<GameObject>();

        object[] Objects = Resources.LoadAll<GameObject>("Prefabs/Enemys");

        foreach (GameObject Element in Objects)
            EnemyList.Add(Element);



        EditorGUILayout.BeginVertical();

        GUILayout.Space(10);
        GUILayout.Label("Create Enemy", EditorStyles.boldLabel);

        /***********************************************************
         * [GUILayout]
         
         * GUILayout.Width(��)
         * GUILayout.Height(��)
         * GUILayout.MinWidth(�� �ּҰ�)
         * GUILayout.MaxWidth(�� �ִ밪)
         * GUILayout.MinHeight(���� �ּҰ�)
         * GUILayout.MaxHeight(���� �ִ밪)
         * GUILayout.ExpandWidth(true or false : ���� Ȯ���� ����ϰų� ���´�)
         * GUILayout.ExpandHeight(true or false : ���� Ȯ���� ����ϰų� ���´�)
          
         **********************************************************/

        // ** �ɼǺ��� �����ϱ� ���� �������� ������. (EditorStyles : boldLabel = ���� �۲� ��Ÿ��)

        for (int i = 0; i < Objects.Length; ++i)
        {
            if (GUILayout.Button("Create Enemy_" + i.ToString(), GUILayout.MaxWidth(500), GUILayout.MaxHeight(25)))
            {
                GameObject Obj = Instantiate(EnemyList[i]);

                Obj.transform.position = Vector3.zero;
                Obj.transform.name = "Enemy" + i.ToString();

                Obj.transform.SetParent(Enemys[i].transform);
            }
        }
        EditorGUILayout.EndVertical();




        GUILayout.Space(15);

        EditorGUILayout.BeginVertical();

        GUILayout.Label("Create Skill", EditorStyles.boldLabel);

        for (int i = 0; i < EmptyObjects.Count; ++i)
        {
            EmptyObjects[i] = (EditorGUILayout.ObjectField("Skill Prefab", EmptyObjects[i], typeof(Sprite), false));

            if (GUILayout.Button("Create Skill"))
            {
                Sprite sp = Instantiate((Sprite)EmptyObjects[i]);

                GameObject TempObj = new GameObject("Skill");
                TempObj.AddComponent<Image>();

                Image Tex = TempObj.GetComponent<Image>();
                Tex.sprite = sp;

                TempObj.transform.SetParent( GameObject.Find("MainMenuCanvas").transform );
            }

            GUILayout.Space(5);
        }

        EditorGUILayout.EndVertical();
    }
}

