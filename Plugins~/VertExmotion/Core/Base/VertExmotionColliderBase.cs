using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kalagaan
{
    /// Compute collision
    /// require a VertExmotionSensor component
    //[RequireComponent(typeof(VertExmotionSensor))]
    public class VertExmotionColliderBase : MonoBehaviour
    {

        [System.Serializable]
        public class CollisionZone
        {
            public Vector3 positionOffset = Vector3.zero;
            public float radius = 1f;
            [HideInInspector]
            public Vector3 collisionVector = Vector3.zero;
            [HideInInspector]
            public RaycastHit[] hits;
        }


        [HideInInspector]
        public string className = "VertExmotionCollider";

        ///Layer mask for physic interactions
        /// 
        public LayerMask m_layerMask = -1;

        ///Smooth factor
        /// 0 : no smooth
        /// 1 : realistic reaction with inertia
        /// 10 : low reaction to physic
        public float m_smooth = 1f;
        public bool m_disableBackwardCollisions = true;

        /// Enable wobble fx                
        public bool m_wobble = false;
        public float m_damping = 1f;
        public float m_bouncing = 1f;
        public float m_limit = 1f;
        public float m_friction = .1f;

        private bool m_collided = false;

        ///List of CollisionZone
        ///add several zone to fit mesh volume
        public List<CollisionZone> m_collisionZones = new List<CollisionZone>();
        //float m_collisionScaleFactor = 1f;
        public List<Collider> m_ignoreColliders = new List<Collider>();

        public bool m_maximizeSphereCollision = true;

        VertExmotionSensorBase m_sensor;

        Collider[] m_hitColliders = new Collider[100];
        RaycastHit[] m_hitResult = new RaycastHit[100];

        public PID_V3 m_pid = new PID_V3();
        Vector3 m_smoothTarget;
        
        protected float m_smoothDamping = 1f;
        protected float m_smoothBouncing = 1f;



        void Start()
        {
            m_sensor = GetComponentInParent<VertExmotionSensorBase>();
            if (m_sensor == null)
            {
                enabled = false;
                Debug.LogError("VertExmotion collider must be a child of a sensor");
            }

            //check on larger sphere to get better precision for unity 4
            //if (Application.unityVersion.StartsWith("4"))
            //   m_collisionScaleFactor = 100f;

            
            m_pid.Init();
        }


        /// <summary>
        /// Ignore collider from colliding with collision zone
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="ignore"></param>
        public void IgnoreCollision( Collider collider, bool ignore )
        {
            if (ignore && !m_ignoreColliders.Contains(collider))
                m_ignoreColliders.Add(collider);

            if( !ignore && m_ignoreColliders.Contains(collider))
                m_ignoreColliders.Remove(collider);
        }


        //void LateUpdate()
        void FixedUpdate()
        {
            Vector3 target = Vector3.zero;
            float count = 0;            
            
            for (int i = 0; i < m_collisionZones.Count; ++i)
            {
                UpdateCollisionZone(m_collisionZones[i]);
                if (m_collisionZones[i].collisionVector != Vector3.zero)
                {
                    if (target == Vector3.zero)
                    {
                        target = m_collisionZones[i].collisionVector;
                    }
                    else
                    {
                        //if( target.magnitude < m_collisionZones[i].collisionVector.magnitude )
                        //  target = m_collisionZones[i].collisionVector;
                        target = Vector3.Lerp( m_collisionZones[i].collisionVector, target, target.magnitude / (target.magnitude+ m_collisionZones[i].collisionVector.magnitude) );
                    }
                    count++;
                }
            }

            if (count > 1)
                target /= count;

            m_smoothTarget = Vector3.Lerp(m_smoothTarget, target, m_sensor.deltaTime * 100f);
            //m_smoothTarget = target;

            //set target
            m_smooth = Mathf.Clamp(m_smooth, 0, 10f);

            
            //if (target.magnitude == 0 && m_wobble )
            if(m_wobble)
            {
                //wobble

                if(m_collided)
                {
                    
                    m_friction = Mathf.Clamp01(m_friction);
                    m_smoothDamping = Mathf.Lerp(m_smoothDamping, Mathf.Lerp(1f, 100f, m_friction), Time.deltaTime * 10f);
                    m_smoothBouncing = Mathf.Lerp(m_smoothBouncing, Mathf.Lerp(1f, .001f, m_friction), Time.deltaTime * 10f);

                    //don't care about limits on collision
                    m_pid.m_params.limits.x = -float.MaxValue;
                    m_pid.m_params.limits.y = float.MaxValue;

                   
                }
                else//collision off
                {
                    
                    m_smoothDamping = Mathf.Lerp(m_smoothDamping, 1f, Time.deltaTime * 10f);
                    m_smoothBouncing = Mathf.Lerp(m_smoothBouncing, 1f, Time.deltaTime * 10f);

                    m_pid.m_params.limits.x = -m_limit;
                    m_pid.m_params.limits.y = m_limit;
                }

                m_pid.m_target = m_smoothTarget;
                m_pid.m_params.kp = m_damping * m_smoothDamping;
                m_pid.m_params.ki = m_bouncing * m_smoothBouncing;

                //Vector3 oldCol = m_sensor.m_collision;
                m_sensor.m_collision = m_pid.Compute(m_sensor.m_collision);                
            }
            else
            {
                //smooth
                if (m_smooth > 0f)
                    m_sensor.m_collision = Vector3.Lerp(m_sensor.m_collision, m_smoothTarget, m_sensor.deltaTime * (10f / m_smooth));
                else
                    m_sensor.m_collision = m_smoothTarget;
            }
        }


        public void UpdateCollisionZone(CollisionZone cz)
        {

            Vector3 collisionCenter = transform.TransformPoint(cz.positionOffset);// transform.position + transform.rotation * cz.positionOffset;

            float radius = cz.radius * VertExmotionBase.GetScaleFactor(transform);
            int nbCol = Physics.OverlapSphereNonAlloc(collisionCenter, radius, m_hitColliders, m_layerMask);

            
            int j = 0;
            
            Vector3 allCollisionVectors = Vector3.zero;
            int nbHitCount = 0;

            while (j < nbCol)
        	{  

                if (m_ignoreColliders.Contains(m_hitColliders[j]))
                {
                    j++;
                    continue;
                }

                //nbColliders++;


                Vector3 HitPoint = m_hitColliders[j].ClosestPointOnBounds(collisionCenter);
                HitPoint = (HitPoint +  m_hitColliders[j].transform.position) * .5f;
                //HitPoint = m_hitColliders[j].transform.position;

                //Debug.DrawLine(HitPoint, collisionCenter, Color.red);

                Vector3 collisionVector = Vector3.zero;

                Vector3 castDir = (HitPoint - collisionCenter).normalized;//use the bounding box closeset point approximation as a cast direction
                int nbHit = Physics.SphereCastNonAlloc(collisionCenter - castDir * radius * 2f, radius, castDir, m_hitResult);

                
                //Debug.DrawLine(collisionCenter - castDir * radius * 2f, collisionCenter);                    

                for (int i=0; i< nbHit; ++i )
                {
                    if (m_hitResult[i].collider != m_hitColliders[j])
                        continue;

                    nbHitCount++;

                    HitPoint = m_hitResult[i].point;

                    //Debug.DrawLine(HitPoint, collisionCenter, Color.red);
                    //Debug.DrawLine(m_hitResult[i].point, m_hitResult[i].point + m_hitResult[i].normal, Color.blue);
                    
                    if (Vector3.Dot((HitPoint - collisionCenter).normalized, m_hitResult[i].normal) < 0 || m_maximizeSphereCollision)
                        collisionVector = (radius - Vector3.Dot(collisionCenter - HitPoint, m_hitResult[i].normal)) * m_hitResult[i].normal;


                    collisionVector = Vector3.ClampMagnitude(collisionVector, radius);

                    //Debug.DrawLine(collisionCenter, collisionCenter + collisionVector, Color.cyan);


                    if (m_disableBackwardCollisions && Vector3.Dot(collisionVector.normalized, transform.forward) > 0)
                        collisionVector -= transform.forward * Vector3.Dot(collisionVector, transform.forward);

                    if (allCollisionVectors.sqrMagnitude > 0)
                    {
                        //multiple collisions
                        float dot1 = Vector3.Dot(allCollisionVectors.normalized, collisionVector);
                        Vector3 proj1 = Vector3.zero;
                        if (dot1 > 0)
                        {
                            //same direction
                            if (dot1 * dot1 > allCollisionVectors.sqrMagnitude)
                                proj1 = allCollisionVectors.normalized * dot1;
                            else
                                proj1 = allCollisionVectors;
                        }
                        else
                        {
                            //opposit direction
                            proj1 = allCollisionVectors + allCollisionVectors.normalized * dot1;
                        }


                        float dot2 = Vector3.Dot(allCollisionVectors, collisionVector.normalized);
                        Vector3 proj2 = Vector3.zero;
                        if (dot2 > 0)
                        {
                            //same direction
                            if (dot2 * dot2 > collisionVector.sqrMagnitude)
                                proj2 = collisionVector.normalized * dot2;
                            else
                                proj2 = collisionVector;
                        }
                        else
                        {
                            //opposit direction
                            proj2 = collisionVector + collisionVector.normalized * dot2;
                        }

                        Vector3 proj3 = Vector3.Dot(proj1.normalized, proj2) * proj1.normalized;
                        allCollisionVectors = proj1 + proj2 - proj3;
                    }
                    else
                    {
                        allCollisionVectors = collisionVector;
                    }
                        
                }

                j++;
        	}

            if (nbHitCount > 0)
            {
                //Debug.DrawLine(collisionCenter, collisionCenter + allCollisionVectors, Color.magenta);

                if (allCollisionVectors.sqrMagnitude > 0)
                {
                    cz.collisionVector = Vector3.Lerp(cz.collisionVector, allCollisionVectors, Time.deltaTime * 100f);
                    //cz.collisionVector = allCollisionVectors;
                    m_collided = Vector3.Dot( m_sensor.m_collision.normalized ,  allCollisionVectors)> m_sensor.m_collision.magnitude;
                }
                else
                {
                    m_collided = true;
                }

                //m_collided = true;
            }
            else
            {
                cz.collisionVector = Vector3.Lerp(cz.collisionVector, allCollisionVectors, Time.deltaTime * 100f);
                //Debug.Log("No collision");
                m_collided = false;
            }
                
            
        }



#if KVTM_DEBUG
        /*
        Color m_gizmoColor = new Color(1f, 1f, 1f, .3f);
        void OnDrawGizmos()
        {
            for (int id = 0; id < m_collisionZones.Count; ++id)
            {
                Gizmos.color = m_gizmoColor;
                Gizmos.DrawWireSphere(transform.position + m_collisionZones[id].positionOffset, m_collisionZones[id].radius * VertExmotionBase.GetScaleFactor(transform));
                Gizmos.color = Color.red;

                if (m_collisionZones[id].hits != null)
                    for (int i = 0; i < m_collisionZones[id].hits.Length; ++i)
                    {
                        Gizmos.DrawSphere(m_collisionZones[id].hits[i].point, .01f);
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(m_collisionZones[id].hits[i].point, m_collisionZones[id].hits[i].normal);
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(m_collisionZones[id].hits[i].point, m_collisionZones[id].hits[i].point + (transform.position + m_collisionZones[id].positionOffset - m_collisionZones[id].hits[i].point).normalized);
                    }

            }
            

        }
        */
#endif





    }
}
