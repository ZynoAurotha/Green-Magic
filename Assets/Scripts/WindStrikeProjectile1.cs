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
        if(GameObject.FindGameObjectWithTag("Leafy2") != null)
        {
            GameObject leafy2 = GameObject.FindGameObjectWithTag("Leafy2");
            _windStrikeDir = ((leafy2.transform.position.x - transform.position.x) > 0) ? Vector2.right : Vector2.left;
        }    
    }
    protected void Update()
    {   
        transform.Translate(_windStrikeDir * _winStrikeMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Leafy2")
        {
            coll.GetComponent<Leafy2>().OnTakeDamage(5);
        }
    }
}
