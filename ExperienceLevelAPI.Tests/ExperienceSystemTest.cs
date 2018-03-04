using System;
using ExperiencLevelAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperiencLevelAPI.Tests
{
    [TestClass]
    public class ExperienceSystemTest
    {
        [TestMethod]
        public void TestLinearSystem()
        {
            LevelModel linearLevels = new LevelModel(100);
            ExperienceSystem hero1 = new ExperienceSystem(linearLevels);

            //Check starting values
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(0, hero1.GetTotalExperience());
            Assert.AreEqual(0, hero1.GetLevel());
            Assert.AreEqual(100, hero1.GetLevelGap());

            //Add some xp
            hero1.AddExperience(20,linearLevels);
            hero1.AddExperience(45,linearLevels);
            hero1.AddExperience(13, linearLevels);
            hero1.SubtractExperience(27);

            Assert.AreEqual(51, hero1.GetCurrentExperience());
            Assert.AreEqual(51, hero1.GetTotalExperience());
            Assert.AreEqual(0, hero1.GetLevel());
            Assert.AreEqual(100, hero1.GetLevelGap());

            //Add xp so we level-up 

            hero1.AddExperience(49, linearLevels);
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(100, hero1.GetTotalExperience());
            Assert.AreEqual(1, hero1.GetLevel());
            Assert.AreEqual(100, hero1.GetLevelGap());

            //Add more xp so we level up two levels, then drop and go back up
            hero1.AddExperience(201, linearLevels);
            Assert.AreEqual(3, hero1.GetLevel());
            Assert.AreEqual(1, hero1.GetCurrentExperience());
            hero1.SubtractExperience(49);
            Assert.AreEqual(-48, hero1.GetCurrentExperience());
            Assert.AreEqual(252, hero1.GetTotalExperience());
            Assert.AreEqual(3, hero1.GetLevel());
            hero1.AddExperience(48,linearLevels);
            //level should still be 3
            Assert.AreEqual(3, hero1.GetLevel());

            //make experience positive again
            hero1.AddExperience(27,linearLevels);

            //Check where we are
            Assert.AreEqual(327, hero1.GetTotalExperience()); //Currently, total experience is 327,
            Assert.AreEqual(27, hero1.GetCurrentExperience());//           current experience is 27, 
            Assert.AreEqual(3, hero1.GetLevel());             //           and level is 3

            //and some calculations
            Assert.AreEqual(173, hero1.ExperienceDelta(5, linearLevels));
            Assert.AreEqual(73, hero1.ExperienceTillLevelUp(linearLevels));
            Assert.AreEqual(27, hero1.ProgressToNextLevel(linearLevels));
            Assert.AreEqual(100, hero1.GetLevelGap());
            Assert.AreEqual(53, hero1.ExperienceToLevel(5347, linearLevels));

            //test ResetExperience()

            hero1.ResetExperience();
            Assert.AreEqual(0, hero1.GetCurrentExperience(), 0);
            Assert.AreEqual(3, hero1.GetLevel(), 3);
                
            //test SetLevel()

            hero1.SetLevel(1, linearLevels);
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(100, hero1.GetTotalExperience());
            Assert.AreEqual(1, hero1.GetLevel());
            Assert.AreEqual(100, hero1.GetLevelGap());
        }

        [TestMethod]
        public void TestQuadraticSystem()
        {
            LevelModel quadrLevels = new LevelModel(100,2,UpgradeType.polynomial);
            ExperienceSystem hero1 = new ExperienceSystem(quadrLevels);

            //Check starting values
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(0, hero1.GetTotalExperience());
            Assert.AreEqual(0, hero1.GetLevel());
            Assert.AreEqual(100, hero1.GetLevelGap());

            //Add some xp
            hero1.AddExperience(20, quadrLevels);
            hero1.AddExperience(45, quadrLevels);
            hero1.AddExperience(13, quadrLevels);
            hero1.SubtractExperience(27);

            Assert.AreEqual(51, hero1.GetCurrentExperience());
            Assert.AreEqual(51, hero1.GetTotalExperience());
            Assert.AreEqual(0, hero1.GetLevel());
            Assert.AreEqual(100, hero1.GetLevelGap());

            //Add xp so we level-up 

            hero1.AddExperience(49, quadrLevels);
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(100, hero1.GetTotalExperience());
            Assert.AreEqual(1, hero1.GetLevel());
            Assert.AreEqual(300, hero1.GetLevelGap());

            //Add more xp so we level up two levels, then drop and go back up
            hero1.AddExperience(2005, quadrLevels);
            Assert.AreEqual(4, hero1.GetLevel());
            Assert.AreEqual(505, hero1.GetCurrentExperience());
            hero1.SubtractExperience(49);
            Assert.AreEqual(456, hero1.GetCurrentExperience());
            Assert.AreEqual(2056, hero1.GetTotalExperience());
            hero1.AddExperience(500, quadrLevels);
            Assert.AreEqual(5, hero1.GetLevel());
            Assert.AreEqual(56,hero1.GetCurrentExperience());
            hero1.SubtractExperience(156);
            Assert.AreEqual(5, hero1.GetLevel());
            Assert.AreEqual(1200, hero1.ExperienceTillLevelUp(quadrLevels));

            //make experience positive again
            hero1.AddExperience(1000, quadrLevels);

            //Check where we are
            Assert.AreEqual(3400, hero1.GetTotalExperience()); //Currently, total experience is 3400,
            Assert.AreEqual(900, hero1.GetCurrentExperience());//           current experience is 900, 
            Assert.AreEqual(5, hero1.GetLevel());             //           and level is 5

            //and some calculations
            Assert.AreEqual(1500, hero1.ExperienceDelta(7, quadrLevels));
            Assert.AreEqual(200, hero1.ExperienceTillLevelUp(quadrLevels));
            Assert.AreEqual(81, hero1.ProgressToNextLevel(quadrLevels),0.1);
            Assert.AreEqual(1100, hero1.GetLevelGap());

            Assert.AreEqual(9,hero1.ExperienceToLevel(9014, quadrLevels));
        

            //test ResetExperience()

            hero1.ResetExperience();
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(5, hero1.GetLevel());

            //test SetLevel()

            hero1.SetLevel(7, quadrLevels);
            Assert.AreEqual(hero1.GetCurrentExperience(), 0);
            Assert.AreEqual(hero1.GetTotalExperience(), 4900);
            Assert.AreEqual(hero1.GetLevel(), 7);
            Assert.AreEqual(hero1.GetLevelGap(), 1500);
        }

        [TestMethod]
        public void TestExponentialSystem()
        {
            LevelModel expLevels = new LevelModel(10, 2, UpgradeType.exponential);
            ExperienceSystem hero1 = new ExperienceSystem(expLevels);

            //Check starting values
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(0, hero1.GetTotalExperience());
            Assert.AreEqual(0, hero1.GetLevel());
            Assert.AreEqual(10, hero1.GetLevelGap());

            //Add some xp
            hero1.AddExperience(2, expLevels);
            hero1.AddExperience(4, expLevels);
            hero1.AddExperience(1, expLevels);
            hero1.SubtractExperience(3);

            Assert.AreEqual(4, hero1.GetCurrentExperience());
            Assert.AreEqual(4, hero1.GetTotalExperience());
            Assert.AreEqual(0, hero1.GetLevel());
            Assert.AreEqual(10, hero1.GetLevelGap());

            //Add xp so we level-up 

            hero1.AddExperience(6, expLevels);
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(10, hero1.GetTotalExperience());
            Assert.AreEqual(1, hero1.GetLevel());
           

            //Add more xp so we level up two levels, then drop and go back up
            hero1.AddExperience(200, expLevels);
            
            Assert.AreEqual(5, hero1.GetLevel());
            Assert.AreEqual(160, hero1.GetLevelGap());
            Assert.AreEqual(50, hero1.GetCurrentExperience());
            hero1.SubtractExperience(49);
            Assert.AreEqual(1, hero1.GetCurrentExperience());
            Assert.AreEqual(161, hero1.GetTotalExperience());
            hero1.AddExperience(500, expLevels);
            Assert.AreEqual(7, hero1.GetLevel());
            Assert.AreEqual(21, hero1.GetCurrentExperience());
            hero1.SubtractExperience(41);
            Assert.AreEqual(-20, hero1.GetCurrentExperience());
            Assert.AreEqual(7, hero1.GetLevel());
            Assert.AreEqual(660, hero1.ExperienceTillLevelUp(expLevels));

            //make experience positive again
            hero1.AddExperience(1000, expLevels);

            //Check where we are
            Assert.AreEqual(1620, hero1.GetTotalExperience()); //Currently, total experience is 1620,
            Assert.AreEqual(340, hero1.GetCurrentExperience());//           current experience is 340, 
            Assert.AreEqual(8, hero1.GetLevel());               //           and level is 

            //and some calculations
            Assert.AreEqual(3500, hero1.ExperienceDelta(10, expLevels));
            Assert.AreEqual(1610, hero1.ExperienceDelta(1, expLevels));
            Assert.AreEqual(940, hero1.ExperienceTillLevelUp(expLevels));
            Assert.AreEqual(26, hero1.ProgressToNextLevel(expLevels), 0.1);
            Assert.AreEqual(1280, hero1.GetLevelGap());

            Assert.AreEqual(6, hero1.ExperienceToLevel(400, expLevels));


            //test ResetExperience()

            hero1.ResetExperience();
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(8, hero1.GetLevel());
            Assert.AreEqual(1280, hero1.GetTotalExperience());

            //test SetLevel()

            hero1.SetLevel(2, expLevels);
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(20, hero1.GetTotalExperience());
            Assert.AreEqual(2, hero1.GetLevel());
            Assert.AreEqual(20, hero1.GetLevelGap());
        }

        [TestMethod]
        public void TestManualSystem()
        {
            long[] arrLevels = new long[] { 0, 55, 120, 270, 390, 510, 790, 1200, 1680, 2090, 2800 };
            LevelModel manLevels = new LevelModel(arrLevels);
            ExperienceSystem hero1 = new ExperienceSystem(manLevels);

            //Check starting values
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(0, hero1.GetTotalExperience());
            Assert.AreEqual(0, hero1.GetLevel());
            Assert.AreEqual(55, hero1.GetLevelGap());

            //Add some xp
            hero1.AddExperience(25, manLevels);
            hero1.AddExperience(12, manLevels);
            hero1.AddExperience(7, manLevels);
            hero1.SubtractExperience(14);

            Assert.AreEqual(30, hero1.GetCurrentExperience());
            Assert.AreEqual(30, hero1.GetTotalExperience());
            Assert.AreEqual(0, hero1.GetLevel());
            Assert.AreEqual(55, hero1.GetLevelGap());

            //Add xp so we level-up 

            hero1.AddExperience(25, manLevels);
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(55, hero1.GetTotalExperience());
            Assert.AreEqual(1, hero1.GetLevel());


            //Add more xp so we level up two levels, then drop and go back up
            hero1.AddExperience(500, manLevels);

            Assert.AreEqual(5, hero1.GetLevel());
            Assert.AreEqual(280, hero1.GetLevelGap());
            Assert.AreEqual(45, hero1.GetCurrentExperience());
            hero1.SubtractExperience(15);
            Assert.AreEqual(30, hero1.GetCurrentExperience());
            Assert.AreEqual(540, hero1.GetTotalExperience());
            hero1.AddExperience(1000, manLevels);
            Assert.AreEqual(7, hero1.GetLevel());
            Assert.AreEqual(340, hero1.GetCurrentExperience());
            hero1.SubtractExperience(400);
            Assert.AreEqual(-60, hero1.GetCurrentExperience());
            Assert.AreEqual(7, hero1.GetLevel());
            Assert.AreEqual(540, hero1.ExperienceTillLevelUp(manLevels));

            //make experience positive again
            hero1.AddExperience(1000, manLevels);

            //Check where we are
            Assert.AreEqual(2140, hero1.GetTotalExperience()); //Currently, total experience is 2140,
            Assert.AreEqual(50, hero1.GetCurrentExperience());//           current experience is 50, 
            Assert.AreEqual(9, hero1.GetLevel());               //           and level is 9 

            //and some calculations
            Assert.AreEqual(1350, hero1.ExperienceDelta(6, manLevels));
            Assert.AreEqual(2085, hero1.ExperienceDelta(1, manLevels));
            Assert.AreEqual(660, hero1.ExperienceTillLevelUp(manLevels));
            Assert.AreEqual(7, hero1.ProgressToNextLevel(manLevels), 0.1);
            Assert.AreEqual(710, hero1.GetLevelGap());

            Assert.AreEqual(7, hero1.ExperienceToLevel(1302, manLevels));


            //test ResetExperience()

            hero1.ResetExperience();
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(9, hero1.GetLevel());
            Assert.AreEqual(2090, hero1.GetTotalExperience());

            //test SetLevel()

            hero1.SetLevel(10, manLevels);
            Assert.AreEqual(0, hero1.GetCurrentExperience());
            Assert.AreEqual(2800, hero1.GetTotalExperience());
            Assert.AreEqual(10, hero1.GetLevel());
            Assert.AreEqual(long.MaxValue-2800, hero1.GetLevelGap());
        }

    }
}
