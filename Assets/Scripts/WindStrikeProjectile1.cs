using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindStrikeProjectile1 : MonoBehaviour
{
    [SerializeField] private float _winStrikeMoveSpeed;
    private Vector2 _windStrikeDir;
  
    // Update is called once per frame

    protected void Start()
    {
        if(GameObject.FindGameObjectWithTag("Character2") != null)
        {
            GameObject leafy2 = GameObject.FindGameObjectWithTag("Character2");
            _windStrikeDir = ((leafy2.transform.position.x - transform.position.x) > 0) ? Vector2.right : Vector2.left;
        }    
    }
    protected void Update()
    {   
        transform.Translate(_windStrikeDir * _winStrikeMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Character2")
        {
            if(GameObject.Find("Leafy2") != null)
            {
                coll.GetComponent<Leafy2>().OnTakeDamage(5);
            }
            if(GameObject.Find("LeafyBOT") != null)
            {
                coll.GetComponent<LeafyBOT>().OnTakeDamage(5);
            }                
        }
    }
}
