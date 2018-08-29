using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavaWrapper : MonoBehaviour
{

	// Use this for initialization
	void Start () {
#if UNITY_ANDROID && !UNITY_EDITOR
        var javaClass = new AndroidJavaObject("Addition");
        javaClass.Call("Addification", 2, 9);
#endif
    }
}
