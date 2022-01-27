using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI层级枚举
/// </summary>
public enum UI_Layer
{
    Bottom,
    Mid,
    Top,
    System,
}

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : Singleton<UIManager>
{
    public RectTransform canvas;
    //private int sortOrder;
    private int bottomSortOrder;
    private int midSortOrder;
    private int topSortOrder;
    private int systemSortOrder;
    private Transform bottom;
    private Transform mid;
    private Transform top;
    private Transform system;

    public UIManager()
    {
        GameObject obj =  ResourcesManager.Instance.AllocGameObject("UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);
        obj = ResourcesManager.Instance.AllocGameObject("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);

        bottom = canvas.Find("Bottom");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");
    }

    private static Dictionary<string, UIWindow> m_windowDic = new Dictionary<string, UIWindow>();
    public Dictionary<uint, UIWindow> m_uiidDic = new Dictionary<uint, UIWindow>();
    private List<UIWindow> m_windowList = new List<UIWindow>();

    private uint uiid;

    public T GetWindow<T>() where T : UIWindow
    {
        string typeName = GetWindowTypeName<T>();
        UIWindow window;
        if( m_windowDic.TryGetValue(typeName,out window))
        {
            return window as T;
        }

        return null;
    }

    public T ShowWindow<T>(UI_Layer layer = UI_Layer.Mid, UnityAction<T> callback = null) where T : UIWindow, new()
    {
        string typeName = GetWindowTypeName<T>();

        T window = GetUIWindowByType(typeName) as T;
        if (window == null)
        {
            window = new T();
            if (!CreateWindowByType(window, typeName, layer))
            {
                return null;
            }
        }
        m_windowList.Add(window);
        window.Show();
        return window;
    }

    public void CloseWindow<T>() where T : UIWindow
    {
        string typeName = GetWindowTypeName<T>();
        CloseWindow(typeName);
    }

    public void CloseWindow(string typeName)
    {
        UIWindow window = GetUIWindowByType(typeName);
        if (window != null)
        {
            CloseWindow(window);
        }
    }

    public void CloseWindow(UIWindow window)
    {
        if (window.IsDestroyed)
        {
            return;
        }

        string typeName = window.GetType().Name;

        UIWindow typeWindow;

        if (m_windowDic.TryGetValue(typeName, out typeWindow) && typeWindow == window)
        {
            m_windowDic.Remove(typeName);
        }

        m_windowList.Remove(window);

        if (m_uiidDic.ContainsKey(uiid))
        {
            m_uiidDic.Remove(uiid);
        }
        switch (window.UILayer)
        {
            case UI_Layer.Bottom:
                bottomSortOrder -= 50;
                break;
            case UI_Layer.Mid:
                midSortOrder -= 50;
                break;
            case UI_Layer.Top:
                topSortOrder -= 50;
                break;
            case UI_Layer.System:
                systemSortOrder -= 50;
                break;
        }
        window.Destroy();

        window = null;
    }

    public string GetWindowTypeName<T>()
    {
        string typeName = typeof(T).Name;
        return typeName;
    }

    public UIWindow GetUIWindowByType(string typeName)
    {
        UIWindow window;
        if (m_windowDic.TryGetValue(typeName, out window))
        {
            return window;
        }

        return null;
    }

    private bool CreateWindowByType(UIWindow window, string typeName, UI_Layer layer = UI_Layer.Mid)
    {
        m_windowDic[typeName] = window;

        string resPath = GetUIResourcePath(typeName);

        if (string.IsNullOrEmpty(resPath))
        {
            Debug.Log("CreateWindowByType failed, typeName:" + typeName);
            return false;
        }

        GameObject uiObj = null;

        uiObj = (GameObject)ResourcesManager.Instance.Load(resPath);
        if (uiObj == null)
        {
            Debug.Log("CreateWindowByType failed, " + typeName + resPath);
            return false;
        }

        uiObj.name = typeName;

        uiid++;

        window.AllocUIID(uiid);

        m_uiidDic.Add(uiid, window);

        Transform father = bottom;
        int baseSort = 0;
        int sortOrder = 0;
        window.UILayer = layer;
        switch (layer)
        {
            case UI_Layer.Bottom:
                baseSort = 0;
                father = bottom;
                sortOrder = bottomSortOrder;
                break;
            case UI_Layer.Mid:
                baseSort = 500;
                father = mid;
                sortOrder = midSortOrder;
                break;
            case UI_Layer.Top:
                baseSort = 1000;
                father = top;
                sortOrder = topSortOrder;
                break;
            case UI_Layer.System:
                baseSort = 2000;
                father = system;
                sortOrder = systemSortOrder;
                break;
        }

        uiObj.transform.SetParent(father);

        RectTransform rectTrans = uiObj.transform as RectTransform;
        rectTrans.SetMax();

        if (!window.Create(this, uiObj))
        {
            Debug.Log("window create failed, typeName: "+ typeName);
            if (uiObj != null)
            {
                Object.Destroy(uiObj);
            }

            return false;
        }

        window.SortingOrder = sortOrder + baseSort;

        switch (layer)
        {
            case UI_Layer.Bottom:
                bottomSortOrder += 50;
                break;
            case UI_Layer.Mid:
                midSortOrder += 50;
                break;
            case UI_Layer.Top:
                topSortOrder += 50;
                break;
            case UI_Layer.System:
                systemSortOrder += 50;
                break;
        }
        return true;
    }
    private string GetUIResourcePath(string typeName)
    {
        string path = string.Format("UI/{0}", typeName);
        return path;
    }

    public void Update()
    {
        var allList = m_windowList;
        for (int i = 0; i < allList.Count; i++)
        {
            UIWindow window = allList[i];
            if (!window.IsDestroyed)
            {
                window.Update();
            }
        }
    }

    public Transform GetLayerFather(UI_Layer layer)
    {
        switch (layer)
        {
            case UI_Layer.Bottom:
                return this.bottom;
            case UI_Layer.Mid:
                return this.mid;
            case UI_Layer.Top:
                return this.top;
            case UI_Layer.System:
                return this.system;

        }
        return null;
    }

    /// <summary>
    /// 给控件添加自定义事件监听
    /// </summary>
    /// <param name="control">控件对象</param>
    /// <param name="type">事件类型</param>
    /// <param name="callback">事件的响应函数</param>
    public static void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();

        if(trigger == null)
        {
            trigger = control.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callback);

        trigger.triggers.Add(entry);
    }
}
