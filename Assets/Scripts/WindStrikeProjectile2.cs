using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindStrikeProjectile2 : MonoBehaviour
{
    [SerializeField] private float _winStrikeMoveSpeed;
    private Vector2 _windStrikeDir;
  
    // Update is called once per frame

    protected void Start()
    {
        if(GameObject.FindGameObjectWithTag("Character1") != null)
        {
            GameObject leafy1 = GameObject.FindGameObjectWithTag("Character1");
            _windStrikeDir = ((leafy1.transform.position.x - transform.position.x) < 0) ? Vector2.left : Vector2.right;
        }    
    }
    protected void Update()
    {   
        transform.Translate(_windStrikeDir * _winStrikeMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Character1")
        {
            coll.GetComponent<Leafy1>().OnTakeDamage(5);
        }
    }
}
