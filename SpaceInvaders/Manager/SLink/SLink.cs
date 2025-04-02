//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
	abstract public class SLink : NodeBase
	{

		protected SLink()
			: base()
		{
			this.baseWash();
		}

		override public void Detach()
		{
			this.baseWash();
		}
		override protected void baseWash()
		{
			this.pNext = null;

			base.baseWash();
		}
		override protected void baseDump()
		{
			if (this.pNext == null)
			{
				Debug.WriteLine("      next: null");
			}
			else
			{
				NodeBase pTmp = (NodeBase)this.pNext;
				Debug.WriteLine("      next: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
			}

			base.baseDump();
		}

		// Data: -----------------------------
		public SLink pNext;

	}
}

// --- End of File ---