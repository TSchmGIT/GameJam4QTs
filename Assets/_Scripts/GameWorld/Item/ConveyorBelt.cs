using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ConveyorBelt : MonoBehaviour
{
	#region Unity References
	private MeshRenderer m_MeshRenderer = null;

	[SerializeField, Range(-5.0f, 5.0f)]
	private float m_ConveyorSpeed = 0.0f;

	#endregion

	#region Public Properties
	public float speed => m_ConveyorSpeed;
	#endregion

	#region Unity Callbacks

	private void Awake()
	{
		m_MeshRenderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		UpdateConveyerBeltTexture();
	}

	#endregion

	#region Private Methods

	private void UpdateConveyerBeltTexture()
	{
		Material material = m_MeshRenderer.material;

		Vector2 currentOffset = material.GetTextureOffset("_MainTex");
		m_MeshRenderer.material.SetTextureOffset("_MainTex", currentOffset + Vector2.up * m_ConveyorSpeed * Time.deltaTime);
		m_MeshRenderer.material.SetTextureScale("_MainTex", new Vector2(transform.lossyScale.x, transform.lossyScale.z));
	}

	#endregion
}
