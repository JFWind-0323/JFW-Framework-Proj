using System;
using System.Collections.Generic;
using Framework.EDA;
using Framework.Singleton;
using UnityEngine;

public class ConditionCenter : Singleton<ConditionCenter>
{
    public Dictionary<ConditionEnum, Func<bool>> conditionsDict = new Dictionary<ConditionEnum, Func<bool>>();

    public void RegisterCondition(ConditionEnum condition, Func<bool> func)
    {
        conditionsDict[condition] = func;
    }

    public Func<bool> GetCondition(ConditionEnum condition)
    {
        if (!conditionsDict.ContainsKey(condition))
        {
            RegisterCondition(condition, () => true);
            Debug.LogError("条件未注册 " + condition + " 已自动注册");
        }

        return conditionsDict[condition];
    }

    public void AddCondition(ConditionEnum condition, Func<bool> func)
    {
        conditionsDict[condition] += func;
    }

    public bool CheckCondition(ConditionEnum condition)
    {
        if (!conditionsDict.ContainsKey(condition))
        {
            RegisterCondition(condition, () => true);
            Debug.LogError("条件未注册 " + condition + " 已自动注册");
        }

        return conditionsDict[condition].Invoke();
    }
}