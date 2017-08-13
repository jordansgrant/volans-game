using UnityEngine;

public class WrapObject : MonoBehaviour
{

    Vector2 wrap;
    Vector2 maxWrap;

    void Awake()
    {
        Vector2 minwrap = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        wrap = new Vector2(minwrap.x - 1, minwrap.y - 1);

        Vector2 maxwrap = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        maxWrap = new Vector2(maxwrap.x + 1, maxwrap.y + 1);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        if (position.x > maxWrap.x)
        {
            position.x = wrap.x;
            transform.position = position;
        }
        if (position.x < wrap.x)
        {
            position.x = maxWrap.x;
            transform.position = position;
        }
        if (position.y > maxWrap.y)
        {
            position.y = wrap.y;
            transform.position = position;
        }
        if (position.y < wrap.y)
        {
            position.y = maxWrap.y;
            transform.position = position;
        }
    }
}