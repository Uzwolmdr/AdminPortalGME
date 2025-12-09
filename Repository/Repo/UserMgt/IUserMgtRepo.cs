using Repository.Entities;
using Repository.Entities.Account;
using Repository.Entities.PurseMgt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo.UserMgt
{
    public interface IUserMgtRepo
    {
        DBResponse CreateUser(UserMgtModel model);
        DBResponse UserEnableDisable(UserEDRequest model);
        RoleList GetAllRoleList(RoleRequest roleRequest);
        LoginResponseCore DoLogin(RequestCore request);
        DropDownResponseWrapper GetDropDownInfo(DropDownRequest request);
        UserEDResponseWrapper GetAllUserList(UserEDRequest model);
         ProductResponseWrapper GetAllProductList();
        DBResponse EnableDisableProduct(EnableDisableProductRequest model);
        ProductResponseWrapper GetAllPendingProductList(EnableDisableProductRequest model);
        DBResponse ApproveRejectProductSetup(EnableDisableProductRequest model);
        UserEditResponse GetUserDetailsById(UserEDRequest request);
        DBResponse ChangePassword(ChangePasswordModel request);
        DBResponse EditUser(UserEditRequest request);
        DBResponse ResetPassword(ResetPasswordModel request);
        RoleList GetOwnOfficeRoleList(RoleRequest model);
        DBResponse UnLockUser(ResetPasswordModel request);
        DBResponse CreateOrganisation(OrganisationModel model);
        OperatorTextCodeWrapper GetOperatorTextCode(OperatorTextCodeRequest request);
        AllOrgPurseBlncWrapper GetAllClientList();
        EditOrganisationModelWrapper GetOrganisationById(PurseBalanceRequest request);
    }
}
