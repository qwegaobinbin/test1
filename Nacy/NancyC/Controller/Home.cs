using Nancy;
using NancyDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication1.Modules.User
{
    public class Home : NancyModule
    {
        //存储
        public static  List<Student> lstStudent = new List<Student>();
        public Home()
        {

            Get["/"] = get =>
            {
                return View["/Index", lstStudent];
            };

            Get["/{ID}"] = get =>
            {
                var ID = get.ID;
                var IdSearch = 0;
                if (!int.TryParse(ID, out IdSearch))
                {
                    return View["/Index", lstStudent];
                }

                var student = lstStudent.Find(s => s.ID == IdSearch);
                if (student == null)
                {
                    return View["/Index", lstStudent];
                }

                lstStudent.Remove(student);
                return View["/Index", lstStudent];
            };

            Get["/Home/modify/{ID}"] = get =>
            {
                var getID = get.ID;
                var ID=0;
                if(!int.TryParse(getID, out ID))
                {
                    return View["/Index", lstStudent];
                }

                var student = lstStudent.Find(s=>s.ID==ID);
                if (student == null)
                {
                    return View["/Index", lstStudent];
                }

                return View["/Home", student];
            };

            Post["/Home/Add"] = add =>
            {  
                var form = Request.Form;
                var Name = form["Name"];
                if(string.IsNullOrEmpty(Name))
                {
                    return View["/Index", lstStudent];
                }

                var student = new Student();
                var random = new Random(); 
                student.ID = random.Next(10000);
                student.Name = Name;
                lstStudent.Add(student);
                return View["/Index", lstStudent];
            };


            Post["/Home/Search"] = search =>
            {
                var form = Request.Form;
                var Name = form["Search"];
                if (string.IsNullOrEmpty(Name))
                {
                    return View["/Index", lstStudent];
                }

                var lst1Student1 = lstStudent.Where(s => s.Name == Name).ToList();
                if (lst1Student1== null||lst1Student1.Count==0)
                {
                    return View["/Index", new List<Student>()];
                }

                return View["/Index", lst1Student1];
            };

            Post["/Home/modify"] = modify =>
            { 
                var form = Request.Form;
                var ID = form["hidden"];
                var Name = form["Name"];
                int idModify = 0;
                if (!int.TryParse(ID, out idModify))
                {
                    return View["/Index", lstStudent];
                }

                var student = lstStudent.Find(s => s.ID == idModify);
                if (student== null)
                {
                    return View["/Index", lstStudent];
                }

                student.Name = Name;
                return View["/Index", lstStudent];
            };
        }
    }
}