using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public GameObject shipModel;
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        this.transform.Rotate(new Vector3(0, 0, -1) * horizontal + new Vector3(1, 0, 0) * vertical, rotateSpeed * Time.deltaTime);
        shipModel.transform.localEulerAngles = new Vector3(vertical * 10, 0, horizontal * -10);

        rigidbody.velocity = transform.rotation * Vector3.forward * speed;

        Shot();
    }
    private void Shot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = PrefabsHelper.CreatePrefab(PrefabEnum.Bullet, shipModel.transform.position, this.transform.rotation);
            var rig = bullet.GetComponent<Rigidbody>();
            rig.velocity = transform.rotation * Vector3.forward * speed * 20;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }

}
