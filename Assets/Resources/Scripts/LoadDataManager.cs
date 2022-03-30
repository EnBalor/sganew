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
        // ** _FileName 해당 위치에 있는 파일들을 불러옴.

        object[] Objects = Resources.LoadAll<Sprite>(_FileName);

        if (Objects == null)
            return null;

        return Objects;
    }
}
