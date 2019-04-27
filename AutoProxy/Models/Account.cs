using System;
using System.Runtime.Serialization;

namespace AutoProxy.Models
{
    [DataContract(Name = "Entity", Namespace = "")]
    public class Account : Entity
    {
        [DataMember(Name = "id", Order = 1)]
        public long Id { get; set; }

        [DataMember(Name = "UserDefinedFields", Order = 2)]
        public UserDefinedField[] UserDefinedFields { get; set; }

        [DataMember(Name = "Address1", Order = 3)]
        public string Address1 { get; set; }

        [DataMember(Name = "AlternatePhone1", Order = 4)]
        public string AlternatePhone1 { get; set; }

        [DataMember(Name = "AlternatePhone2", Order = 5)]
        public string AlternatePhone2 { get; set; }

        [DataMember(Name = "City", Order = 6)]
        public string City { get; set; }

        [DataMember(Name = "Country", Order = 7)]
        public string Country { get; set; }

        [DataMember(Name = "CreateDate", Order = 8)]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "Fax", Order = 9)]
        public string Fax { get; set; }

        [DataMember(Name = "KeyAccountIcon", Order = 10)]
        public int KeyAccountIcon { get; set; }

        [DataMember(Name = "LastActivityDate", Order = 11)]
        public DateTime LastActivityDate { get; set; }

        [DataMember(Name = "MarketSegmentID", Order = 12)]
        public int MarketSegmentID { get; set; }

        [DataMember(Name = "AccountName", Order = 13)]
        public string AccountName { get; set; }

        [DataMember(Name = "AccountNumber", Order = 14)]
        public string AccountNumber { get; set; }

        [DataMember(Name = "OwnerResourceID", Order = 15)]
        public int OwnerResourceID { get; set; }

        [DataMember(Name = "ParentAccountID", Order = 16)]
        public int ParentAccountID { get; set; }

        [DataMember(Name = "Phone", Order = 17)]
        public string Phone { get; set; }

        [DataMember(Name = "PostalCode", Order = 18)]
        public string PostalCode { get; set; }

        [DataMember(Name = "SICCode", Order = 19)]
        public string SICCode { get; set; }

        [DataMember(Name = "State", Order = 20)]
        public string State { get; set; }

        [DataMember(Name = "StockMarket", Order = 21)]
        public string StockMarket { get; set; }

        [DataMember(Name = "StockSymbol", Order = 22)]
        public string StockSymbol { get; set; }

        [DataMember(Name = "TerritoryID", Order = 23)]
        public int TerritoryID { get; set; }

        [DataMember(Name = "AccountType", Order = 24)]
        public short AccountType { get; set; }

        [DataMember(Name = "WebAddress", Order = 25)]
        public string WebAddress { get; set; }

        [DataMember(Name = "Active", Order = 26)]
        public bool Active { get; set; }

        [DataMember(Name = "ClientPortalActive", Order = 27)]
        public bool ClientPortalActive { get; set; }

        [DataMember(Name = "TaskFireActive", Order = 28)]
        public bool TaskFireActive { get; set; }

        [DataMember(Name = "TaxExempt", Order = 29)]
        public bool TaxExempt { get; set; }

        [DataMember(Name = "TaxRegionID", Order = 30)]
        public int TaxRegionID { get; set; }

        [DataMember(Name = "TaxID", Order = 31)]
        public string TaxID { get; set; }

        [DataMember(Name = "AdditionalAddressInformation", Order = 32)]
        public string AdditionalAddressInformation { get; set; }

        [DataMember(Name = "CountryID", Order = 33)]
        public int CountryID { get; set; }

        [DataMember(Name = "BillToAddressToUse", Order = 34)]
        public int BillToAddressToUse { get; set; }

        [DataMember(Name = "BillToAttention", Order = 35)]
        public string BillToAttention { get; set; }

        [DataMember(Name = "BillToAddress1", Order = 36)]
        public string BillToAddress1 { get; set; }

        [DataMember(Name = "BillToAddress2", Order = 37)]
        public string BillToAddress2 { get; set; }

        [DataMember(Name = "BillToCity", Order = 38)]
        public string BillToCity { get; set; }

        [DataMember(Name = "BillToState", Order = 39)]
        public string BillToState { get; set; }

        [DataMember(Name = "BillToZipCode", Order = 40)]
        public string BillToZipCode { get; set; }

        [DataMember(Name = "BillToCountryID", Order = 41)]
        public int BillToCountryID { get; set; }

        [DataMember(Name = "BillToAdditionalAddressInformation", Order = 42)]
        public string BillToAdditionalAddressInformation { get; set; }

        [DataMember(Name = "InvoiceMethod", Order = 43)]
        public int InvoiceMethod { get; set; }

        [DataMember(Name = "InvoiceNonContractItemsToParentAccount", Order = 44)]
        public bool InvoiceNonContractItemsToParentAccount { get; set; }

        [DataMember(Name = "QuoteTemplateID", Order = 45)]
        public int QuoteTemplateID { get; set; }

        [DataMember(Name = "QuoteEmailMessageID", Order = 46)]
        public int QuoteEmailMessageID { get; set; }

        [DataMember(Name = "InvoiceTemplateID", Order = 47)]
        public int InvoiceTemplateID { get; set; }

        [DataMember(Name = "InvoiceEmailMessageID", Order = 48)]
        public int InvoiceEmailMessageID { get; set; }

        [DataMember(Name = "CurrencyID", Order = 49)]
        public int CurrencyID { get; set; }
    }
}
