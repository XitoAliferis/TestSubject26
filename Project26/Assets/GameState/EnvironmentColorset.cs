using UnityEngine;

[CreateAssetMenu(fileName = "New Colorset", menuName = "Definitions/Environment Colorset")]
public class EnvironmentColorset : ScriptableObject {

	[ColorUsage(false, true)]
	public Color skyColor;
	[ColorUsage(false, true)]
	public Color equatorColor;
	[ColorUsage(false, true)]
	public Color groundColor;

	public void Apply() {
		RenderSettings.ambientSkyColor = skyColor;
		RenderSettings.ambientEquatorColor = equatorColor;
		RenderSettings.ambientGroundColor = groundColor;
	}

	public static EnvironmentColorset FromCurrent() {
		EnvironmentColorset n = CreateInstance<EnvironmentColorset>();
		n.skyColor = RenderSettings.ambientSkyColor;
		n.equatorColor = RenderSettings.ambientEquatorColor;
		n.groundColor = RenderSettings.ambientGroundColor;
		return n;
	}
}