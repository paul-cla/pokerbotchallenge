using BotWars.Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BotTests
    {
        [Test]
        public void should_create_a_bot()
        {
            const string name = "Name";
            const int startingChipCount = 300;
            const int handLimit = 100;

            var bot = new Bot(name, startingChipCount, handLimit);

            Assert.That(bot.Name, Is.EqualTo(name));
            Assert.That(bot.ChipCount, Is.EqualTo(300));
            Assert.That(bot.HandLimit, Is.EqualTo(100));
        }

        [Test]
        public void should_return_a_move()
        {
            const string name = "Name";
            const int startingChipCount = 300;
            const int handLimit = 100;

            var bot = new Bot(name, startingChipCount, handLimit);

            var move = bot.Move();

            Assert.That(move, Is.EqualTo(Moves.CALL));
        }
    }
}