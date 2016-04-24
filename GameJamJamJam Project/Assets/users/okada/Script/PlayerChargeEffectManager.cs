using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerChargeEffectManager : MonoBehaviour {

    public List<GameObject> Effects;

    public GameObject PlayerStatus;

    public List<GameObject> Trails;

    status status;
    item.eExpType lastType = item.eExpType.None;
    GameObject effect;
    // ao jump
    // red kinsetu
    // kiiro enkyori

	// Use this for initialization
	void Start ()
    {
        status = PlayerStatus.GetComponent<status>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!status)
            return;

        if( status.getLastKillExp() != lastType )
        {
            lastType = status.getLastKillExp();
            Destroy(effect);
            foreach( var trail in Trails )
            {
                trail.SetActive(false);
            }

            switch(lastType)
            {
                case item.eExpType.Jump:

                    effect = Instantiate(Effects[1], transform.position, Quaternion.identity) as GameObject;
                    Trails[1].SetActive(true);
                    break;
                case item.eExpType.MeleePow:

                    effect = Instantiate(Effects[0], transform.position, Quaternion.identity) as GameObject;
                    Trails[0].SetActive(true);
                    break;
                case item.eExpType.ShotPow:

                    effect = Instantiate(Effects[2], transform.position, Quaternion.identity) as GameObject;
                    Trails[2].SetActive(true);
                    break;
            }

            effect.transform.parent = this.transform;
        }
	}
}
