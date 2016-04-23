using UnityEngine;
using System.Collections;

public class PlayerMeshController : MonoBehaviour {

    public PlayerController PlController;
    public CharacterController CharaController;

    public Animator Anim;

	// Use this for initialization
	void Start ()
    {
        PlController = this.gameObject.transform.parent.GetComponent<PlayerController>();
        CharaController = this.gameObject.transform.parent.GetComponent<CharacterController>();
        Anim = this.gameObject.GetComponent<Animator>();

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
        Anim.SetBool("UseAttack", true);
        StopCoroutine("ResetAnimation");
        StartCoroutine("ResetAnimation");
    }

    void PlayAttackFar()
    {
        // Anim.Play("skill");

        Anim.SetBool("UseSkill", true);
        StopCoroutine("ResetAnimation");
        StartCoroutine("ResetAnimation");
    }

    private IEnumerator ResetAnimation()
    {
        
        yield return new WaitForSeconds(0.3f);

        Anim.SetBool("UseAttack", false);
        Anim.SetBool("UseSkill", false);
    }
}
