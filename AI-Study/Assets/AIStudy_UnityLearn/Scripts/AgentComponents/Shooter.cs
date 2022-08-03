using UnityEngine;

public class Shooter : AgentComponent
{
    [SerializeField] private Transform weapon = null;
    [SerializeField] private Transform muzzle = null;
    [SerializeField] private Projectile projectilePrefab = null;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float fireRate = 1f;
    // If angle between transform and target vector is more
    // than anglePrecision, do not shoot.
    [SerializeField] private float anglePrecision = 10f;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private bool autopilot = false;

    private Transform target;
    private Driver driver;
    private bool isWeaponReady;

    public override void Init(Agent owner)
    {
        base.Init(owner);
        isWeaponReady = true;
        target = owner.Target;
        owner.OnTargetChanged += (target) => this.target = target;
    }

    public override void Tick()
    {
        if (!target)
            return;

        if (!autopilot)
        {
            Rotate(Input.GetAxis("Vertical"));

            if (canShoot && isWeaponReady && Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        else
        {
            float? angle = AutoRotate();

            if(canShoot)
            {
                float angleToTarget = Vector3.Angle(target.position - weapon.position, transform.forward);
                if (angle != null && isWeaponReady &&  angleToTarget < anglePrecision)
                {
                    isWeaponReady = false;
                    Shoot();
                    Invoke("WeaponReady", fireRate);
                }
            }    
        }
    }

    private void WeaponReady()
    {
        isWeaponReady = true;
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.Init(owner.transform, muzzle);
    }

    private void Rotate(float input)
    {
        weapon.Rotate(input * rotationSpeed * Time.deltaTime, 0, 0);
    }

    private float? AutoRotate()
    {
        float? angle = CalculateAngle(false);

        if(angle != null)
        {
            weapon.localEulerAngles = new Vector3(360 - (float)angle, 0f, 0f);
        }

        return angle;
    }

    private float? CalculateAngle(bool low)
    {
        Vector3 direction = target.position - weapon.position;
        float y = direction.y;
        direction.y = 0;
        float x = direction.magnitude;
        float gravity = 9.81f;
        float squareSpeed = projectilePrefab.Speed * projectilePrefab.Speed;
        float underSquareRoot = (squareSpeed * squareSpeed) - gravity * (gravity * x * x + 2 * y * squareSpeed);

        if(underSquareRoot >= 0f)
        {
            float root = Mathf.Sqrt(underSquareRoot);
            float highAngle = squareSpeed + root;
            float lowAngle = squareSpeed - root;

            if(low)
            {
                return Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg;
            }
            else
            {
                return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg;
            }
        }

        return null;
    }
}
