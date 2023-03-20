using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharSelect2 : MonoBehaviour
{
    private int _index;
    //public event Action OnPinkFrame;
    public event Action OnFreezyScript2;

    void Start()
    {
        _index = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            _index++;
            if(_index > IconTable.getCharIconArray().Length - 1)
            {
                _index = 0;
            }
        }else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            _index--;
            if(_index < 0)
            {
                _index = IconTable.getCharIconArray().Length - 1;
            }
        }

        transform.position = IconTable.getCharIconArray()[_index].transform.position;
        transform.localScale = IconTable.getCharIconArray()[_index].transform.localScale;

        
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            PlayerInfoTable.setPlayer2(IconTable.getCharIconArray()[_index]);
            //Debug.Log("Player2   :" + IconTable.getCharIconArray()[_index]);
            if(OnFreezyScript2 != null)
            {
                OnFreezyScript2();
            }     
        }          
    }
}
