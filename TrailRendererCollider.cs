using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TrailRendererCollider : MonoBehaviour {
    LineRenderer line;
    List<Vector3> vertex;
    RaycastHit hit;
    public float minVertexDistance;
    public GameObject[] explosions;

    private bool check = true;

    private float timer = 0;
	// Use this for initialization
	void Start () {
        vertex = new List<Vector3>();  
        vertex.Add(transform.position);
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(vertex[vertex.Count - 1], transform.position)> minVertexDistance) {
            vertex.Add(transform.position);
        }

        timer += Time.deltaTime;
        if (timer > 0.1f)
        {   
            timer = 0;
            checkCollision();
        }


    }

    void checkCollision() {
        for (int i=1;i<vertex.Count-1;++i)
        {
            if (Physics.Linecast(vertex[i - 1], vertex[i],out hit))
            {
                Debug.Log(hit.collider.GetType());
                if (hit.collider.GetType() != typeof(CharacterController) && check) {
                    StartCoroutine(destroy(hit.collider.gameObject));
                    check = false;
                }
            }
        }
    }

    IEnumerator destroy(GameObject go)
    {
        Debug.Log("DESTROY  "+go.name);
        foreach (GameObject explosion in explosions)
        {
            Destroy(Instantiate(explosion, hit.point + (Random.insideUnitSphere * 5) + Vector3.up*2, Quaternion.identity), 3);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(go);
        check = true;
        yield return null;
    }
}
