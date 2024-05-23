using MediatR;
using MediatRDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediatRDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IMediator mediator;

        public ContactsController(IMediator mediator) => this.mediator = mediator;

        [HttpGet("{id}")]
        public async Task<Contact> GetContact([FromRoute]Query query) 
            => await this.mediator.Send(query); 

        #region Nested Classes

        public class Query : IRequest<Contact>
        {
            public int Id { get; set; }
        }

        public class ContactHandler : IRequestHandler<Query, Contact>
        {
            private readonly ContactsContext context;

            public ContactHandler(ContactsContext context) => this.context = context;

            public Task<Contact> Handle(Query request, CancellationToken cancellationToken)
            {
                return this.context.Contacts.Where(c => c.Id == request.Id).SingleOrDefaultAsync();
            }
        }
        #endregion
    }
}
