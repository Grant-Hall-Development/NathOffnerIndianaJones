using PlayFab.ClientModels;
using System.Collections;
using UnityEngine;

namespace Base.Managers
{
    public class ActiveLoginDetails : Singleton<ActiveLoginDetails>
    {
        public static string entityID;
        public static string entityType;

        public static void SetEntityDetails(LoginResult result)
        {
            entityID = result.EntityToken.Entity.Id;
            entityType = result.EntityToken.Entity.Type;
        }
    }
}