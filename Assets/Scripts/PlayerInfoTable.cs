using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfoTable 
{
   private static GameObject _player1;

   private static GameObject _player2;

   public static void setPlayer1(GameObject player1)   
   {
         if(_player1 == null){
            _player1 = player1;
         }    
   }
   public static GameObject getPlayer1()   
   {
        return _player1;
   }

   public static void setPlayer2(GameObject player2)   
   {
        _player2 = player2;
   }
   public static GameObject getPlayer2()   
   {
        return _player2;
   }
}
