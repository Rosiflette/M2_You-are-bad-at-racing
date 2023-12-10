using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour
{
    [SerializeField] GameObject confettis;

    private void OnTriggerStay(Collider other)
    {
        // player needs to be stop in the finish zone to win :)
        if (other.tag == "Player")
        {
            Rigidbody r = other.transform.parent.GetComponentInChildren<Rigidbody>();
            float velocity = Mathf.Round(r.velocity.magnitude * 10) / 10;
            if (velocity == 0)
            {
                GameManager.Instance.FinishGame();

                Vector3 extents = GetComponent<Collider>().bounds.extents * 3 / 4;
                Vector3 pos = new Vector3(transform.position.x + extents.x, other.transform.position.y, transform.position.z + extents.z);
                Instantiate(confettis, pos, Quaternion.identity);
                pos = new Vector3(transform.position.x - extents.x, other.transform.position.y, transform.position.z - extents.z);
                Instantiate(confettis, pos, Quaternion.identity);
                pos = new Vector3(transform.position.x + extents.x, other.transform.position.y, transform.position.z - extents.z);
                Instantiate(confettis, pos, Quaternion.identity);
                pos = new Vector3(transform.position.x - extents.x, other.transform.position.y, transform.position.z + extents.z);
                Instantiate(confettis, pos, Quaternion.identity);

                gameObject.SetActive(false);
            }
        }
    }
}
