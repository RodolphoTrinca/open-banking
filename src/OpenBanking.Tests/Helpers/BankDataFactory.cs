using MongoDB.Bson;
using OpenBanking.Application.Entity;
using System;
using System.Collections.Generic;
namespace OpenBanking.Tests.Helpers
{
    public class BankDataFactory
    {
        private Guid _id;
        private string _name;
        private string _status;
        private List<AutorizationServer> _autorizationServers;

        public BankDataFactory()
        {
            var autorizationServer = new AutorizationServerFactory().Build();

            _autorizationServers = new List<AutorizationServer>()
            {
                autorizationServer
            };

            _id = Guid.NewGuid();
        }

        public BankDataFactory WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public BankDataFactory WithName(string name)
        {
            _name = name;
            return this;
        }

        public BankDataFactory WithStatus(string status)
        {
            _status = status;
            return this;
        }

        public BankDataFactory WithAutorizationServers(List<AutorizationServer> autorizationServers)
        {
            _autorizationServers = autorizationServers;
            return this;
        }

        public BankData Build()
        {
            return new BankData()
            {
                Id = _id,
                Name = _name,
                Status = _status,
                AutorizationServers = _autorizationServers
            };
        }
    }
}
