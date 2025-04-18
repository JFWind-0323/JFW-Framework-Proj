using UnityEngine;
using UnityEngine.Events;

namespace Framework.EDA
{
    public class TriggerBase:MonoBehaviour
    {
        [Header("进入触发器时触发条件")]
        public ConditionEnum enterCondition;
        [Header("退出触发器时触发条件")]
        public ConditionEnum exitCondition;
        [Header("进入触发器时触发")]
        public UnityEvent onCollisionEnter;
        [Header("退出触发器时触发")]
        public UnityEvent onCollisionExit;
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (ConditionCenter.Instance.Check(enterCondition))
            {
                onCollisionEnter.Invoke();
            }
        }

        protected virtual void OnCollisionExit(Collision collision)
        {
            if (ConditionCenter.Instance.Check(exitCondition))
            {
                onCollisionExit.Invoke();
            }
        }
    }
}