using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
    public class OperatorConstants
    {
        public const string CDMA = "CDMA";
        public const string NTC = "NTC";
        public const string SMART = "SMART";
        public const string NCELL = "NCELL";
    }
    public class ResponseCodeMessage
    {
        public const string OPERATOR_NOT_FOUND = "Operator not found";
        public const string PRODUCT_NOT_FOUND = "Product not found";
        public const string SMS_SETTING_NOT_FOUND = "SMS settings credentials not found";
        public const string SMS_SEND = "SMS Send Successfully";
        public const string SMS_GATEWAY_ERROR = "Error On SMS GateWay";
        public const string FAILED_INSERT_LOG = "Failed  to Insert  SMS request Log";
        public const string FAILED_EXCEPTION = "Exception Occured";
        public const string SUCCESS = "Operation Success";
        public const string FAILED = "Operation Failed";
        public const string FAILED_INVALID_THIRDPARTY_TXN = "Invalid Transaction/Reference ID";
        public const string SUSPICIOUS = "Suspicious Transaction ";
        public const string API_CONNECT_FAILED = "Api Connection Failed";
        
    }

    public class FlagCodeConstant
    {
        public const string INSERT_FLAG = "Insert";
        public const string UPDATE_FLAG = "Update";
        public const string OPERATOR_DETAIL_FLAG = "GetOperatorDetails";
        public const string DOTRAN_FLAG = "DoTran";
        public const string CANCEL_FLAG = "Cancel";

    }
    public class StatusConstant
    {
        public const int SUCCESS = 0;
        public const int FAILED = 1;
        public const int PENDING = 2;
        public const int CANCEL = 3;


    }
    public class ResponseCodeConstant
    {
        public const int SUCCESS = 100;
        public const int FAILED = 101;
        public const int EXCEPTION = 999;
        public const int SUSPICIOUS = 102;
        public const string RSND = "RSND";

    }
    public class ProductConstant
    {
        public const string PSTN = "PSTN";
        public const string DISHOME = "DISHOME";
        public const string NEA = "NEA";
        public const string SIMTV = "SIMTV";
        public const string VIANET = "VIANET";
        public const string TOPUP = "TOPUP";
        public const string WORLDLINK = "WORLDLINK";
        public const string LOAD_TO_WALLET= "LTOW";
        public const string DIYALO_KHANEPANI = "DIYALO";
        public const string H2O_KHANEPANI = "H2O";
        public const string HETAUDA_KHANEPANI = "HETAUDA";
        public const string HEARTSUN_KHANEPANI = "HEARTSUN";
        public const string THAIBA_KHANEPANI = "THAIBA";
        public const string NEPAL_KHANEPANI = "NEPALKHANEPANI";
        public const string CLASSIC_TECH = "CTECH";
        public const string SUBISU = "SUBISU";
        public const string CLEARTV = "SUBISUDTV";
        public const string WEBSURFER_INTERNET = "WEBSURFER";
        public const string PRABHUNET = "PRABHUNET";
        public const string MEROTV = "MEROTV";
        public const string PRABHUTV = "PRABHUTV";
        public const string SKYTV = "SKYTV";
        public const string TECHMIND = "TECHMIND";
        public const string PALSNET = "PALSNET";
        public const string LOOPINTERNET = "LOOP";
        public const string FIRSTLINK = "FIRSTLINK";
        public const string ARROWNET = "ARROWNET";
        public const string MANAKAMANA_TICKETING = "MANAKAMANA";
        public const string CHANDRAGIRI_TICKETING = "CHANDRAGIRI";
        public const string DOMESTIC_AIRLINES_TICKETING = "DOMAIRLINES";
        public const string DIS_HOME_FIBER_NET = "DISHFNET";
        public const string RELIANT_INTERNET = "RELIANT";
        public const string IME_LIFE_RENEWAL = "IMELIFER";
        public const string NET_TV = "NETTV";
        public const string SONY_LIV= "SONYLIV";





        public const string PRODUCT_TYPE_INTERNET = "INTERNET";
        public const string PRODUCT_TYPE_TV = "TV";
        public const string PRODUCT_TYPE_WATER = "WATER";
        public const string PRODUCT_TYPE_TICKETING = "TICKETING";
        public const string PRODUCT_TYPE_INSURANCE = "INSURANCE";
        public const string DOMESTIC_REMITTANCE = "RSND";
        public const string PRODUCT_TYPE_DOMESTIC_REMITTANCE = "DOMESTICREMIT";


    }

    public class PurseTxnConstant
    {
        public const string DEPOSIT = "0";
        public const string TXN = "1";
        public const string REVERSAL = "2";
        public const string WITHDRAW = "3";
        public const string  DEBIT = "D";
        public const string CREDIT = "C";
    }
  
}
