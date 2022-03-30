using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    private static SkillManager Instance = null;

    public static SkillManager GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = new SkillManager();
            return Instance;
        }
    }
}
