using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities.PurseMgt
{
    public class UserMgtModel
    {
        public string UserName { get; set; }
        public string OrganisationCode { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get;  set; }
    }
    public class UserEDRequest
    {
        public string UserName { get; set; }
        public string LoginUser { get; set; }
        public string Status { get; set; }
        public string ModifiedBy { get; set; }
        public string OrganisationCode { get; set; }
        public string LoginUserOrgCode { get; set; }
    }

    public class UserEDResponse
    {
        public string UserName { get; set; }
        public int Status { get; set; }
        public string  FullName { get; set; }
        public string CreatedBy { get;  set; }
        public string OrganisationCode { get; set; }
    }

    public class UserEDResponseWrapper
    {
        public List<UserEDResponse> UserList { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
    public class ProductResponse
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public  bool Status { get; set; }
        public string CreatedBy { get; set; }
        public string RowId { get; set; }
    }
    public class ProductResponseWrapper
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public List<ProductResponse> ProductList { get; set; }
    }
    public class EnableDisableProductRequest
    {
        public string UserName { get; set; }
        public string ProductId { get; set; }
        public bool Status { get; set; }
        public string  RowId { get; set; }
        public string  Flag  { get; set; }
        public string ProductName { get; set; }
    }
    public class RoleModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }

    }
    public class RoleList
    {
        public List<RoleModel> Roles { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
    public class RoleRequest {
        public  bool IsHoRole { get; set; }
        public bool IsOrgRole { get; set; }
        public string UserName { get; set; }
        public string OrganisationCode { get; set; }
    }

    public class UserEditResponse
    {
        public UserMgtModel UserData { get; set; }
        public List<RoleModel> RoleList { get; set; }
        public int ResponseCode { get; set; }
        public string  ResponseDescription { get; set; }
    }

    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string UserName { get; set; }
        public string OrganisationCode { get; set; }

    }

    public class UserEditRequest
    {
        public string UserName { get; set; }
        public string OrganisationCode { get; set; }
        public string FullName { get; set; }
        public  string  Role { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
    }

    public class ResetPasswordModel
    {
        public string NewPassword { get; set; }
        public string UserName { get; set; }
        public string CreatedBy { get; set; }
        public string OrganisationCode { get; set; }
    }

    public class OrganisationModel
    {
        
        public string Flag { get; set; }
        public string UserName { get; set; }
        public string OrganisationCode { get; set; }
        public string Password { get; set; }
        public string OrganisationName { get; set; }
        public string ClientType { get; set; }

        public int   Status { get; set; }
        public string WalletId { get; set; }
        public string NtcTextCode { get; set; }
        public string NtcUserName { get; set; }
        public string NtcPassword { get; set; }
        public string NtcSmsId { get; set; }
        public string NcellTextCode { get; set; }
        public string NcellUserName { get; set; }
        public string NcellPassword { get; set; }
        public string NcellSmsId { get; set; }
        public string SmartCellTextCode { get; set; }
        public string SmartCellUserName { get; set; }
        public string SmartCellPassword { get; set; }
        public string SmartCellSmsId { get; set; }
        public string CreatedBy { get; set; }

    }

    public class OperatorTextCodeModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TextCode { get; set; }
        public string SmsId { get; set; }
    }

    public class OperatorTextCodeWrapper
    {
        public List<OperatorTextCodeModel> NtcOperator { get; set; }
        public List<OperatorTextCodeModel> NcellOperator { get; set; }
        public List<OperatorTextCodeModel> SmartcellOperator { get; set; }
        
    }

    public class OperatorTextCodeRequest
    {
        public string Flag { get; set; }
        public string UserName { get; set; }

    }

    public class EditOrganisationModelWrapper
    {
        public OrganisationModel OrganisationData { get; set; }
        public OperatorTextCodeWrapper OperatorList { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
}
