using OAuth2.Models;
using OAuth2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Services
{
    public class StateService
    {
        public List<StatesVM> GetAllStates()
        {
            try
            {
                using (var db = new OAuth2Entities())
                {
                    return db.States.Select(item => new StatesVM
                    {
                        Id = item.Id,
                        StateName = item.StateName
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}