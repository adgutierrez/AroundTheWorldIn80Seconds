using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour {

	void Update () {

        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.x += Time.deltaTime / 40f;
        mat.mainTextureOffset = offset;

//        offset.x += transform.position.x / transform.localScale.x / 10f;
//        offset.y += transform.position.y / transform.localScale.y;
	}
}
