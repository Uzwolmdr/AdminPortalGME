using Newtonsoft.Json;
using Repository.Connection;
using Repository.Entities;
using Repository.Entities.Account;
using Repository.Entities.PurseMgt;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Repo.UserMgt
{
    public  class UserMgtRepo : IUserMgtRepo
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<UserMgtRepo>();

        SwiftDao dao;
        public UserMgtRepo()
        {
            dao = new SwiftDao();
        }

        public DBResponse CreateUser(UserMgtModel model)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag= 'CreateUser'";
                sql += ",@UserName=" + dao.FilterString(model.UserName);
                sql += ",@Password=" + dao.FilterString(model.Password);
                sql += ",@OrganisationCode=" + dao.FilterString(model.OrganisationCode);
                sql += ",@UserType=" + dao.FilterQuote(Convert.ToString(model.UserType));
                sql += ",@FullName=" + dao.FilterString(Convert.ToString(model.FullName));
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(model.Status));
                sql += ",@Role=" + dao.FilterString(Convert.ToString(model.Role));
                sql += ",@CreatedBy=" + dao.FilterString(Convert.ToString(model.CreatedBy));
                Log.Information("Create User with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("Create User execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public DBResponse UserEnableDisable(UserEDRequest model)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag=" + dao.FilterString("EnableDisableUser");
                sql += ",@UserName=" + dao.FilterString(model.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(model.OrganisationCode);
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(model.Status));
                sql += ",@CreatedBy=" + dao.FilterString(Convert.ToString(model.ModifiedBy));
                Log.Information("User Enable Disable with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                Log.Debug(string.Format("UserEnableDisable response :{0}", JsonConvert.SerializeObject(Response)));
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("User Enable Disable execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public RoleList GetAllRoleList(RoleRequest model)
        {


            RoleList roleList = new RoleList()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            var data = new List<RoleModel>();
            string sql = "EXEC SW_PROC_USER_MGT @Flag= 'GetRoleList'";
            sql += ",@IsHORole=" + dao.FilterString( Convert.ToString(model.IsHoRole));
            sql += ",@IsOrgRole=" + dao.FilterString(Convert.ToString(model.IsOrgRole));
            Log.Information("Get All RoleList with Query :{0}", sql);


            try
            {
                var response = dao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        data.Add(new RoleModel
                        {
                            RoleId = dr["RoleId"].ToString(),
                            RoleName = dr["RoleName"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Get All Role List execption occured with Message:{0}", ex.Message);
            }
            roleList.ResponseCode = 100;
            roleList.ResponseDescription = "Success";
            roleList.Roles = data;
            return roleList;
        }

        public LoginResponseCore DoLogin(RequestCore request)
        {
            var loginResponseCore = new LoginResponseCore();
            var data = new List<RoleModel>();
            string sql = "EXEC SW_PROC_USER_MGT @Flag= 'DoLogin'";
            sql += " ,@UserName= " + dao.FilterString(request.Username);
            sql += " ,@Password= " + dao.FilterString(request.Password);
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            Log.Information("Do Login with Query :{0}", sql);

            try
            {
                var response = dao.ExecuteDataset(sql);
                if (response.Tables[0] != null)
                {
                    var dbRes = response.Tables[0];
                    loginResponseCore.Username = Convert.ToString(dbRes.Rows[0]["Username"].ToString());
                    loginResponseCore.Password = Convert.ToString(dbRes.Rows[0]["LoginPassword"].ToString());
                    loginResponseCore.Organisation = Convert.ToString(dbRes.Rows[0]["OrganisationCode"].ToString());
                    loginResponseCore.FullName = Convert.ToString(dbRes.Rows[0]["FullName"].ToString());
                    loginResponseCore.UserType = Convert.ToInt32(dbRes.Rows[0]["UserType"].ToString());
                    loginResponseCore.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    loginResponseCore.ResponseDescription = Convert.ToString(dbRes.Rows[0]["Msg"].ToString());

                }

                if (response.Tables.Count > 1 )
                {
                    foreach (DataRow dr in response.Tables[1].Rows)
                    {
                        data.Add(new RoleModel
                        {
                            RoleId = dr["RoleId"].ToString()

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Do Login execption occured with Message:{0}", ex.Message);

            }
            loginResponseCore.RoleList = data;
            return loginResponseCore;
        }

        public DropDownResponseWrapper GetDropDownInfo(DropDownRequest request)
        {
            DropDownResponseWrapper response = new DropDownResponseWrapper();
            List<DropDownResponse> _list = new List<DropDownResponse>();
            var sql = "SW_PROC_GATEWAY_REPORT";
            sql += " @Flag= " + dao.FilterString(request.Flag);
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            sql += " ,@FromDate= " + dao.FilterString(request.FromDate);
            sql += " ,@ToDate= " + dao.FilterString(request.ToDate);
            sql += " ,@Product= " + dao.FilterString(request.Product);
            Log.Information("Get DropDown Info :{0}", sql);
            try
            {
                var dbTable = dao.ExecuteDataTable(sql);

                if (dbTable != null)
                {
                    foreach (DataRow dr in dbTable.Rows)
                    {
                        _list.Add(new DropDownResponse
                        {
                            Text = Convert.ToString(dr["Text"]),
                            Value = Convert.ToString(dr["Value"]),

                        });

                    }
                    response.DropDownDetails = _list;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Get DropDown Info sql execution Exception Occured:{0}", ex.Message);
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
            }
            return response;
        }

        public UserEDResponseWrapper GetAllUserList(UserEDRequest request)
        {
            UserEDResponseWrapper data = new UserEDResponseWrapper();
            string sql = "SW_PROC_USER_MGT @Flag ='GetUserList'";
            sql += " ,@UserName= " + dao.FilterString(request.UserName);
            sql += " ,@OrganisationCode= " + dao.FilterString(request.OrganisationCode);
            Log.Information("Get All User List with  Query :{0}", sql);

            List<UserEDResponse> responseParam = new List<UserEDResponse>();
            try
            {
                var response = dao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new UserEDResponse
                        {
                            UserName = Convert.ToString(dr["UserName"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            Status = Convert.ToInt32(dr["Status"]),
                            CreatedBy = Convert.ToString(dr["CreatedBy"]),
                            OrganisationCode= Convert.ToString(dr["OrganisationCode"])
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("Get All User List execption occured with Message:{0}", ex.Message);

            }
            if (responseParam.Count > 0)
            {
                data.ResponseCode = 100;
                data.ResponseDescription = "Success";
                data.UserList = responseParam;
            }
            else
            {
                data.ResponseCode = 101;
                data.ResponseDescription = "Failed";
                data.UserList = new List<UserEDResponse>();
            }
            return data;
        }

        public ProductResponseWrapper GetAllProductList()
        {
            ProductResponseWrapper data = new ProductResponseWrapper();
            string sql = "SW_PROC_USER_MGT @Flag ='GetProductList'";
            Log.Information("Get All Product List with  Query :{0}", sql);

            List<ProductResponse> responseParam = new List<ProductResponse>();
            try
            {
                var response = dao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new ProductResponse
                        {
                            ProductId = Convert.ToString(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            Status = Convert.ToBoolean(dr["Status"]),
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("Get All ProductList execption occured with Message:{0}", ex.Message);

            }
            if (responseParam.Count > 0)
            {
                data.ResponseCode = 100;
                data.ResponseDescription = "Success";
                data.ProductList = responseParam;
            }
            else
            {
                data.ResponseCode = 101;
                data.ResponseDescription = "Failed";
                data.ProductList = new List<ProductResponse>();
            }
            return data;
        }

        public DBResponse    EnableDisableProduct(EnableDisableProductRequest model)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag= 'EnableDisableProduct'";
                sql += ",@UserName=" + dao.FilterString(model.UserName);
                sql += ",@ProductId=" + dao.FilterString(model.ProductId);
                sql += ",@ProductName=" + dao.FilterString(model.ProductName);
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(model.Status));
                Log.Information("Enable Disable Product with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("Enable Disable Product execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public ProductResponseWrapper GetAllPendingProductList(EnableDisableProductRequest model)
        {
            ProductResponseWrapper data = new ProductResponseWrapper();
            string sql = "SW_PROC_USER_MGT @Flag ='GetPendingProductList'";
            sql += ",@UserName=" + dao.FilterString(model.UserName);
            Log.Debug("Get All Pending Product List with  Query :{0}", sql);

            List<ProductResponse> responseParam = new List<ProductResponse>();
            try
            {
                var response = dao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new ProductResponse
                        {
                            ProductId = Convert.ToString(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            CreatedBy = Convert.ToString(dr["CreatedBy"]),
                            Status = Convert.ToBoolean(dr["Status"]),
                            RowId = Convert.ToString(dr["RowId"])
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Information("Get All Pending Product List execption occured with Message:{0}", ex.Message);

            }
            if (responseParam.Count > 0)
            {
                data.ResponseCode = 100;
                data.ResponseDescription = "Success";
                data.ProductList = responseParam;
            }
            else
            {
                data.ResponseCode = 101;
                data.ResponseDescription = "Failed";
                data.ProductList = new List<ProductResponse>();
            }
            return data;
        }

        public DBResponse ApproveRejectProductSetup(EnableDisableProductRequest model)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag=" + dao.FilterString(model.Flag);
                sql += ",@UserName=" + dao.FilterString(model.UserName);
                sql += ",@RowId=" + dao.FilterString(model.RowId);
                Log.Information("Approve Reject Product Setup with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("Approve Reject Product Setup execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public UserEditResponse GetUserDetailsById(UserEDRequest request)
        {

            UserEditResponse data = new UserEditResponse();
            string sql = "SW_PROC_USER_MGT @Flag ='GetUserDetailById'";
            sql += ",@UserName=" + dao.FilterString(request.UserName);
            sql += ",@LoginUser=" + dao.FilterString(request.LoginUser);
            sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
            sql += ",@LoginUserOrgCode=" + dao.FilterString(request.LoginUserOrgCode);
            Log.Information("Get User Details ById with  Query :{0}", sql);
            UserMgtModel model = new UserMgtModel();
            List<RoleModel> roleList = new List<RoleModel>();
            try
            {
                var dataSet = dao.ExecuteDataset(sql);


                if (dataSet != null)
                {
                    var userDataTable = dataSet.Tables[0];
                    model = new UserMgtModel
                    {
                        UserName = Convert.ToString(userDataTable.Rows[0]["UserName"]),
                        OrganisationCode = Convert.ToString(userDataTable.Rows[0]["OrganisationCode"]),
                        Password = Convert.ToString(userDataTable.Rows[0]["LoginPassword"]),
                        FullName = Convert.ToString(userDataTable.Rows[0]["FullName"]),
                        Status = Convert.ToInt32(userDataTable.Rows[0]["IsActive"]) == 1 ? true : false
                    };
                    if (dataSet.Tables.Count > 1)
                    {
                        foreach (DataRow dr in dataSet.Tables[1].Rows)
                        {
                            roleList.Add(new RoleModel
                            {
                                RoleId = Convert.ToString(dr["RoleId"]),
                                RoleName = Convert.ToString(dr["RoleName"]),
                                Selected =  Convert.ToBoolean(dr["IsSelected"])

                            }); 
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("GetUser Details ById execption occured with Message:{0}", ex.Message);

            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                data.ResponseCode = 100;
                data.ResponseDescription = "Success";
                data.UserData = model;
                data.RoleList = roleList;
            }
            else
            {
                data.ResponseCode = 101;
                data.ResponseDescription = "Failed";
            }
            return data;
        }

        public DBResponse ChangePassword(ChangePasswordModel model)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag= 'ChangePassword'";
                sql += ",@UserName=" + dao.FilterString(model.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(model.OrganisationCode);
                sql += ",@Password=" + dao.FilterString(model.CurrentPassword);
                sql += ",@NewPassword=" + dao.FilterString(model.NewPassword);
                Log.Information("Change Password with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("Change Password execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public DBResponse EditUser(UserEditRequest request)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag= 'EditUser'";
                sql += ",@UserName=" + dao.FilterString(request.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(request.OrganisationCode);
                sql += ",@FullName=" + dao.FilterString(Convert.ToString(request.FullName));
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(request.Status));
                sql += ",@Role=" + dao.FilterString(Convert.ToString(request.Role));
                sql += ",@CreatedBy=" + dao.FilterString(Convert.ToString(request.CreatedBy));
                Log.Information("Edit User with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("EditUser execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public DBResponse ResetPassword(ResetPasswordModel model)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag= 'ResetPassword'";
                sql += ",@UserName=" + dao.FilterString(model.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(model.OrganisationCode);
                sql += ",@NewPassword=" + dao.FilterString(model.NewPassword);
                sql += ",@CreatedBy=" + dao.FilterString(model.CreatedBy);
                Log.Information("ResetPassword with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("ResetPassword execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public RoleList GetOwnOfficeRoleList(RoleRequest model)
        {
            RoleList roleList = new RoleList()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            var data = new List<RoleModel>();
            string sql = "EXEC SW_PROC_USER_MGT @Flag= 'GetOwnOfficeRole'";
            sql += ",@UserName=" + dao.FilterString(Convert.ToString(model.UserName));
            sql += ",@OrganisationCode=" + dao.FilterString(Convert.ToString(model.OrganisationCode));
            Log.Information("GetOwnOfficeRoleList with Query :{0}", sql);


            try
            {
                var response = dao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        data.Add(new RoleModel
                        {
                            RoleId = dr["RoleId"].ToString(),
                            RoleName = dr["RoleName"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetOwnOfficeRoleList execption occured with Message:{0}", ex.Message);
            }
            roleList.ResponseCode = 100;
            roleList.ResponseDescription = "Success";
            roleList.Roles = data;
            return roleList;
        }

        public DBResponse UnLockUser(ResetPasswordModel model)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag= 'UnLockUser'";
                sql += ",@UserName=" + dao.FilterString(model.UserName);
                sql += ",@OrganisationCode=" + dao.FilterString(model.OrganisationCode);
                sql += ",@CreatedBy=" + dao.FilterString(model.CreatedBy);
                Log.Information("UnLockUser with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("UnLockUser execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public OperatorTextCodeWrapper GetOperatorTextCode(OperatorTextCodeRequest request)
        {
            OperatorTextCodeWrapper response = new OperatorTextCodeWrapper();
            List<OperatorTextCodeModel> ntcDetails = new List<OperatorTextCodeModel>();
            List<OperatorTextCodeModel> ncellDetails = new List<OperatorTextCodeModel>();
            List<OperatorTextCodeModel> smartCellDetails = new List<OperatorTextCodeModel>();
            var sql = "SW_PROC_USER_MGT";
            sql += " @Flag= " + dao.FilterString(request.Flag);
            Log.Information("GetOperatorTextCode :{0}", sql);
            try
            {
                var dataSet = dao.ExecuteDataset(sql);
                if (dataSet != null)
                {
                    
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        ntcDetails.Add(new OperatorTextCodeModel
                        {
                            TextCode = Convert.ToString(dr["TextCode"]),
                            UserName = Convert.ToString(dr["UserName"]),
                            Password = Convert.ToString(dr["Password"]),
                            SmsId = Convert.ToString(dr["SmsId"]),
                        });

                    }

                    foreach (DataRow dr in dataSet.Tables[1].Rows)
                    {
                        ncellDetails.Add(new OperatorTextCodeModel
                        {
                            TextCode = Convert.ToString(dr["TextCode"]),
                            UserName = Convert.ToString(dr["UserName"]),
                            Password = Convert.ToString(dr["Password"]),
                            SmsId = Convert.ToString(dr["SmsId"]),

                        });

                    }

                    foreach (DataRow dr in dataSet.Tables[2].Rows)
                    {
                        smartCellDetails.Add(new OperatorTextCodeModel
                        {
                            TextCode = Convert.ToString(dr["TextCode"]),
                            UserName = Convert.ToString(dr["UserName"]),
                            Password = Convert.ToString(dr["Password"]),
                            SmsId = Convert.ToString(dr["SmsId"]),

                        });

                    }

                    response.NtcOperator = ntcDetails;
                    response.NcellOperator = ncellDetails;
                    response.SmartcellOperator = smartCellDetails;
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetOperatorTextCode Info sql execution Exception Occured:{0}", ex.Message);
                
            }
            return response;
        }


        public DBResponse CreateOrganisation(OrganisationModel model)
        {
            DBResponse Response = new DBResponse()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"
            };
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag=" + dao.FilterString(model.Flag);
                sql += ",@UserName=" + dao.FilterString(model.UserName);
                sql += ",@Password=" + dao.FilterString(model.Password);
                sql += ",@OrganisationCode=" + dao.FilterString(model.OrganisationCode);
                sql += ",@OrganisationName=" + dao.FilterString(model.OrganisationName);
                sql += ",@ClientType=" + dao.FilterString(model.ClientType);
                sql += ",@NtcTextCode=" + dao.FilterString(Convert.ToString(model.NtcTextCode));
                sql += ",@NtcSmsId=" + dao.FilterString(Convert.ToString(model.NtcSmsId));
                sql += ",@NtcUserName=" + dao.FilterString(Convert.ToString(model.NtcUserName));
                sql += ",@NtcPassword=" + dao.FilterString(Convert.ToString(model.NtcPassword));

                sql += ",@NcellTextCode=" + dao.FilterString(Convert.ToString(model.NcellTextCode));
                sql += ",@NcellSmsId=" + dao.FilterString(Convert.ToString(model.NcellSmsId));
                sql += ",@NcellUserName=" + dao.FilterString(Convert.ToString(model.NcellUserName));
                sql += ",@NcellPassword=" + dao.FilterString(Convert.ToString(model.NcellPassword));

                sql += ",@SmartCellTextCode=" + dao.FilterString(Convert.ToString(model.SmartCellTextCode));
                sql += ",@SmartCellSmsId=" + dao.FilterString(Convert.ToString(model.SmartCellSmsId));
                sql += ",@SmartCellUserName=" + dao.FilterString(Convert.ToString(model.SmartCellUserName));
                sql += ",@SmartCellPassword=" + dao.FilterString(Convert.ToString(model.SmartCellPassword));

                sql += ",@WalletId=" + dao.FilterQuote(Convert.ToString(model.WalletId));
                sql += ",@CreatedBy=" + dao.FilterString(Convert.ToString(model.CreatedBy));
                sql += ",@Status=" + dao.FilterQuote(Convert.ToString(model.Status));


                Log.Information("Create Organisation with Query :{0}", sql);
                var dbRes = dao.ExecuteDataTable(sql);
                if (dbRes != null)
                {
                    Response.ResponseCode = Convert.ToInt32(dbRes.Rows[0]["Code"].ToString());
                    Response.ResponseDescription = dbRes.Rows[0]["Msg"].ToString();
                }
                return Response;
            }
            catch (Exception ex)
            {
                Response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                Response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("Create Organisation execption occured with Message:{0}", ex.Message);
                return Response;
            }
        }

        public AllOrgPurseBlncWrapper GetAllClientList()
        {
            var purseResponse = new AllOrgPurseBlncWrapper
            {
                ResponseCode = 101,
                ResponseDescription = "No List Found",
                Data = new List<AllOrgPurseBalance>()
            };
            var responseParam = new List<AllOrgPurseBalance>();

            var sql = "SW_PROC_USER_MGT @Flag ='GetClientList'";
            Log.Information("GetAllClientList with  Query :{0}", sql);

            try
            {
                var response = dao.ExecuteDataTable(sql);
                if (response != null)
                {
                    foreach (DataRow dr in response.Rows)
                    {
                        responseParam.Add(new AllOrgPurseBalance
                        {
                            OrganisationCode = dr["OrganisationCode"].ToString(),
                            Status = Convert.ToBoolean(dr["IsActive"]),
                            OrganisationName = dr["Organisation"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetAllClientList execption occured with Message:{0}", ex.Message);

            }
            if (responseParam.Count > 0)
            {
                purseResponse.ResponseCode = 100;
                purseResponse.ResponseDescription = "Success";
            }
            purseResponse.Data = responseParam;
            return purseResponse;
        }

        public EditOrganisationModelWrapper GetOrganisationById(PurseBalanceRequest  orgRequest)
        {
            EditOrganisationModelWrapper response = new EditOrganisationModelWrapper()
            {
                ResponseCode = 101,
                ResponseDescription = "Internal Error"

            };

            var model = new OrganisationModel();
           
            try
            {
                string sql = "EXEC SW_PROC_USER_MGT @Flag= 'GetOrgById'";
                sql += ",@OrganisationCode=" + dao.FilterString(orgRequest.OrganisationCode);

                Log.Information("GetOrganisationById with Query :{0}", sql);
                var dbRes = dao.ExecuteDataRow(sql);
                if (dbRes != null)
                {
                    model.UserName = Convert.ToString(dbRes["UserName"]);
                    model.Password = Convert.ToString(dbRes["Password"]);
                    model.OrganisationCode = Convert.ToString(dbRes["OrganisationCode"]);
                    model.OrganisationName = Convert.ToString(dbRes["Organisation"]);
                    model.ClientType = Convert.ToString(dbRes["ClientType"]);
                    model.NtcTextCode = Convert.ToString(dbRes["NtcTextCode"]);
                    model.NtcSmsId = Convert.ToString(dbRes["NtcSmsId"]);
                    model.NtcUserName = Convert.ToString(dbRes["NtcUserName"]);
                    model.NtcPassword = Convert.ToString(dbRes["NtcPassword"]);

                    model.NcellTextCode = Convert.ToString(dbRes["NcellTextCode"]);
                    model.NcellSmsId = Convert.ToString(dbRes["NcellSmsId"]);
                    model.NcellUserName = Convert.ToString(dbRes["NcellUserName"]);
                    model.NcellPassword = Convert.ToString(dbRes["NcellPassword"]);

                    model.SmartCellTextCode = Convert.ToString(dbRes["SmartTextCode"]);
                    model.SmartCellSmsId = Convert.ToString(dbRes["SmartSmsId"]);
                    model.SmartCellUserName = Convert.ToString(dbRes["SmartUserName"]);
                    model.SmartCellPassword = Convert.ToString(dbRes["SmartPassword"]);

                    model.WalletId = Convert.ToString(dbRes["Wallet"]);
                    model.Status = Convert.ToInt32(dbRes["IsActive"]);

                }
                response.OrganisationData = model;
                OperatorTextCodeRequest request = new OperatorTextCodeRequest
                {
                    Flag = "GetTextCodeDetail"
                };
                response.OperatorList = GetOperatorTextCode(request);
                response.ResponseCode = 100;
                response.ResponseDescription = "Success";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseCode = ResponseCodeConstant.EXCEPTION;
                response.ResponseDescription = ResponseCodeMessage.FAILED_EXCEPTION;
                Log.Error("GetOrganisationById exception occured with Message:{0}", ex.Message);
                return response;
            }
        }
    }

}
