using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Arrow : MonoBehaviour {
    public GameObject explosionEffect=null;
    public RoleType roleType;
    public bool isLocal = false;
    private Rigidbody rgd;
    public int speed = 5;

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rgd.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ShootPerson);
            if (isLocal)
            {
                bool playerIsLocal = other.GetComponent<PlayerInfo>().isLocal;
                if (isLocal != playerIsLocal)
                {
                    GameFacade.Instance.SendAttack(Random.Range(10, 20));
                }
            }
        }
        else
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Miss);
        }
        GameObject.Destroy(this.gameObject);
    }
}
