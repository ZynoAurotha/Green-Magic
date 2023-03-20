using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IconTable
{
    private static GameObject[] _charIconArray;

    private static Transform _inputTransform;

    public static void setcharIconArray(int arr)
    {
        _charIconArray = new GameObject[arr];
    }

    public static GameObject[] getCharIconArray()
    {
        return _charIconArray;
    }

    public static void setInputTransform(Transform inputTransform)
    {
        _inputTransform = inputTransform;
    }
    public static Transform getInputTransform()
    {
        return _inputTransform;
    }

    public static void AddGameobject()
    {
        for(int i = 0; i < _charIconArray.Length; i++)
        {
            _charIconArray[i] = _inputTransform.GetChild(i).gameObject;
            //Debug.Log(_charIconArray[i]);
        }
    }
}
