using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class SceneController : MonoBehaviour
{
    public List<GameObject> outlines = new List<GameObject>();
    public GameObject playerObject;
    public float coneRadius = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        int layerMask = 1 << 6;

        RaycastHit hit;
        Vector3 rayDirection = playerObject.transform.GetChild(0).transform.TransformDirection(Vector3.forward);
        if (Physics.SphereCast(playerObject.transform.position, coneRadius, rayDirection, out hit, 3, layerMask))
        {
            hit.transform.gameObject.GetComponent<Outline>().enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.transform.gameObject.tag == "KeyItem")
                {
                    hit.transform.gameObject.SetActive(false);
                }
                else if (hit.transform.gameObject.tag == "NPC")
                {
                    GetComponent<DialogManager>().SetDialog(hit.transform.gameObject.GetComponent<NPCHandler>().dialog);
                }
            }
        }

        Debug.DrawLine(playerObject.transform.position, playerObject.transform.position + rayDirection.normalized * 3, Color.red, 0.1f);

        foreach (GameObject item in outlines)
        {
            if (hit.transform == null || hit.transform.gameObject != item)
            {
                item.GetComponent<Outline>().enabled = false;
            }
        }
    }
}
