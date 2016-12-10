using UnityEngine;
namespace Lime.PhysicsUtility {
    public class LimeTrajectory {
        public static float AngleToCoordinate(Vector3 origin, Vector3 target, float power) {
            Vector3 newTarget = origin - target;
            float g = 9.81f; // gravity
            float v = power; // velocity
            float x = newTarget.x; // target x
            float y = newTarget.y; // target y
            float s = (v * v * v * v) - g * (g * (x * x) + 2 * y * (v * v)); //substitution
            float o = Mathf.Atan(((v * v) + Mathf.Sqrt(s)) / (g * x)); // launch angle
            //Debug.Log(o);
            return o;

            /*
              Vector3 newOrigin = origin - target;

            float xx = newOrigin.x;
            float yy = newOrigin.y;
            float xz = newOrigin.z;
            float velocity = power;
            float gravity = UnityEngine.Physics.gravity.y;

            float x = Mathf.Sqrt(xx * xx + xz * xz);
            return Vector3.zero;
            */
        }

        /// <summary>
        /// Calculates velocity needed for origin to reach target with given initialAngle
        /// 
        /// Example: rigidbody.velocity = LaunchVelocity(this.transform.position, target.transform.position, 35);
        /// or:      rigidbody.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
        /// based on: http://forum.unity3d.com/threads/how-to-calculate-force-needed-to-jump-towards-target-point.372288/
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="initialAngle"></param>
        /// <returns></returns>
        public static Vector3 LaunchVelocityToCoordinate(Vector3 origin, Vector3 target, float initialAngle) {

   
            float gravity = Physics.gravity.magnitude;
            float angleRadians = initialAngle * Mathf.Deg2Rad;

            // Positions of this object and the target on the same plane
            Vector3 planarTarget = new Vector3(target.x, 0, target.z);
            Vector3 planarOrigin = new Vector3(origin.x, 0, origin.z);

            float distance = Vector3.Distance(planarTarget, planarOrigin);


            float yOffset = origin.y - target.y;


            float initialVelocity = (1 / Mathf.Cos(angleRadians)) * Mathf.Sqrt((0.5f * gravity * (distance * distance)) / (distance * Mathf.Tan(angleRadians) + yOffset));
            //    float initialVelocity = (1 / Mathf.Cos(angleRadians)) * Mathf.Sqrt((0.5f * gravity * (distance * distance)) / (distance * Mathf.Tan(angleRadians) + yOffset));


            Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angleRadians), initialVelocity * Mathf.Cos(angleRadians));



            // Rotate our velocity to match the direction between the two objects
            float angleBetweenObjects = (Vector3.Angle(Vector3.forward, planarTarget - planarOrigin));

            // Flip angle if shooting towards negative x axis
            if (planarTarget.x < planarOrigin.x) {
                angleBetweenObjects *= -1;
            }

            Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

            return finalVelocity;

        }

        public static Vector3 LaunchVelocityToCoordinate(Vector3 origin, Vector3 target, bool useLowArc, float projectileSpeed) {
            float initialAngle = AngleRequiredToHitTarget(origin, target, useLowArc, projectileSpeed);
            return LaunchVelocityToCoordinate(origin, target, initialAngle);
        }

        /// <summary>
        /// Uses 
        /// </summary>
        /// <param name="origin">where to launch from</param>
        /// <param name="target">target</param>
        /// <param name="positiveAngle"> </param>
        /// <param name="lowerArc"> Use the lower arc or higher arc </param>
        /// <param name="projectileSpeed">speed of projectile</param>
        /// <returns>The </returns>
        public static float AngleRequiredToHitTarget(Vector3 origin, Vector3 target, bool useLowerArc, float projectileSpeed) {

            float y = target.y - origin.y;
            target.y = origin.y = 0;

            // Get distance
            float xx = target.x - origin.x;
            float xz = target.z - origin.z;
            float x = Mathf.Sqrt(xx * xx + xz * xz);

            float g = Physics.gravity.magnitude;
            float v = projectileSpeed;

            float toSquare = (v * v * v * v) - (g * (g * (x * x) + 2 * (y) * (v * v)));
            // if toSquare <= 0 not enough speed
            

            float squared = Mathf.Sqrt(toSquare);
            float rads = 0f;
            if (useLowerArc) {
                rads = Mathf.Atan(((v * v) - squared) / (g * x));
            } else {
                rads = Mathf.Atan(((v * v) + squared) / (g * x));
            }
        
            return rads * Mathf.Rad2Deg;
        }

    
    }
}
