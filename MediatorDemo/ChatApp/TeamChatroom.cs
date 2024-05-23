using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorDemo.ChatApp
{
    public class TeamChatroom : Chatroom
    {
        private List<TeamMember> members = new();

        public override void Register(TeamMember member)
        {
            member.SetChatRoom(this);
            this.members.Add(member);    
        }

        public override void Send(string from, string message)
        {
            this.members.ForEach(m => m.Receive(from, message));
        }

        public void RegisterMembers(params TeamMember[] teamMembers)
        {
            teamMembers.ToList().ForEach(this.Register);
        }

        public override void SendTo<T>(string from, string message)
        {
            this.members.OfType<T>().ToList().ForEach(m => m.Receive(from, message));
        }
    }
}
