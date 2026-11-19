using UnityEngine;

public class BaseEnemyMovement
{
    Camera cam;
    Vector3 targetPosition;
    Transform self;
    float speed = 3f;
    public BaseEnemyMovement(Transform self)
    {
        cam = Camera.main;
        this.self = self;
        PickNewTarget();
    }
    public void move()
    {
        Vector3 direction = (targetPosition - self.position).normalized;
        self.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(self.position, targetPosition) < 0.1f)
        {
            PickNewTarget();
        }
    }
    void PickNewTarget()
    {
        float x = Random.Range(0, 1f);
        float y = Random.Range(0.5f, 1f);
        targetPosition = cam.ViewportToWorldPoint(new Vector3(x, y, Mathf.Abs(cam.transform.position.z - self.position.z)));
    }

}
