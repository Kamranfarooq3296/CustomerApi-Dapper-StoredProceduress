﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.IServices
{
    public interface IDbServices
    {
        Task<T> GetAsync<T>(string command, object parms);
        Task<List<T>> GetAll<T>(string command, object parms);
        Task<int> EditData(string command, object parms);
    }
}
