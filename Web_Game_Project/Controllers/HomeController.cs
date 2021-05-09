using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using MyLibrary;
using static MyLibrary.mylib;

namespace Web_Game_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Word()
        {
            return View();
        }
        public ActionResult Log_Out()
        {
            //string aa = global.sock_list[0].RemoteEndPoint.ToString();
            // Console.WriteLine($"{aa}");
            Socket ex = global.sock_list[0];
            ex.Close();
            //return View();
            //Console.WriteLine($"{aa}");
            return RedirectToAction("Login");
        }
        public ActionResult Signup()
        {

            string uid = Request.QueryString["newID"];
            string pwd = Request.QueryString["newPWD"];
            if(uid != null)
            {
                //SQLDB db = new SQLDB(@"Data Source=192.168.0.85;Initial Catalog=myDB;Persist Security Info=True;User ID=kosta;Password=kosta");
                // 새롭게 입력받은 데이터를 가지고 DB update
                // main page로 Redirection
            }
            return View();
        }

        public ActionResult Login()
        {
            // 쿠키 검색 -> 로그인 정보 존재 시 
            if (Session["uid"] != null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Your login page.";
            string uid = Request.QueryString["UID"];
            string pwd = Request.QueryString["PWD"];

            if (uid != null)
            {

                SQLDB db = new SQLDB(@"Data Source=192.168.0.85;Initial Catalog=myDB;Persist Security Info=True;User ID=kosta;Password=kosta");
                //SQLDB db= new SQLDB(@"Data Source = 192.168.0.80; User ID = kosta6; Password = kosta");


                if (pwd == db.Get($"select pwd from users where uid='{uid}'").ToString().Trim())
                {
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sock.Connect("192.168.0.85", 9000);
                    global.sock_list.Add(sock);
                    sock.Send(new byte[] { 1, 1, 1 });

                    //쿠키에 연결된 아이디 기록
                    //Session["uid"] = uid;
                    //Session["account"] = db.Get($"select account from users where uid='{uid}'");
                    //인수 하나더 추가 시, 두번째 인수는 컨트롤러 명- Default는 현재 컨트롤러
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

    }
}