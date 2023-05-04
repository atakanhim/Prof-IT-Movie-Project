using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Hubs
{
    public class CommentHub : Hub
    {
        // Film sayfasına bildirim gitmesi için film grubuna katılım sağlar.
        public async Task JoinFilmGroup(string filmId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, filmId);
        }

        //public async Task LeaveFilmGroup(string filmId)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, filmId);
        //}
        // string userName, , string date, string likeCount
        public async Task AddComment(string filmId, string comment)
        {
            //var newComment = new SignalNewCommentDto
            //{
            //    UserName = userName,
            //    Text = comment,
            //    Date = date,
            //    LikeCount = int.Parse(likeCount)
            //};
            //var serializedComment = JsonConvert.SerializeObject(newComment);
            await Clients.Group(filmId).SendAsync("ReceiveComment", comment);
        }

       
    }
}
