using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject arrowPrefab;
    private Animator anim;
    private Transform leftHandTrans;
    private Transform shootPoint;
    private PlayerManager playerMng;
	void Start () {
        anim = GetComponent<Animator>();
        shootPoint = transform.Find("ShootPoint");
	}
	
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
                Invoke("Shoot", 0.1f);
            }
        }
	}
    public void SetPlayerMng(PlayerManager playerMng)
    {
        this.playerMng = playerMng;
    }
    private void Shoot()
    {
        playerMng.Shoot(arrowPrefab, shootPoint.position, transform.rotation);
    }
}
