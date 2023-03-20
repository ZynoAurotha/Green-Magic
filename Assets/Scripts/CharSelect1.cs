using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharSelect1 : MonoBehaviour
{
    private int _index;
    public event Action OnFreezyScript1;

    void Start()
    {
        _index = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            _index++;
            if(_index > IconTable.getCharIconArray().Length - 1)
            {
                _index = 0;
            }
        }else if(Input.GetKeyDown(KeyCode.W))
        {
            _index--;
            if(_index < 0)
            {
                _index = IconTable.getCharIconArray().Length - 1;
            }
        }

        transform.position = IconTable.getCharIconArray()[_index].transform.position;
        transform.localScale = IconTable.getCharIconArray()[_index].transform.localScale;
          
        if(Input.GetKeyDown(KeyCode.J))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            PlayerInfoTable.setPlayer1(IconTable.getCharIconArray()[_index]);
            //Debug.Log("Player1   :" + IconTable.getCharIconArray()[_index]);
            if(OnFreezyScript1 != null)
            {
                OnFreezyScript1();
            }     
        }   
    }
}
