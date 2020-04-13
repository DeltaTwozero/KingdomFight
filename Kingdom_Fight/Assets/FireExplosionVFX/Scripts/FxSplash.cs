using UnityEngine;
using System.Collections;

public class FxSplash : MonoBehaviour {
	private Rigidbody rb;
	public ParticleSystem Ps_Splash;
	public ParticleSystem Ps_Trail;
    public GameObject killZone;

    //Audio
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip breakClip;
	
	void Start() {
		rb = this.GetComponent<Rigidbody>();
		mh = this.GetComponent<MeshRenderer>();
	}

	void OnCollisionEnter(Collision collision) {
		// Debug-draw all contact points and normals
		mh.enabled = false;
		Ps_Trail.Stop();
		Ps_Splash.Play();
        killZone.SetActive(true);
        audioSource.PlayOneShot(breakClip);

        Invoke("DestroyFx",2f);
	}

	void DestroyFx()
	{
		Destroy(this.gameObject);
	}
	private MeshRenderer mh;
}
