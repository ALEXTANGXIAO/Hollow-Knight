using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class CanvasUtils
{
    public static void SetMax(this RectTransform rectTransform)
    {
        if (rectTransform == null)
        {
            return;
        }
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;

        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
    }

    /// <summary>
    /// 调整 RectTransform 组件中的 Left、Bottom 属性
    /// </summary>
    /// <param name="rt">引用目标 RectTransform 对象</param>
    /// <param name="left">Left值</param>
    /// <param name="bottom">Bottom值</param>
    public static void LeftBottom(RectTransform rectTransform, float left, float bottom)
    {
        // float value1 = rt.offsetMax.x + left;
        // float value2 = rt.offsetMax.y + bottom;

        // rt.offsetMin = new Vector2(left, bottom);
        // rt.offsetMax = new Vector2(rt.offsetMax.x + value1, rt.offsetMax.y + value2);

        Vector2 size = rectTransform.rect.size;
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left, size.x);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, bottom, size.y);
    }
    /// <summary>
    /// 调整 RectTransform 组件中的 Left、Top 属性
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="left">Left值</param>
    /// <param name="top">Top值</param>
    public static void LeftTop(RectTransform rectTransform, float left, float top)
    {
        // float value1 = rt.offsetMin.y - top;
        // float value2 = rt.offsetMax.x + left;
        // Debug.Log(value1);
        // Debug.Log(value2);

        // rt.offsetMin = new Vector2(left, rt.offsetMin.y + value1);
        // rt.offsetMax = new Vector2(rt.offsetMax.x + value2, -top);

        Vector2 size = rectTransform.rect.size;
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left, size.x);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, top, size.y);
    }
    /// <summary>
    /// 调整 RectTransform 组件中的 Right、Bottom 属性
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="right">Right值</param>
    /// <param name="bottom">Bottom值</param>
    public static void RightBottom(RectTransform rectTransform, float right, float bottom)
    {
        // float value1 = rt.offsetMin.x - right;
        // float value2 = rt.offsetMax.y + bottom;

        // rt.offsetMin = new Vector2(rt.offsetMin.x + value1, bottom);
        // rt.offsetMax = new Vector2(-right, rt.offsetMax.y + value2);

        Vector2 size = rectTransform.rect.size;
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, right, size.x);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, bottom, size.y);
    }
    /// <summary>
    /// 调整 RectTransform 组件中的 Right、Top 属性
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="right">Right值</param>
    /// <param name="top">Top值</param>
    public static void RightTop(RectTransform rectTransform, float right, float top)
    {
        // float value1 = rt.offsetMin.x - right;
        // float value2 = rt.offsetMin.y - top;

        // rt.offsetMin = new Vector2(rt.offsetMin.x + value1, rt.offsetMin.y + value2);
        // rt.offsetMax = new Vector2(-right, -top);

        Vector2 size = rectTransform.rect.size;
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, right, size.x);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, top, size.y);
    }
    public static void SetCenter(this RectTransform rectTransform, float x = 0, float y = 0)
    {
        Vector2 size = rectTransform.rect.size;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        rectTransform.localPosition = new Vector2(x, y);
    }
}