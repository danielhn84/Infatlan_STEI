
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace Infatlan_STEI_Agencias.classes
{
    public class LdapService
    {
        public LdapService() { }
        public System.Data.DataTable GetDatosUsuario(string domain, string username)
        {
            DataTable vDatosAD = new DataTable();           
            try
            {
                DirectorySearcher search = new DirectorySearcher(domain);
                //search.Filter = "(&(objectClass=user)(anr=" + username + "))";
                search.Filter = "(&(objectClass=user)(DisplayName=*" + username + "*))";
                search.PropertiesToLoad.Add("givenName");
                search.PropertiesToLoad.Add("sn");
                search.PropertiesToLoad.Add("mail");
                SearchResultCollection result = search.FindAll();

                vDatosAD.Columns.Add("givenName");
                vDatosAD.Columns.Add("sn");
                vDatosAD.Columns.Add("mail");

                foreach (SearchResult item in result)
                {
                    try
                    {
                        vDatosAD.Rows.Add(
                            item.Properties["givenName"][0].ToString(),
                            item.Properties["sn"][0].ToString(),
                            item.Properties["mail"][0].ToString()
                            );
                    
                    }
                    catch {}
                }
            }
            catch
            {
                throw;
            }
            return vDatosAD;
        }
    }
}