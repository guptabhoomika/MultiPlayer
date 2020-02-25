using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CowBoyManager : MonoBehaviourPun
{
    public GameObject cam;
    public float moveSpeed=5;
    public SpriteRenderer sprite;
    public Animator anim;
    public PhotonView photonView;
    private bool allowMovement= true;
    // Start is called before the first frame update
    void Awake()
    {
       if(photonView.IsMine)
        {
            cam.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            checkMoments();
        }


        
    }

    private void checkMoments()
    {
        if(allowMovement)
        {
            var Movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
            transform.position += Movement * moveSpeed * Time.deltaTime;
        }
        
        if(Input.GetKeyDown(KeyCode.RightControl) && anim.GetBool("Ismove")==false)
        {
            shoot();
        }else if(Input.GetKeyUp(KeyCode.RightControl))
        {
            anim.SetBool("IsSHot", false);
            allowMovement = true;
        }
        if(Input.GetKeyDown(KeyCode.D) && anim.GetBool("IsSHot")==false)
        {
            anim.SetBool("Ismove", true);
            //sprite.flipX = false; not synched over network
            photonView.RPC("FlipRight", RpcTarget.AllBuffered);
        }
       else if(Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Ismove", false);
        }
        if (Input.GetKeyDown(KeyCode.A) && anim.GetBool("IsSHot") == false)
        {
            anim.SetBool("Ismove", true);
            //sprite.flipX = true;
            photonView.RPC("FlipLeft", RpcTarget.AllBuffered);
        }
       else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Ismove", false);
        }
    }

    private void shoot()
    {
        anim.SetBool("IsSHot", true);
        allowMovement = false;
    }
    [PunRPC]
    private void FlipRight()
    {
        sprite.flipX = false;
    }
    [PunRPC]
    private void FlipLeft()
    {
        sprite.flipX = true;
    }
}
