using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshModifiers
{
	[AddComponentMenu (MeshModifierConstants.ADD_EXPERIMENTAL_COMP_NAME + "Anti Modifier")]
	public class AntiModifier : MeshModifierBase
	{
		#region Public Properties

		public Transform effector;

		public float scale = 1f;

		#endregion



		#region Private Properties

		private Vector3 localEffectorPosition;

		#endregion



		#region Inherited Methods

		public override void PreMod ()
		{
			base.PreMod ();

			localEffectorPosition = modObject.transform.InverseTransformPoint (effector.position);
		}

		protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}