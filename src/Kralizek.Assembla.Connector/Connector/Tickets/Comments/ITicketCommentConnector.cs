﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kralizek.Assembla.Connector.Tickets.Comments
{
    public interface ITicketCommentConnector
    {
        Task<IReadOnlyList<Comment>> GetAllInTicketAsync(string spaceIdOrWikiName, int ticketNumber, int? page = null, int? pageSize = null);

        Task<Comment> GetAsync(string spaceIdOrWikiName, int ticketNumber, int commentId);

        Task<Comment> CreateAsync(string spaceIdOrWikiName, int ticketNumber, Comment comment);

        Task UpdateAsync(string spaceIdOrWikiName, int ticketNumber, Comment comment);
    }
}