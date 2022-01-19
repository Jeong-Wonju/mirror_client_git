using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool mShuttingDown = false;
    private static object mLock = new object();
    private static T mInstance;
    public static T Instance
    {
        get
        {
            if (mShuttingDown)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                return null;
            }
            lock (mLock)
            {
                if (mInstance == null)
                {
                    mInstance = (T)FindObjectOfType(typeof(T));
                    if (mInstance == null)
                    {
                        var singletonObject = new GameObject();
                        mInstance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"{typeof(T)} (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return mInstance;
            }
        }
    }
    private void OnApplicationQuit() => mShuttingDown = true;
    private void OnDestroy() => mShuttingDown = true;
}
