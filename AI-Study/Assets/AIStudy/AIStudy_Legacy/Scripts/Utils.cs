using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Utils
{
    public static void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    public static void ClearAndPrint(string messsage)
    {
        ClearLog();
        Debug.Log(messsage);
    }

    public static float RandomBinomial()
    {
        return (UnityEngine.Random.Range(0.0f, 1.0f) - UnityEngine.Random.Range(0.0f, 1.0f));
    }

    /// <summary>
    /// Calculates the right direction of an object with a given rotation. Equals to transform.right.
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Vector3 RightDirection2D(Vector3 rotation)
    {
        float xPos = Mathf.Cos(rotation.z * Mathf.Deg2Rad) * Mathf.Cos(rotation.y * Mathf.Deg2Rad);
        float yPos = Mathf.Sin(rotation.z * Mathf.Deg2Rad);
        float zPos = Mathf.Sin(rotation.y * Mathf.Deg2Rad) * Mathf.Cos(rotation.z * Mathf.Deg2Rad);

        Vector3 calculatedDirection = new Vector3(xPos, yPos, zPos);
        return calculatedDirection;
    }
}
