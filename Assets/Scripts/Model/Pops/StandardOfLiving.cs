#nullable enable

namespace Model.Pops
{
    public class StandardOfLiving
    {
        private LivingLevel level = LivingLevel.Poor;
        public LivingLevel Level => level;
    }

    public enum LivingLevel
    {
        Poor = 1,
        Middle = 2,
        Rich = 3
    }
}