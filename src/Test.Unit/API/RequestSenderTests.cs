using ApiWrapper.Implementation.RequestSender;
using NUnit.Framework;

namespace Test.Unit
{
    public class RequestSenderTests
    {
        [Test]
        public void Riot_Request_Sender_Builder_Creation_By_Builder_Correct_Type()
        {
            RequestSenderDirector director = new RequestSenderDirector();
            director.builder = new RiotRequestSenderBuilder();
            Assert.IsTrue(director.builder.GetType() == typeof(RiotRequestSenderBuilder));
        }

        [Test]
        public void Riot_Request_Creation_By_Builder_Correct_Type()
        {
            RequestSenderDirector director = new RequestSenderDirector();
            director.builder = new RiotRequestSenderBuilder();
            director.Construct();
            Assert.IsTrue(director.builder.GetRequestSender().GetType() == typeof(RiotRequestSender));
        }

        [Test]
        public void Riot_Request_Sender_Creation_By_Builder_Castable()
        {
            RequestSenderDirector director = new RequestSenderDirector();
            director.builder = new RiotRequestSenderBuilder();
            director.Construct();
            Assert.DoesNotThrow(() =>
            {
                RiotRequestSender sender = (RiotRequestSender) director.builder.GetRequestSender();
            });
        }
    }
}