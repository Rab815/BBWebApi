using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace BloombergWebAPICore
{
    public enum BloombergRequestServiceTypes
    {
        HistoricalDataRequest = 0,
        IntraDayTickRequest,
        IntraDayBarRequest,
        ReferenceDataRequest,
        PortfolioDataRequest,
        BeqsReqest
    }

    public enum BloombergResponseServiceTypes
    {
        HistoricalDataResponse = 0,
        IntraDayTickResponse,
        IntraDayBarResponse,
        ReferenceDataResponse,
        PortfolioDataResponse,
        BeqsReqest
    }

    public enum GoldKey
    {
        [Display(Name = "None")]
        NONE,
        [Display(Name = "Law")]
        LAW,
        [Display(Name = "Govt")]
        GOVT,
        [Display(Name = "Corp")]
        CORP,
        [Display(Name = "Mtge")]
        MTGE,
        [Display(Name = "M-mkt")]
        MMKT,
        [Display(Name = "Muni")]
        MUNI,
        [Display(Name = "Pfd")]
        PFD,
        [Display(Name = "Equity")]
        EQUITY,
        [Display(Name = "Cmdty")]
        CMDTY,
        [Display(Name = "Index")]
        INDEX,
        [Display(Name = "Crncy")]
        CRNCY,
        [Display(Name = "Alpha")]
        ALPHA
    }

    public enum IdentifierType
    {
        NONE,
        MODEL,
        CUSIP,
        ISIN,
        TICKER
    }


    public static class EnumLookup
    {
        public static readonly Dictionary<Enum, string> IdentifierTypeName = new Dictionary<Enum, string>()
        {
            {IdentifierType.NONE, "NONE"},
            {IdentifierType.MODEL, "MODEL"},
            {IdentifierType.CUSIP, "CUSIP"},
            {IdentifierType.ISIN, "ISIN"},
            {IdentifierType.TICKER, "TICKER"},
        };

        public static readonly Dictionary<Enum, string> GoldKeyName = new Dictionary<Enum, string>()
        {
            {GoldKey.NONE, "None"},
            {GoldKey.LAW, "Law"},
            {GoldKey.GOVT, "Govt"},
            {GoldKey.CORP, "Corp"},
            {GoldKey.MTGE, "Mtge"},
            {GoldKey.MMKT, "M-Mkt"},
            {GoldKey.MUNI, "Muni"},
            {GoldKey.PFD, "Pfd"},
            {GoldKey.EQUITY, "Equity"},
            {GoldKey.CMDTY, "Cmdty"},
            {GoldKey.INDEX, "Index"},
            {GoldKey.CRNCY, "Crncy"},
            {GoldKey.ALPHA, "Alpha"},
        };
    }
}
