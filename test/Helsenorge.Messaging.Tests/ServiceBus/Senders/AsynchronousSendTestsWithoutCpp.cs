using System;
using Helsenorge.Messaging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helsenorge.Messaging.Tests.ServiceBus.Senders
{
    [TestClass]
    public class AsynchronousSendTestsWithoutCpp : BaseTest
    {
        public const int CommunicationPartyWithoutCppHerId = 94867;

        [TestInitialize]
        public override void Setup()
        {
            SetupInternal(CommunicationPartyWithoutCppHerId);
        }
        
        private OutgoingMessage CreateMessageForCommunicationPartyWithoutCpp()
        {
            return new OutgoingMessage()
            {
                ToHerId = CommunicationPartyWithoutCppHerId,
                CpaId = Guid.Empty,
                Payload = GenericMessage,
                MessageFunction = "DIALOG_INNBYGGER_EKONTAKT",
                MessageId = Guid.NewGuid().ToString("D"),
                ScheduledSendTimeUtc = DateTime.Now,
                PersonalId = "12345"
            };
        }

        [TestMethod]
        public void Send_Asynchronous_CommunicationPartyWithoutCpp_OK()
        {
            var message = CreateMessageForCommunicationPartyWithoutCpp();

            RunAndHandleException(Client.SendAndContinueAsync(Logger, message));

            Assert.AreEqual(1, MockFactory.OtherParty.Asynchronous.Messages.Count);
        }
    }
}
