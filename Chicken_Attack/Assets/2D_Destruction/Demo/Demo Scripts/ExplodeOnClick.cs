using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Explodable))]
public class ExplodeOnClick : MonoBehaviour {

	private Explodable _explodable;
    public bool Hit;
	void Start()
	{
		_explodable = GetComponent<Explodable>();
	}
	void OnMouseDown()
	{
		_explodable.explode();
		ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();

        Vector2  a= new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        ef.doExplosion(a);
        print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Hit)
        {
            _explodable.explode();
            ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
            ContactPoint2D contact = collision.contacts[0];
            Vector2 a = contact.point;

            ef.doExplosion(a);
        }
    }
}
