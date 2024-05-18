using MongoDB.Bson;
using OpenBanking.Application.Entity;
using System;
using System.Collections.Generic;
namespace OpenBanking.Tests.Helpers
{
    public class BankDataFactory
    {
        private ObjectId _id;
        private string _name;
        private string _logoURI;
        private string _configurarionURL;
        private string _discoveryAuthorization;

        public BankDataFactory WithId(ObjectId id)
        {
            _id = id;
            return this;
        }

        public BankDataFactory WithName(string name)
        {
            _name = name;
            return this;
        }

        public BankDataFactory WithLogoURI(string logoURI)
        {
            _logoURI = logoURI;
            return this;
        }

        public BankDataFactory WithConfigurationURL(string configurationURL)
        {
            _configurarionURL = configurationURL;
            return this;
        }

        public BankDataFactory WIthDiscoveryAuthorization(string discoveryAuthorization)
        {
            _discoveryAuthorization = discoveryAuthorization;
            return this;
        }

        public BankData CreateBankData()
        {
            return new BankData()
            {
                Id = _id,
                Name = _name,
                LogoURI = _logoURI,
                ConfigurarionURL = _configurarionURL,
                DiscoveryAuthorization = _discoveryAuthorization
            };
        }
    }
}
