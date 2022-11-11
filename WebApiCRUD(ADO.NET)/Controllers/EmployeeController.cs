using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using WebApiCRUD_ADO.NET_.Models;

namespace WebApiCRUD_ADO.NET_.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEnumerable<Class1> get()
        {
            try {
                List<Class1> obj = new List<Class1>();
                string cs = "data source=DESKTOP-USHDUOR;database=webapi_crud_db;integrated security=true";
                SqlConnection con = new SqlConnection(cs);     // or
                                                               //SqlConnection con = new SqlConnection("data source=DESKTOP-USHDUOR;database=webapi_crud_db;integrated security=true");
                                                               //or
                                                               //SqlConnection con = new SqlConnection();
                                                               //con.ConnectionString="data source=DESKTOP-USHDUOR;database=webapi_crud_db;integrated security=true";
                                                               //Note: The ConnectionString parameter is a string made up of Key/Value pairs that have the information required to create a connection object.
                con.Open();
                string qry = "select * from Employee";
                SqlCommand com = new SqlCommand(qry, con);
                SqlDataAdapter adpt = new SqlDataAdapter(com);
                DataTable tab = new DataTable();
                adpt.Fill(tab);
                foreach (DataRow r in tab.Rows)
                {
                    obj.Add(new Class1
                    {
                        Id = Convert.ToInt32(r["id"]),
                        Name = r["name"].ToString(),
                        Gender = r["gender"].ToString(),
                        Age = Convert.ToInt32(r["age"]),
                        Designation = r["designation"].ToString()
                    });
                }
                con.Close();
                return obj;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
           
        }

        [HttpPost]
        public  HttpResponseMessage Post(Class1 employee)
        {
           
            try
            {
                List<Class1> obj = new List<Class1>();
                string cs = "data source=DESKTOP-USHDUOR;database=webapi_crud_db;integrated security=true";
                SqlConnection con = new SqlConnection(cs);

                    SqlCommand com = new SqlCommand("Saveemployee", con);
                    com.CommandType = CommandType.StoredProcedure;
                    con.Open();

                com.Parameters.AddWithValue("@name", employee.Name);
                        com.Parameters.AddWithValue("@gender", employee.Gender);
                        com.Parameters.AddWithValue("@age", employee.Age);
                com.Parameters.AddWithValue("@designation", employee.Designation);
                com.Parameters.AddWithValue("@id", SqlDbType.Int).Direction=ParameterDirection.Output ;

                int a = com.ExecuteNonQuery();
                string getid = com.Parameters["@id"].Value.ToString();
                employee.Id = Convert.ToInt32(getid);
                if (a!= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                
                     
                //if (a == 0)
                //{
                //    return null;
                //}
                //else
                //    //com.Parameters.AddWithValue("@id", employee.Id);
                //    return ;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //[HttpPost]
        //public IHttpActionResult Post(Class1[] employee)
        //{
        //    try
        //    {
        //        string cs = "data source=DESKTOP-USHDUOR;database=webapi_crud_db;integrated security=true";
        //        using (SqlConnection con = new SqlConnection(cs))
        //        {
        //            //string qry = "INSERT INTO Employee VALUES('"+employee.Name+"','"+employee.Gender+"',"+employee.Age+",'"+employee.Designation+"')";
        //            SqlCommand com = new SqlCommand("PostData", con);
        //            com.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            var length = employee.Count();
        //            foreach(var c in employee)
        //            {
        //                com.Parameters.AddWithValue("@id",c.Id);
        //                com.Parameters.AddWithValue("@name", c.Name);
        //                com.Parameters.AddWithValue("@gender",c.Gender);
        //                com.Parameters.AddWithValue("@age",c.Age);
        //                com.Parameters.AddWithValue("@designation",c.Designation);
        //                com.ExecuteNonQuery();
        //            }
        //            //if (a == 0)
        //            //{
        //            //    return null;
        //            //}
        //            //else
        //            //    //com.Parameters.AddWithValue("@id", employee.Id);
        //            //    return Json(employee);
        //        }
        //        return Json(employee);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }

        //}

        public IEnumerable<Class1> get(int id)
        {
            try
            {
                List<Class1> obj = new List<Class1>();
                string cs = "data source=DESKTOP-USHDUOR;database=webapi_crud_db;integrated security=true";
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                string qry = "select * from Employee where id="+id;
                SqlCommand com = new SqlCommand(qry, con);
                SqlDataAdapter adpt = new SqlDataAdapter(com);
                DataTable tab = new DataTable();
                adpt.Fill(tab);
                foreach (DataRow r in tab.Rows)
                {
                    obj.Add(new Class1
                    {
                        Id = Convert.ToInt32(r["id"]),
                        Name = r["name"].ToString(),
                        Gender = r["gender"].ToString(),
                        Age = Convert.ToInt32(r["age"]),
                        Designation = r["designation"].ToString()
                    });
                }
                con.Close();
                return obj;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
