using System;
using System.Collections.Generic;

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
        NONE,
        LAW,
        GOVT,
        CORP,
        MTGE,
        MMKT,
        MUNI,
        PFD,
        EQUITY,
        CMDTY,
        INDEX,
        CRNCY,
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
            {GoldKey.CORP,"Corp"},
            {GoldKey.MTGE,"Mtge"},
            {GoldKey.MMKT,"M-Mkt"},
            {GoldKey.MUNI,"Muni"},
            {GoldKey.PFD,"Pfd"},
            {GoldKey.EQUITY,"Equity"},
            {GoldKey.CMDTY,"Cmdty"},
            {GoldKey.INDEX,"Index"},
            {GoldKey.CRNCY,"Crncy"},
            {GoldKey.ALPHA,"Alpha"},
        };
    }



}
