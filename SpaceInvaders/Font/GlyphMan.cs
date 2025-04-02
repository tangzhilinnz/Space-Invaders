//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Xml;
using System.Diagnostics;

namespace SE456
{
	public class GlyphMan : ManBase
	{
		//----------------------------------------------------------------------
		// Constructor
		//----------------------------------------------------------------------
		private GlyphMan(int reserveNum = 3, int reserveGrow = 1)
				: base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
		{
			// initialize derived data here
			GlyphMan.psNodeCompare = new Glyph();
			Debug.Assert(GlyphMan.psNodeCompare != null);
		}

		//----------------------------------------------------------------------
		// Static Methods
		//----------------------------------------------------------------------
		public static void Create(int reserveNum = 3, int reserveGrow = 1)
		{
			// make sure values are ressonable 
			Debug.Assert(reserveNum > 0);
			Debug.Assert(reserveGrow > 0);

			// initialize the singleton here
			Debug.Assert(pInstance == null);

			// Do the initialization
			if (pInstance == null)
			{
				pInstance = new GlyphMan(reserveNum, reserveGrow);
			}
		}
		public static void Destroy()
		{
		}

		public static Glyph Add(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
		{
			GlyphMan pMan = GlyphMan.privGetInstance();

			Glyph pNode = (Glyph)pMan.baseAdd();
			Debug.Assert(pNode != null);

			pNode.Set(name, key, textName, x, y, width, height);
			return pNode;
		}

		public static void AddXml(String assetName, Glyph.Name glyphName, Texture.Name textName)
		{
			System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

			int key = -1;
			int x = -1;
			int y = -1;
			int width = -1;
			int height = -1;

			// I'm sure there is a better way to do this... but this works for now
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element: // The node is an element.
						if (reader.GetAttribute("key") != null)
						{
							key = Convert.ToInt32(reader.GetAttribute("key"));
						}
						else if (reader.Name == "x")
						{
							while (reader.Read())
							{
								if (reader.NodeType == XmlNodeType.Text)
								{
									x = Convert.ToInt32(reader.Value);
									break;
								}
							}
						}
						else if (reader.Name == "y")
						{
							while (reader.Read())
							{
								if (reader.NodeType == XmlNodeType.Text)
								{
									y = Convert.ToInt32(reader.Value);
									break;
								}
							}
						}
						else if (reader.Name == "width")
						{
							while (reader.Read())
							{
								if (reader.NodeType == XmlNodeType.Text)
								{
									width = Convert.ToInt32(reader.Value);
									break;
								}
							}
						}
						else if (reader.Name == "height")
						{
							while (reader.Read())
							{
								if (reader.NodeType == XmlNodeType.Text)
								{
									height = Convert.ToInt32(reader.Value);
									break;
								}
							}
						}
						break;

					case XmlNodeType.EndElement: //Display the end of the element 
						if (reader.Name == "character")
						{
							// have all the data... so now create a glyph
							// Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
							GlyphMan.Add(glyphName, key, textName, x, y, width, height);
						}
						break;
				}
			}

			// Debug.Write("\n");
		}

		public static Glyph Find(Glyph.Name _name, int _key)
		{
			GlyphMan pMan = GlyphMan.privGetInstance();
			Debug.Assert(pMan != null);

			// Compare functions only compares two Nodes

			// So:  Use the Compare Node - as a reference
			//      use in the Compare() function
			GlyphMan.psNodeCompare.mName = _name;
			GlyphMan.psNodeCompare.key = _key;

			Glyph pData = (Glyph)pMan.baseFind(GlyphMan.psNodeCompare);
			return pData;
		}

		public static void Remove(Glyph pImage)
		{
			Debug.Assert(pImage != null);

			GlyphMan pMan = GlyphMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseRemove(pImage);
		}
		public static void Dump()
		{
			GlyphMan pMan = GlyphMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseDump();
		}

		//------------------------------------
		// Override Abstract methods
		//------------------------------------
		override protected NodeBase derivedCreateNode()
		{
			NodeBase pNodeBase = new Glyph();
			Debug.Assert(pNodeBase != null);

			return pNodeBase;
		}

		//------------------------------------
		// Private methods
		//------------------------------------
		private static GlyphMan privGetInstance()
		{
			// Safety - this forces users to call Create() first 
			// before using class
			Debug.Assert(pInstance != null);

			return pInstance;
		}

		//------------------------------------
		// Data: unique data for this manager 
		//------------------------------------
		private static Glyph psNodeCompare;
		private static GlyphMan pInstance = null;

	}
}

// --- End of File ---
