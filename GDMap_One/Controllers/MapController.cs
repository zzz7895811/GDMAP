using GDMap_One.Models;
using GDMap_One.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace GDMap_One.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index()
        {
            string sql = "select id,name,path,center,title,intro from Area";
            SQLiteCommand cmd = new SQLiteCommand(sql);
            DataSet ds = SqliteHelper.ExecuteDataSet(cmd);
            List<Area_Model> line = new List<Area_Model>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    line.Add(new Area_Model { name = r[1].ToString(), path = r[2].ToString(), center = r[3].ToString(), title = r[4].ToString(), intro = r[5].ToString() });
                }
            }
            ViewBag.MeshLinePath = JsonConvert.SerializeObject(line);
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            string sql = "select id,name,path,center,title,intro from Area where id=@id";
            SQLiteParameter[] sQLiteParameter = {
                new SQLiteParameter {  ParameterName="@id", DbType=DbType.Int32, Value=id },
            };
            SQLiteCommand cmd = new SQLiteCommand(sql);
            DataSet result = SqliteHelper.ExecuteDataSet(sql, CommandType.Text, sQLiteParameter);
            Area_Model area_Model = new Area_Model
            {
                id = int.Parse(result.Tables[0].Rows[0]["id"].ToString()),
                name = result.Tables[0].Rows[0]["name"].ToString(),
                path = result.Tables[0].Rows[0]["path"].ToString(),
                center = result.Tables[0].Rows[0]["center"].ToString(),
                title = result.Tables[0].Rows[0]["title"].ToString(),
                intro = result.Tables[0].Rows[0]["intro"].ToString(),
            };
            ViewBag.EditModel = JsonConvert.SerializeObject(area_Model);
            return View();
        }
        public ActionResult CreateArea()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
        public JsonResult GetListPage(int currPage, int pageSize, string areaName)
        {
            int total = 0;
            string strWhere = "1=1";
            if (!string.IsNullOrEmpty(areaName))
            {
                strWhere += $" and name like '%{areaName}%' ";
            }
            DataTable dt = SqliteHelper.SelectPaging("Area", "id,name,path,center,title,intro", strWhere, "id", pageSize, currPage, out total);
            List<Area_Model> line = new List<Area_Model>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    line.Add(new Area_Model { id = Convert.ToInt32(r[0].ToString()), name = r[1].ToString(), path = r[2].ToString(), center = r[3].ToString(), title = r[4].ToString(), intro = r[5].ToString() });
                }
            }
            return Json(new { total, items = line }, JsonRequestBehavior.AllowGet);
        }
        public bool AddData(Area_Model model)
        {
            string sql = "insert into Area(name,path,center,title,intro) values (@name,@path,@center,@title,@intro)";
            SQLiteCommand cmd = new SQLiteCommand(sql);
            SQLiteParameter[] sQLiteParameter = {
                new SQLiteParameter {  ParameterName="@name", DbType=DbType.String, Value=model.name },
                new SQLiteParameter{ ParameterName="@path", DbType=DbType.String, Value= model.path.TrimEnd(';') },
                new SQLiteParameter{ ParameterName="@center", DbType=DbType.String, Value= model.center.TrimEnd(';')},
                new SQLiteParameter{ ParameterName="@title", DbType=DbType.String, Value= model.title.TrimEnd(';') },
                new SQLiteParameter{ ParameterName="@intro", DbType=DbType.String, Value= model.intro.TrimEnd(';')}
            };
            int result = SqliteHelper.ExecuteNonQuery(sql, CommandType.Text, sQLiteParameter);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }
        public bool EditData(Area_Model model)
        {
            string sql = "update Area set name=@name,path=@path,center=@center,title=@title,intro=@intro where id=@id";
            SQLiteCommand cmd = new SQLiteCommand(sql);
            SQLiteParameter[] sQLiteParameter = {
                new SQLiteParameter {  ParameterName="@name", DbType=DbType.String, Value=model.name },
                new SQLiteParameter{ ParameterName="@path", DbType=DbType.String, Value= model.path.TrimEnd(';') },
                new SQLiteParameter{ ParameterName="@center", DbType=DbType.String, Value= model.center.TrimEnd(';')},
                 new SQLiteParameter{ ParameterName="@title", DbType=DbType.String, Value= model.title.TrimEnd(';')},
                  new SQLiteParameter{ ParameterName="@intro", DbType=DbType.String, Value= model.intro.TrimEnd(';')},
                new SQLiteParameter{ ParameterName="@id", DbType=DbType.String, Value= model.id},
            };
            int result = SqliteHelper.ExecuteNonQuery(sql, CommandType.Text, sQLiteParameter);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }
        public bool Delete(int id)
        {
            string sql = "delete from Area where id=@id";
            SQLiteParameter[] sQLiteParameter = {
                new SQLiteParameter {  ParameterName="@id", DbType=DbType.Int32, Value=id },
            };
            int result = SqliteHelper.ExecuteNonQuery(sql, CommandType.Text, sQLiteParameter);
            if (result > 0)
            {
                return true;
            }
            else { return false; }

        }
    }
}