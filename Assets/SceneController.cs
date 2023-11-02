using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class SceneController : MonoBehaviour
{
    public List<GameObject> outlines = new List<GameObject>();
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        int layerMask = 1 << 6;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerObject.transform.TransformDirection(Vector3.forward), out hit, 1000, layerMask))
        {
            Debug.Log("Hit");
            hit.transform.gameObject.GetComponent<Outline>().enabled = true;
        } else {
            Debug.Log("Miss");
        }

        return;
        foreach (GameObject item in outlines) {
            UnityEngine.Debug.Log(Vector3.Distance(item.transform.position, playerObject.transform.position));
            if (Vector3.Distance(item.transform.position, playerObject.transform.position) < 3) {
                item.GetComponent<Outline>().enabled = true;
                if (Input.GetKeyDown(KeyCode.E)) {
                    if (item.tag == "KeyItem") {
                        item.SetActive(false);
                    } else if (item.tag == "NPC") {
                        UnityEngine.Debug.Log(item.GetComponent<NPCHandler>().dialog);
                    }
                }
            } else {
                item.GetComponent<Outline>().enabled = false;
            }
        }
    }
}
