using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MoonlightStudioExtensions
{
    public static void RestartLevel(this UnityEngine.SceneManagement.SceneManager _val)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void RestartTransform(this Transform _tr)
    {
        _tr.position = Vector3.zero;
        _tr.rotation = Quaternion.Euler(Vector3.zero);
    }
    public static void Shuffle<T>(this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
