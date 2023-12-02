using Eventmi.Core.Contracts;
using Eventmi.Core.Models;
using Eventmi.Infrastructure.Data.Common;
using Eventmi.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventmi.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository eventRepository;

        public EventService(IRepository _eventRepository)
        {
            eventRepository = _eventRepository;
        }
        public async Task CreateAsync(EventModel model)
        {
            if (model.Id > 0)
            {
                bool exists = eventRepository.GetById<Event>(model.Id) != null;
                throw new ArgumentException("Събитието вече съществува!");
            }

            Event newEvent = new Event()
            {
                Name = model.Name,
                Start = model.Start,
                End = model.End,
                IsActive = true,
                Place = new Address()
                {
                    Street = model.StreetAddress,
                    TownId = model.TownId,                    
                }
            };

            await eventRepository.AddAsync(newEvent);
            await eventRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Event @event = await eventRepository.GetById<Event>(id);

            if (@event != null)
            {
                eventRepository.Delete(@event);
                await eventRepository.SaveChangesAsync();
            }
        } 

        public async Task EditAsync(EventModel model)
        {
            Event @event = await eventRepository.GetById<Event>(model.Id);

            if (@event == null)
            {
                throw new ArgumentException("Събитието не съществува!");
            }

            @event.Name = model.Name;
            @event.Start = model.Start;
            @event.End = model.End;
            @event.Place.Street = model.StreetAddress;
            @event.Place.TownId = model.TownId;

            await eventRepository.SaveChangesAsync();
        }
        public async Task<EventModel> GetByIdAsync(int id)
        {
            Event @event = await eventRepository
                .AllReadOnly<Event>()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null)
            {
                throw new ArgumentException("Събитието не съществува!");
            }

            return new EventModel()
            {
                Name = @event.Name,
                Start = @event.Start,
                End = @event.End,
                StreetAddress = @event.Place.Street,
                TownId = @event.Place.TownId
            };
        }

        public async Task<IEnumerable<EventModel>> GetAllAsync()
        {
            return await eventRepository.AllReadOnly<Event>()
                .Select(e => new EventModel()
                {
                    Name = e.Name,
                    Start = e.Start,
                    End = e.End,
                    StreetAddress = e.Place.Street,
                    TownId = e.Place.TownId
                })
                .ToListAsync();
        }
        
    }
}
