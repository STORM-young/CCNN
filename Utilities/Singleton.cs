using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义泛型类 Singleton<T>，继承自 MonoBehaviour，其中 T 必须是 Singleton<T> 的子类
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // 静态变量，用于保存唯一的实例
    private static T instance;

    // 公共静态属性，用于获取单例实例
    public static T Instance
    {
        get => instance;
    }

    // 在对象被实例化时调用的 Unity 回调方法
    protected virtual void Awake()
    {
        // 如果实例不为空，表示已经存在其他实例，销毁当前对象
        if (instance != null)
            Destroy(gameObject);
        else
            // 否则，将当前实例赋给静态变量
            instance = (T)this;
    }

    // 在对象被销毁时调用的 Unity 回调方法
    protected virtual void OnDestroy()
    {
        // 如果当前实例是唯一实例，将静态变量置为空
        if (instance == this)
            instance = null;
    }
}
