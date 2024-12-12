using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class LinearTablesTool
    {
        
        /// <summary>
        /// 从列表初始化字典，字典的键为列表索引，值为列表中的元素
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dict"></param>
        /// <typeparam name="T"></typeparam>
        public static void InitDictFromList<T>(List<T> list, out Dictionary<int, T> dict)
        {
            dict = new Dictionary<int, T>();
            for (var i = 0; i < list.Count; i++)
                if (!dict.ContainsKey(i))
                    dict.Add(i, list[i]);
        }
        /// <summary>
        /// 从数组初始化字典，字典的键为数组索引，值为数组中的元素
        /// </summary>
        /// <param name="list">数组</param>
        /// <param name="dict">目标字典</param>
        /// <typeparam name="T">值类型</typeparam>
        public static void InitDictFromArray<T>(T[] list, out Dictionary<int, T> dict)
        {
            dict = new Dictionary<int, T>();
            for (var i = 0; i < list.Length; i++)
                if (!dict.ContainsKey(i))
                    dict.Add(i, list[i]);
        }


        
        /// <summary>
        /// 打印字典的所有键值对
        /// </summary>
        /// <param name="dict"></param>
        /// <typeparam name="TKey">字典的键类型</typeparam>
        /// <typeparam name="TValue">字典的值类型</typeparam>
        public static void PrintDict<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            foreach (var item in dict) Debug.Log(item.Key + " : " + item.Value);
        }

        public static void Print<T>(this T IEnumerable) where T : IEnumerable
        {
            foreach (var item in IEnumerable) 
                Debug.Log(item.ToString());
        }
        
        /// <summary>
        /// 将二维数组转换为一维数组
        /// </summary>
        /// <param name="array2D"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] ToArray<T>(this T[,] array2D)
        {
            var row = array2D.GetLength(0);
            var col = array2D.GetLength(1);
            var length = row * col;
            var array = new T[length];
            for (var i = 0; i < row; i++)
            for (var j = 0; j < col; j++)
                array[i * col + j] = array2D[i, j];
            return array;
        }
        
        /// <summary>
        /// 将一维数组转换为指定宽度和高度的二维数组
        /// </summary>
        /// <param name="array"></param>
        /// <param name="width">二维数组的宽度</param>
        /// <param name="height">二维数组的高度</param>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <returns></returns>
        public static T[,] ToArray2D<T>(this T[] array, int width, int height)
        {
            var array2D = new T[width, height];
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                array2D[i, j] = array[i * height + j];
            return array2D;
        }
    }
}