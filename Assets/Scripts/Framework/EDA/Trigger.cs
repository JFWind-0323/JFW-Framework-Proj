using UnityEngine;
using UnityEngine.Events;

namespace Framework.EDA
{
    public enum TriggerShape
    {
        Sphere,
        Box,
    }

    public class Trigger : MonoBehaviour
    {
        /*
         * 选择形状自动更行碰撞体
         * 保持碰撞体积显示
         * UnityEvent事件配置
         *
         */
        [Header("颜色")] public Color color = Color.red;

        [Header("形状")] public TriggerShape shape = TriggerShape.Sphere;

        [Header("进入触发器时触发条件")] public ConditionEnum enterCondition;

        [Header("退出触发器时触发条件")] public ConditionEnum exitCondition;

        [Header("进入触发器时触发")] public UnityEvent onCollisionEnter;

        [Header("退出触发器时触发")] public UnityEvent onCollisionExit;

        #region 属性

        private TriggerShape triggerShape
        {
            get => shape;
            set
            {
                shape = value;
                OnShapeChanged(value);
            }
        }
        

        private Vector3 correctionScale
        {
            get
            {
                Vector3 combinedScale = transform.localScale;
                Transform parent = transform.parent;

                // 递归计算所有父物体的缩放
                while (parent != null)
                {
                    combinedScale = Vector3.Scale(combinedScale, parent.localScale);
                    parent = parent.parent;
                }

                return combinedScale;
            }
        }

        private Collider _collider;

        #endregion

        #region 工具方法

        void RemoveComponent<T>() where T : Collider
        {
            var component = GetComponent<T>();
            if (component != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(component);
                }
                else
                {
                    DestroyImmediate(component);
                }
            }
        }

        void OnShapeChanged(TriggerShape newShape)
        {
            Debug.Log("Shape changed to " + newShape);
            // 先检查是否已存在需要的碰撞体
            if (HasCorrectCollider(newShape)) return;

            // 删除旧组件
            foreach (var anyCollider in GetComponents<Collider>())
            {
                RemoveComponent<Collider>();
            }

            Debug.Log("Add new collider");
            // 添加新组件
            switch (newShape)
            {
                case TriggerShape.Sphere:
                    gameObject.AddComponent<SphereCollider>().isTrigger = true;
                    break;
                case TriggerShape.Box:
                    gameObject.AddComponent<BoxCollider>().isTrigger = true;
                    break;
            }
        }

        // 检查是否已存在对应类型的碰撞体
        bool HasCorrectCollider(TriggerShape shape)
        {
            return shape switch
            {
                TriggerShape.Sphere => GetComponent<SphereCollider>() != null,
                TriggerShape.Box => GetComponent<BoxCollider>() != null,
                _ => false
            };
        }


        private void DrawGizmos(TriggerShape shape)
        {
            switch (shape)
            {
                case TriggerShape.Sphere:
                    SphereCollider sphereCollider = GetComponent<SphereCollider>();
                    if (sphereCollider != null)
                        Gizmos.DrawWireSphere(transform.position, sphereCollider.radius * correctionScale.x);
                    break;
                case TriggerShape.Box:
                    BoxCollider boxCollider = GetComponent<BoxCollider>();
                    Vector3 size = Vector3.Scale(boxCollider.size, correctionScale);
                    if (boxCollider != null)
                        Gizmos.DrawWireCube(transform.position + boxCollider.center, size);
                    break;
            }
        }

        #endregion

        #region 触发器

        protected void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (enterCondition == ConditionEnum.None)
            {
                onCollisionEnter.Invoke();
                return;
            }

            if (ConditionCenter.Instance.Check(enterCondition))
            {
                onCollisionEnter.Invoke();
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (enterCondition == ConditionEnum.None)
            {
                onCollisionEnter.Invoke();
                return;
            }

            if (ConditionCenter.Instance.Check(exitCondition))
            {
                onCollisionExit.Invoke();
            }
        }

        #endregion

        #region Unity

#if UNITY_EDITOR

        void OnValidate()
        {
            // 延迟到下一帧执行
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (this == null) return; // 对象被销毁时跳过
                if (!Application.isPlaying)
                    triggerShape = shape;
            };
        }
#endif


        protected void OnDrawGizmos()
        {
            Gizmos.color = color;
            DrawGizmos(triggerShape);
        }

        #endregion
    }
}