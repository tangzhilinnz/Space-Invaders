//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SE456
{
    public class MissileSpawnEvent : Command
    {
        public MissileSpawnEvent(Random pRandom)
        {
            this.pMissileRoot = GameObjectNodeMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(this.pMissileRoot != null);

            this.pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Alien_Batch);
            Debug.Assert(this.pSB_Aliens != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);
            Debug.Assert(this.pSB_Boxes != null);

            this.pRandom = pRandom;
        }

        override public void Execute(float deltaTime)
        {
            //Debug.WriteLine("event: {0}", deltaTime);

            // Create missile
            float value = this.pRandom.Next(300, 700);

            Missile pMissile = new Missile(SpriteGame.Name.Missile, value, 100);
            //     Debug.WriteLine("----x:{0}", value);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectNodeMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pMissile);

        }


        GameObject pMissileRoot;
        SpriteBatch pSB_Aliens;
        SpriteBatch pSB_Boxes;
        Random pRandom;
    }
}

// --- End of File ---
