using LibHutech_DoAnLapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibHutech_DoAnLapTrinhWeb.Controllers
{
    public class CheckHistoryController : ApiController
    {
        private LibHutechEntities content = new LibHutechEntities();
        // GET: api/CheckHistory
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CheckHistory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CheckHistory
        public void Post([FromBody]LichSu lsu)
        {
            if (lsu.MaSV != null) { 
            var q = content.LichSus.Where(ls=>ls.MaSach==lsu.MaSach && ls.MaSV==ls.MaSV).FirstOrDefault();
            if(q==null){
                content.LichSus.Add(lsu);
            }
            else
            {
                q.SoTrang=lsu.SoTrang;
            }
            content.SaveChanges();
            }
        }

        // PUT: api/CheckHistory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CheckHistory/5
        public void Delete(int id)
        {
        }
    }
}
