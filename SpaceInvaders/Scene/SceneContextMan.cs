using System;
using System.Data;
using System.Diagnostics;
using static SE456.SceneContext;

namespace SE456
{
    public class SceneContextMan
    {
        private SceneContextMan()
        {
            this.poSceneContext = null;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create()
        {
            Debug.Assert(psInstance == null);

            if (psInstance == null)
            {
                psInstance = new SceneContextMan();
            }
        }

        public static void Destroy()
        {
            SceneContextMan pMan = SceneContextMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.poSceneContext = null;
            psInstance = null;
        }

        public static void Set(SceneContext pScene)
        {
            Debug.Assert(pScene != null);

            SceneContextMan pMan = SceneContextMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.poSceneContext = pScene;
        }

        public static SceneContext Get()
        {
            SceneContextMan pMan = SceneContextMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.poSceneContext;
        }

        public static void Update(float time)
        {
            SceneContextMan pMan = SceneContextMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.poSceneContext.GetState().Update(time);
        }

        public static void Draw()
        {
            SceneContextMan pMan = SceneContextMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.poSceneContext.GetState().Draw();
        }

        public static void SetState(Scene eScene)
        {
            SceneContextMan pMan = SceneContextMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.poSceneContext.SetState(eScene);
        }

        public static SceneState GetState()
        {
            SceneContextMan pMan = SceneContextMan.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.poSceneContext.GetState();
        }

        //----------------------------------------------------------------------
        // Private Methods
        //----------------------------------------------------------------------
        private static SceneContextMan privGetInstance()
        {
            Debug.Assert(psInstance != null);
            return psInstance;
        }

        //----------------------------------------------------------------------
        // Data Members
        //----------------------------------------------------------------------
        private SceneContext poSceneContext;
        private static SceneContextMan psInstance = null;
    }
}