using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public static class NonlinearTableUtility
    {
    }

    public class TreeNode<T>
    {
        public T Value { get; set; }

        public TreeNode(T value)
        {
            Value = value;
        }
    }

    public class Tree<T>
    {
        public TreeNode<T> root;

        // 邻接表：父节点 → 子节点列表
        private Dictionary<TreeNode<T>, List<TreeNode<T>>> adjacencyList = new();

        // 记录所有节点（快速查找用）
        private HashSet<TreeNode<T>> allNodes = new();

        public Tree(T rootValue)
        {
            root = new TreeNode<T>(rootValue);
            adjacencyList[root] = new List<TreeNode<T>>();
            allNodes.Add(root);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="childValue"></param>
        /// <returns> 将子节点返回 </returns>
        /// <exception cref="ArgumentException"></exception>
        public TreeNode<T> AddNode(TreeNode<T> parent, T childValue)
        {
            if (!allNodes.Contains(parent))
            {
                throw new ArgumentException("父节点不存在");
            }

            var child = new TreeNode<T>(childValue);

            adjacencyList[parent].Add(child);
            allNodes.Add(child);
            if (!adjacencyList.ContainsKey(child))
            {
                adjacencyList.Add(child, new List<TreeNode<T>>());
            }

            return child;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node"></param>
        public void RemoveNode(TreeNode<T> node)
        {
            if (!allNodes.Contains(node))
            {
                return;
            }

            foreach (var child in adjacencyList[node])
            {
                RemoveNode(child);
            }

            foreach (var parent in adjacencyList.Keys)
            {
                if (adjacencyList[parent].Contains(node))
                {
                    adjacencyList[parent].Remove(node);
                    break;
                }
            }

            adjacencyList.Remove(node);
            allNodes.Remove(node);
        }

        /// <summary>
        /// 更新节点值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        /// <exception cref="ArgumentException"></exception>
        public void UpdateNode(TreeNode<T> node, T newValue)
        {
            if (!allNodes.Contains(node)) throw new ArgumentException("节点不存在");
            node.Value = newValue;
        }

        /// <summary>
        /// 判断节点是否存在
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool ContainsNode(TreeNode<T> node)
        {
            return allNodes.Contains(node);
        }
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="childIndex">子节点在子节点列表中的索引</param>
        /// <returns></returns>
        public TreeNode<T> GetChildNode(TreeNode<T> node, int childIndex = 0)
        {
            adjacencyList.TryGetValue(node, out var children);
            if (children == null || childIndex >= children.Count)
            {
                Debug.LogWarning($"节点{node.Value}没有第{childIndex + 1}个子节点");
                return null;
            }

            return children[childIndex];
        }

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public List<TreeNode<T>> GetChildren(TreeNode<T> parent)
        {
            return adjacencyList.TryGetValue(parent, out var children)
                ? new List<TreeNode<T>>(children)
                : new List<TreeNode<T>>();
        }

        /// <summary>
        /// 查找节点，使用值匹配
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TreeNode<T> FindNode(T value)
        {
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node.Value.Equals(value))
                {
                    return node;
                }

                foreach (var child in adjacencyList[node])
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        // 在 Tree<T> 类中新增方法
        /// <summary>
        /// 查找节点，使用自定义匹配逻辑
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public TreeNode<T> FindNode(Predicate<T> match)
        {
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (match(node.Value)) // 使用自定义匹配逻辑
                {
                    return node;
                }

                foreach (var child in adjacencyList[node])
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

  

        // 遍历模式枚举
        public enum TraversalMode
        {
            PreOrder, // 前序遍历（根-左-右）
            PostOrder // 后序遍历（左-右-根）
        }

        public List<TreeNode<T>> GetDFSIterative(TraversalMode mode = TraversalMode.PreOrder)
        {
            var result = new List<TreeNode<T>>();
            var visited = new HashSet<TreeNode<T>>(); // 新增已访问记录
            var stack = new Stack<(TreeNode<T> node, bool processed)>();

            stack.Push((root, false));

            while (stack.Count > 0)
            {
                var (current, processed) = stack.Pop();

                if (processed)
                {
                    if (mode == TraversalMode.PostOrder) result.Add(current);
                    continue;
                }

                // 检测循环引用
                if (visited.Contains(current))
                {
                    Debug.LogWarning($"检测到循环引用: {current.Value}");
                    continue;
                }

                visited.Add(current);

                if (mode == TraversalMode.PreOrder) result.Add(current);

                var children = GetChildren(current);
                stack.Push((current, true));

                // 保持原有逆序压栈逻辑
                foreach (var child in children.AsEnumerable().Reverse())
                {
                    stack.Push((child, false));
                }
            }

            return result;
        }
    }
}