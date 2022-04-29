using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class CollisionInfo {
        public readonly Vector2 normal;
        public readonly GameObject other;
        public readonly float timeOfImpact;

        public CollisionInfo(Vector2 pNormal, GameObject pOther, float pTimeOfImpact) {
            normal = pNormal;
            other = pOther;
            timeOfImpact = pTimeOfImpact;
        }
    }
}