using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class UFOCategory : Leaf
    {
        public enum Type
        {
            UFO,
            UFORoot,
            Unitialized
        }

        protected UFOCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, UFOCategory.Type bombType)
            : base(name, spriteName, posX, posY)
        {
            this.UFOType = bombType;
        }

        // Data: ---------------
        ~UFOCategory()
        {
        }

        // this is just a placeholder, who knows what data will be stored here
        protected UFOCategory.Type UFOType;

    }
}

// --- End of File ---
