using Repository.Entities;
using Repository.Entities.Insurance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo.Insurance
{
    public interface IInsuranceRepo
    {
         DbLogResponse InsertIMELifeRenewalPayment(IMELifeRenewalPaymentDetail request);
    }
}
