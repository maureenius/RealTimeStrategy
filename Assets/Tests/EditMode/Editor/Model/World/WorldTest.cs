using System;
using NUnit.Framework;

namespace Tests.EditMode.Editor.Model.World
{
    public class WorldTest
    {
        private global::Model.World.World world = new global::Model.World.World();

        [Test]
        public void Constructor()
        {
        }

        [Test]
        public void DoOneTurn()
        {
            world.DoOneTurn();
            
            Assert.AreEqual(world.Date, new DateTime(1000, 1, 2));
        }
    }
}