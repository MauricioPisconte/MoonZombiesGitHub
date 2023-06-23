using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    private BoxCollider m_boxcol;
    // Start is called before the first frame update
    void Start()
    {
        m_boxcol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Enemigos"))
        {
            var enemyController = col.GetComponent<EnemyController>();
            enemyController.TakeDamage(2.5f);
        }
    }
}
