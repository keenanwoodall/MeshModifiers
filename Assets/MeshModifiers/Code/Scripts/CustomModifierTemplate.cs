using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshModifiers;

// If you want to add this modifier to the component menu, uncomment the line below and type in your modifier name.
//[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "-Your Modifier Name Here-")]
public class CustomModifierTemplate : MeshModifierBase
{
	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		/* READ ME OR REMOVE ME
		
		This method receices the position and normal of a vertice.
		Whatever Vector3 is returned will be the new position of the vertice.


		For example, the line below randomly moves the vertice.

		basePosition += new Vector3 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f));


		You also have access to the ModifierObject, which handles this modifier along with any other ones attached to the same GameObject.
		From there you can info you might need.

		For example, Time.time doesn't work very well when calculations need to take place over multiple frames. If you want a value to animate over time, use modObject.Time!

		*/

		// Return an unmodified vertice.
		return basePosition;
	}
}
