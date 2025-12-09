using Repository.Entities.Account;
using Repository.Entities.PurseMgt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo.PurseMgt
{
    public interface IPurseMgtRepo
    {
        DBResponse DoPurseTransaction(PurseMgtRequestModel requsetModel) ;
        DBResponse AddWithdrawPurseMoney(PurseMgtRequestModel request);
       PurseDrCrResponseWrapper GetPendingAddWithdrawPurseMoney(PurseMgtRequestModel requsetModel);
        DBResponse ApproveAddWithdrawPurseMoney(PurseMgtRequestModel request);
        DBResponse RejectAddWithdrawPurseMoney(PurseMgtRequestModel request);
        string CreateTransactionId();
        SoaWrapperResponse GetSoaDetails(SoaRequest request);
        PurseBalanceResponse GetPurseBalance(PurseBalanceRequest request);
        AllOrgPurseBlncWrapper GetAllOrgPurseBalance();
    }
}
