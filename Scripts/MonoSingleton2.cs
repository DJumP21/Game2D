using UnityEngine;


public abstract class MonoSingleton2<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool global = true;
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance =(T)FindObjectOfType<T>();
            }
            return instance;
        }

    }
    //使用Awake 而不用Start 目的：为了让不同物体的单例脚本可以同时执行
    void Awake()
    {
        Debug.LogWarningFormat("{0}[{1}] Awake", typeof(T), this.GetInstanceID());
        if (global) 
        {
            //如果脚本不为空而且不是当前脚本
            if (instance!=null && instance!=this.GetComponent<T>())
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
            instance = this.GetComponent<T>();
        } 
        this.OnStart();
    }

    protected virtual void OnStart()
    {

    }
}