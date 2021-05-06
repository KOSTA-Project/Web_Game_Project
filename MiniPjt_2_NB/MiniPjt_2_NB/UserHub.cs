using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniPjt_2_NB
{
    public class UserHub : Hub
    {
        public void Send(string name, string msg)
        {
            // 클라이언트 업데이트를 위한 addNewMessageToPage 메소드 호출
            Clients.All.addNewMessageToPage(name, msg);
        }
    }
}