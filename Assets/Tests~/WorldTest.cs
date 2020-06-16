using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WorldTest
    {
        private World world;
        
        [Test]
        public void Constructor()
        {
            world = new World();
        }

        [Test]
        public void DoOneTurn()
        {
            world = new World();
            world.DoOneTurn();

            var tmp = 1;
        }
    }
}
