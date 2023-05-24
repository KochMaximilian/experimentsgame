using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlayerController : GenericPlayerController , IIce
{
    private List<Collision> colliders = new List<Collision>();
    IceAnimationState ias;
    public  Transform carried = null;
    public Vector3 currentCarriedOffset = new Vector3(0,0,0);
    public Vector3 originalScale;
    public Vector3 minimumScale;

    override public void Start() {
        base.Start();
        originalScale = transform.localScale;
        minimumScale = originalScale * 0.5f;
        ias = GetComponent<IceAnimationState>();
    }

    override public void Update(){
        base.Update();
        if (carried != null) {
            Vector3 cariedOffset = currentCarriedOffset;
            cariedOffset.x *= direction.x;
            carried.position = transform.position + cariedOffset;
        }
        if (base.horizontalInput == 0) {
            ias.Stand();
        } else {
            ias.Move();
        }
        if (base.isOnGround) {
            ias.Land();
        }
    }

    override public void Jump() {
        base.Jump();
        ias.Jump();
    }
    override public void Action(){
        if (Input.GetKeyDown(playerInputAction)) {
        if (carried != null) {
            carried.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            Collider cls = carried.GetComponent<Collider>();
            cls.enabled = ! cls.enabled;
            carried = null;
            return;
        }

        RaycastHit hit;
        Vector3 pos = transform.position;
        if (Physics.SphereCast(pos, 2f, direction, out hit, 1.5f)) {
            IPickupAble pikup = hit.transform.GetComponent<IPickupAble>();
            if (pikup != null) {
                currentCarriedOffset = pikup.GetPickUpOffset();
                hit.collider.enabled = !hit.collider.enabled;
                carried = hit.transform;
                return;
            }
        }
        }
        /*foreach(Collision cl in colliders) {
            IPickupAble pikup = cl.transform.GetComponent<IPickupAble>();
            if (pikup != null) {
                currentCarriedOffset = pikup.GetPickUpOffset();
                cl.collider.enabled = !cl.collider.enabled;
                carried = cl.transform;
                return;
            }
        }*/
    }

    override public void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
        /*if (!colliders.Contains(collision)) { 
            IPickupAble pikup = collision.transform.GetComponent<IPickupAble>();
            if (pikup != null) {
                Debug.Log("got one!");
            }
            colliders.Add(collision);
        }*/
     }
 
     private void OnCollisionExit (Collision other) {
         colliders.Remove(other);
     }

     public void Shrink() {
        Vector3 newScale = transform.localScale * 0.95f;
                Debug.Log("spacer");
             Debug.Log(minimumScale.x);
             Debug.Log(newScale.x);
         if (newScale.x < minimumScale.x) {

             transform.localScale = minimumScale;
             return;
         }
         transform.localScale = newScale;
     } 

     public void Grow() {
         Vector3 newScale = transform.localScale * 1.1f;
         if (newScale.x > originalScale.x) {
             transform.localScale = originalScale;
             return;
        }
        transform.localScale = newScale;
     }
}
