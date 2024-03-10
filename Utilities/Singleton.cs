using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���巺���� Singleton<T>���̳��� MonoBehaviour������ T ������ Singleton<T> ������
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // ��̬���������ڱ���Ψһ��ʵ��
    private static T instance;

    // ������̬���ԣ����ڻ�ȡ����ʵ��
    public static T Instance
    {
        get => instance;
    }

    // �ڶ���ʵ����ʱ���õ� Unity �ص�����
    protected virtual void Awake()
    {
        // ���ʵ����Ϊ�գ���ʾ�Ѿ���������ʵ�������ٵ�ǰ����
        if (instance != null)
            Destroy(gameObject);
        else
            // ���򣬽���ǰʵ��������̬����
            instance = (T)this;
    }

    // �ڶ�������ʱ���õ� Unity �ص�����
    protected virtual void OnDestroy()
    {
        // �����ǰʵ����Ψһʵ��������̬������Ϊ��
        if (instance == this)
            instance = null;
    }
}
