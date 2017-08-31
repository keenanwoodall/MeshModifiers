namespace MeshModifiers
{
	public class MeshModifierConstants
	{
		#region Run-time Constants

		public const int MAX_MOD_FRAMES = 10;

		#endregion



		#region Editor Constants

		public const string 
			ADD_COMP_BASE_NAME = "Mesh Modifier/",
			ADD_COMP_DEBUG_NAME = ADD_COMP_BASE_NAME + "Debug/",
			ADD_EXPERIMENTAL_COMP_NAME = ADD_COMP_BASE_NAME + "Experimental/";

		public const string
			MODIFIER_STRENGTH_VERTEX_MASK_TOOLTIP = "Using a vertex painting plugin you can paint a color on to the mesh. Choose what color channel you painted into and the modifier strength will be controlled by the vertices' color value in that channel.",
			NORMALS_QUALITY_TOOLTIP = "When set to Low Quality, the smooothing angle is not taken into account. High Quality is very expensive and shouldn't be used every frame, if possible. You can call RefreshSurfaces manually to calculate High Quality normals at the time of your choosing.",
			SMOOTHING_ANGLE_TOOLTIP = "The smoothing angle is only used when calculation is high quality. The mesh can never be less smooth than the imported mesh.",
			UPDATE_WHEN_HIDDEN_TOOLTIP = "Only performs modifications when this object is being rendered by the camera.",
			MODIFY_FRAMES_TOOLTIP = "The number of frames that the modification calculations are performed over. The higher this value goes, the faster the program will run; at the cost of visual fidelity. This parameter also has diminishing returns.";

		#endregion
	}
}