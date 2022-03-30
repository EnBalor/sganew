using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataManager : MonoBehaviour
{
    private static LoadDataManager Instance = null;
    public static LoadDataManager GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = new LoadDataManager();
            return Instance;
        }
    }

    public object[] LoadData(string _FileName)
    {
        // ** _FileName �ش� ��ġ�� �ִ� ���ϵ��� �ҷ���.

        object[] Objects = Resources.LoadAll<Sprite>(_FileName);

        if (Objects == null)
            return null;

        return Objects;
    }
}
