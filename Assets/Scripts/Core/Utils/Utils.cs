
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static string ToColor(this string str, string colorStr)
    {
        if (string.IsNullOrEmpty(str))
        {
            str = string.Format("<color=#{0}>{1}</color >",colorStr,str);
        }
        return str; 
    }

    public static void Show(this GameObject gameObject,bool show)
    {
        if (gameObject!= null)
        {
            gameObject.transform.localScale = show ? Vector3.one : Vector3.zero;
        }
    }

    /// <summary>
    /// 存储字典的值到列表里面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static List<W> SaveDicValueToList<T, W>(Dictionary<T, W> dic)
    {
        List<W> listW = new List<W>();
        var ienuDic = dic.GetEnumerator();

        while (ienuDic.MoveNext())
        {
            listW.Add(ienuDic.Current.Value);
        }

        return listW;
    }

    public static List<W> ToList<T, W>(this Dictionary<T, W> dic)
    {
        List<W> listW = new List<W>();

        var ienuDic = dic.GetEnumerator();

        while (ienuDic.MoveNext())
        {
            listW.Add(ienuDic.Current.Value);
        }

        return listW;
    }
}