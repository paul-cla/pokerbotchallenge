using System;
using BotWars.Models;
using BotWars.Services;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BotManagerTests
    {
        [Test]
        public void should_manage_a_bot()
        {
            var bot = new Bot("Bot", 1, 1);
            var botmanager = new BotManager();
            botmanager.AddBot(bot);

            Assert.That(botmanager.Move(), Is.EqualTo("CALL"));
        } 
        
        [Test]
        public void cshould_throw_exception_if_no_bot_created_before_calling_update()
        {
            var botmanager = new BotManager();
            const string command = "CARD";
            const string data = "A";
            
            var ex = Assert.Throws<BotNotCreatedException>(() =>  botmanager.Update(command, data));
            Assert.That(ex.Message, Is.EqualTo("You've not setup the bot"));
        }
        
        [Test]
        public void should_bet_max_if_ace()
        {
            var bot = new Bot("Bot", 10, 1);
            var botmanager = new BotManager();
            botmanager.AddBot(bot);

            const string command = "CARD";
            const string data = "A";

            botmanager.Update(command, data);

           Assert.That(botmanager.Move(), Is.EqualTo("BET:10"));
           Assert.That(botmanager.Bot.ChipCount, Is.EqualTo(0));
        }

        [TestCase("2", "FOLD", 10)]
        [TestCase("3", "FOLD", 10)]
        [TestCase("4", "FOLD", 10)]
        [TestCase("5", "FOLD", 10)]
        [TestCase("6", "BET", 9)]
        [TestCase("7", "CALL", 10)]
        [TestCase("8", "CALL", 10)]
        [TestCase("9", "CALL", 10)]
        [TestCase("10", "CALL", 10)]
        [TestCase("J", "BET:2", 8)]
        [TestCase("Q", "BET:3", 7)]
        [TestCase("K", "BET:5", 5)]
        [TestCase("A", "BET:10", 0)]
        public void should_bet(string card, string move, int chipCount)
        {
            var bot = new Bot("Bot", 10, 1);
            var botmanager = new BotManager();
            botmanager.AddBot(bot);

            const string command = "CARD";
            string data = card;

            botmanager.Update(command, data);

           Assert.That(botmanager.Move(), Is.EqualTo(move));
           Assert.That(botmanager.Bot.ChipCount, Is.EqualTo(chipCount));
        } 
        
        [Test]
        public void should_bet_max_twice_with_two_aces_and_a_chip_win()
        {
            var bot = new Bot("Bot", 10, 1);
            var botmanager = new BotManager();
            botmanager.AddBot(bot);

            Assert.That(botmanager.Bot.ChipCount, Is.EqualTo(10));
            var command = "CARD";
            var data = "A";
            botmanager.Update(command, data);
            Assert.That(botmanager.Bot.ChipCount, Is.EqualTo(0));

            command = "RECEIVE_CHIPS";
            data = "20";
            botmanager.Update(command, data);
            Assert.That(botmanager.Bot.ChipCount, Is.EqualTo(20));
            
            command = "CARD";
            data = "A";
            botmanager.Update(command, data);
            Assert.That(botmanager.Bot.ChipCount, Is.EqualTo(0));
            Assert.That(botmanager.Move(), Is.EqualTo("BET:20"));
        } 
        
        [Test]
        public void should_increase_chip_count_if_recieved()
        {
            var bot = new Bot("Bot", 10, 1);
            var botmanager = new BotManager();
            botmanager.AddBot(bot);

            const string command = "RECEIVE_CHIPS";
            const string data = "10";

            botmanager.Update(command, data);

            Assert.That(botmanager.Bot.ChipCount, Is.EqualTo(20));
        }  
        
        [Test]
        public void should_subtract_a_chip_if_blind()
        {
            var bot = new Bot("Bot", 10, 1);
            var botmanager = new BotManager();
            botmanager.AddBot(bot);

            const string command = "POST_BLIND";
            string data = string.Empty;

            botmanager.Update(command, data);

            Assert.That(botmanager.Bot.ChipCount, Is.EqualTo(9));
        }
    }
}
