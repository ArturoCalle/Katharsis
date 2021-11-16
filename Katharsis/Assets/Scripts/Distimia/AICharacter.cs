using UnityEngine;

namespace UnityStandardAssets.Assets.ThirdPerson
{
	[RequireComponent(typeof(Animator))]
	public class AICharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_JumpPower = 12f;
		[Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		Animator m_Animator;
		bool m_IsGrounded;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		public bool IsAlive = true;


		void Start()
		{
			m_Animator = GetComponent<Animator>();
		}
		/**
		 * Este metodo recibe las variables de control de DistimiaIA. Según los parametros, controla las animaciones que debe correr y
		 * retorna la velocidad que será luego asignada al NavMesh Agent
		 */
		public float Move(Vector3 move, bool enojado, bool ansioso, bool golpearArriba, bool golpearAbajo)
		{
			if(golpearArriba)
            {
				m_Animator.Play("GolpeAlto");
            }else if (golpearAbajo)
            {
				m_Animator.Play("GolpeBajo");
			}
			if(m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("GolpeAlto") || 
				m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("GolpeBajo"))
            {
				return 0f;
            }
			if (enojado)
			{
				m_Animator.SetBool("Enojado", true);
				Desplazar(move);
				return 10f;
			}else
            {
				m_Animator.SetBool("Enojado", false);
				if (m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Caminar"))
				{
					Desplazar(move);
					return 5f;
				}
			}
			if (ansioso)
			{
				m_Animator.speed = 2;
				Desplazar(move);
				return 8f;
            }
            else
            {
				if (m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Caminar"))
				{
					Desplazar(move);
					return 5f;
                }
                else
                {
					return 0;
                }
			}
		}

        public void PlayAnsiedad()
        {
			m_Animator.Play("Ansiedad");
        }
		/**
		 * metodo editado de  UnityStandardAssets.Assets.ThirdPerson
		 */
		private void Desplazar(Vector3 move)
        {
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();
		}

		public void ApplyExtraTurnRotation()
		{
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}
		/**
		 * lanza un raycast en la base del 
		 */
        void CheckGroundStatus()
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
				m_Animator.applyRootMotion = true;
			}
			else
			{
				m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
				m_Animator.applyRootMotion = false;
			}
		}
	}

}
