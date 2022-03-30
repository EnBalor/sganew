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
         * [EditorStyles]  글꼴
         * 
         * 참고
         * https://docs.unity3d.com/kr/530/ScriptReference/EditorStyles.html
         * 
         * boldFont	굵은 글꼴(Bold)
         * boldLabel	굵은 글꼴 스타일
         * centeredGreyMiniLabel	작은 글꼴의 라벨 스타일
         * colorField	색상 필드에서 사용되는 스타일
         * foldout	EditorGUI.Foldout에서 사용되는 스타일
         * foldoutPreDrop	EditorGUI.Foldout에서 사용되는 스타일
         * helpBox	EditorGUI.HelpBox에 사용되는 스타일
         * inspectorDefaultMargins	관리자에서 사용되는 기본 여백을 얻기 위해 콘텐츠를 이 스타일로 수직 그룹에서 랩합니다.
         * inspectorFullWidthMargins	관리자에서 사용되는 폭의 여백을 얻기 위해 콘텐츠를 이 스타일로 수직 그룹에서 랩합니다.
         * label	라벨이 붙는 모든 EditorGUI에서 사용되는 스타일
         * largeLabel	큰 글꼴의 라벨 스타일
         * layerMaskField	레이어 마스크에서 사용되는 스타일
         * miniBoldFont	작고 굵은 글꼴
         * miniBoldLabel	작은 글씨로 굵은 라벨에 사용되는 스타일
         * miniButton	작은 버튼의 스타일
         * miniButtonLeft	작고 좌편향인 버튼 스타일
         * miniButtonMid	작고 중앙에 가까운 버튼 스타일
         * miniButtonRight	작고 우편향인 버튼 스타일
         * miniFont	    작은 글꼴
         * miniLabel	작은 글꼴의 라벨 스타일
         * miniTextField	작은 텍스트 필드에서 사용되는 스타일
         * numberField	숫자를 입력하는 필드에서 사용되는 스타일
         * objectField	오브젝트 필드에서 사용되는 스타일
         * objectFieldMiniThumb	Style used for object fields that have a thumbnail (e.g Textures).
         * objectFieldThumb	오브젝트 필드에서 사용되는 아이콘 이미지의 스타일
         * popup	EditorGUI.Popup과 EditorGUI.EnumPopup에서 사용되는 스타일
         * radioButton	라디오 버튼에 사용되는 스타일
         * standardFont	일반적인 표준 글꼴
         * textArea	EditorGUI.TextArea에 사용되는 스타일
         * textField	EditorGUI.TextField에서 사용되는 스타일
         * toggle	EditorGUI.Toggle에서 사용되는 스타일
         * toggleGroup	EditorGUILayout.BeginToggleGroup에서 사용되는 스타일
         * toolbar	창 상단에 표시할 도구 모음의 배경 스타일
         * toolbarButton	도구 모음 단추에서 사용되는 스타일
         * toolbarDropDown	도구 모음의 드롭 다운에서 사용되는 스타일
         * toolbarPopup	도구 모음의 팝업에서 사용되는 스타일
         * toolbarTextField	도구 모음의 텍스트 필드에 사용되는 스타일
         * whiteBoldLabel	흰색 글자이고 굵은 라벨 스타일
         * whiteLabel	흰색 문자 라벨 스타일
         * whiteLargeLabel	흰색 글자의 큰 글꼴 라벨 스타일
         * whiteMiniLabel	흰색 문자 작은 글꼴의 라벨 스타일
         * wordWrappedLabel	범위 이상의 문자가 입력되었을 때, 반환하는 스타일
         * wordWrappedMiniLabel	작은 글꼴로 범위 이상의 문자가 입력되었을 때, 반환하는 스타일
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
         
         * GUILayout.Width(값)
         * GUILayout.Height(값)
         * GUILayout.MinWidth(폭 최소값)
         * GUILayout.MaxWidth(폭 최대값)
         * GUILayout.MinHeight(높이 최소값)
         * GUILayout.MaxHeight(높이 최대값)
         * GUILayout.ExpandWidth(true or false : 수평 확장을 허용하거나 막는다)
         * GUILayout.ExpandHeight(true or false : 수직 확장을 허용하거나 막는다)
          
         **********************************************************/

        // ** 옵션별로 정리하기 위해 단위별로 구분함. (EditorStyles : boldLabel = 굵은 글꼴 스타일)

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

