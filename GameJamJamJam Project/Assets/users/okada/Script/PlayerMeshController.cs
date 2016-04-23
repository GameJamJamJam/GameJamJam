using UnityEngine;
using System.Collections;

public class PlayerMeshController : MonoBehaviour {

    public PlayerController PlController;
    public CharacterController CharaController;

    public Animation Anim;

	// Use this for initialization
	void Start ()
    {
        PlController = this.gameObject.transform.parent.GetComponent<PlayerController>();
        CharaController = this.gameObject.transform.parent.GetComponent<CharacterController>();
        //Anim = this.gameObject.GetComponent<Animation>();

        PlController.EventAttackNear += PlayAttackNear;
        PlController.EventAttackFar += PlayAttackFar;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var v = CharaController.velocity;

        var look = v;
        look.y = 0.0f;
        look.z = -5.0f;
        this.gameObject.transform.rotation = Quaternion.LookRotation( look );
    }

    void PlayAttackNear()
    {
        //Anim.Play("attack");
        
        StopCoroutine("ResetAnimation");
        StartCoroutine("ResetAnimation");
    }

    void PlayAttackFar()
    {
       // Anim.Play("skill");
        StopCoroutine("ResetAnimation");
        StartCoroutine("ResetAnimation");
    }

    private IEnumerator ResetAnimation()
    {
        
        yield return new WaitForSeconds(1);

        //Anim.Play("run");
    }
}
