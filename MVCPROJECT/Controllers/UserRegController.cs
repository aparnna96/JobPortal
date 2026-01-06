using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPROJECT.Models;

namespace MVCPROJECT.Controllers
{
    public class UserRegController : Controller
    {
        // GET: UserReg
        MVCPOJECTEntities dbobj = new MVCPOJECTEntities();
        public ActionResult Insertuser_pageload()
        {
            //checkboxlist
            UserInsert user = new UserInsert();
            user.MyFavouriteQual = getQualificationData();
            return View(user);
        }
        public List<CheckBoxListHelper> getQualificationData()
        {
            List<CheckBoxListHelper> sts = new List<CheckBoxListHelper>()
            {
                new CheckBoxListHelper{Value="SSLC",Text="SSLC",Ischecked=true},
                new CheckBoxListHelper{Value="PLUS TWO",Text="PLUS TWO",Ischecked=false},
                new CheckBoxListHelper{Value="BCA",Text="BCA",Ischecked=false},
                new CheckBoxListHelper{Value="MCA",Text="MCA",Ischecked=false},
                new CheckBoxListHelper{Value="BTECH",Text="BTECH",Ischecked=false},
            };
            return sts;
        }
        public ActionResult InsertUser_Click(UserInsert clsobj, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var getmaxid = dbobj.sp_maxloginid("", "").FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int RegId = 0;
                if (mid == 0)
                {
                    RegId = 1;
                }
                else
                {
                    RegId = mid + 1;
                }
                var quid = string.Join(",", clsobj.SelectedQual);
                clsobj.Qual = quid;//set
                clsobj.MyFavouriteQual = getQualificationData();//get

                //get
                dbobj.sp_userreg(clsobj.Name, clsobj.Gender, clsobj.Email, clsobj.Phone, clsobj.Age, clsobj.Address, clsobj.Qual, clsobj.Experience, clsobj.Skills, "active");
                dbobj.sp_LoginInsert(RegId, clsobj.Username, clsobj.Password, "user");
                clsobj.Usermsg = "Successfully inserted";
                return View("Insertuser_pageload", clsobj);

            }
            else
            {
                clsobj.MyFavouriteQual = getQualificationData();

                return View("Insertuser_pageload", clsobj);
            }

        }
    }
}