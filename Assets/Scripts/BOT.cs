using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOT : Character
{
    [SerializeField] protected int RandMoveX;
    [SerializeField] protected int RandMoveY;
    [SerializeField] private float _startWaitTimeX;
    [SerializeField] private float _startWaitTimeY;
    [SerializeField] private int _randNum;
    private float _WaitTimeX;
    private float _WaitTimeY;
    protected override void Start()
    {
         _WaitTimeX =_startWaitTimeX;
         _WaitTimeY =_startWaitTimeY;
        //OnSetupCharracters();  
    }

    protected override void FixedUpdate()
    {
        if(_WaitTimeX <= 0)
        {
            _randNum = Random.Range(1,4);
            switch(_randNum)
            {
                case 1:
                   RandMoveX = 0;
                   RandMoveY = 0;
                   break;
                case 2:
                   RandMoveX = 1;
                   RandMoveY = 0;
                   break;
                case 3:
                   RandMoveX = -1;
                   RandMoveY = 0;
                   break;
           }
           _WaitTimeX = _startWaitTimeX;
        }else
        {
            _WaitTimeX -= Time.deltaTime;
        }          
    }

  
}
