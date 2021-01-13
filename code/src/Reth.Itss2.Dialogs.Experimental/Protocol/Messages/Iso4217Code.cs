// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Globalization;

using Reth.Itss2.Dialogs.Standard.Diagnostics;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages
{
    public class Iso4217Code:IComparable<Iso4217Code>, IEquatable<Iso4217Code>
    {
        public static readonly Iso4217Code AED = new Iso4217Code( "AED" );
        public static readonly Iso4217Code AFN = new Iso4217Code( "AFN" );
        public static readonly Iso4217Code ALL = new Iso4217Code( "ALL" );
        public static readonly Iso4217Code AMD = new Iso4217Code( "AMD" );
        public static readonly Iso4217Code ANG = new Iso4217Code( "ANG" );
        public static readonly Iso4217Code AOA = new Iso4217Code( "AOA" );
        public static readonly Iso4217Code ARS = new Iso4217Code( "ARS" );
        public static readonly Iso4217Code AUD = new Iso4217Code( "AUD" );
        public static readonly Iso4217Code AWG = new Iso4217Code( "AWG" );
        public static readonly Iso4217Code AZN = new Iso4217Code( "AZN" );
        public static readonly Iso4217Code BAM = new Iso4217Code( "BAM" );
        public static readonly Iso4217Code BBD = new Iso4217Code( "BBD" );
        public static readonly Iso4217Code BDT = new Iso4217Code( "BDT" );
        public static readonly Iso4217Code BGN = new Iso4217Code( "BGN" );
        public static readonly Iso4217Code BHD = new Iso4217Code( "BHD" );
        public static readonly Iso4217Code BIF = new Iso4217Code( "BIF" );
        public static readonly Iso4217Code BMD = new Iso4217Code( "BMD" );
        public static readonly Iso4217Code BND = new Iso4217Code( "BND" );
        public static readonly Iso4217Code BOB = new Iso4217Code( "BOB" );
        public static readonly Iso4217Code BOV = new Iso4217Code( "BOV" );
        public static readonly Iso4217Code BRL = new Iso4217Code( "BRL" );
        public static readonly Iso4217Code BSD = new Iso4217Code( "BSD" );
        public static readonly Iso4217Code BTN = new Iso4217Code( "BTN" );
        public static readonly Iso4217Code BWP = new Iso4217Code( "BWP" );
        public static readonly Iso4217Code BYR = new Iso4217Code( "BYR" );
        public static readonly Iso4217Code BZD = new Iso4217Code( "BZD" );
        public static readonly Iso4217Code CAD = new Iso4217Code( "CAD" );
        public static readonly Iso4217Code CDF = new Iso4217Code( "CDF" );
        public static readonly Iso4217Code CHE = new Iso4217Code( "CHE" );
        public static readonly Iso4217Code CHF = new Iso4217Code( "CHF" );
        public static readonly Iso4217Code CHW = new Iso4217Code( "CHW" );
        public static readonly Iso4217Code CLF = new Iso4217Code( "CLF" );
        public static readonly Iso4217Code CLP = new Iso4217Code( "CLP" );
        public static readonly Iso4217Code CNY = new Iso4217Code( "CNY" );
        public static readonly Iso4217Code COP = new Iso4217Code( "COP" );
        public static readonly Iso4217Code COU = new Iso4217Code( "COU" );
        public static readonly Iso4217Code CRC = new Iso4217Code( "CRC" );
        public static readonly Iso4217Code CUC = new Iso4217Code( "CUC" );
        public static readonly Iso4217Code CUP = new Iso4217Code( "CUP" );
        public static readonly Iso4217Code CVE = new Iso4217Code( "CVE" );
        public static readonly Iso4217Code CZK = new Iso4217Code( "CZK" );
        public static readonly Iso4217Code DJF = new Iso4217Code( "DJF" );
        public static readonly Iso4217Code DKK = new Iso4217Code( "DKK" );
        public static readonly Iso4217Code DOP = new Iso4217Code( "DOP" );
        public static readonly Iso4217Code DZD = new Iso4217Code( "DZD" );
        public static readonly Iso4217Code EGP = new Iso4217Code( "EGP" );
        public static readonly Iso4217Code ERN = new Iso4217Code( "ERN" );
        public static readonly Iso4217Code ETB = new Iso4217Code( "ETB" );
        public static readonly Iso4217Code EUR = new Iso4217Code( "EUR" );
        public static readonly Iso4217Code FJD = new Iso4217Code( "FJD" );
        public static readonly Iso4217Code FKP = new Iso4217Code( "FKP" );
        public static readonly Iso4217Code GBP = new Iso4217Code( "GBP" );
        public static readonly Iso4217Code GEL = new Iso4217Code( "GEL" );
        public static readonly Iso4217Code GHS = new Iso4217Code( "GHS" );
        public static readonly Iso4217Code GIP = new Iso4217Code( "GIP" );
        public static readonly Iso4217Code GMD = new Iso4217Code( "GMD" );
        public static readonly Iso4217Code GNF = new Iso4217Code( "GNF" );
        public static readonly Iso4217Code GTQ = new Iso4217Code( "GTQ" );
        public static readonly Iso4217Code GYD = new Iso4217Code( "GYD" );
        public static readonly Iso4217Code HKD = new Iso4217Code( "HKD" );
        public static readonly Iso4217Code HNL = new Iso4217Code( "HNL" );
        public static readonly Iso4217Code HRK = new Iso4217Code( "HRK" );
        public static readonly Iso4217Code HTG = new Iso4217Code( "HTG" );
        public static readonly Iso4217Code HUF = new Iso4217Code( "HUF" );
        public static readonly Iso4217Code IDR = new Iso4217Code( "IDR" );
        public static readonly Iso4217Code ILS = new Iso4217Code( "ILS" );
        public static readonly Iso4217Code INR = new Iso4217Code( "INR" );
        public static readonly Iso4217Code IQD = new Iso4217Code( "IQD" );
        public static readonly Iso4217Code IRR = new Iso4217Code( "IRR" );
        public static readonly Iso4217Code ISK = new Iso4217Code( "ISK" );
        public static readonly Iso4217Code JMD = new Iso4217Code( "JMD" );
        public static readonly Iso4217Code JOD = new Iso4217Code( "JOD" );
        public static readonly Iso4217Code JPY = new Iso4217Code( "JPY" );
        public static readonly Iso4217Code KES = new Iso4217Code( "KES" );
        public static readonly Iso4217Code KGS = new Iso4217Code( "KGS" );
        public static readonly Iso4217Code KHR = new Iso4217Code( "KHR" );
        public static readonly Iso4217Code KMF = new Iso4217Code( "KMF" );
        public static readonly Iso4217Code KPW = new Iso4217Code( "KPW" );
        public static readonly Iso4217Code KRW = new Iso4217Code( "KRW" );
        public static readonly Iso4217Code KWD = new Iso4217Code( "KWD" );
        public static readonly Iso4217Code KYD = new Iso4217Code( "KYD" );
        public static readonly Iso4217Code KZT = new Iso4217Code( "KZT" );
        public static readonly Iso4217Code LAK = new Iso4217Code( "LAK" );
        public static readonly Iso4217Code LBP = new Iso4217Code( "LBP" );
        public static readonly Iso4217Code LKR = new Iso4217Code( "LKR" );
        public static readonly Iso4217Code LRD = new Iso4217Code( "LRD" );
        public static readonly Iso4217Code LSL = new Iso4217Code( "LSL" );
        public static readonly Iso4217Code LTL = new Iso4217Code( "LTL" );
        public static readonly Iso4217Code LVL = new Iso4217Code( "LVL" );
        public static readonly Iso4217Code LYD = new Iso4217Code( "LYD" );
        public static readonly Iso4217Code MAD = new Iso4217Code( "MAD" );
        public static readonly Iso4217Code MDL = new Iso4217Code( "MDL" );
        public static readonly Iso4217Code MGA = new Iso4217Code( "MGA" );
        public static readonly Iso4217Code MKD = new Iso4217Code( "MKD" );
        public static readonly Iso4217Code MMK = new Iso4217Code( "MMK" );
        public static readonly Iso4217Code MNT = new Iso4217Code( "MNT" );
        public static readonly Iso4217Code MOP = new Iso4217Code( "MOP" );
        public static readonly Iso4217Code MRO = new Iso4217Code( "MRO" );
        public static readonly Iso4217Code MUR = new Iso4217Code( "MUR" );
        public static readonly Iso4217Code MVR = new Iso4217Code( "MVR" );
        public static readonly Iso4217Code MWK = new Iso4217Code( "MWK" );
        public static readonly Iso4217Code MXN = new Iso4217Code( "MXN" );
        public static readonly Iso4217Code MXV = new Iso4217Code( "MXV" );
        public static readonly Iso4217Code MYR = new Iso4217Code( "MYR" );
        public static readonly Iso4217Code MZN = new Iso4217Code( "MZN" );
        public static readonly Iso4217Code NAD = new Iso4217Code( "NAD" );
        public static readonly Iso4217Code NGN = new Iso4217Code( "NGN" );
        public static readonly Iso4217Code NIO = new Iso4217Code( "NIO" );
        public static readonly Iso4217Code NOK = new Iso4217Code( "NOK" );
        public static readonly Iso4217Code NPR = new Iso4217Code( "NPR" );
        public static readonly Iso4217Code NZD = new Iso4217Code( "NZD" );
        public static readonly Iso4217Code OMR = new Iso4217Code( "OMR" );
        public static readonly Iso4217Code PAB = new Iso4217Code( "PAB" );
        public static readonly Iso4217Code PEN = new Iso4217Code( "PEN" );
        public static readonly Iso4217Code PGK = new Iso4217Code( "PGK" );
        public static readonly Iso4217Code PHP = new Iso4217Code( "PHP" );
        public static readonly Iso4217Code PKR = new Iso4217Code( "PKR" );
        public static readonly Iso4217Code PLN = new Iso4217Code( "PLN" );
        public static readonly Iso4217Code PYG = new Iso4217Code( "PYG" );
        public static readonly Iso4217Code QAR = new Iso4217Code( "QAR" );
        public static readonly Iso4217Code RON = new Iso4217Code( "RON" );
        public static readonly Iso4217Code RSD = new Iso4217Code( "RSD" );
        public static readonly Iso4217Code RUB = new Iso4217Code( "RUB" );
        public static readonly Iso4217Code RWF = new Iso4217Code( "RWF" );
        public static readonly Iso4217Code SAR = new Iso4217Code( "SAR" );
        public static readonly Iso4217Code SBD = new Iso4217Code( "SBD" );
        public static readonly Iso4217Code SCR = new Iso4217Code( "SCR" );
        public static readonly Iso4217Code SDG = new Iso4217Code( "SDG" );
        public static readonly Iso4217Code SEK = new Iso4217Code( "SEK" );
        public static readonly Iso4217Code SGD = new Iso4217Code( "SGD" );
        public static readonly Iso4217Code SHP = new Iso4217Code( "SHP" );
        public static readonly Iso4217Code SLL = new Iso4217Code( "SLL" );
        public static readonly Iso4217Code SOS = new Iso4217Code( "SOS" );
        public static readonly Iso4217Code SRD = new Iso4217Code( "SRD" );
        public static readonly Iso4217Code SSP = new Iso4217Code( "SSP" );
        public static readonly Iso4217Code STD = new Iso4217Code( "STD" );
        public static readonly Iso4217Code SYP = new Iso4217Code( "SYP" );
        public static readonly Iso4217Code SZL = new Iso4217Code( "SZL" );
        public static readonly Iso4217Code THB = new Iso4217Code( "THB" );
        public static readonly Iso4217Code TJS = new Iso4217Code( "TJS" );
        public static readonly Iso4217Code TMT = new Iso4217Code( "TMT" );
        public static readonly Iso4217Code TND = new Iso4217Code( "TND" );
        public static readonly Iso4217Code TOP = new Iso4217Code( "TOP" );
        public static readonly Iso4217Code TRY = new Iso4217Code( "TRY" );
        public static readonly Iso4217Code TTD = new Iso4217Code( "TTD" );
        public static readonly Iso4217Code TWD = new Iso4217Code( "TWD" );
        public static readonly Iso4217Code TZS = new Iso4217Code( "TZS" );
        public static readonly Iso4217Code UAH = new Iso4217Code( "UAH" );
        public static readonly Iso4217Code UGX = new Iso4217Code( "UGX" );
        public static readonly Iso4217Code USD = new Iso4217Code( "USD" );
        public static readonly Iso4217Code USN = new Iso4217Code( "USN" );
        public static readonly Iso4217Code USS = new Iso4217Code( "USS" );
        public static readonly Iso4217Code UYI = new Iso4217Code( "UYI" );
        public static readonly Iso4217Code UYU = new Iso4217Code( "UYU" );
        public static readonly Iso4217Code UZS = new Iso4217Code( "UZS" );
        public static readonly Iso4217Code VEF = new Iso4217Code( "VEF" );
        public static readonly Iso4217Code VND = new Iso4217Code( "VND" );
        public static readonly Iso4217Code VUV = new Iso4217Code( "VUV" );
        public static readonly Iso4217Code WST = new Iso4217Code( "WST" );
        public static readonly Iso4217Code XAF = new Iso4217Code( "XAF" );
        public static readonly Iso4217Code XCD = new Iso4217Code( "XCD" );
        public static readonly Iso4217Code XOF = new Iso4217Code( "XOF" );
        public static readonly Iso4217Code XPF = new Iso4217Code( "XPF" );
        public static readonly Iso4217Code YER = new Iso4217Code( "YER" );
        public static readonly Iso4217Code ZAR = new Iso4217Code( "ZAR" );
        public static readonly Iso4217Code ZMW = new Iso4217Code( "ZMW" );

        private static Dictionary<String, Iso4217Code> Codes
        {
            get;
        } = new Dictionary<String, Iso4217Code>();

        static Iso4217Code()
        {
            Iso4217Code.Codes.Add( "AED", Iso4217Code.AED );
            Iso4217Code.Codes.Add( "AFN", Iso4217Code.AFN );
            Iso4217Code.Codes.Add( "ALL", Iso4217Code.ALL );
            Iso4217Code.Codes.Add( "AMD", Iso4217Code.AMD );
            Iso4217Code.Codes.Add( "ANG", Iso4217Code.ANG );
            Iso4217Code.Codes.Add( "AOA", Iso4217Code.AOA );
            Iso4217Code.Codes.Add( "ARS", Iso4217Code.ARS );
            Iso4217Code.Codes.Add( "AUD", Iso4217Code.AUD );
            Iso4217Code.Codes.Add( "AWG", Iso4217Code.AWG );
            Iso4217Code.Codes.Add( "AZN", Iso4217Code.AZN );
            Iso4217Code.Codes.Add( "BAM", Iso4217Code.BAM );
            Iso4217Code.Codes.Add( "BBD", Iso4217Code.BBD );
            Iso4217Code.Codes.Add( "BDT", Iso4217Code.BDT );
            Iso4217Code.Codes.Add( "BGN", Iso4217Code.BGN );
            Iso4217Code.Codes.Add( "BHD", Iso4217Code.BHD );
            Iso4217Code.Codes.Add( "BIF", Iso4217Code.BIF );
            Iso4217Code.Codes.Add( "BMD", Iso4217Code.BMD );
            Iso4217Code.Codes.Add( "BND", Iso4217Code.BND );
            Iso4217Code.Codes.Add( "BOB", Iso4217Code.BOB );
            Iso4217Code.Codes.Add( "BOV", Iso4217Code.BOV );
            Iso4217Code.Codes.Add( "BRL", Iso4217Code.BRL );
            Iso4217Code.Codes.Add( "BSD", Iso4217Code.BSD );
            Iso4217Code.Codes.Add( "BTN", Iso4217Code.BTN );
            Iso4217Code.Codes.Add( "BWP", Iso4217Code.BWP );
            Iso4217Code.Codes.Add( "BYR", Iso4217Code.BYR );
            Iso4217Code.Codes.Add( "BZD", Iso4217Code.BZD );
            Iso4217Code.Codes.Add( "CAD", Iso4217Code.CAD );
            Iso4217Code.Codes.Add( "CDF", Iso4217Code.CDF );
            Iso4217Code.Codes.Add( "CHE", Iso4217Code.CHE );
            Iso4217Code.Codes.Add( "CHF", Iso4217Code.CHF );
            Iso4217Code.Codes.Add( "CHW", Iso4217Code.CHW );
            Iso4217Code.Codes.Add( "CLF", Iso4217Code.CLF );
            Iso4217Code.Codes.Add( "CLP", Iso4217Code.CLP );
            Iso4217Code.Codes.Add( "CNY", Iso4217Code.CNY );
            Iso4217Code.Codes.Add( "COP", Iso4217Code.COP );
            Iso4217Code.Codes.Add( "COU", Iso4217Code.COU );
            Iso4217Code.Codes.Add( "CRC", Iso4217Code.CRC );
            Iso4217Code.Codes.Add( "CUC", Iso4217Code.CUC );
            Iso4217Code.Codes.Add( "CUP", Iso4217Code.CUP );
            Iso4217Code.Codes.Add( "CVE", Iso4217Code.CVE );
            Iso4217Code.Codes.Add( "CZK", Iso4217Code.CZK );
            Iso4217Code.Codes.Add( "DJF", Iso4217Code.DJF );
            Iso4217Code.Codes.Add( "DKK", Iso4217Code.DKK );
            Iso4217Code.Codes.Add( "DOP", Iso4217Code.DOP );
            Iso4217Code.Codes.Add( "DZD", Iso4217Code.DZD );
            Iso4217Code.Codes.Add( "EGP", Iso4217Code.EGP );
            Iso4217Code.Codes.Add( "ERN", Iso4217Code.ERN );
            Iso4217Code.Codes.Add( "ETB", Iso4217Code.ETB );
            Iso4217Code.Codes.Add( "EUR", Iso4217Code.EUR );
            Iso4217Code.Codes.Add( "FJD", Iso4217Code.FJD );
            Iso4217Code.Codes.Add( "FKP", Iso4217Code.FKP );
            Iso4217Code.Codes.Add( "GBP", Iso4217Code.GBP );
            Iso4217Code.Codes.Add( "GEL", Iso4217Code.GEL );
            Iso4217Code.Codes.Add( "GHS", Iso4217Code.GHS );
            Iso4217Code.Codes.Add( "GIP", Iso4217Code.GIP );
            Iso4217Code.Codes.Add( "GMD", Iso4217Code.GMD );
            Iso4217Code.Codes.Add( "GNF", Iso4217Code.GNF );
            Iso4217Code.Codes.Add( "GTQ", Iso4217Code.GTQ );
            Iso4217Code.Codes.Add( "GYD", Iso4217Code.GYD );
            Iso4217Code.Codes.Add( "HKD", Iso4217Code.HKD );
            Iso4217Code.Codes.Add( "HNL", Iso4217Code.HNL );
            Iso4217Code.Codes.Add( "HRK", Iso4217Code.HRK );
            Iso4217Code.Codes.Add( "HTG", Iso4217Code.HTG );
            Iso4217Code.Codes.Add( "HUF", Iso4217Code.HUF );
            Iso4217Code.Codes.Add( "IDR", Iso4217Code.IDR );
            Iso4217Code.Codes.Add( "ILS", Iso4217Code.ILS );
            Iso4217Code.Codes.Add( "INR", Iso4217Code.INR );
            Iso4217Code.Codes.Add( "IQD", Iso4217Code.IQD );
            Iso4217Code.Codes.Add( "IRR", Iso4217Code.IRR );
            Iso4217Code.Codes.Add( "ISK", Iso4217Code.ISK );
            Iso4217Code.Codes.Add( "JMD", Iso4217Code.JMD );
            Iso4217Code.Codes.Add( "JOD", Iso4217Code.JOD );
            Iso4217Code.Codes.Add( "JPY", Iso4217Code.JPY );
            Iso4217Code.Codes.Add( "KES", Iso4217Code.KES );
            Iso4217Code.Codes.Add( "KGS", Iso4217Code.KGS );
            Iso4217Code.Codes.Add( "KHR", Iso4217Code.KHR );
            Iso4217Code.Codes.Add( "KMF", Iso4217Code.KMF );
            Iso4217Code.Codes.Add( "KPW", Iso4217Code.KPW );
            Iso4217Code.Codes.Add( "KRW", Iso4217Code.KRW );
            Iso4217Code.Codes.Add( "KWD", Iso4217Code.KWD );
            Iso4217Code.Codes.Add( "KYD", Iso4217Code.KYD );
            Iso4217Code.Codes.Add( "KZT", Iso4217Code.KZT );
            Iso4217Code.Codes.Add( "LAK", Iso4217Code.LAK );
            Iso4217Code.Codes.Add( "LBP", Iso4217Code.LBP );
            Iso4217Code.Codes.Add( "LKR", Iso4217Code.LKR );
            Iso4217Code.Codes.Add( "LRD", Iso4217Code.LRD );
            Iso4217Code.Codes.Add( "LSL", Iso4217Code.LSL );
            Iso4217Code.Codes.Add( "LTL", Iso4217Code.LTL );
            Iso4217Code.Codes.Add( "LVL", Iso4217Code.LVL );
            Iso4217Code.Codes.Add( "LYD", Iso4217Code.LYD );
            Iso4217Code.Codes.Add( "MAD", Iso4217Code.MAD );
            Iso4217Code.Codes.Add( "MDL", Iso4217Code.MDL );
            Iso4217Code.Codes.Add( "MGA", Iso4217Code.MGA );
            Iso4217Code.Codes.Add( "MKD", Iso4217Code.MKD );
            Iso4217Code.Codes.Add( "MMK", Iso4217Code.MMK );
            Iso4217Code.Codes.Add( "MNT", Iso4217Code.MNT );
            Iso4217Code.Codes.Add( "MOP", Iso4217Code.MOP );
            Iso4217Code.Codes.Add( "MRO", Iso4217Code.MRO );
            Iso4217Code.Codes.Add( "MUR", Iso4217Code.MUR );
            Iso4217Code.Codes.Add( "MVR", Iso4217Code.MVR );
            Iso4217Code.Codes.Add( "MWK", Iso4217Code.MWK );
            Iso4217Code.Codes.Add( "MXN", Iso4217Code.MXN );
            Iso4217Code.Codes.Add( "MXV", Iso4217Code.MXV );
            Iso4217Code.Codes.Add( "MYR", Iso4217Code.MYR );
            Iso4217Code.Codes.Add( "MZN", Iso4217Code.MZN );
            Iso4217Code.Codes.Add( "NAD", Iso4217Code.NAD );
            Iso4217Code.Codes.Add( "NGN", Iso4217Code.NGN );
            Iso4217Code.Codes.Add( "NIO", Iso4217Code.NIO );
            Iso4217Code.Codes.Add( "NOK", Iso4217Code.NOK );
            Iso4217Code.Codes.Add( "NPR", Iso4217Code.NPR );
            Iso4217Code.Codes.Add( "NZD", Iso4217Code.NZD );
            Iso4217Code.Codes.Add( "OMR", Iso4217Code.OMR );
            Iso4217Code.Codes.Add( "PAB", Iso4217Code.PAB );
            Iso4217Code.Codes.Add( "PEN", Iso4217Code.PEN );
            Iso4217Code.Codes.Add( "PGK", Iso4217Code.PGK );
            Iso4217Code.Codes.Add( "PHP", Iso4217Code.PHP );
            Iso4217Code.Codes.Add( "PKR", Iso4217Code.PKR );
            Iso4217Code.Codes.Add( "PLN", Iso4217Code.PLN );
            Iso4217Code.Codes.Add( "PYG", Iso4217Code.PYG );
            Iso4217Code.Codes.Add( "QAR", Iso4217Code.QAR );
            Iso4217Code.Codes.Add( "RON", Iso4217Code.RON );
            Iso4217Code.Codes.Add( "RSD", Iso4217Code.RSD );
            Iso4217Code.Codes.Add( "RUB", Iso4217Code.RUB );
            Iso4217Code.Codes.Add( "RWF", Iso4217Code.RWF );
            Iso4217Code.Codes.Add( "SAR", Iso4217Code.SAR );
            Iso4217Code.Codes.Add( "SBD", Iso4217Code.SBD );
            Iso4217Code.Codes.Add( "SCR", Iso4217Code.SCR );
            Iso4217Code.Codes.Add( "SDG", Iso4217Code.SDG );
            Iso4217Code.Codes.Add( "SEK", Iso4217Code.SEK );
            Iso4217Code.Codes.Add( "SGD", Iso4217Code.SGD );
            Iso4217Code.Codes.Add( "SHP", Iso4217Code.SHP );
            Iso4217Code.Codes.Add( "SLL", Iso4217Code.SLL );
            Iso4217Code.Codes.Add( "SOS", Iso4217Code.SOS );
            Iso4217Code.Codes.Add( "SRD", Iso4217Code.SRD );
            Iso4217Code.Codes.Add( "SSP", Iso4217Code.SSP );
            Iso4217Code.Codes.Add( "STD", Iso4217Code.STD );
            Iso4217Code.Codes.Add( "SYP", Iso4217Code.SYP );
            Iso4217Code.Codes.Add( "SZL", Iso4217Code.SZL );
            Iso4217Code.Codes.Add( "THB", Iso4217Code.THB );
            Iso4217Code.Codes.Add( "TJS", Iso4217Code.TJS );
            Iso4217Code.Codes.Add( "TMT", Iso4217Code.TMT );
            Iso4217Code.Codes.Add( "TND", Iso4217Code.TND );
            Iso4217Code.Codes.Add( "TOP", Iso4217Code.TOP );
            Iso4217Code.Codes.Add( "TRY", Iso4217Code.TRY );
            Iso4217Code.Codes.Add( "TTD", Iso4217Code.TTD );
            Iso4217Code.Codes.Add( "TWD", Iso4217Code.TWD );
            Iso4217Code.Codes.Add( "TZS", Iso4217Code.TZS );
            Iso4217Code.Codes.Add( "UAH", Iso4217Code.UAH );
            Iso4217Code.Codes.Add( "UGX", Iso4217Code.UGX );
            Iso4217Code.Codes.Add( "USD", Iso4217Code.USD );
            Iso4217Code.Codes.Add( "USN", Iso4217Code.USN );
            Iso4217Code.Codes.Add( "USS", Iso4217Code.USS );
            Iso4217Code.Codes.Add( "UYI", Iso4217Code.UYI );
            Iso4217Code.Codes.Add( "UYU", Iso4217Code.UYU );
            Iso4217Code.Codes.Add( "UZS", Iso4217Code.UZS );
            Iso4217Code.Codes.Add( "VEF", Iso4217Code.VEF );
            Iso4217Code.Codes.Add( "VND", Iso4217Code.VND );
            Iso4217Code.Codes.Add( "VUV", Iso4217Code.VUV );
            Iso4217Code.Codes.Add( "WST", Iso4217Code.WST );
            Iso4217Code.Codes.Add( "XAF", Iso4217Code.XAF );
            Iso4217Code.Codes.Add( "XCD", Iso4217Code.XCD );
            Iso4217Code.Codes.Add( "XOF", Iso4217Code.XOF );
            Iso4217Code.Codes.Add( "XPF", Iso4217Code.XPF );
            Iso4217Code.Codes.Add( "YER", Iso4217Code.YER );
            Iso4217Code.Codes.Add( "ZAR", Iso4217Code.ZAR );
            Iso4217Code.Codes.Add( "ZMW", Iso4217Code.ZMW );
        }

        public static Iso4217Code Parse( String value )
        {
            try
            {
                return Iso4217Code.Codes[ value ];
            }catch( Exception ex )
            {
                throw Assert.Exception( new ArgumentException( $"The value '{ value }' is not a valid currency code.", ex ) );
            }
        }

        public static bool TryParse( String value, out Iso4217Code? result )
        {
            result = default( Iso4217Code );

            bool success = false;
            
            if( value is not null )
            {
                success = Iso4217Code.Codes.TryGetValue( value, out result );
            }

            return success;
        }

        public static bool operator==( Iso4217Code? left, Iso4217Code? right )
		{
			return Iso4217Code.Equals( left, right );
		}
		
		public static bool operator!=( Iso4217Code? left, Iso4217Code? right )
		{
			return !( Iso4217Code.Equals( left, right ) );
		}

        public static bool operator<( Iso4217Code? left, Iso4217Code? right )
		{
			return ( Iso4217Code.Compare( left, right ) < 0 );
		}
		
		public static bool operator<=( Iso4217Code? left, Iso4217Code? right )
		{
			return ( Iso4217Code.Compare( left, right ) <= 0 );
		}
		
		public static bool operator>( Iso4217Code? left, Iso4217Code? right )
		{
            return ( Iso4217Code.Compare( left, right ) > 0 );
		}
		
		public static bool operator>=( Iso4217Code? left, Iso4217Code? right )
		{
			return ( Iso4217Code.Compare( left, right ) >= 0 );
		}

        public static bool Equals( Iso4217Code? left, Iso4217Code? right )
		{
            return String.Equals( left?.Value, right?.Value, StringComparison.OrdinalIgnoreCase );
		}

        public static int Compare( Iso4217Code? left, Iso4217Code? right )
        {
            return String.CompareOrdinal( left?.Value, right?.Value );
        }

        public static IEnumerable<Iso4217Code> GetCodes()
        {
            return Iso4217Code.Codes.Values;
        }

        private Iso4217Code( String value )
        {
            this.Value = value;
        }

        public String Value
        {
            get;
        }
        
        public override bool Equals( Object obj )
		{
			return this.Equals( obj as Iso4217Code );
		}
		
		public bool Equals( Iso4217Code? other )
		{
            return Iso4217Code.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public int CompareTo( Iso4217Code other )
		{
            return Iso4217Code.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value.ToString( CultureInfo.InvariantCulture );
        }
    }
}
