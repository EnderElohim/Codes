using UnityEngine;

public class SingletonExample : MonoBehaviour
{
    private static scrSingletonExample _instance = null;

    public static scrSingletonExample Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            //If you reload same scene, this will make sure you only have one. 
            DestroyImmediate(this.gameObject);
        }
        else
        {
            _instance = this;
            //if you wanna keep this object on another scene this will make it happen.
            //if you don't wanna move it to different scene, remove "DontDestroyOnLoad"
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
