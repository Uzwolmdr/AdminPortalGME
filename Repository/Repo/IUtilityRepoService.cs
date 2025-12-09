using System.Collections.Generic;
using Repository.Entities;
using Repository.Entities.Account;
using Repository.Entities.Deno;
using Repository.Entities.Utility;

namespace Repository.Repo
{
    public interface IUtilityRepoService
    {
        CoreResponse NEARequestLog(NEAServiceRequest request);
        CoreResponse DishhomeRequestLog(DishHomeCoreRequest request);
        ApiResponse GetDenos();
        CoreResponse Reconcile(ReconcileRequestCore request);
        DbLogResponse InsertGatewayProductPaymentLog(ProductInsertLogRequest productInsertLogRequest);
        CoreResponse MultipleReconcile(ReconcileMultipleRequest request);
        CoreResponse CancelDomesticRemittance(Entities.LoadWallet.CancelDomesticRemittance request);
        CoreResponse InsertGatewayRemittancePaymentLog(Entities.LoadWallet.SendDomesticRemittanceRequestModel request);
        Entities.LoadWallet.SendMoneyEditWrapper GetDomesticRemittanceDetails(string senderMobileNo, string tranId);
        DbLogResponse UpdateDomesticRemittanceDetails(Entities.LoadWallet.EditRemitTransaction request);
        StatusResponse GetDomesticRemitTransactionStatus(StatusRequest request);
    }
}
