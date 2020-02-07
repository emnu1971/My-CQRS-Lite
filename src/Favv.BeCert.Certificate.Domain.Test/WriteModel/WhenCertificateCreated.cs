using CQRSlite.Events;
using CQRSlite.Tests.Extensions.TestHelpers;
using Favv.BeCert.Certificate.Domain.ReadModel.Events;
using Favv.BeCert.Certificate.Domain.WriteModel.Aggregates;
using Favv.BeCert.Certificate.Domain.WriteModel.Commands;
using Favv.BeCert.Certificate.Domain.WriteModel.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Favv.BeCert.Certificate.Domain.Test.WriteModel
{
    public class WhenCertificateCreated : Specification<CertificateItem, CertificateItemCommandHandlers, CreateCertificateItemCommand>
    {
        private Guid _id;

        protected override CertificateItemCommandHandlers BuildHandler()
        {
            return new CertificateItemCommandHandlers(Session);
        }

        protected override IEnumerable<IEvent> Given()
        {
            _id = Guid.NewGuid();
            return new List<IEvent>();
        }

        protected override CreateCertificateItemCommand When()
        {
            return new CreateCertificateItemCommand(_id,"ABC001","MEAT","Cow","Belgium");
        }

        [Then]
        public void Should_create_one_event()
        {
            Assert.Equal(1, PublishedEvents.Count);
        }

        [Then]
        public void Should_create_correct_event()
        {
            Assert.IsType<CertificateItemCreatedEvent>(PublishedEvents.First());
        }

        [Then]
        public void Should_save_enterprisenumber()
        {
            Assert.Equal("ABC001", ((CertificateItemCreatedEvent)PublishedEvents.First()).EnterpriseNumber);
        }

    }
}
