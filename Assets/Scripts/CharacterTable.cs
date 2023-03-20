using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterTable
{
    private static GameObject[] _characterPlayer1;
    private static GameObject[] _characterPlayer2;

    private static Transform _inputTransform1;
    private static Transform _inputTransform2;

    public static void setcharacterPlayer1Array(int arr)
    {
        _characterPlayer1 = new GameObject[arr];
    }
    public static void setcharacterPlayer2Array(int arr)
    {
        _characterPlayer2 = new GameObject[arr];
    }

    public static GameObject[] getcharacterPlayer1Array()
    {
        return _characterPlayer1;
    }
    public static GameObject[] getcharacterPlayer2Array()
    {
        return _characterPlayer2;
    }

    public static void setInputTransform1(Transform inputTransform1)
    {
        _inputTransform1 = inputTransform1;
    }
     public static void setInputTransform2(Transform inputTransform2)
    {
        _inputTransform2 = inputTransform2;
    }
    public static Transform getInputTransform1()
    {
        return _inputTransform1;
    }
    public static Transform getInputTransform2()
    {
        return _inputTransform2;
    }

    public static void AddCharacterPlayer1()
    {
        for(int i = 0; i < _characterPlayer1.Length; i++)
        {
            _characterPlayer1[i] = _inputTransform1.GetChild(i).gameObject;
            //Debug.Log(_charIconArray[i]);
        }
    }
     public static void AddGameobject()
    {
        for(int i = 0; i < _characterPlayer2.Length; i++)
        {
            _characterPlayer2[i] = _inputTransform2.GetChild(i).gameObject;
            //Debug.Log(_charIconArray[i]);
        }
    }
}
