using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Repository.Repo
{
    public interface IRepoService
    {
        string SaveMoRequests();
        string UpdateMoRequests();

    }
}
