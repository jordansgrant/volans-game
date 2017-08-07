using UnityEngine;

public class evade_objects : MonoBehaviour
{ 
    public float speed = 2;

    // Update is called once per frame
    void Update()
    {

        //RaycastHit2D hit = Physics2D.CircleCast(turret.transform.position, 2.0f, transform.right);
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 3.0f, 1 << LayerMask.NameToLayer("Foreground"));
        if (!hit.name.Contains("enemy"))
        {
            Debug.Log("Here hit: " + hit.name);
            MoveAway(hit.transform.position);
        }
    }

    private void MoveAway(Vector3 position)
    {
        Vector3 dir = position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        // perform rotation slowly toward player 
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);

        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
      
    }
}
